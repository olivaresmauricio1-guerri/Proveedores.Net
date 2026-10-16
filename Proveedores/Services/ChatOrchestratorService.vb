Imports System.Text.Json
Imports System.Threading

''' <summary>
''' Coordina la conversación con Ollama y ejecuta exclusivamente herramientas permitidas.
''' Nunca acepta ni ejecuta SQL generado por el modelo.
''' </summary>
Public Class ChatOrchestratorService
    Implements IDisposable

    Private Const MaximoIteraciones As Integer = 6
    Private Const MaximoMensajesConservados As Integer = 30

    Private ReadOnly _ollama As OllamaClientService
    Private ReadOnly _cuentas As ChatCuentaCorrienteService
    Private ReadOnly _vehiculos As ChatVehiculosService
    Private ReadOnly _disposeOllama As Boolean
    Private ReadOnly _mensajes As New List(Of OllamaChatMessage)
    Private ReadOnly _tools As List(Of OllamaToolDefinition)
    Private ReadOnly _semaphore As New SemaphoreSlim(1, 1)
    Private ReadOnly _jsonOptions As New JsonSerializerOptions With {.PropertyNameCaseInsensitive = True}

    Public Sub New(Optional ollama As OllamaClientService = Nothing,
                   Optional cuentas As ChatCuentaCorrienteService = Nothing,
                   Optional vehiculos As ChatVehiculosService = Nothing)
        If ollama Is Nothing Then
            _ollama = New OllamaClientService()
            _disposeOllama = True
        Else
            _ollama = ollama
        End If

        _cuentas = If(cuentas, New ChatCuentaCorrienteService())
        _vehiculos = If(vehiculos, New ChatVehiculosService())
        _tools = CrearHerramientas()
        ReiniciarConversacion()
    End Sub

    Public ReadOnly Property Ollama As OllamaClientService
        Get
            Return _ollama
        End Get
    End Property

    Public Sub ReiniciarConversacion()
        _mensajes.Clear()
        _mensajes.Add(New OllamaChatMessage With {
            .Role = "system",
            .Content =
                "Sos un asistente interno del sistema de proveedores. Respondé siempre en español claro. " &
                $"La fecha actual del sistema es {Date.Today:yyyy-MM-dd}; usala para interpretar hoy, este mes y este año. " &
                "Los datos contables solo pueden obtenerse usando las herramientas disponibles: nunca inventes datos, " &
                "nunca generes SQL y nunca afirmes que una consulta se realizó si no recibiste el resultado de una herramienta. " &
                "Si el usuario indica un número de cuenta, usalo directamente. Si indica un nombre, primero buscá el proveedor. " &
                "Si la búsqueda devuelve varias coincidencias, pedí que elija una antes de hacer consultas individuales. " &
                "Un saldo calculado positivo significa deuda con el proveedor; un saldo negativo significa crédito a favor nuestro por pagos en exceso. " &
                "Al informar facturas pendientes, usá el saldo pendiente calculado por la herramienta y mencioná el total informado. " &
                "Si el usuario pide exportar el resultado actual a Excel, usá solicitar_exportacion_excel. " &
                "La deuda total debe distinguir deuda bruta, creditos a favor y posicion neta. " &
                "Gasto o compras mensuales son movimientos que aumentan la deuda; pagos son movimientos que la reducen. " &
                "No uses vencimientos, prioridad de pago ni dias de atraso porque las fechas de vencimiento no son confiables. " &
                "Para periodos sin fechas explicitas usa los valores predeterminados de cada herramienta. " &
                "Cuando haya un listado o una serie mensual, explica el resumen y deja el detalle en la grilla. " &
                "Si el usuario pide servicios o una comparacion sin indicar vehiculos, usa listar_vehiculos y pedi que elija uno o varios codigos. " &
                "Las patentes son texto: si el usuario indica patentes, envialas en patentes y el sistema resolvera sus IdVehiculo. Nunca conviertas una patente a numero ni inventes un IdVehiculo. " &
                "Cuando el usuario indique codigos IdVehiculo, envia todos sus codigos numericos en ids_vehiculo. " &
                "Los rankings de servicios admiten agrupacion total, sucursal o vehiculo; respeta la agrupacion solicitada. " &
                "Las facturas impagas por periodo se filtran por fecha de la factura, no por fecha de vencimiento. " &
                "Los importes, totales, diferencias y saldos deben copiarse de las herramientas; no los recalcules ni los inventes."
        })
    End Sub

    Public Async Function EnviarAsync(pregunta As String,
                                      Optional cancellationToken As CancellationToken = Nothing) As Task(Of ChatOrchestratorResponse)
        If String.IsNullOrWhiteSpace(pregunta) Then
            Throw New ArgumentException("La pregunta no puede estar vacía.", NameOf(pregunta))
        End If

        Await _semaphore.WaitAsync(cancellationToken).ConfigureAwait(False)
        Try
            RecortarHistorial()
            _mensajes.Add(New OllamaChatMessage With {.Role = "user", .Content = pregunta.Trim()})

            Dim resultado As New ChatOrchestratorResponse()
            For iteracion As Integer = 1 To MaximoIteraciones
                Dim response = Await _ollama.EnviarAsync(_mensajes, _tools, cancellationToken).ConfigureAwait(False)
                Dim mensaje = response.Message
                _mensajes.Add(mensaje)

                If mensaje.ToolCalls Is Nothing OrElse mensaje.ToolCalls.Count = 0 Then
                    resultado.Texto = If(mensaje.Content, String.Empty).Trim()
                    If resultado.Texto = String.Empty Then
                        resultado.Texto = "No pude elaborar una respuesta con los datos disponibles."
                    End If
                    Return resultado
                End If

                For Each toolCall In mensaje.ToolCalls
                    cancellationToken.ThrowIfCancellationRequested()
                    Dim ejecucion = EjecutarHerramienta(toolCall)
                    resultado.HerramientasEjecutadas.Add(ejecucion.Nombre)
                    resultado.Resultados.Add(ejecucion)
                    If String.Equals(ejecucion.Nombre, "solicitar_exportacion_excel", StringComparison.OrdinalIgnoreCase) Then
                        resultado.SolicitarExportacionExcel = True
                    End If
                    _mensajes.Add(New OllamaChatMessage With {
                        .Role = "tool",
                        .ToolName = ejecucion.Nombre,
                        .Content = JsonSerializer.Serialize(ejecucion.Datos, _jsonOptions)
                    })
                Next
            Next

            resultado.Texto = "La consulta excedió la cantidad máxima de pasos permitidos. Reformulá el pedido con un proveedor específico."
            Return resultado
        Finally
            _semaphore.Release()
        End Try
    End Function

    Private Function EjecutarHerramienta(toolCall As OllamaToolCall) As ChatToolResult
        If toolCall Is Nothing OrElse toolCall.Function Is Nothing Then
            Return ResultadoError("herramienta_invalida", "Ollama solicitó una herramienta sin definición.")
        End If

        Dim nombre = If(toolCall.Function.Name, String.Empty).Trim()
        Try
            Select Case nombre
                Case "buscar_proveedores"
                    Dim texto = ObtenerTexto(toolCall.Function.Arguments, "texto")
                    Dim limite = ObtenerEntero(toolCall.Function.Arguments, "limite", 10, 1, 20)
                    Dim proveedores = _cuentas.BuscarProveedores(texto, limite)
                    Dim datos = New With {
                        .exito = True,
                        .cantidad = proveedores.Count,
                        .proveedores = proveedores.Select(Function(p) New With {
                            .nro_cuenta = p.NroCuenta,
                            .nombre = p.Nombre,
                            .cuit = p.Cuit
                        }).ToList()
                    }
                    Return New ChatToolResult With {.Nombre = nombre, .Datos = datos}

                Case "listar_facturas_pendientes"
                    Dim nroCuenta = ObtenerEntero(toolCall.Function.Arguments, "nro_cuenta", 0, 1, Integer.MaxValue)
                    If nroCuenta <= 0 Then
                        Return ResultadoError(nombre, "El número de cuenta debe ser mayor que cero.")
                    End If

                    Dim limite = ObtenerEntero(toolCall.Function.Arguments, "limite", 100, 1, 200)
                    Dim facturas = _cuentas.ListarFacturasPendientes(nroCuenta, limite)
                    Dim total = facturas.Sum(Function(f) f.SaldoPendiente)
                    Dim datos = New With {
                        .exito = True,
                        .nro_cuenta = nroCuenta,
                        .cantidad = facturas.Count,
                        .saldo_pendiente_total = total,
                        .facturas = facturas
                    }
                    Return New ChatToolResult With {.Nombre = nombre, .Datos = datos}

                Case "obtener_saldo_actual"
                    Dim nroCuenta = ObtenerEntero(toolCall.Function.Arguments, "nro_cuenta", 0, 1, Integer.MaxValue)
                    If nroCuenta <= 0 Then Return ResultadoError(nombre, "El número de cuenta debe ser mayor que cero.")

                    Dim resumen = _cuentas.ObtenerSaldoActualCalculado(nroCuenta)
                    Dim datos = New With {
                        .exito = True,
                        .nro_cuenta = nroCuenta,
                        .proveedor = resumen.Proveedor.Nombre,
                        .cuit = resumen.Proveedor.Cuit,
                        .saldo_calculado = resumen.SaldoCalculado,
                        .tipo_saldo = DescribirSaldo(resumen.SaldoCalculado),
                        .saldo_actual_maestro = resumen.Proveedor.SaldoActualMaestro,
                        .diferencia_con_saldo_actual = resumen.DiferenciaConSaldoActual,
                        .cantidad_movimientos = resumen.Movimientos.Count
                    }
                    Return New ChatToolResult With {.Nombre = nombre, .Datos = datos}

                Case "obtener_resumen_historico"
                    Dim nroCuenta = ObtenerEntero(toolCall.Function.Arguments, "nro_cuenta", 0, 1, Integer.MaxValue)
                    If nroCuenta <= 0 Then Return ResultadoError(nombre, "El número de cuenta debe ser mayor que cero.")

                    Dim desde = ObtenerFecha(toolCall.Function.Arguments, "desde", New Date(Date.Today.Year, 1, 1))
                    Dim hasta = ObtenerFecha(toolCall.Function.Arguments, "hasta", Date.Today)
                    If desde > hasta Then Return ResultadoError(nombre, "La fecha desde no puede ser posterior a la fecha hasta.")

                    Dim limite = ObtenerEntero(toolCall.Function.Arguments, "limite_movimientos", 100, 1, 200)
                    Dim resumen = _cuentas.ObtenerResumenHistorico(nroCuenta, desde, hasta)
                    Dim cantidad = resumen.Movimientos.Count
                    Dim movimientos = resumen.Movimientos.Skip(Math.Max(0, cantidad - limite)).ToList()
                    Dim diferencia As Decimal? = Nothing
                    If hasta.Date >= Date.Today Then diferencia = resumen.DiferenciaConSaldoActual

                    Dim datos = New With {
                        .exito = True,
                        .nro_cuenta = nroCuenta,
                        .proveedor = resumen.Proveedor.Nombre,
                        .desde = resumen.Desde,
                        .hasta = resumen.Hasta,
                        .saldo_inicial = resumen.SaldoInicial,
                        .total_debe = resumen.Movimientos.Sum(Function(m) m.Debe),
                        .total_haber = resumen.Movimientos.Sum(Function(m) m.Haber),
                        .saldo_final = resumen.SaldoCalculado,
                        .tipo_saldo = DescribirSaldo(resumen.SaldoCalculado),
                        .saldo_actual_maestro = resumen.Proveedor.SaldoActualMaestro,
                        .diferencia_con_saldo_actual = diferencia,
                        .cantidad_movimientos = cantidad,
                        .movimientos_mostrados = movimientos.Count,
                        .detalle_truncado = cantidad > movimientos.Count,
                        .movimientos = movimientos
                    }
                    Return New ChatToolResult With {.Nombre = nombre, .Datos = datos}

                Case "listar_mayores_deudas"
                    Dim limite = ObtenerEntero(toolCall.Function.Arguments, "limite", 10, 1, 50)
                    Dim proveedores = _cuentas.ListarMayoresDeudas(limite)
                    Dim datos = New With {
                        .exito = True,
                        .cantidad = proveedores.Count,
                        .criterio = "Saldo histórico calculado positivo, ordenado de mayor a menor",
                        .deuda_total_listada = proveedores.Sum(Function(p) p.SaldoCalculado),
                        .proveedores = proveedores
                    }
                    Return New ChatToolResult With {.Nombre = nombre, .Datos = datos}

                Case "analizar_deuda_total"
                    Dim limite = ObtenerEntero(toolCall.Function.Arguments, "limite_detalle", 50, 1, 200)
                    Dim analisis = _cuentas.ObtenerAnalisisDeudaTotal(limite)
                    Dim datos = New With {
                        .exito = True, .deuda_bruta = analisis.DeudaBruta,
                        .creditos_a_favor = analisis.CreditosAFavor,
                        .posicion_neta = analisis.PosicionNeta,
                        .cantidad_con_deuda = analisis.CantidadConDeuda,
                        .cantidad_con_credito = analisis.CantidadConCredito,
                        .proveedores = analisis.Proveedores}
                    Return New ChatToolResult With {.Nombre = nombre, .Datos = datos}

                Case "analizar_gasto_mensual"
                    Dim inicioDefecto = New Date(Date.Today.Year, Date.Today.Month, 1).AddMonths(-11)
                    Dim desde = ObtenerFecha(toolCall.Function.Arguments, "desde", inicioDefecto)
                    Dim hasta = ObtenerFecha(toolCall.Function.Arguments, "hasta", Date.Today)
                    If desde > hasta Then Return ResultadoError(nombre, "La fecha desde no puede ser posterior a la fecha hasta.")
                    Dim analisis = _cuentas.ObtenerPromedioComprasMensuales(desde, hasta)
                    Dim datos = New With {
                        .exito = True, .desde = analisis.Desde, .hasta = analisis.Hasta,
                        .total_compras = analisis.TotalPeriodo,
                        .promedio_mensual = analisis.PromedioMensual,
                        .cantidad_meses = analisis.Meses.Count, .meses = analisis.Meses}
                    Return New ChatToolResult With {.Nombre = nombre, .Datos = datos}

                Case "listar_proveedores_mas_pagados"
                    Dim desde = ObtenerFecha(toolCall.Function.Arguments, "desde", New Date(Date.Today.Year, 1, 1))
                    Dim hasta = ObtenerFecha(toolCall.Function.Arguments, "hasta", Date.Today)
                    If desde > hasta Then Return ResultadoError(nombre, "La fecha desde no puede ser posterior a la fecha hasta.")
                    Dim limite = ObtenerEntero(toolCall.Function.Arguments, "limite", 10, 1, 100)
                    Dim proveedores = _cuentas.ListarProveedoresMasPagados(desde, hasta, limite)
                    Dim datos = New With {
                        .exito = True, .desde = desde, .hasta = hasta,
                        .total_pagado_listado = proveedores.Sum(Function(p) p.TotalPagado),
                        .cantidad = proveedores.Count, .proveedores = proveedores}
                    Return New ChatToolResult With {.Nombre = nombre, .Datos = datos}

                Case "obtener_evolucion_mensual"
                    Dim inicioDefecto = New Date(Date.Today.Year, Date.Today.Month, 1).AddMonths(-11)
                    Dim desde = ObtenerFecha(toolCall.Function.Arguments, "desde", inicioDefecto)
                    Dim hasta = ObtenerFecha(toolCall.Function.Arguments, "hasta", Date.Today)
                    If desde > hasta Then Return ResultadoError(nombre, "La fecha desde no puede ser posterior a la fecha hasta.")
                    Dim meses = _cuentas.ObtenerEvolucionMensual(desde, hasta)
                    Dim datos = New With {
                        .exito = True, .desde = desde, .hasta = hasta,
                        .cantidad_meses = meses.Count, .meses = meses}
                    Return New ChatToolResult With {.Nombre = nombre, .Datos = datos}

                Case "listar_diferencias_saldo"
                    Dim tolerancia = ObtenerDecimal(toolCall.Function.Arguments, "tolerancia", 1D, 0D)
                    Dim limite = ObtenerEntero(toolCall.Function.Arguments, "limite", 100, 1, 500)
                    Dim proveedores = _cuentas.ListarDiferenciasSaldo(tolerancia, limite)
                    Dim datos = New With {
                        .exito = True, .tolerancia = tolerancia,
                        .cantidad = proveedores.Count, .proveedores = proveedores}
                    Return New ChatToolResult With {.Nombre = nombre, .Datos = datos}

                Case "listar_facturas_pendientes_periodo"
                    Dim desde = ObtenerFecha(toolCall.Function.Arguments, "desde", New Date(Date.Today.Year, Date.Today.Month, 1))
                    Dim hasta = ObtenerFecha(toolCall.Function.Arguments, "hasta", Date.Today)
                    If desde > hasta Then Return ResultadoError(nombre, "La fecha desde no puede ser posterior a la fecha hasta.")
                    Dim nroCuenta = ObtenerEntero(toolCall.Function.Arguments, "nro_cuenta", 0, 0, Integer.MaxValue)
                    Dim limite = ObtenerEntero(toolCall.Function.Arguments, "limite", 500, 1, 1000)
                    Dim facturas = _cuentas.ListarFacturasPendientesPeriodo(desde, hasta, nroCuenta, limite)
                    Dim datos = New With {
                        .exito = True, .desde = desde, .hasta = hasta,
                        .nro_cuenta = If(nroCuenta > 0, CType(nroCuenta, Integer?), Nothing),
                        .cantidad = facturas.Count,
                        .saldo_pendiente_total = facturas.Sum(Function(f) f.SaldoPendiente),
                        .facturas = facturas}
                    Return New ChatToolResult With {.Nombre = nombre, .Datos = datos}

                Case "listar_vehiculos"
                    Dim texto = ObtenerTexto(toolCall.Function.Arguments, "texto")
                    Dim soloActivos = ObtenerBooleano(toolCall.Function.Arguments, "solo_activos", True)
                    Dim limite = ObtenerEntero(toolCall.Function.Arguments, "limite", 200, 1, 500)
                    Dim vehiculos = _vehiculos.ListarVehiculos(texto, soloActivos, limite)
                    Dim datos = New With {
                        .exito = True, .cantidad = vehiculos.Count, .vehiculos = vehiculos}
                    Return New ChatToolResult With {.Nombre = nombre, .Datos = datos}

                Case "listar_servicios_realizados"
                    Dim ids = ResolverVehiculos(toolCall.Function.Arguments)
                    If ids.Count = 0 Then
                        Dim vehiculos = _vehiculos.ListarVehiculos(String.Empty, True, 200)
                        Return New ChatToolResult With {
                            .Nombre = nombre,
                            .Datos = New With {
                                .exito = True, .requiere_seleccion = True,
                                .mensaje = "Indique uno o varios IdVehiculo.", .vehiculos = vehiculos}}
                    End If
                    Dim desde = ObtenerFecha(toolCall.Function.Arguments, "desde", New Date(Date.Today.Year, 1, 1))
                    Dim hasta = ObtenerFecha(toolCall.Function.Arguments, "hasta", Date.Today)
                    If desde > hasta Then Return ResultadoError(nombre, "La fecha desde no puede ser posterior a la fecha hasta.")
                    Dim limite = ObtenerEntero(toolCall.Function.Arguments, "limite", 500, 1, 1000)
                    Dim servicios = _vehiculos.ListarServiciosRealizados(desde, hasta, ids, limite)
                    Dim datos = New With {
                        .exito = True, .desde = desde, .hasta = hasta,
                        .ids_vehiculo = ids, .cantidad = servicios.Count,
                        .total_importe = servicios.Sum(Function(s) s.TotalServicio), .servicios = servicios}
                    Return New ChatToolResult With {.Nombre = nombre, .Datos = datos}

                Case "comparar_servicios_vehiculos"
                    Dim ids = ResolverVehiculos(toolCall.Function.Arguments)
                    If ids.Count < 2 Then
                        Dim vehiculos = _vehiculos.ListarVehiculos(String.Empty, True, 200)
                        Return New ChatToolResult With {
                            .Nombre = nombre,
                            .Datos = New With {
                                .exito = True, .requiere_seleccion = True,
                                .mensaje = "Indique dos o mas IdVehiculo para comparar.", .vehiculos = vehiculos}}
                    End If
                    Dim desde = ObtenerFecha(toolCall.Function.Arguments, "desde", New Date(Date.Today.Year, 1, 1))
                    Dim hasta = ObtenerFecha(toolCall.Function.Arguments, "hasta", Date.Today)
                    If desde > hasta Then Return ResultadoError(nombre, "La fecha desde no puede ser posterior a la fecha hasta.")
                    Dim comparacion = _vehiculos.CompararServiciosVehiculos(desde, hasta, ids)
                    Dim datos = New With {
                        .exito = True, .desde = desde, .hasta = hasta,
                        .ids_vehiculo = ids, .cantidad_filas = comparacion.Count, .comparacion = comparacion}
                    Return New ChatToolResult With {.Nombre = nombre, .Datos = datos}

                Case "ranking_servicios"
                    Dim desde = ObtenerFecha(toolCall.Function.Arguments, "desde", New Date(Date.Today.Year, 1, 1))
                    Dim hasta = ObtenerFecha(toolCall.Function.Arguments, "hasta", Date.Today)
                    If desde > hasta Then Return ResultadoError(nombre, "La fecha desde no puede ser posterior a la fecha hasta.")
                    Dim agrupacion = ObtenerTexto(toolCall.Function.Arguments, "agrupacion")
                    Dim limite = ObtenerEntero(toolCall.Function.Arguments, "limite", 50, 1, 500)
                    Dim ranking = _vehiculos.RankingServicios(desde, hasta, agrupacion, limite)
                    Dim datos = New With {
                        .exito = True, .desde = desde, .hasta = hasta,
                        .agrupacion = If(String.IsNullOrWhiteSpace(agrupacion), "total", agrupacion),
                        .cantidad_filas = ranking.Count, .ranking = ranking}
                    Return New ChatToolResult With {.Nombre = nombre, .Datos = datos}

                Case "solicitar_exportacion_excel"
                    Return New ChatToolResult With {
                        .Nombre = nombre,
                        .Datos = New With {.exito = True, .mensaje = "La aplicación abrirá el diálogo Guardar como para exportar el resultado visible."}
                    }

                Case Else
                    Return ResultadoError(nombre, $"La herramienta '{nombre}' no está permitida.")
            End Select
        Catch ex As Exception
            Return ResultadoError(nombre, ex.Message)
        End Try
    End Function

    Private Shared Function CrearHerramientas() As List(Of OllamaToolDefinition)
        Dim buscarParametros = JsonSerializer.SerializeToElement(New With {
            .type = "object",
            .required = New String() {"texto"},
            .properties = New With {
                .texto = New With {
                    .type = "string",
                    .description = "Nombre, CUIT o número de cuenta indicado por el usuario."
                },
                .limite = New With {
                    .type = "integer",
                    .description = "Cantidad máxima de coincidencias, entre 1 y 20."
                }
            }
        })

        Dim facturasParametros = JsonSerializer.SerializeToElement(New With {
            .type = "object",
            .required = New String() {"nro_cuenta"},
            .properties = New With {
                .nro_cuenta = New With {
                    .type = "integer",
                    .description = "Número de cuenta exacto del proveedor."
                },
                .limite = New With {
                    .type = "integer",
                    .description = "Cantidad máxima de facturas, entre 1 y 200."
                }
            }
        })

        Dim saldoParametros = JsonSerializer.SerializeToElement(New With {
            .type = "object",
            .required = New String() {"nro_cuenta"},
            .properties = New With {
                .nro_cuenta = New With {.type = "integer", .description = "Número de cuenta exacto del proveedor."}
            }
        })

        Dim resumenParametros = JsonSerializer.SerializeToElement(New With {
            .type = "object",
            .required = New String() {"nro_cuenta"},
            .properties = New With {
                .nro_cuenta = New With {.type = "integer", .description = "Número de cuenta exacto del proveedor."},
                .desde = New With {.type = "string", .description = "Fecha inicial inclusiva en formato YYYY-MM-DD. Si se omite, comienza el 1 de enero del año actual."},
                .hasta = New With {.type = "string", .description = "Fecha final inclusiva en formato YYYY-MM-DD. Si se omite, usa la fecha de hoy."},
                .limite_movimientos = New With {.type = "integer", .description = "Cantidad máxima de movimientos detallados, entre 1 y 200."}
            }
        })

        Dim mayoresDeudasParametros = JsonSerializer.SerializeToElement(New With {
            .type = "object",
            .properties = New With {
                .limite = New With {.type = "integer", .description = "Cantidad de proveedores a listar, entre 1 y 50."}
            }
        })

        Dim deudaTotalParametros = JsonSerializer.SerializeToElement(New With {
            .type = "object",
            .properties = New With {
                .limite_detalle = New With {.type = "integer", .description = "Cantidad de proveedores deudores a detallar, entre 1 y 200."}}
        })

        Dim periodoParametros = JsonSerializer.SerializeToElement(New With {
            .type = "object",
            .properties = New With {
                .desde = New With {.type = "string", .description = "Fecha inicial inclusiva YYYY-MM-DD."},
                .hasta = New With {.type = "string", .description = "Fecha final inclusiva YYYY-MM-DD."}}
        })

        Dim pagosParametros = JsonSerializer.SerializeToElement(New With {
            .type = "object",
            .properties = New With {
                .desde = New With {.type = "string", .description = "Fecha inicial inclusiva YYYY-MM-DD."},
                .hasta = New With {.type = "string", .description = "Fecha final inclusiva YYYY-MM-DD."},
                .limite = New With {.type = "integer", .description = "Cantidad de proveedores, entre 1 y 100."}}
        })

        Dim diferenciasParametros = JsonSerializer.SerializeToElement(New With {
            .type = "object",
            .properties = New With {
                .tolerancia = New With {.type = "number", .description = "Diferencia absoluta minima a informar. Por defecto 1."},
                .limite = New With {.type = "integer", .description = "Cantidad maxima de proveedores, entre 1 y 500."}}
        })

        Dim facturasPeriodoParametros = JsonSerializer.SerializeToElement(New With {
            .type = "object",
            .properties = New With {
                .desde = New With {.type = "string", .description = "Fecha de factura inicial inclusiva YYYY-MM-DD."},
                .hasta = New With {.type = "string", .description = "Fecha de factura final inclusiva YYYY-MM-DD."},
                .nro_cuenta = New With {.type = "integer", .description = "Proveedor opcional. Omitir para todos."},
                .limite = New With {.type = "integer", .description = "Cantidad maxima, entre 1 y 1000."}}
        })

        Dim vehiculosParametros = JsonSerializer.SerializeToElement(New With {
            .type = "object",
            .properties = New With {
                .texto = New With {.type = "string", .description = "Patente, marca o modelo opcional."},
                .solo_activos = New With {.type = "boolean", .description = "Por defecto true."},
                .limite = New With {.type = "integer", .description = "Cantidad maxima, entre 1 y 500."}}
        })

        Dim serviciosParametros = JsonSerializer.SerializeToElement(New With {
            .type = "object",
            .properties = New With {
                .ids_vehiculo = New With {
                    .type = "array", .items = New With {.type = "integer"},
                    .description = "Identificadores numericos IdVehiculo. Para patentes usar patentes."},
                .patentes = New With {
                    .type = "array", .items = New With {.type = "string"},
                    .description = "Una o varias patentes completas como texto, por ejemplo AG347UL. El sistema resuelve sus IdVehiculo."},
                .desde = New With {.type = "string", .description = "Fecha inicial inclusiva YYYY-MM-DD."},
                .hasta = New With {.type = "string", .description = "Fecha final inclusiva YYYY-MM-DD."},
                .limite = New With {.type = "integer", .description = "Cantidad maxima de servicios, entre 1 y 1000."}}
        })

        Dim comparacionParametros = JsonSerializer.SerializeToElement(New With {
            .type = "object",
            .properties = New With {
                .ids_vehiculo = New With {
                    .type = "array", .items = New With {.type = "integer"},
                    .description = "Identificadores numericos IdVehiculo a comparar. Para patentes usar patentes."},
                .patentes = New With {
                    .type = "array", .items = New With {.type = "string"},
                    .description = "Patentes completas de los vehiculos a comparar como texto. El sistema resuelve sus IdVehiculo."},
                .desde = New With {.type = "string", .description = "Fecha inicial inclusiva YYYY-MM-DD."},
                .hasta = New With {.type = "string", .description = "Fecha final inclusiva YYYY-MM-DD."}}
        })

        Dim rankingParametros = JsonSerializer.SerializeToElement(New With {
            .type = "object",
            .properties = New With {
                .desde = New With {.type = "string", .description = "Fecha inicial inclusiva YYYY-MM-DD."},
                .hasta = New With {.type = "string", .description = "Fecha final inclusiva YYYY-MM-DD."},
                .agrupacion = New With {
                    .type = "string", .enum = New String() {"total", "sucursal", "vehiculo"},
                    .description = "Nivel solicitado para el ranking."},
                .limite = New With {.type = "integer", .description = "Cantidad maxima de filas, entre 1 y 500."}}
        })

        Dim exportarParametros = JsonSerializer.SerializeToElement(New With {
            .type = "object",
            .properties = New Dictionary(Of String, Object)()
        })

        Return New List(Of OllamaToolDefinition) From {
            New OllamaToolDefinition With {
                .Function = New OllamaToolFunctionDefinition With {
                    .Name = "buscar_proveedores",
                    .Description = "Busca proveedores activos por nombre, CUIT o número de cuenta. Usar antes de consultar facturas cuando no se conoce el número de cuenta exacto.",
                    .Parameters = buscarParametros
                }
            },
            New OllamaToolDefinition With {
                .Function = New OllamaToolFunctionDefinition With {
                    .Name = "listar_facturas_pendientes",
                    .Description = "Lista las facturas impagas o parcialmente pagadas de un proveedor. El saldo pendiente de cada factura ya está calculado como Monto menos ACuenta.",
                    .Parameters = facturasParametros
                }
            },
            New OllamaToolDefinition With {
                .Function = New OllamaToolFunctionDefinition With {
                    .Name = "obtener_saldo_actual",
                    .Description = "Obtiene el saldo real actual de un proveedor calculado con DetaCtaCte y DetaCtaCteAnual y lo compara con MaeCtaCte.SaldoActual.",
                    .Parameters = saldoParametros
                }
            },
            New OllamaToolDefinition With {
                .Function = New OllamaToolFunctionDefinition With {
                    .Name = "obtener_resumen_historico",
                    .Description = "Obtiene el resumen histórico de cuenta de un proveedor para un período, con saldo inicial, debe, haber, saldo final y movimientos.",
                    .Parameters = resumenParametros
                }
            },
            New OllamaToolDefinition With {
                .Function = New OllamaToolFunctionDefinition With {
                    .Name = "listar_mayores_deudas",
                    .Description = "Lista los proveedores a los que más se les debe según el saldo histórico calculado. Solo incluye saldos positivos.",
                    .Parameters = mayoresDeudasParametros
                }
            },
            New OllamaToolDefinition With {
                .Function = New OllamaToolFunctionDefinition With {
                    .Name = "analizar_deuda_total",
                    .Description = "Calcula la deuda bruta total, los creditos a favor, la posicion neta y la participacion de cada proveedor usando todo el historial.",
                    .Parameters = deudaTotalParametros
                }
            },
            New OllamaToolDefinition With {
                .Function = New OllamaToolFunctionDefinition With {
                    .Name = "analizar_gasto_mensual",
                    .Description = "Calcula compras o gasto en proveedores por mes y su promedio para un periodo. Son movimientos que aumentan la deuda, no pagos realizados.",
                    .Parameters = periodoParametros
                }
            },
            New OllamaToolDefinition With {
                .Function = New OllamaToolFunctionDefinition With {
                    .Name = "listar_proveedores_mas_pagados",
                    .Description = "Lista a que proveedores se les pago mas en un periodo. Usa movimientos que reducen la deuda.",
                    .Parameters = pagosParametros
                }
            },
            New OllamaToolDefinition With {
                .Function = New OllamaToolFunctionDefinition With {
                    .Name = "obtener_evolucion_mensual",
                    .Description = "Muestra por mes compras, pagos, variacion, deuda bruta, creditos y posicion neta al cierre.",
                    .Parameters = periodoParametros
                }
            },
            New OllamaToolDefinition With {
                .Function = New OllamaToolFunctionDefinition With {
                    .Name = "listar_diferencias_saldo",
                    .Description = "Lista proveedores cuyo saldo historico calculado difiere de MaeCtaCte.SaldoActual.",
                    .Parameters = diferenciasParametros
                }
            },
            New OllamaToolDefinition With {
                .Function = New OllamaToolFunctionDefinition With {
                    .Name = "listar_facturas_pendientes_periodo",
                    .Description = "Lista facturas impagas o parcialmente pagadas cuya fecha de factura pertenece a un periodo, para todos los proveedores o uno opcional.",
                    .Parameters = facturasPeriodoParametros
                }
            },
            New OllamaToolDefinition With {
                .Function = New OllamaToolFunctionDefinition With {
                    .Name = "listar_vehiculos",
                    .Description = "Lista vehiculos con IdVehiculo, patente, descripcion, kilometraje actual y sucursal. Usar para que el usuario elija vehiculos.",
                    .Parameters = vehiculosParametros
                }
            },
            New OllamaToolDefinition With {
                .Function = New OllamaToolFunctionDefinition With {
                    .Name = "listar_servicios_realizados",
                    .Description = "Lista servicios realizados en un periodo para vehiculos indicados por patentes de texto o ids_vehiculo numericos. Sin vehiculos, devuelve un listado para seleccion.",
                    .Parameters = serviciosParametros
                }
            },
            New OllamaToolDefinition With {
                .Function = New OllamaToolFunctionDefinition With {
                    .Name = "comparar_servicios_vehiculos",
                    .Description = "Compara servicios entre dos o mas vehiculos indicados por patentes de texto o ids_vehiculo numericos. Si faltan vehiculos, devuelve un listado para seleccion.",
                    .Parameters = comparacionParametros
                }
            },
            New OllamaToolDefinition With {
                .Function = New OllamaToolFunctionDefinition With {
                    .Name = "ranking_servicios",
                    .Description = "Ranking de tipos de servicios por frecuencia en un periodo, agrupado en total general, por sucursal o por vehiculo.",
                    .Parameters = rankingParametros
                }
            },
            New OllamaToolDefinition With {
                .Function = New OllamaToolFunctionDefinition With {
                    .Name = "solicitar_exportacion_excel",
                    .Description = "Solicita exportar a un archivo Excel el último resultado tabular visible. Usar solo cuando el usuario pida explícitamente exportar, descargar o guardar el resultado en Excel.",
                    .Parameters = exportarParametros
                }
            }
        }
    End Function

    Private Shared Function ObtenerTexto(argumentos As JsonElement, propiedad As String) As String
        If argumentos.ValueKind <> JsonValueKind.Object Then Return String.Empty
        Dim valor As JsonElement
        If Not argumentos.TryGetProperty(propiedad, valor) Then Return String.Empty
        If valor.ValueKind = JsonValueKind.String Then Return If(valor.GetString(), String.Empty).Trim()
        Return valor.ToString().Trim()
    End Function

    Private Shared Function ObtenerEntero(argumentos As JsonElement,
                                          propiedad As String,
                                          valorDefecto As Integer,
                                          minimo As Integer,
                                          maximo As Integer) As Integer
        If argumentos.ValueKind <> JsonValueKind.Object Then Return valorDefecto

        Dim valor As JsonElement
        If Not argumentos.TryGetProperty(propiedad, valor) Then Return valorDefecto

        Dim numero As Integer
        If valor.ValueKind = JsonValueKind.Number AndAlso valor.TryGetInt32(numero) Then
            Return Math.Max(minimo, Math.Min(maximo, numero))
        End If
        If valor.ValueKind = JsonValueKind.String AndAlso Integer.TryParse(valor.GetString(), numero) Then
            Return Math.Max(minimo, Math.Min(maximo, numero))
        End If
        Return valorDefecto
    End Function

    Private Function ResolverVehiculos(argumentos As JsonElement) As List(Of Integer)
        Dim ids As New List(Of Integer)
        Dim patentes = ObtenerReferenciasVehiculo(argumentos, "patentes")
        ' Compatibilidad si el modelo envia una patente en el campo de identificadores.
        For Each referencia In ObtenerReferenciasVehiculo(argumentos, "ids_vehiculo")
            Dim id As Integer
            If Not Integer.TryParse(referencia, id) Then
                patentes.Add(referencia)
            ElseIf id <= 0 Then
                Throw New ArgumentException("Los IdVehiculo deben ser mayores que cero.")
            Else
                ids.Add(id)
            End If
        Next
        If ids.Count + patentes.Count > 100 Then
            Throw New ArgumentException("Se pueden consultar hasta 100 vehículos por vez.")
        End If
        ids.AddRange(_vehiculos.ResolverIdsPorPatentes(patentes))
        Return ids.Distinct().ToList()
    End Function

    Private Shared Function ObtenerReferenciasVehiculo(argumentos As JsonElement, propiedad As String) As List(Of String)
        Dim resultado As New List(Of String)
        Dim valor As JsonElement
        If argumentos.ValueKind <> JsonValueKind.Object OrElse Not argumentos.TryGetProperty(propiedad, valor) Then Return resultado
        If valor.ValueKind = JsonValueKind.Null Then Return resultado
        Dim elementos = If(valor.ValueKind = JsonValueKind.Array, valor.EnumerateArray().ToList(), New List(Of JsonElement) From {valor})
        For Each elemento In elementos
            If elemento.ValueKind <> JsonValueKind.String AndAlso elemento.ValueKind <> JsonValueKind.Number Then
                Throw New ArgumentException("Los vehículos deben indicarse mediante patentes de texto o identificadores numéricos.")
            End If
            Dim texto = If(elemento.ValueKind = JsonValueKind.String, elemento.GetString(), elemento.ToString())
            For Each parte In texto.Split(New Char() {","c, ";"c}, StringSplitOptions.RemoveEmptyEntries)
                If Not String.IsNullOrWhiteSpace(parte) Then resultado.Add(parte.Trim())
            Next
        Next
        Return resultado.Distinct(StringComparer.OrdinalIgnoreCase).ToList()
    End Function

    Private Shared Function ObtenerBooleano(argumentos As JsonElement, propiedad As String,
                                             valorDefecto As Boolean) As Boolean
        If argumentos.ValueKind <> JsonValueKind.Object Then Return valorDefecto
        Dim valor As JsonElement
        If Not argumentos.TryGetProperty(propiedad, valor) Then Return valorDefecto
        If valor.ValueKind = JsonValueKind.True Then Return True
        If valor.ValueKind = JsonValueKind.False Then Return False
        If valor.ValueKind = JsonValueKind.String Then
            Dim resultado As Boolean
            If Boolean.TryParse(valor.GetString(), resultado) Then Return resultado
        End If
        Return valorDefecto
    End Function

    Private Shared Function ObtenerDecimal(argumentos As JsonElement, propiedad As String,
                                            valorDefecto As Decimal, minimo As Decimal) As Decimal
        Dim texto = ObtenerTexto(argumentos, propiedad)
        If texto = String.Empty Then Return valorDefecto
        Dim numero As Decimal
        If Decimal.TryParse(texto, Globalization.NumberStyles.Any,
                            Globalization.CultureInfo.InvariantCulture, numero) Then
            Return Math.Max(minimo, numero)
        End If
        Return valorDefecto
    End Function

    Private Shared Function ObtenerFecha(argumentos As JsonElement, propiedad As String, valorDefecto As DateTime) As DateTime
        Dim texto = ObtenerTexto(argumentos, propiedad)
        If texto = String.Empty Then Return valorDefecto.Date

        Dim fecha As DateTime
        If DateTime.TryParse(texto, Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, fecha) Then
            Return fecha.Date
        End If
        Return valorDefecto.Date
    End Function

    Private Shared Function DescribirSaldo(saldo As Decimal) As String
        If saldo > 0D Then
            Return "Deuda con el proveedor"
        End If
        If saldo < 0D Then
            Return "Crédito a favor nuestro por pagos en exceso"
        End If
        Return "Cuenta sin saldo"
    End Function

    Private Shared Function ResultadoError(nombre As String, mensaje As String) As ChatToolResult
        Return New ChatToolResult With {
            .Nombre = If(nombre, String.Empty),
            .Datos = New With {.exito = False, .error = mensaje}
        }
    End Function

    Private Sub RecortarHistorial()
        If _mensajes.Count <= MaximoMensajesConservados Then Return
        Dim sistema = _mensajes(0)
        Dim recientes = _mensajes.Skip(Math.Max(1, _mensajes.Count - (MaximoMensajesConservados - 1))).ToList()
        _mensajes.Clear()
        _mensajes.Add(sistema)
        _mensajes.AddRange(recientes)
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        _semaphore.Dispose()
        If _disposeOllama Then _ollama.Dispose()
    End Sub
End Class
