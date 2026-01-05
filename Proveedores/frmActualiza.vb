Imports DSM = DataSourceManager.Lib.DataSourceManager
Imports DBM = DataSourceManager.Lib.DatabaseManager
Imports System.Runtime.CompilerServices

Public Class frmActualiza
    Private Shared instancia As frmActualiza

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmActualiza()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmActualiza_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Private Sub frmActualiza_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtpFechaHasta.Value = Date.Now
    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        btnActualizar.Enabled = False
        btnSalir.Enabled = False
        Actualizar()
        btnActualizar.Enabled = True
        btnSalir.Enabled = True
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub Actualizar()
        Dim sqlUpdateNovedades = "UPDATE MaeCtaCte SET SaldoActual = 0 WHERE (SaldoActual < 1) And (SaldoActual > -1) And (SaldoActual <> 0)"
        DSM.Execute(DSM.Proveedores, sqlUpdateNovedades)

        lblControlando.ForeColor = Color.LightSeaGreen
        lblControlando.Refresh()

        Dim sqlControlarNovedades = "SELECT * FROM NOVECTACTE WHERE IDIMPUTACION = 0 OR NROCUENTA = 0;"
        Dim dtNovedades As DataTable = DSM.ExecuteQuery(DSM.Proveedores, sqlControlarNovedades)
        If dtNovedades.Rows.Count > 0 Then
            MessageBox.Show("NOVEDADES SIN IMPUTACION 01, 54 O  FALTA NROCUENTA ..... ABORTADO")
            Exit Sub
        End If

        Dim sqlUpdateCobradoIDI50 = "UPDATE DetaCtaCte SET DetaCtaCte.Cobrado = 1 where idimputacion > 50;"
        DSM.Execute(DSM.Proveedores, sqlUpdateCobradoIDI50)

        Dim sqlDeleteCta01 = "DELETE FROM DetaCtaCte where NROCUENTA = 0;"
        DSM.Execute(DSM.Proveedores, sqlDeleteCta01)

        Dim sqlUpdateNoveCtaCte = "
            UPDATE NoveCtaCte SET IdCtaCte = MaeCtaCte.IdCtaCte 
            FROM NoveCtaCte JOIN MAECTACTE ON NoveCtaCte.NroCuenta = MaeCtaCte.NroCuenta"
        DSM.Execute(DSM.Proveedores, sqlUpdateNoveCtaCte)

        Dim sqlBKP = "
            INSERT INTO bkpNove (
                IdCtaCte, NroCuenta, NroFactura, Monto, NroComprobante, NombreComprobante, Condicion, Fecha, IdImputacion, CtaMonto, Monto1, 
                CtaMonto1, Monto2, CtaMonto2, ComprasRNI, CtaRNI, Neto105, CtaNeto105, Neto21, Cta21, Neto27, Cta27, Exento, CtaExento, IVA, CtaIva, Ganancias, 
                CtaGanancia, Retenciva, CtaRetencion, IngresosB, CtaIB, ACuenta, FechaVto, TipoValor, NroCheque, RegInterno, Sucursal, Cobrado, Asiento,cai, ActSaldo, 
                ActDeta, FondoFijo, cierre
            ) SELECT IdCtaCte, NroCuenta, NroFactura, Monto, NroComprobante, NombreComprobante, Condicion, Fecha, IdImputacion, CtaMonto, Monto1, 
                CtaMonto1, Monto2, CtaMonto2, ComprasRNI, CtaRNI, Neto105, CtaNeto105, Neto21, Cta21, Neto27, Cta27, Exento, CtaExento, IVA, CtaIva, Ganancias, 
                CtaGanancia, Retenciva, CtaRetencion, IngresosB, CtaIB, ACuenta, FechaVto, TipoValor, NroCheque, RegInterno, Sucursal, Cobrado, Asiento,cai, ActSaldo, 
                ActDeta, FondoFijo, FORMAT(@ahora, 'dd/MM/yyyy-HH:mm') FROM NoveCtaCte
            WHERE novectaCte.fecha <= format(@hasta, 'yyyy-MM-dd')"
        Dim parsBKP = CmdParams("@ahora", Date.Now, "@hasta", dtpFechaHasta.Value)
        DSM.Execute(DSM.Proveedores, sqlBKP, parsBKP)

        Dim sqlReorganizarNove = "SELECT * FROM NoveCtaCte WHERE IdCtaCte <> 0 AND NOVECTACTE.FECHA <= FORMAT(@hasta, 'yyyy-MM-dd')"
        Dim parsReorganizarNove = CmdParams("@hasta", dtpFechaHasta.Value)
        Dim dtNove As DataTable = DSM.ExecuteQuery(DSM.Proveedores, sqlReorganizarNove, parsReorganizarNove)

        Dim sqlColocacionNombresComprobante = "
            UPDATE NoveCtaCte SET NoveCtaCte.NombreComprobante = 'Orden de Pago'
            FROM NoveCtaCte WHERE NoveCtaCte.IDIMPUTACION = 54"
        DSM.Execute(DSM.Proveedores, sqlColocacionNombresComprobante)

        Dim sqlColocacionIDMaestro = "
            UPDATE NoveCtaCte SET NoveCtaCte.IdCtaCte = MaeCtaCte.IdCtaCte FROM NoveCtaCte JOIN MAECTACTE 
            ON NoveCtaCte.NroCuenta = MaeCtaCte.NroCuenta"
        DSM.Execute(DSM.Proveedores, sqlColocacionIDMaestro)

        Dim sqlAutocancelables = "Select codcontable from plancuentas where autocancelable = 1"
        Dim dtAutocancelables As DataTable = DSM.ExecuteQuery(DSM.Proveedores, sqlAutocancelables)
        If dtAutocancelables.Rows.Count > 0 Then
            For Each row In dtAutocancelables.Rows
                Dim sqlUpdate = "
                    UPDATE NoveCtaCte SET NoveCtaCte.Cobrado = 1 
                    WHERE idimputacion IN(1,2,59) and (NoveCtaCte.CtaMonto = @codContable)"
                Dim parsUpdate = CmdParams("@codContable", row("CodContable"))
                DSM.Execute(DSM.Proveedores, sqlUpdate, parsUpdate)
            Next
        End If

        'Comprobacion de que hay registros en el archivo NoveCtaCte
        Dim sqlComprobarNove = "SELECT * FROM NoveCtaCte WHERE IdCtaCte <> 0 AND NOVECTACTE.FECHA <= @hasta"
        Dim parsComprobarNove = CmdParams("@hasta", dtpFechaHasta.Value)
        Dim dtNoveComprobar As DataTable = DSM.ExecuteQuery(DSM.Proveedores, sqlComprobarNove, parsComprobarNove)
        If dtNoveComprobar.Rows.Count = 0 Then
            MessageBox.Show("No hay registros de Novedades para Procesar...")
            Exit Sub
        End If

        lblControlando.ForeColor = SystemColors.ControlText
        lblControlando.Refresh()
        lblActualizando.ForeColor = Color.LightSeaGreen
        lblActualizando.Refresh()

        'Calculo de los saldos
        For Each NoveRow In dtNoveComprobar.Rows
            If NoveRow("cobrado") = 0 Then
                Dim sqlCuentas = "Select * from MaeCtaCte Where NroCuenta = @NroCuenta"
                Dim parsCuentas = CmdParams("@NroCuenta", NoveRow("NroCuenta"))
                Dim dtCuentas As DataTable = DSM.ExecuteQuery(DSM.Proveedores, sqlCuentas, parsCuentas)

                If dtCuentas.Rows.Count > 0 Then
                    Dim MaeRow = dtCuentas.Rows(0)

                    If NoveRow("ActSaldo") = 0 Then
                        Select Case NoveRow("idimputacion")
                            Case 1, 2, 10, 11
                                Dim ultimaCompra = DateValue(Date.Now)
                                If NoveRow("NroCuenta") = 6288 Then
                                    Dim saldoActual = MaeRow("SaldoActual") + NoveRow("Monto")
                                    Dim sqlActualizarSaldo =
                                        "UPDATE MaeCtaCte SET SaldoActual = @SaldoActual, UltimaCompra = @UltimaCompra WHERE NroCuenta = @NroCuenta"
                                    Dim parsActualizarSaldo = CmdParams("@SaldoActual", saldoActual, "@UltimaCompra", ultimaCompra, "@NroCuenta", NoveRow("NroCuenta"))
                                    DSM.Execute(DSM.Proveedores, sqlActualizarSaldo, parsActualizarSaldo)
                                Else
                                    Dim saldoActual = MaeRow("SaldoActual") + NoveRow("Monto") + NoveRow("Monto1") + NoveRow("Monto2")
                                    Dim sqlActualizarSaldo = "UPDATE MaeCtaCte SET SaldoActual = @SaldoActual WHERE NroCuenta = @NroCuenta"
                                    Dim parsActualizarSaldo = CmdParams("@SaldoActual", saldoActual, "@UltimaCompra", ultimaCompra, "@NroCuenta", NoveRow("NroCuenta"))
                                    DSM.Execute(DSM.Proveedores, sqlActualizarSaldo, parsActualizarSaldo)
                                End If
                            Case 5
                                Dim saldoDto = MaeRow("SALDODTO") + NoveRow("Monto") + NoveRow("Monto1") + NoveRow("Monto2")
                                Dim saldoActual = MaeRow("SaldoActual") + NoveRow("Monto") + NoveRow("Monto1") + NoveRow("Monto2")
                                Dim sqlActualizarSaldo = "UPDATE MaeCtaCte SET SaldoActual = @SaldoActual, SaldoDto = @SaldoDto WHERE NroCuenta = @NroCuenta"
                                Dim parsActualizarSaldo = CmdParams("@SaldoActual", saldoActual, "@SaldoDto", saldoDto, "@NroCuenta", NoveRow("NroCuenta"))
                                DSM.Execute(DSM.Proveedores, sqlActualizarSaldo, parsActualizarSaldo)
                            Case 56, 57
                                Dim saldoDto = MaeRow("SALDODTO") - NoveRow("Monto") - NoveRow("Monto1") - NoveRow("Monto2")
                                Dim saldoActual = MaeRow("SaldoActual") - NoveRow("Monto") - NoveRow("Monto1") - NoveRow("Monto2")
                                Dim sqlActualizarSaldo = "UPDATE MaeCtaCte SET SaldoActual = @SaldoActual, SaldoDto = @SaldoDto WHERE NroCuenta = @NroCuenta"
                                Dim parsActualizarSaldo = CmdParams("@SaldoActual", saldoActual, "@SaldoDto", saldoDto, "@NroCuenta", NoveRow("NroCuenta"))
                                DSM.Execute(DSM.Proveedores, sqlActualizarSaldo, parsActualizarSaldo)
                            Case 54, 55, 58, 59
                                Dim saldoActual = MaeRow("SaldoActual") - NoveRow("Monto") - NoveRow("Monto1") - NoveRow("Monto2")
                                Dim sqlActualizarSaldo = "UPDATE MaeCtaCte SET SaldoActual = @SaldoActual WHERE NroCuenta = @NroCuenta"
                                Dim parsActualizarSaldo = CmdParams("@SaldoActual", saldoActual, "@NroCuenta", NoveRow("NroCuenta"))
                                DSM.Execute(DSM.Proveedores, sqlActualizarSaldo, parsActualizarSaldo)
                        End Select
                    End If
                End If
            End If
        Next

        'Vuelco de los registros al DetaCtaCte (VB.NET)
        Dim sqlVuelcoRegDeta = "
            SELECT 
                NoveCtaCte.IdDetaCtaCte, NoveCtaCte.NroCuenta, NoveCtaCte.NroFactura, NoveCtaCte.NroComprobante,
                NoveCtaCte.NombreComprobante, NoveCtaCte.Condicion, NoveCtaCte.Fecha, NoveCtaCte.IdImputacion,
                ([Monto] + [Monto1] + [Monto2]) AS Expr1, NoveCtaCte.CtaMonto, NoveCtaCte.ComprasRNI,
                NoveCtaCte.CtaRNI, NoveCtaCte.Neto105, NoveCtaCte.CtaNeto105, NoveCtaCte.Neto21,
                NoveCtaCte.Cta21, NoveCtaCte.Neto27, NoveCtaCte.Cta27,
                NoveCtaCte.Exento, NoveCtaCte.CtaExento, NoveCtaCte.IVA, NoveCtaCte.CtaIva,
                NoveCtaCte.Ganancias, NoveCtaCte.CtaGanancia, NoveCtaCte.Retenciva, NoveCtaCte.CtaRetencion,
                NoveCtaCte.IngresosB,  NoveCtaCte.CtaIB, NoveCtaCte.IngresosB2, NoveCtaCte.CtaIB2,
                NoveCtaCte.IngresosB3, NoveCtaCte.CtaIB3, NoveCtaCte.IngresosB4, NoveCtaCte.CtaIB4,
                NoveCtaCte.IngresosB5, NoveCtaCte.CtaIB5, NoveCtaCte.IngresosB6, NoveCtaCte.CtaIB6,
                NoveCtaCte.ACuenta, NoveCtaCte.FechaVto, NoveCtaCte.TipoValor, NoveCtaCte.NroCheque,
                NoveCtaCte.RegInterno, NoveCtaCte.Sucursal, NoveCtaCte.Cobrado, NoveCtaCte.FondoFijo,
                NoveCtaCte.Comentario, NoveCtaCte.Rubro, NoveCtaCte.Cai, NoveCtaCte.Dolar,
                NoveCtaCte.NroDespacho, NoveCtaCte.PuntoDeVenta
            FROM NoveCtaCte
            WHERE NoveCtaCte.ActDeta = 0 AND NoveCtaCte.Fecha <= @hasta"
        Dim parsVuelcoRegDeta = CmdParams("@hasta", dtpFechaHasta.Value)
        Dim dtVuelcoDeta As DataTable = DSM.ExecuteQuery(DSM.Proveedores, sqlVuelcoRegDeta, parsVuelcoRegDeta)

        For Each row As DataRow In dtVuelcoDeta.Rows
            Dim idDetaCtaCte As Object = row("IdDetaCtaCte")

            'normalización de campos "cuenta" (en VB6 comparabas con "")
            Dim ctaMonto = If(IsDBNull(row("CtaMonto")) OrElse row("CtaMonto").ToString().Trim() = "", DBNull.Value, row("CtaMonto"))
            Dim ctaRni = If(IsDBNull(row("CtaRNI")) OrElse row("CtaRNI").ToString().Trim() = "", DBNull.Value, row("CtaRNI"))
            Dim ctaNeto105 = If(IsDBNull(row("CtaNeto105")) OrElse row("CtaNeto105").ToString().Trim() = "", DBNull.Value, row("CtaNeto105"))
            Dim cta21 = If(IsDBNull(row("Cta21")) OrElse row("Cta21").ToString().Trim() = "", DBNull.Value, row("Cta21"))
            Dim cta27 = If(IsDBNull(row("Cta27")) OrElse row("Cta27").ToString().Trim() = "", DBNull.Value, row("Cta27"))
            Dim ctaExento = If(IsDBNull(row("CtaExento")) OrElse row("CtaExento").ToString().Trim() = "", DBNull.Value, row("CtaExento"))
            Dim ctaIva = If(IsDBNull(row("CtaIva")) OrElse row("CtaIva").ToString().Trim() = "", DBNull.Value, row("CtaIva"))
            Dim ctaGanancia = If(IsDBNull(row("CtaGanancia")) OrElse row("CtaGanancia").ToString().Trim() = "", DBNull.Value, row("CtaGanancia"))
            Dim ctaRetencion = If(IsDBNull(row("CtaRetencion")) OrElse row("CtaRetencion").ToString().Trim() = "", DBNull.Value, row("CtaRetencion"))
            Dim ctaIb = If(IsDBNull(row("CtaIB")) OrElse row("CtaIB").ToString().Trim() = "", DBNull.Value, row("CtaIB"))
            Dim ctaIb2 = If(IsDBNull(row("CtaIB2")) OrElse row("CtaIB2").ToString().Trim() = "", DBNull.Value, row("CtaIB2"))
            Dim ctaIb3 = If(IsDBNull(row("CtaIB3")) OrElse row("CtaIB3").ToString().Trim() = "", DBNull.Value, row("CtaIB3"))
            Dim ctaIb4 = If(IsDBNull(row("CtaIB4")) OrElse row("CtaIB4").ToString().Trim() = "", DBNull.Value, row("CtaIB4"))
            Dim ctaIb5 = If(IsDBNull(row("CtaIB5")) OrElse row("CtaIB5").ToString().Trim() = "", DBNull.Value, row("CtaIB5"))
            Dim ctaIb6 = If(IsDBNull(row("CtaIB6")) OrElse row("CtaIB6").ToString().Trim() = "", DBNull.Value, row("CtaIB6"))

            Dim sqlInsertDeta = "
                INSERT INTO DetaCtaCte (
                  IdDetaCtaCte, NroCuenta, NroFactura, NroComprobante, NombreComprobante, Condicion, Fecha, IdImputacion,
                  Monto, CtaMonto, ComprasRNI, CtaRNI, Neto105, CtaNeto105, Neto21, Cta21,
                  Neto27, Cta27, Exento, CtaExento, IVA, CtaIva, Ganancias, CtaGanancia, Retenciva, CtaRetencion,
                  IngresosB, CtaIB, IngresosB2, CtaIB2, IngresosB3, CtaIB3, IngresosB4, CtaIB4,
                  IngresosB5, CtaIB5, IngresosB6, CtaIB6, ACuenta, FechaVto, TipoValor, NroCheque,
                  RegInterno, Sucursal, Cobrado, FondoFijo, Comentario, Rubro, Cai, Dolar, NroDespacho, PuntoDeVenta
                ) VALUES (
                  @IdDetaCtaCte, @NroCuenta, @NroFactura, @NroComprobante, @NombreComprobante, @Condicion, @Fecha, @IdImputacion,
                  @Monto, @CtaMonto, @ComprasRNI, @CtaRNI, @Neto105, @CtaNeto105, @Neto21, @Cta21,
                  @Neto27, @Cta27, @Exento, @CtaExento, @IVA, @CtaIva, @Ganancias, @CtaGanancia, @Retenciva, @CtaRetencion,
                  @IngresosB, @CtaIB, @IngresosB2, @CtaIB2, @IngresosB3, @CtaIB3, @IngresosB4, @CtaIB4,
                  @IngresosB5, @CtaIB5, @IngresosB6, @CtaIB6, @ACuenta, @FechaVto, @TipoValor, @NroCheque,
                  @RegInterno, @Sucursal, @Cobrado, @FondoFijo, @Comentario, @Rubro, @Cai, @Dolar, @NroDespacho, @PuntoDeVenta
                )"

            Dim parsInsert = CmdParams(
                "@IdDetaCtaCte", idDetaCtaCte, "@NroCuenta", row("NroCuenta"), "@NroFactura", row("NroFactura"),
                "@NroComprobante", row("NroComprobante"), "@NombreComprobante", row("NombreComprobante"), "@Condicion", row("Condicion"),
                "@Fecha", row("Fecha"), "@IdImputacion", row("IdImputacion"), "@Monto", row("Expr1"),
                "@CtaMonto", ctaMonto, "@ComprasRNI", row("ComprasRNI"), "@CtaRNI", ctaRni,
                "@Neto105", row("Neto105"), "@CtaNeto105", ctaNeto105, "@Neto21", row("Neto21"),
                "@Cta21", cta21, "@Neto27", row("Neto27"), "@Cta27", cta27,
                "@Exento", row("Exento"), "@CtaExento", ctaExento, "@IVA", row("IVA"),
                "@CtaIva", ctaIva, "@Ganancias", row("Ganancias"), "@CtaGanancia", ctaGanancia,
                "@Retenciva", row("Retenciva"), "@CtaRetencion", ctaRetencion, "@IngresosB", row("IngresosB"),
                "@CtaIB", ctaIb, "@IngresosB2", row("IngresosB2"), "@CtaIB2", ctaIb2,
                "@IngresosB3", row("IngresosB3"), "@CtaIB3", ctaIb3, "@IngresosB4", row("IngresosB4"),
                "@CtaIB4", ctaIb4, "@IngresosB5", row("IngresosB5"), "@CtaIB5", ctaIb5,
                "@IngresosB6", row("IngresosB6"), "@CtaIB6", ctaIb6, "@ACuenta", row("ACuenta"),
                "@FechaVto", row("FechaVto"), "@TipoValor", row("TipoValor"), "@NroCheque", row("NroCheque"),
                "@RegInterno", row("RegInterno"), "@Sucursal", row("Sucursal"), "@Cobrado", row("Cobrado"),
                "@FondoFijo", row("FondoFijo"), "@Comentario", row("Comentario"), "@Rubro", row("Rubro"),
                "@Cai", If(IsDBNull(row("Cai")), DBNull.Value, row("Cai")), "@Dolar", row("Dolar"), "@NroDespacho", row("NroDespacho"),
                "@PuntoDeVenta", row("PuntoDeVenta")
            )
            DSM.Execute(DSM.Proveedores, sqlInsertDeta, parsInsert)

            'Update por IdDetaCtaCte (clave que comparten ambas tablas)
            Dim sqlUpdateNove = "UPDATE NoveCtaCte SET ActDeta = 1 WHERE IdDetaCtaCte = @IdDetaCtaCte;"
            Dim parsUpdateNove = CmdParams("@IdDetaCtaCte", idDetaCtaCte)
            DSM.Execute(DSM.Proveedores, sqlUpdateNove, parsUpdateNove)
        Next

        lblActualizando.ForeColor = SystemColors.ControlText
        lblActualizando.Refresh()
        lblGenerando.ForeColor = Color.LightSeaGreen
        lblGenerando.Refresh()

        'Armado de Asiento para contabilidad
        Dim propio As Integer = 0

        'dtNove: DataTable con registros de NoveCtaCte a procesar (de donde venías iterando)
        'Si tu variable se llama distinto, cambiala.
        For Each row In dtNove.Rows

            'Si ya tiene asiento, lo salteamos (equivalente al GoTo Salto)
            If Not IsDBNull(row("Asiento")) AndAlso CInt(row("Asiento")) = 1 Then
                Continue For
            End If

            'Tomar el próximo NroAsiento (sin transacción)
            Dim sqlGetAsiento = "SELECT NroAsiento FROM NroAsiento;"
            Dim dtAsiento As DataTable = DSM.ExecuteQuery(DSM.Contabilidad, sqlGetAsiento)

            If dtAsiento.Rows.Count > 0 AndAlso Not IsDBNull(dtAsiento.Rows(0)("NroAsiento")) Then
                propio = CInt(dtAsiento.Rows(0)("NroAsiento")) + 1
                Dim sqlUpdAsiento = "UPDATE NroAsiento SET NroAsiento = @nro;"
                Dim parsUpdAsiento = CmdParams("@nro", propio)
                DSM.Execute(DSM.Contabilidad, sqlUpdAsiento, parsUpdAsiento)
            Else
                propio = 1
                Dim sqlInitAsiento = "UPDATE NroAsiento SET NroAsiento = @nro;"
                Dim parsInitAsiento = CmdParams("@nro", propio)
                DSM.Execute(DSM.Contabilidad, sqlInitAsiento, parsInitAsiento)
            End If

            Dim idImputacion As Integer = 0
            If Not IsDBNull(row("IdImputacion")) Then idImputacion = CInt(row("IdImputacion"))

            Dim fecha As Date = Date.Now
            If Not IsDBNull(row("Fecha")) Then fecha = CDate(row("Fecha"))

            Dim nroComprobante As String = ""
            If Not IsDBNull(row("NroComprobante")) Then nroComprobante = CStr(row("NroComprobante"))

            Dim ctamonto As String = ""
            If Not IsDBNull(row("CtaMonto")) Then ctamonto = CStr(row("CtaMonto"))

            Dim ctaMonto1 As String = ""
            If dtNove.Columns.Contains("CtaMonto1") AndAlso Not IsDBNull(row("CtaMonto1")) Then ctaMonto1 = CStr(row("CtaMonto1"))

            Dim ctaMonto2 As String = ""
            If dtNove.Columns.Contains("CtaMonto2") AndAlso Not IsDBNull(row("CtaMonto2")) Then ctaMonto2 = CStr(row("CtaMonto2"))

            Dim comprasRni As Decimal = 0
            If Not IsDBNull(row("ComprasRNI")) Then comprasRni = CDec(row("ComprasRNI"))

            Dim neto21 As Decimal = 0
            If Not IsDBNull(row("Neto21")) Then neto21 = CDec(row("Neto21"))

            Dim neto105 As Decimal = 0
            If Not IsDBNull(row("Neto105")) Then neto105 = CDec(row("Neto105"))

            Dim neto27 As Decimal = 0
            If Not IsDBNull(row("Neto27")) Then neto27 = CDec(row("Neto27"))

            Dim exento As Decimal = 0
            If Not IsDBNull(row("Exento")) Then exento = CDec(row("Exento"))

            Dim iva As Decimal = 0
            If Not IsDBNull(row("IVA")) Then iva = CDec(row("IVA"))

            Dim ganancias As Decimal = 0
            If Not IsDBNull(row("Ganancias")) Then ganancias = CDec(row("Ganancias"))

            Dim retencIva As Decimal = 0
            If Not IsDBNull(row("Retenciva")) Then retencIva = CDec(row("Retenciva"))

            Dim ingresosB As Decimal = 0
            If Not IsDBNull(row("IngresosB")) Then ingresosB = CDec(row("IngresosB"))

            Dim ingresosB2 As Decimal = 0
            If dtNove.Columns.Contains("IngresosB2") AndAlso Not IsDBNull(row("IngresosB2")) Then ingresosB2 = CDec(row("IngresosB2"))

            Dim ingresosB3 As Decimal = 0
            If dtNove.Columns.Contains("IngresosB3") AndAlso Not IsDBNull(row("IngresosB3")) Then ingresosB3 = CDec(row("IngresosB3"))

            Dim ingresosB4 As Decimal = 0
            If dtNove.Columns.Contains("IngresosB4") AndAlso Not IsDBNull(row("IngresosB4")) Then ingresosB4 = CDec(row("IngresosB4"))

            Dim monto As Decimal = 0
            If Not IsDBNull(row("Monto")) Then monto = CDec(row("Monto"))

            Dim monto1 As Decimal = 0
            If dtNove.Columns.Contains("Monto1") AndAlso Not IsDBNull(row("Monto1")) Then monto1 = CDec(row("Monto1"))

            Dim monto2 As Decimal = 0
            If dtNove.Columns.Contains("Monto2") AndAlso Not IsDBNull(row("Monto2")) Then monto2 = CDec(row("Monto2"))

            Dim ctaRni As String = ""
            If Not IsDBNull(row("CtaRNI")) Then ctaRni = CStr(row("CtaRNI"))

            Dim cta21 As String = ""
            If Not IsDBNull(row("Cta21")) Then cta21 = CStr(row("Cta21"))

            Dim ctaNeto105 As String = ""
            If Not IsDBNull(row("CtaNeto105")) Then ctaNeto105 = CStr(row("CtaNeto105"))

            Dim cta27 As String = ""
            If Not IsDBNull(row("Cta27")) Then cta27 = CStr(row("Cta27"))

            Dim ctaExento As String = ""
            If Not IsDBNull(row("CtaExento")) Then ctaExento = CStr(row("CtaExento"))

            Dim ctaIva As String = ""
            If Not IsDBNull(row("CtaIva")) Then ctaIva = CStr(row("CtaIva"))

            Dim ctaGanancia As String = ""
            If Not IsDBNull(row("CtaGanancia")) Then ctaGanancia = CStr(row("CtaGanancia"))

            Dim ctaRetencion As String = ""
            If Not IsDBNull(row("CtaRetencion")) Then ctaRetencion = CStr(row("CtaRetencion"))

            Dim ctaIB As String = ""
            If Not IsDBNull(row("CtaIB")) Then ctaIB = CStr(row("CtaIB"))

            Dim ctaIB2 As String = ""
            If dtNove.Columns.Contains("CtaIB2") AndAlso Not IsDBNull(row("CtaIB2")) Then ctaIB2 = CStr(row("CtaIB2"))

            Dim ctaIB3 As String = ""
            If dtNove.Columns.Contains("CtaIB3") AndAlso Not IsDBNull(row("CtaIB3")) Then ctaIB3 = CStr(row("CtaIB3"))

            Dim ctaIB4 As String = ""
            If dtNove.Columns.Contains("CtaIB4") AndAlso Not IsDBNull(row("CtaIB4")) Then ctaIB4 = CStr(row("CtaIB4"))

            If idImputacion <> 59 Then

                'Cuentas al Haber (VB6: "H")
                If ctaRni <> "" AndAlso comprasRni > 0 Then RegistraCuenta(56, ctaRni, "H", comprasRni, propio, fecha)
                If cta21 <> "" AndAlso neto21 > 0 Then RegistraCuenta(56, cta21, "H", neto21, propio, fecha)
                If ctaNeto105 <> "" AndAlso neto105 > 0 Then RegistraCuenta(56, ctaNeto105, "H", neto105, propio, fecha)
                If cta27 <> "" AndAlso neto27 > 0 Then RegistraCuenta(56, cta27, "H", neto27, propio, fecha)
                If ctaExento <> "" AndAlso exento > 0 Then RegistraCuenta(56, ctaExento, "H", exento, propio, fecha)
                If ctaIva <> "" AndAlso iva > 0 Then RegistraCuenta(56, ctaIva, "H", iva, propio, fecha)
                If ctaGanancia <> "" AndAlso ganancias > 0 Then RegistraCuenta(56, ctaGanancia, "H", ganancias, propio, fecha)
                If ctaRetencion <> "" AndAlso retencIva > 0 Then RegistraCuenta(56, ctaRetencion, "H", retencIva, propio, fecha)
                If ctaIB <> "" AndAlso ingresosB > 0 Then RegistraCuenta(56, ctaIB, "H", ingresosB, propio, fecha)
                If ctaIB2 <> "" AndAlso ingresosB2 > 0 Then RegistraCuenta(56, ctaIB2, "H", ingresosB2, propio, fecha)
                If ctaIB3 <> "" AndAlso ingresosB3 > 0 Then RegistraCuenta(56, ctaIB3, "H", ingresosB3, propio, fecha)
                If ctaIB4 <> "" AndAlso ingresosB4 > 0 Then RegistraCuenta(56, ctaIB4, "H", ingresosB4, propio, fecha)

                'Armado del Debe (VB6: "D") con texto fijo
                Dim leyenda = "NC Proveedores  " & nroComprobante
                If ctamonto <> "" AndAlso monto > 0 Then RegistraCuenta(56, ctamonto, "D", monto, propio, fecha, leyenda)
                If ctaMonto1 <> "" AndAlso monto1 > 0 Then RegistraCuenta(56, ctaMonto1, "D", monto1, propio, fecha, leyenda)
                If ctaMonto2 <> "" AndAlso monto2 > 0 Then RegistraCuenta(56, ctaMonto2, "D", monto2, propio, fecha, leyenda)

            Else

                'Cuentas al Debe (VB6: "D")
                If ctaRni <> "" AndAlso comprasRni > 0 Then RegistraCuenta(56, ctaRni, "D", comprasRni, propio, fecha)
                If cta21 <> "" AndAlso neto21 > 0 Then RegistraCuenta(56, cta21, "D", neto21, propio, fecha)
                If ctaNeto105 <> "" AndAlso neto105 > 0 Then RegistraCuenta(56, ctaNeto105, "D", neto105, propio, fecha)
                If cta27 <> "" AndAlso neto27 > 0 Then RegistraCuenta(56, cta27, "D", neto27, propio, fecha)
                If ctaExento <> "" AndAlso exento > 0 Then RegistraCuenta(56, ctaExento, "D", exento, propio, fecha)
                If ctaIva <> "" AndAlso iva > 0 Then RegistraCuenta(56, ctaIva, "D", iva, propio, fecha)
                If ctaGanancia <> "" AndAlso ganancias > 0 Then RegistraCuenta(56, ctaGanancia, "D", ganancias, propio, fecha)
                If ctaRetencion <> "" AndAlso retencIva > 0 Then RegistraCuenta(56, ctaRetencion, "D", retencIva, propio, fecha)
                If ctaIB <> "" AndAlso ingresosB > 0 Then RegistraCuenta(56, ctaIB, "D", ingresosB, propio, fecha)
                If ctaIB2 <> "" AndAlso ingresosB2 > 0 Then RegistraCuenta(56, ctaIB2, "D", ingresosB2, propio, fecha)
                If ctaIB3 <> "" AndAlso ingresosB3 > 0 Then RegistraCuenta(56, ctaIB3, "D", ingresosB3, propio, fecha)
                If ctaIB4 <> "" AndAlso ingresosB4 > 0 Then RegistraCuenta(56, ctaIB4, "D", ingresosB4, propio, fecha)

                'Cartel (leyenda) para el Haber
                Dim cartel As String = ""
                If idImputacion = 1 OrElse idImputacion = 2 OrElse idImputacion = 10 Then
                    cartel = "Fac. Proveedores " & nroComprobante
                End If
                If idImputacion = 6 Then
                    cartel = "Certificado Retenciòn " & nroComprobante
                End If
                If idImputacion = 11 Then
                    If dtNove.Columns.Contains("Comentario") AndAlso Not IsDBNull(row("Comentario")) Then
                        cartel = CStr(row("Comentario"))
                    End If
                End If
                If idImputacion = 2 Then
                    cartel = "N.D Proveedores " & nroComprobante
                End If
                If idImputacion = 56 OrElse idImputacion = 54 Then
                    cartel = "O.P Proveedores " & nroComprobante
                End If
                If cartel = " " Then cartel = ".."

                'Haber (VB6: "H")
                If ctamonto <> "" AndAlso monto > 0 Then RegistraCuenta(56, ctamonto, "H", monto, propio, fecha, cartel)
                If ctaMonto1 <> "" AndAlso monto1 > 0 Then RegistraCuenta(56, ctaMonto1, "H", monto1, propio, fecha, cartel)
                If ctaMonto2 <> "" AndAlso monto2 > 0 Then RegistraCuenta(56, ctaMonto2, "H", monto2, propio, fecha, cartel)

            End If

            'Marcar asiento en NoveCtaCte usando el ID REAL (IdDetaCtaCte)
            Dim sqlMarcaAsiento = "UPDATE NoveCtaCte SET Asiento = 1 WHERE IdDetaCtaCte = @id;"
            Dim parsMarcaAsiento = CmdParams("@id", row("IdDetaCtaCte"))
            DSM.Execute(DSM.Proveedores, sqlMarcaAsiento, parsMarcaAsiento)

        Next


        Dim sqlMae = "SELECT * FROM MaeCtaCte WHERE SaldoActual > 0;"
        Dim dtMae As DataTable = DSM.ExecuteQuery(DSM.Proveedores, sqlMae)

        If dtMae.Rows.Count > 0 Then
            For Each maeRow In dtMae.Rows
                Dim nroCuenta As Long = CLng(maeRow("NroCuenta"))
                Dim saldoActual As Double = CDbl(maeRow("SaldoActual"))
                Dim saldo As Double = 0

                Dim sqlDeta = "
                  SELECT *
                  FROM DetaCtaCte
                  WHERE Cobrado = 0
                    AND IdImputacion IN (1,2,10)
                    AND NroCuenta = @NroCuenta
                  ORDER BY Fecha DESC"
                Dim parsDeta = CmdParams("@NroCuenta", nroCuenta)
                Dim dtDeta As DataTable = DSM.ExecuteQuery(DSM.Proveedores, sqlDeta, parsDeta)

                For Each detaRow In dtDeta.Rows
                    saldo = saldo + CDbl(detaRow("Monto"))

                    Dim cobrado As Integer
                    If (saldo - 10) > saldoActual Then
                        cobrado = 1
                    Else
                        cobrado = 0
                    End If

                    Dim sqlUpdateCob = "UPDATE DetaCtaCte SET Cobrado = @Cobrado WHERE IdDetaCtaCte = @IdDetaCtaCte;"
                    Dim parsUpdateCob = CmdParams("@Cobrado", cobrado, "@IdDetaCtaCte", detaRow("IdDetaCtaCte"))
                    DSM.Execute(DSM.Proveedores, sqlUpdateCob, parsUpdateCob)
                Next
            Next
        End If

        'Despachos de importación cancelados automaticamente
        Dim sqlCobImp = "UPDATE DetaCtaCte SET Cobrado = 1 WHERE IdImputacion IN (6,11);"
        DSM.Execute(DSM.Proveedores, sqlCobImp)

        'Los > 50 a negativo
        Dim sqlNeg = "
              UPDATE DetaCtaCte
              SET
                Monto      = [Monto]      * -1,
                ComprasRNI = [ComprasRNI] * -1,
                Neto105    = [Neto105]    * -1,
                Neto21     = [Neto21]     * -1,
                Neto27     = [Neto27]     * -1,
                Exento     = [Exento]     * -1,
                IVA        = [IVA]        * -1,
                Ganancias  = [Ganancias]  * -1,
                Retenciva  = [Retenciva]  * -1,
                IngresosB  = [IngresosB]  * -1
              WHERE IdImputacion > 50
                AND Monto > 0
                AND ComprasRNI > 0
                AND Neto105 > 0
                AND Neto21 > 0
                AND Neto27 > 0
                AND Exento > 0
                AND IVA > 0
                AND Ganancias > 0
            "
        DSM.Execute(DSM.Proveedores, sqlNeg)

        'Borrado de los registros del NOVECTACTE
        Dim sqlDeleteNove = "DELETE FROM NoveCtaCte WHERE Fecha <= @hasta;"
        Dim parsDeleteNove = CmdParams("@hasta", dtpFechaHasta.Value)
        DSM.Execute(DSM.Proveedores, sqlDeleteNove, parsDeleteNove)

        MessageBox.Show("Se ha realizado la tarea de Actualización en forma Satisfactoria.")
    End Sub

    Private Sub RegistraCuenta(cabAsiento As Long, codContable As String, imputa As String,
                           monto As Double, ppio As Double, fecha As Date, Optional obs As String = "")

        Return

        '  '--------------------------------------------------------
        '  ' 1) Traigo la cuenta del plan (antes: PCrs)
        '  '--------------------------------------------------------
        '  Dim sqlPlan = "
        '    SELECT TOP (1) *
        '    FROM PlanCuentas
        '    WHERE CodContable = @CodContable
        '      AND IdSucursal = @IdSucursal;"

        '  Dim parsPlan = CmdParams(
        '    "@CodContable", codContable,
        '    "@IdSucursal", Sucursal
        '  )

        '  Dim dtPlan As DataTable = DSM.ExecuteQuery(DSM.Contabilidad, sqlPlan, parsPlan)

        '  If dtPlan.Rows.Count = 0 Then
        '    MessageBox.Show("Cuenta inexiste asiento incorrecto....controle")
        '    Exit Sub
        '  End If

        '  Dim planRow = dtPlan.Rows(0)

        '  '--------------------------------------------------------
        '  ' 2) Inserto movimiento (antes: abrir Movimientos + AddNew)
        '  '--------------------------------------------------------
        '  Dim debe As Double = 0
        '  Dim haber As Double = 0

        '  Select Case (imputa Or "").Trim().ToUpper()
        '    Case "D"
        '      debe = monto
        '    Case "H"
        '      haber = monto
        '  End Select

        '  ' En VB6 usabas: Date & "-" & Time (string). Acá lo dejo igual.
        '  Dim procesadoStr As String = Date.Now.ToString("dd/MM/yyyy-HH:mm:ss")

        '  Dim sqlInsertMovi = "
        '    INSERT INTO Movimientos (
        '      IdSucursal,
        '      FechaHora,
        '      IDCabAsiento,

        '      Grupo,
        '      Familia,
        '      Individuo,
        '      Cuenta,
        '      CodContable,
        '      Descripcion,

        '      Procesado,
        '      Imputa,
        '      Debe,
        '      Haber,
        '      Usuario,
        '      Observaciones,
        '      IDAsiento
        '    )
        '    VALUES (
        '      @IdSucursal,
        '      @FechaHora,
        '      @IDCabAsiento,

        '      @Grupo,
        '      @Familia,
        '      @Individuo,
        '      @Cuenta,
        '      @CodContable,
        '      @Descripcion,

        '      @Procesado,
        '      @Imputa,
        '      @Debe,
        '      @Haber,
        '      @Usuario,
        '      @Observaciones,
        '      @IDAsiento
        '    );"

        '  Dim parsInsert = CmdParams(
        '    "@IdSucursal", Sucursal,
        '    "@FechaHora", fecha,
        '    "@IDCabAsiento", cabAsiento,

        '    "@Grupo", planRow("Grupo"),
        '    "@Familia", planRow("Familia"),
        '    "@Individuo", planRow("Individuo"),
        '    "@Cuenta", planRow("Cuenta"),
        '    "@CodContable", planRow("CodContable"),
        '    "@Descripcion", planRow("Descripcion"),

        '    "@Procesado", procesadoStr,
        '    "@Imputa", imputa,
        '    "@Debe", debe,
        '    "@Haber", haber,
        '    "@Usuario", UserStr,
        '    "@Observaciones", If(String.IsNullOrWhiteSpace(obs), DBNull.Value, obs),
        '    "@IDAsiento", Propio
        '  )

        '  DSM.Execute(DSM.Contabilidad, sqlInsertMovi, parsInsert)

    End Sub
End Class