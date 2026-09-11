Imports System.Data.SqlClient
Imports System.Linq
Imports System.Collections.Generic
Imports DSM = DataSourceManager.Lib.DataSourceManager


Public Class frmServicios
    Inherits Form

    Private Shared instancia As frmServicios
    Private idServicioActual As Integer = 0
    Private modoActual As FormMode = FormMode.CONSULTA
    Private ReadOnly listaTipos As New List(Of Object)
    Private ReadOnly listaRepuestos As New List(Of Object)
    Private _sucursalesTbl As DataTable

    Private Enum FormMode
        CONSULTA
        ALTA
        MODIFICACION
    End Enum

    Private Sub CambiarModo(nuevoModo As FormMode)
        modoActual = nuevoModo
        SetControlesEnabled(nuevoModo <> FormMode.CONSULTA, gbCabecera, gbItems)
        gbBusqueda.Enabled = (nuevoModo = FormMode.CONSULTA)
        DgvListado.Enabled = (nuevoModo = FormMode.CONSULTA)
        CmdAgregar.Enabled = (nuevoModo = FormMode.CONSULTA)
        btnModificar.Enabled = (nuevoModo = FormMode.CONSULTA AndAlso idServicioActual > 0)
        CmdBorrar.Enabled = (nuevoModo = FormMode.CONSULTA AndAlso idServicioActual > 0)
        cmdAceptar.Enabled = (nuevoModo <> FormMode.CONSULTA)
        CmdCancelar.Enabled = (nuevoModo <> FormMode.CONSULTA)
        btnBuscar.Enabled = (nuevoModo = FormMode.CONSULTA)
        CmdSalir.Enabled = True

        txtIdServicio.Enabled = False
        txtNombreProv.Enabled = False
        txtSubTotalItems.Enabled = False
        txtTotalServicio.Enabled = False
    End Sub

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmServicios()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmServicios_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        instancia = Nothing
    End Sub

    Private Sub frmServicios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ConfigurarGrillas()
        CargarCombos()
        CargarSucursales()
        dtpBusqDesde.Value = Date.Today.AddMonths(-3)
        dtpBusqHasta.Value = Date.Today
        dtpFechaServicio.Value = Date.Today
        dtpFechaFactura.Value = Date.Today
        BuscarServicios()
        CambiarModo(FormMode.CONSULTA)
    End Sub
    Public Sub ConfigurarGrillaServicios()
        'CONFIGURACION DE GRILLA DE LISTADO

        If DgvListado IsNot Nothing AndAlso DgvListado.Columns.Count > 0 Then
            For Each col As DataGridViewColumn In DgvListado.Columns
                col.Visible = False
            Next

            If DgvListado.Columns.Contains("IdServicio") Then
                DgvListado.Columns("IdServicio").Visible = False
                DgvListado.Columns("IdServicio").HeaderText = "Id Servicio"
                DgvListado.Columns("IdServicio").Width = 100
            End If
            If DgvListado.Columns.Contains("FechaServicio") Then
                DgvListado.Columns("FechaServicio").Visible = True
                DgvListado.Columns("FechaServicio").HeaderText = "Fecha Servicio"
                DgvListado.Columns("FechaServicio").Width = 70
            End If
            If DgvListado.Columns.Contains("Patente") Then
                DgvListado.Columns("Patente").Visible = True
                DgvListado.Columns("Patente").HeaderText = "Patente"
                DgvListado.Columns("Patente").Width = 40
            End If
            If DgvListado.Columns.Contains("NroCuentaProv") Then
                DgvListado.Columns("NroCuentaProv").Visible = True
                DgvListado.Columns("NroCuentaProv").HeaderText = "NroCuenta"
                DgvListado.Columns("NroCuentaProv").Width = 70
                DgvListado.Columns("NroCuentaProv").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
            End If
            If DgvListado.Columns.Contains("ProveedorNombre") Then
                DgvListado.Columns("ProveedorNombre").Visible = True
                DgvListado.Columns("ProveedorNombre").HeaderText = "Proveedor"
                DgvListado.Columns("ProveedorNombre").Width = 550
            End If
            If DgvListado.Columns.Contains("PuntoDeVenta") Then
                DgvListado.Columns("PuntoDeVenta").Visible = True
                DgvListado.Columns("PuntoDeVenta").HeaderText = "PtoVta"
                DgvListado.Columns("PuntoDeVenta").Width = 40
                DgvListado.Columns("PuntoDeVenta").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End If
            If DgvListado.Columns.Contains("NroFactura") Then
                DgvListado.Columns("NroFactura").Visible = True
                DgvListado.Columns("NroFactura").HeaderText = "Nro. Factura"
                DgvListado.Columns("NroFactura").Width = 100
                DgvListado.Columns("NroFactura").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End If
            If DgvListado.Columns.Contains("FechaFactura") Then
                DgvListado.Columns("FechaFactura").Visible = True
                DgvListado.Columns("FechaFactura").HeaderText = "Fec. Factura"
                DgvListado.Columns("FechaFactura").Width = 70
            End If
            If DgvListado.Columns.Contains("TotalServicio") Then
                DgvListado.Columns("TotalServicio").Visible = True
                DgvListado.Columns("TotalServicio").HeaderText = "Total Servicio"
                DgvListado.Columns("TotalServicio").Width = 100
                DgvListado.Columns("TotalServicio").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
                DgvListado.Columns("TotalServicio").DefaultCellStyle.Format = "C2"
            End If
            If DgvListado.Columns.Contains("Observaciones") Then
                DgvListado.Columns("Observaciones").Visible = True
                DgvListado.Columns("Observaciones").HeaderText = "Observaciones"
                DgvListado.Columns("Observaciones").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End If
        End If
    End Sub
    Private Sub ConfigurarGrillas()
        ConfigurarEstiloGrid(DgvListado)
        ConfigurarEstiloGrid(DgvItems)
        DgvListado.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DgvListado.MultiSelect = True
        DgvItems.SelectionMode = DataGridViewSelectionMode.CellSelect


        'CONFIGURACIÓN DE GRILLA DE ITEMS
        Dim itemsSource As New DataTable()
        itemsSource.Columns.Add("IdItem", GetType(Integer))
        itemsSource.Columns.Add("NroRenglon", GetType(Integer))
        itemsSource.Columns.Add("TipoItem", GetType(String))
        itemsSource.Columns.Add("IdTipoServicio", GetType(Integer))
        itemsSource.Columns.Add("IdRepuesto", GetType(Integer))
        itemsSource.Columns.Add("IdComboSeleccionado", GetType(Integer))
        itemsSource.Columns.Add("DescripcionItem", GetType(String))
        itemsSource.Columns.Add("Cantidad", GetType(Decimal))
        itemsSource.Columns.Add("PrecioUnitario", GetType(Decimal))
        itemsSource.Columns.Add("SubtotalItem", GetType(Decimal))
        itemsSource.Columns.Add("GarantiaKm", GetType(Integer))
        itemsSource.Columns.Add("GarantiaMeses", GetType(Integer))
        itemsSource.Columns.Add("ModoItem", GetType(String))
        DgvItems.DataSource = itemsSource

        DgvItems.AutoGenerateColumns = False

        DgvItems.Columns.Clear()
        Dim colTipo As New DataGridViewComboBoxColumn()
        colTipo.Name = "colTipo"
        colTipo.HeaderText = "Tipo"
        colTipo.DisplayMember = "Display"
        colTipo.ValueMember = "Value"
        colTipo.DataSource = {
            New With {.Display = "Servicio", .Value = "S"},
            New With {.Display = "Repuesto", .Value = "R"}
        }.ToList()
        colTipo.DataPropertyName = "TipoItem"
        colTipo.Width = 130
        DgvItems.Columns.Add(colTipo)

        Dim colDesc As New DataGridViewComboBoxColumn()
        colDesc.Name = "colDesc"
        colDesc.HeaderText = "Descripción"
        colDesc.DisplayMember = "Display"
        colDesc.ValueMember = "Id"
        colDesc.DataPropertyName = "IdComboSeleccionado"
        colDesc.Width = 330
        DgvItems.Columns.Add(colDesc)

        Dim colCant As New DataGridViewTextBoxColumn()
        colCant.Name = "colCant"
        colCant.HeaderText = "Cantidad"
        colCant.DataPropertyName = "Cantidad"
        colCant.Width = 80
        colCant.DefaultCellStyle.Format = "N2"
        colCant.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItems.Columns.Add(colCant)

        Dim colPrecio As New DataGridViewTextBoxColumn()
        colPrecio.Name = "colPrecio"
        colPrecio.HeaderText = "Precio Unitario"
        colPrecio.DataPropertyName = "PrecioUnitario"
        colPrecio.Width = 130
        colPrecio.DefaultCellStyle.Format = "N2"
        colPrecio.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItems.Columns.Add(colPrecio)

        Dim colSubtotal As New DataGridViewTextBoxColumn()
        colSubtotal.Name = "colSubtotal"
        colSubtotal.HeaderText = "Subtotal"
        colSubtotal.DataPropertyName = "SubtotalItem"
        colSubtotal.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        colSubtotal.ReadOnly = True
        colSubtotal.DefaultCellStyle.Format = "N2"
        colSubtotal.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        DgvItems.Columns.Add(colSubtotal)

        'Dim colGKm As New DataGridViewTextBoxColumn()
        'colGKm.Name = "colGKm"
        'colGKm.HeaderText = "Gar. Km"
        'colGKm.DataPropertyName = "GarantiaKm"
        'colGKm.Width = 75
        'colGKm.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvItems.Columns.Add(colGKm)

        'Dim colGMes As New DataGridViewTextBoxColumn()
        'colGMes.Name = "colGMes"
        'colGMes.HeaderText = "Gar. Meses"
        'colGMes.DataPropertyName = "GarantiaMeses"
        'colGMes.Width = 75
        'colGMes.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        'DgvItems.Columns.Add(colGMes)

        If DgvItems.Columns.Contains("IdItem") Then DgvItems.Columns("IdItem").Visible = False
        If DgvItems.Columns.Contains("NroRenglon") Then DgvItems.Columns("NroRenglon").Visible = False
        If DgvItems.Columns.Contains("IdTipoServicio") Then DgvItems.Columns("IdTipoServicio").Visible = False
        If DgvItems.Columns.Contains("IdRepuesto") Then DgvItems.Columns("IdRepuesto").Visible = False
        If DgvItems.Columns.Contains("DescripcionItem") Then DgvItems.Columns("DescripcionItem").Visible = False
        If DgvItems.Columns.Contains("ModoItem") Then DgvItems.Columns("ModoItem").Visible = False

        ConfigurarGrillaServicios()
    End Sub

    Private Sub CargarCombos()
        cboVehiculo.Items.Clear()
        cboBusqVehiculo.Items.Clear()
        cboBusqVehiculo.Items.Add("<Todos>")
        Try
            Dim dt As DataTable = DSM.ExecuteQuery(DSM.Proveedores, "SELECT IdVehiculo, Patente, ISNULL(Marca,'') + ' - ' + ISNULL(Modelo,'') AS Info FROM Vehiculos WHERE Activo = 1 ORDER BY Patente", Nothing)
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                For Each r As DataRow In dt.Rows
                    cboVehiculo.Items.Add(New VehiculoItem() With {.IdVehiculo = CInt(r("IdVehiculo")), .Patente = r("Patente").ToString(), .Info = r("Info").ToString()})
                    cboBusqVehiculo.Items.Add(New VehiculoItem() With {.IdVehiculo = CInt(r("IdVehiculo")), .Patente = r("Patente").ToString(), .Info = r("Info").ToString()})
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudieron cargar los vehículos: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        listaTipos.Clear()
        listaRepuestos.Clear()
        Try
            Dim dtTipo As DataTable = DSM.ExecuteQuery(DSM.Proveedores, "SELECT IdTipoServicio, Descripcion, CostoRef, KmRecomendado, DiasRecomendados FROM TipoServicio WHERE Activo = 1 ORDER BY Descripcion", Nothing)
            If dtTipo IsNot Nothing Then
                For Each r As DataRow In dtTipo.Rows
                    listaTipos.Add(New With {
                        .Id = CInt(r("IdTipoServicio")),
                        .Display = r("Descripcion").ToString(),
                        .CostoRef = If(r.IsNull("CostoRef"), 0D, CDec(r("CostoRef"))),
                        .GarKm = If(r.IsNull("KmRecomendado"), 0, CInt(r("KmRecomendado"))),
                        .GarMeses = If(r.IsNull("DiasRecomendados"), 0, CInt(r("DiasRecomendado")) \ 30)
                    })
                Next
            End If
            Dim dtRep As DataTable = DSM.ExecuteQuery(DSM.Proveedores, "SELECT IdRepuesto, ISNULL(Codigo,'') + ' - ' + Descripcion AS DescripcionFull, Descripcion, CostoRef, Rubro FROM Repuestos WHERE Activo = 1 ORDER BY DescripcionFull", Nothing)
            If dtRep IsNot Nothing Then
                For Each r As DataRow In dtRep.Rows
                    listaRepuestos.Add(New With {
                        .Id = CInt(r("IdRepuesto")),
                        .Display = r("DescripcionFull").ToString(),
                        .CostoRef = If(r.IsNull("CostoRef"), 0D, CDec(r("CostoRef")))
                    })
                Next
            End If
        Catch ex As Exception
            MessageBox.Show("No se pudieron cargar tipos de servicio/repuestos: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


        cboBusqVehiculo.SelectedIndex = 0
    End Sub
    Private Sub CargarSucursales()

        Try
            _sucursalesTbl = DSM.ExecuteQuery(DSM.Stock,
                "SELECT IdSucursal, Descripcion FROM Sucursales WHERE Codigo IN(1,3,10,13,14,15,16,21,23) ORDER BY Descripcion", Nothing)
            If _sucursalesTbl Is Nothing Then _sucursalesTbl = New DataTable()
            cboSucursal.BeginUpdate()
            cboSucursal.Items.Clear()
            cboSucursal.Items.Add("<Todas>")
            cboSucursal.DisplayMember = "Descripcion"
            cboSucursal.ValueMember = "IdSucursal"
            For Each r As DataRow In _sucursalesTbl.Rows
                cboSucursal.Items.Add(New SucursalItem With {
                    .IdSucursal = Convert.ToInt32(r("IdSucursal")),
                    .Descripcion = Convert.ToString(r("Descripcion"))
                })
            Next
        Catch
        Finally
            cboSucursal.EndUpdate()
        End Try
    End Sub
    Private Class SucursalItem
        Public Property IdSucursal As Integer
        Public Property Descripcion As String
        Public Overrides Function ToString() As String
            Return Descripcion
        End Function
    End Class
    Private Class VehiculoItem
        Public Property IdVehiculo As Integer
        Public Property Patente As String
        Public Property Info As String
        Public Overrides Function ToString() As String
            Return Patente & " - " & Info
        End Function
    End Class

    Private Sub BuscarServicios()
        Dim sql As String = "SELECT s.IdServicio, s.FechaServicio, v.Patente, v.idSucursal, s.NroCuentaProv, s.ProveedorNombre, s.PuntoDeVenta, s.NroFactura,  s.TotalServicio, s.Observaciones FROM Servicios s LEFT JOIN Vehiculos v ON s.IdVehiculo = v.IdVehiculo WHERE 1=1"
        Dim pars As New Dictionary(Of String, Object)()
        If cboBusqVehiculo.SelectedItem IsNot Nothing AndAlso TypeOf cboBusqVehiculo.SelectedItem Is VehiculoItem Then
            Dim sel = DirectCast(cboBusqVehiculo.SelectedItem, VehiculoItem)
            If sel.IdVehiculo > 0 Then
                sql &= " AND s.IdVehiculo = @IdVehiculo"
                pars("@IdVehiculo") = sel.IdVehiculo
            End If
        End If
        If cboSucursal.SelectedItem IsNot Nothing AndAlso TypeOf cboSucursal.SelectedItem Is SucursalItem Then
            Dim sel = DirectCast(cboSucursal.SelectedItem, SucursalItem)
            If sel.IdSucursal > 0 Then
                sql &= " AND s.IdSucursal = @IdSucursal"
                pars("@IdSucursal") = sel.IdSucursal
            End If
        End If
        sql &= " AND s.FechaServicio BETWEEN @Desde AND @Hasta"
        pars("@Desde") = dtpBusqDesde.Value.Date
        pars("@Hasta") = dtpBusqHasta.Value.Date.AddDays(1).AddSeconds(-1)
        sql &= " ORDER BY s.FechaServicio DESC, s.IdServicio DESC"

        Dim dt As DataTable = DSM.ExecuteQuery(DSM.Proveedores, sql, pars)
        DgvListado.DataSource = dt
        If DgvListado.Columns.Contains("IdServicio") Then DgvListado.Columns("IdServicio").Visible = False
        For Each c As DataGridViewColumn In DgvListado.Columns
            If c.Visible Then c.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        Next

        If dt Is Nothing OrElse dt.Rows.Count = 0 Then
            LimpiarCampos()
            Exit Sub
        End If

        If idServicioActual > 0 Then
            For Each drv As DataGridViewRow In DgvListado.Rows
                Dim idRow = If(drv.Cells("IdServicio").Value Is Nothing OrElse drv.Cells("IdServicio").Value Is DBNull.Value, 0, CInt(drv.Cells("IdServicio").Value))
                If idRow = idServicioActual Then
                    DgvListado.CurrentCell = drv.Cells.Cast(Of DataGridViewCell).FirstOrDefault(Function(c) c.Visible)
                    Exit For
                End If
            Next
        End If

        If DgvListado.CurrentRow Is Nothing Then
            DgvListado.Rows(0).Selected = True
            DgvListado.CurrentCell = DgvListado.Rows(0).Cells.Cast(Of DataGridViewCell).FirstOrDefault(Function(c) c.Visible)
        End If
        ConfigurarGrillaServicios()
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        idServicioActual = 0
        BuscarServicios()
    End Sub

    Private Sub DgvListado_SelectionChanged(sender As Object, e As EventArgs) Handles DgvListado.SelectionChanged
        If modoActual = FormMode.CONSULTA Then CargarServicioSeleccionado()
    End Sub

    Private Sub CargarServicioSeleccionado()
        If DgvListado.CurrentRow Is Nothing OrElse DgvListado.CurrentRow.Cells("IdServicio").Value Is Nothing OrElse DgvListado.CurrentRow.Cells("IdServicio").Value Is DBNull.Value Then
            LimpiarCampos()
            Exit Sub
        End If
        Dim idSel = CInt(DgvListado.CurrentRow.Cells("IdServicio").Value)
        If idSel <= 0 Then
            LimpiarCampos()
            Exit Sub
        End If
        idServicioActual = idSel
        Dim sql As String = "SELECT * FROM Servicios WHERE IdServicio = @Id"
        Dim dtServicio = DSM.ExecuteQuery(DSM.Proveedores, sql, CmdParams("@Id", idServicioActual))
        If dtServicio Is Nothing OrElse dtServicio.Rows.Count = 0 Then
            LimpiarCampos()
            Return
        End If
        Dim row As DataRow = dtServicio.Rows(0)

        cboVehiculo.SelectedIndex = -1
        For i = 0 To cboVehiculo.Items.Count - 1
            If TypeOf cboVehiculo.Items(i) Is VehiculoItem AndAlso DirectCast(cboVehiculo.Items(i), VehiculoItem).IdVehiculo = CInt(row("IdVehiculo")) Then
                cboVehiculo.SelectedIndex = i
                Exit For
            End If
        Next

        txtNroCuentaProv.Text = If(row.IsNull("NroCuentaProv"), "", row("NroCuentaProv").ToString())
        txtNombreProv.Text = If(row.IsNull("ProveedorNombre"), "", row("ProveedorNombre").ToString())
        dtpFechaServicio.Value = CDate(row("FechaServicio"))
        txtKmServicio.Text = row("KmServicio").ToString()
        txtNroFactura.Text = If(row.IsNull("NroFactura"), "", row("NroFactura").ToString())
        txtPuntoDeVenta.Text = If(row.IsNull("PuntoDeVenta"), "", row("PuntoDeVenta").ToString())
        dtpFechaFactura.Value = If(row.IsNull("FechaFactura"), Date.Today, CDate(row("FechaFactura")))
        txtObservaciones.Text = If(row.IsNull("Observaciones"), "", row("Observaciones").ToString())
        txtOtrosGastos.Text = CDec(row("OtrosGastos")).ToString("N2")
        txtSubTotalItems.Text = CDec(row("SubTotalItems")).ToString("N2")
        txtTotalServicio.Text = CDec(row("TotalServicio")).ToString("N2")
        txtIdServicio.Text = idServicioActual.ToString()

        Dim dtItems As DataTable = DSM.ExecuteQuery(DSM.Proveedores, "SELECT * FROM ServiciosItems WHERE IdServicio = @Id ORDER BY NroRenglon", CmdParams("@Id", idServicioActual))
        Dim tblItems = TryCast(DgvItems.DataSource, DataTable)
        If tblItems Is Nothing Then Return
        tblItems.Rows.Clear()
        For Each r As DataRow In dtItems.Rows
            Dim nr = tblItems.NewRow()
            nr("IdItem") = CInt(r("IdItem"))
            nr("NroRenglon") = CInt(r("NroRenglon"))
            nr("TipoItem") = r("TipoItem").ToString()
            nr("IdTipoServicio") = If(r.IsNull("IdTipoServicio"), 0, CInt(r("IdTipoServicio")))
            nr("IdRepuesto") = If(r.IsNull("IdRepuesto"), 0, CInt(r("IdRepuesto")))
            nr("IdComboSeleccionado") = If(r("TipoItem").ToString() = "S", If(r.IsNull("IdTipoServicio"), 0, CInt(r("IdTipoServicio"))), If(r.IsNull("IdRepuesto"), 0, CInt(r("IdRepuesto"))))
            nr("DescripcionItem") = If(r.IsNull("DescripcionItem"), "", r("DescripcionItem").ToString())
            nr("Cantidad") = CDec(r("Cantidad"))
            nr("PrecioUnitario") = CDec(r("PrecioUnitario"))
            nr("SubtotalItem") = CDec(r("SubtotalItem"))
            nr("GarantiaKm") = If(r.IsNull("GarantiaKm"), 0, CInt(r("GarantiaKm")))
            nr("GarantiaMeses") = If(r.IsNull("GarantiaMeses"), 0, CInt(r("GarantiaMeses")))
            nr("ModoItem") = "SIN_CAMBIO"
            tblItems.Rows.Add(nr)
        Next
    End Sub

    Private Sub LimpiarCampos()
        idServicioActual = 0
        txtIdServicio.Text = ""
        cboVehiculo.SelectedIndex = -1
        txtNroCuentaProv.Clear()
        txtNombreProv.Clear()
        dtpFechaServicio.Value = Date.Today
        txtKmServicio.Clear()
        txtNroFactura.Clear()
        txtPuntoDeVenta.Clear()
        dtpFechaFactura.Value = Date.Today
        txtObservaciones.Clear()
        txtOtrosGastos.Text = "0,00"
        txtSubTotalItems.Text = "0,00"
        txtTotalServicio.Text = "0,00"
        Dim tbl = TryCast(DgvItems.DataSource, DataTable)
        If tbl IsNot Nothing Then tbl.Rows.Clear()
    End Sub

    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles CmdAgregar.Click
        LimpiarCampos()
        CambiarModo(FormMode.ALTA)
        cboVehiculo.Focus()
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        If idServicioActual <= 0 Then Exit Sub
        CambiarModo(FormMode.MODIFICACION)
    End Sub

    Private Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles CmdCancelar.Click
        CambiarModo(FormMode.CONSULTA)
        CargarServicioSeleccionado()
    End Sub

    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Close()
    End Sub

    Private Sub btnBuscarProv_Click(sender As Object, e As EventArgs) Handles btnBuscarProv.Click
        Try
            Using frm As New frmProveedoresSelector()
                frm.StartPosition = FormStartPosition.CenterScreen
                If frm.ShowDialog(Me) = DialogResult.OK AndAlso frm.Seleccion IsNot Nothing Then
                    Dim nroCuenta As Integer
                    If frm.Seleccion.ContainsKey("NroCuenta") AndAlso
                       Integer.TryParse(Convert.ToString(frm.Seleccion("NroCuenta")), nroCuenta) AndAlso
                       nroCuenta > 0 Then

                        txtNroCuentaProv.Text = nroCuenta.ToString()
                        txtNombreProv.Text =
                            If(frm.Seleccion.ContainsKey("Nombre"),
                               Convert.ToString(frm.Seleccion("Nombre")),
                               String.Empty)
                    End If
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al abrir selector de proveedores: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtNroCuentaProv_Leave(sender As Object, e As EventArgs) Handles txtNroCuentaProv.Leave
        If String.IsNullOrWhiteSpace(txtNroCuentaProv.Text) Then
            txtNombreProv.Clear()
            Exit Sub
        End If
        Dim n As Integer = 0
        If Not Integer.TryParse(txtNroCuentaProv.Text, n) OrElse n <= 0 Then
            txtNroCuentaProv.Clear()
            txtNombreProv.Clear()
            Exit Sub
        End If
        Try
            Dim dt As DataTable = DSM.ExecuteQuery(DSM.Proveedores, "SELECT NroCuenta, Nombre FROM MaeCtaCte WHERE NroCuenta = @Nro AND FechaBaja IS NULL", CmdParams("@Nro", n))
            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                txtNombreProv.Text = dt.Rows(0)("Nombre").ToString()
            Else
                MessageBox.Show("Proveedor no encontrado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtNroCuentaProv.Clear()
                txtNombreProv.Clear()
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnAgregarItem_Click(sender As Object, e As EventArgs) Handles btnAgregarItem.Click
        If modoActual = FormMode.CONSULTA Then Exit Sub
        Dim dt = TryCast(DgvItems.DataSource, DataTable)
        If dt Is Nothing Then Exit Sub
        Dim nroRenglon = If(dt.Rows.Count = 0, 1, CInt(dt.Compute("MAX(NroRenglon)", Nothing)) + 1)
        Dim nr = dt.NewRow()
        nr("IdItem") = 0
        nr("NroRenglon") = nroRenglon
        nr("TipoItem") = "S"
        nr("IdTipoServicio") = 0
        nr("IdRepuesto") = 0
        nr("IdComboSeleccionado") = 0
        nr("DescripcionItem") = ""
        nr("Cantidad") = 1D
        nr("PrecioUnitario") = 0D
        nr("SubtotalItem") = 0D
        nr("GarantiaKm") = 0
        nr("GarantiaMeses") = 0
        nr("ModoItem") = "ALTA"
        dt.Rows.Add(nr)
    End Sub

    Private Sub btnQuitarItem_Click(sender As Object, e As EventArgs) Handles btnQuitarItem.Click
        If modoActual = FormMode.CONSULTA Then Exit Sub
        If DgvItems.CurrentRow Is Nothing Then Exit Sub
        Dim drv = DirectCast(DgvItems.CurrentRow.DataBoundItem, DataRowView)
        If drv Is Nothing Then Exit Sub
        If CStr(drv("ModoItem")) = "ALTA" Then
            drv.Row.Delete()
        Else
            drv("ModoItem") = "BAJA"
        End If
        RecalcularTotales()
    End Sub

    Private Sub DgvItems_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DgvItems.CellValueChanged
        If e.RowIndex < 0 OrElse e.RowIndex >= DgvItems.Rows.Count Then Exit Sub
        Dim drv = TryCast(DgvItems.Rows(e.RowIndex).DataBoundItem, DataRowView)
        If drv Is Nothing Then Exit Sub
        Dim row = drv.Row

        If DgvItems.Columns(e.ColumnIndex) Is DgvItems.Columns("colTipo") Then
            row("IdTipoServicio") = 0
            row("IdRepuesto") = 0
            row("IdComboSeleccionado") = 0
            row("DescripcionItem") = ""
            row("PrecioUnitario") = 0D
            row("GarantiaKm") = 0
            row("GarantiaMeses") = 0
            row("SubtotalItem") = CDec(row("Cantidad")) * CDec(row("PrecioUnitario"))
            DgvItems.InvalidateRow(e.RowIndex)
        End If

        If DgvItems.Columns(e.ColumnIndex) Is DgvItems.Columns("colDesc") Then
            Dim tipo = CStr(row("TipoItem"))
            Dim selId = If(row.IsNull("IdComboSeleccionado"), 0, CInt(row("IdComboSeleccionado")))
            If selId <= 0 Then
                row("DescripcionItem") = ""
                row("PrecioUnitario") = 0D
            Else
                Dim lista = If(tipo = "S", listaTipos, listaRepuestos)
                Dim match = lista.Cast(Of Object).FirstOrDefault(Function(o) CInt(o.GetType().GetProperty("Id").GetValue(o, Nothing)) = selId)
                If match IsNot Nothing Then
                    Dim disp = Convert.ToString(match.GetType().GetProperty("Display").GetValue(match, Nothing))
                    Dim costo = CDec(match.GetType().GetProperty("CostoRef").GetValue(match, Nothing))
                    If tipo = "S" Then
                        row("IdTipoServicio") = selId
                        row("IdRepuesto") = 0
                        Dim gkm = CInt(match.GetType().GetProperty("GarKm").GetValue(match, Nothing))
                        Dim gmes = CInt(match.GetType().GetProperty("GarMeses").GetValue(match, Nothing))
                        row("GarantiaKm") = gkm
                        row("GarantiaMeses") = gmes
                    Else
                        row("IdRepuesto") = selId
                        row("IdTipoServicio") = 0
                    End If
                    row("DescripcionItem") = disp
                    row("PrecioUnitario") = costo
                End If
                row("SubtotalItem") = CDec(row("Cantidad")) * CDec(row("PrecioUnitario"))
                If CStr(row("ModoItem")) <> "ALTA" Then row("ModoItem") = "MODIF"
            End If
            RecalcularTotales()
        End If
    End Sub

    Private Sub DgvItems_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles DgvItems.EditingControlShowing
        Dim combo = TryCast(e.Control, ComboBox)
        If combo Is Nothing Then Exit Sub
        If DgvItems.CurrentCell Is Nothing Then Exit Sub
        Dim colName = DgvItems.Columns(DgvItems.CurrentCell.ColumnIndex).Name
        If colName <> "colDesc" Then Exit Sub
        Dim drv = TryCast(DgvItems.Rows(DgvItems.CurrentCell.RowIndex).DataBoundItem, DataRowView)
        If drv Is Nothing Then Exit Sub
        Dim tipo = CStr(drv.Row("TipoItem"))
        Dim lista = If(tipo = "S", listaTipos, listaRepuestos)
        combo.DataSource = lista
        combo.DisplayMember = "Display"
        combo.ValueMember = "Id"
        RemoveHandler combo.SelectedIndexChanged, AddressOf ComboDesc_SelectedIndexChanged
        AddHandler combo.SelectedIndexChanged, AddressOf ComboDesc_SelectedIndexChanged
    End Sub

    Private Sub ComboDesc_SelectedIndexChanged(sender As Object, e As EventArgs)
        If DgvItems.CurrentCell Is Nothing Then Exit Sub
        DgvItems.CommitEdit(DataGridViewDataErrorContexts.Commit)
    End Sub

    Private Sub DgvItems_DataError(sender As Object, e As DataGridViewDataErrorEventArgs) Handles DgvItems.DataError
        e.ThrowException = False
    End Sub

    Private Sub DgvItems_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DgvItems.CellFormatting
        If e.RowIndex < 0 OrElse e.RowIndex >= DgvItems.Rows.Count Then Exit Sub
        If DgvItems.Columns(e.ColumnIndex).Name <> "colDesc" Then Exit Sub
        Dim drv = TryCast(DgvItems.Rows(e.RowIndex).DataBoundItem, DataRowView)
        If drv Is Nothing Then Exit Sub
        Dim row = drv.Row
        Dim tipo = CStr(row("TipoItem"))
        Dim idSel = If(row.IsNull("IdComboSeleccionado"), 0, CInt(row("IdComboSeleccionado")))
        If idSel <= 0 Then
            e.Value = ""
        Else
            Dim lista = If(tipo = "S", listaTipos, listaRepuestos)
            Dim match = lista.Cast(Of Object).FirstOrDefault(Function(o) CInt(o.GetType().GetProperty("Id").GetValue(o, Nothing)) = idSel)
            e.Value = If(match Is Nothing, "", Convert.ToString(match.GetType().GetProperty("Display").GetValue(match, Nothing)))
        End If
        e.FormattingApplied = True
    End Sub

    Private Sub DgvItems_CellParsing(sender As Object, e As DataGridViewCellParsingEventArgs) Handles DgvItems.CellParsing
        If e.RowIndex < 0 OrElse e.RowIndex >= DgvItems.Rows.Count Then Exit Sub
        If DgvItems.Columns(e.ColumnIndex).Name <> "colDesc" Then Exit Sub
        Dim drv = TryCast(DgvItems.Rows(e.RowIndex).DataBoundItem, DataRowView)
        If drv Is Nothing Then Exit Sub
        Dim row = drv.Row
        Dim tipo = CStr(row("TipoItem"))
        Dim lista = If(tipo = "S", listaTipos, listaRepuestos)
        Dim selText = If(e.Value Is Nothing, "", e.Value.ToString())
        Dim idSel As Integer = 0
        If Integer.TryParse(selText, idSel) Then
            e.Value = If(idSel <= 0, CObj(DBNull.Value), CObj(idSel))
            e.ParsingApplied = True
            Exit Sub
        End If
        For Each o In lista
            Dim disp = Convert.ToString(o.GetType().GetProperty("Display").GetValue(o, Nothing))
            Dim idP = CInt(o.GetType().GetProperty("Id").GetValue(o, Nothing))
            If disp = selText Then
                idSel = idP
                Exit For
            End If
        Next
        e.Value = If(idSel <= 0, CObj(DBNull.Value), CObj(idSel))
        e.ParsingApplied = True
    End Sub

    Private Sub DgvItems_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DgvItems.RowsAdded
    End Sub

    Private Sub DgvItems_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DgvItems.CellEndEdit
        If e.RowIndex < 0 OrElse e.RowIndex >= DgvItems.Rows.Count Then Exit Sub
        Dim drv = DirectCast(DgvItems.Rows(e.RowIndex).DataBoundItem, DataRowView)
        If drv Is Nothing Then Exit Sub
        Dim row = drv.Row
        Dim cant As Decimal = 0, prec As Decimal = 0
        Decimal.TryParse(row("Cantidad").ToString(), cant)
        Decimal.TryParse(row("PrecioUnitario").ToString(), prec)
        row("SubtotalItem") = cant * prec
        If CStr(row("ModoItem")) <> "ALTA" Then row("ModoItem") = "MODIF"
        RecalcularTotales()
    End Sub

    Private Sub DgvItems_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles DgvItems.CurrentCellDirtyStateChanged
        If DgvItems.IsCurrentCellDirty Then DgvItems.CommitEdit(DataGridViewDataErrorContexts.Commit)
    End Sub

    Private Sub txtOtrosGastos_Leave(sender As Object, e As EventArgs) Handles txtOtrosGastos.Leave
        Dim d As Decimal = 0
        Decimal.TryParse(txtOtrosGastos.Text, d)
        txtOtrosGastos.Text = d.ToString("N2")
        RecalcularTotales()
    End Sub

    Private Sub RecalcularTotales()
        Dim dt = TryCast(DgvItems.DataSource, DataTable)
        Dim subtotal As Decimal = 0
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
            For Each r As DataRow In dt.Rows
                If r.RowState = DataRowState.Deleted Then Continue For
                If CStr(r("ModoItem")) = "BAJA" Then Continue For
                subtotal += CDec(r("SubtotalItem"))
            Next
        End If
        txtSubTotalItems.Text = subtotal.ToString("N2")
        Dim otros As Decimal = 0
        Decimal.TryParse(txtOtrosGastos.Text, otros)
        txtTotalServicio.Text = (subtotal + otros).ToString("N2")
    End Sub

    Private Sub cmdAceptar_Click(sender As Object, e As EventArgs) Handles cmdAceptar.Click
        If cboVehiculo.SelectedItem Is Nothing OrElse Not TypeOf cboVehiculo.SelectedItem Is VehiculoItem Then
            MessageBox.Show("Seleccione un vehículo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboVehiculo.Focus()
            Exit Sub
        End If
        Dim idVehiculo = DirectCast(cboVehiculo.SelectedItem, VehiculoItem).IdVehiculo
        Dim SqlSucursal As String = "SELECT IdSucursal FROM Vehiculos WHERE IdVehiculo = @IdVehiculo"
        Dim dtSucursal As DataTable = DSM.ExecuteQuery(DSM.Proveedores, SqlSucursal, CmdParams("@IdVehiculo", idVehiculo))
        Dim idSucursal As Integer = 0
        If dtSucursal IsNot Nothing AndAlso dtSucursal.Rows.Count > 0 Then
            idSucursal = CInt(dtSucursal.Rows(0)("IdSucursal"))
        End If

        Dim nroCuentaProv As Integer? = Nothing
        If Not String.IsNullOrWhiteSpace(txtNroCuentaProv.Text) Then
            Dim n As Integer = 0
            If Not Integer.TryParse(txtNroCuentaProv.Text, n) OrElse n <= 0 Then
                MessageBox.Show("Nro. de cuenta del proveedor no válido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtNroCuentaProv.Focus()
                Exit Sub
            End If
            nroCuentaProv = n
        End If
        If nroCuentaProv.HasValue AndAlso String.IsNullOrWhiteSpace(txtNombreProv.Text) Then
            MessageBox.Show("Debe asociar un proveedor válido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtNroCuentaProv.Focus()
            Exit Sub
        End If

        Dim kmServ As Integer = 0
        If Not String.IsNullOrWhiteSpace(txtKmServicio.Text) AndAlso Not Integer.TryParse(txtKmServicio.Text, kmServ) Then
            MessageBox.Show("Km del servicio no válido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtKmServicio.Focus()
            Exit Sub
        End If

        Dim idTipoServicio As Integer = 0
        Dim dt = TryCast(DgvItems.DataSource, DataTable)
        If dt IsNot Nothing Then
            For Each r As DataRow In dt.Rows
                If r.RowState = DataRowState.Deleted Then Continue For
                If CStr(r("ModoItem")) = "BAJA" Then Continue For
                If CStr(r("TipoItem")) = "S" AndAlso CInt(r("IdTipoServicio")) > 0 Then
                    idTipoServicio = CInt(r("IdTipoServicio"))
                    Exit For
                End If
            Next
        End If

        If dt Is Nothing OrElse dt.Rows.Cast(Of DataRow).Where(Function(r) r.RowState <> DataRowState.Deleted AndAlso CStr(r("ModoItem")) <> "BAJA").Count() = 0 Then
            If MessageBox.Show("El servicio no tiene ítems. ¿Desea guardar de todos modos?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
                Exit Sub
            End If
        End If

        Dim otrosG As Decimal = 0, subtotalI As Decimal = 0, totalS As Decimal = 0
        Decimal.TryParse(txtOtrosGastos.Text, otrosG)
        Decimal.TryParse(txtSubTotalItems.Text, subtotalI)
        Decimal.TryParse(txtTotalServicio.Text, totalS)

        Dim fecFact As Date? = Nothing
        If Not String.IsNullOrWhiteSpace(txtNroFactura.Text) OrElse Not String.IsNullOrWhiteSpace(txtPuntoDeVenta.Text) Then
            fecFact = dtpFechaFactura.Value.Date
        End If

        Dim usuarioId As String = If(String.IsNullOrWhiteSpace(General.UsuarioActual), DBNull.Value, CObj(General.UsuarioActual))
        Try
            Dim sqlCabe As String
            Dim params As Dictionary(Of String, Object)
            If modoActual = FormMode.ALTA Then
                sqlCabe = "INSERT INTO Servicios (IdVehiculo,NroCuentaProv,ProveedorNombre,FechaServicio,KmServicio,IdTipoServicio,NroFactura,NroComprobante,PuntoDeVenta,FechaFactura,Observaciones,SubTotalItems,OtrosGastos,TotalServicio,IdUsuarioCrea,FechaHoraCrea,IdUsuarioMod,FechaHoraMod,IdSucursal) VALUES (@IdVehiculo,@NroCta,@ProvNom,@FecServ,@KmServ,@IdTipoServ,@NroFac,@NroCom,@PV,@FecFac,@Obs,@SubTot,@OtrosG,@Total,@UsrC,@FecC,@UsrC,@FecC,@idsucursal); SELECT SCOPE_IDENTITY();"
                params = CmdParams(
                    "@IdVehiculo", idVehiculo,
                    "@NroCta", If(nroCuentaProv.HasValue, CObj(nroCuentaProv.Value), DBNull.Value),
                    "@ProvNom", If(nroCuentaProv.HasValue, txtNombreProv.Text, DBNull.Value),
                    "@FecServ", dtpFechaServicio.Value.Date,
                    "@KmServ", kmServ,
                    "@IdTipoServ", If(idTipoServicio = 0, DBNull.Value, CObj(idTipoServicio)),
                    "@NroFac", If(String.IsNullOrWhiteSpace(txtNroFactura.Text), DBNull.Value, txtNroFactura.Text),
                    "@NroCom", DBNull.Value,
                    "@PV", If(String.IsNullOrWhiteSpace(txtPuntoDeVenta.Text), DBNull.Value, txtPuntoDeVenta.Text),
                    "@FecFac", If(fecFact.HasValue, CObj(fecFact.Value), DBNull.Value),
                    "@Obs", If(String.IsNullOrWhiteSpace(txtObservaciones.Text), DBNull.Value, txtObservaciones.Text),
                    "@SubTot", subtotalI,
                    "@OtrosG", otrosG,
                    "@Total", totalS,
                    "@UsrC", usuarioId,
                    "@FecC", Date.Now,
                    "@idsucursal", idSucursal
                )
                Dim obj = DSM.ExecuteQuery(DSM.Proveedores, sqlCabe, params)
                idServicioActual = CInt(CDec(obj.Rows(0)(0)))
            Else
                sqlCabe = "UPDATE Servicios SET IdVehiculo=@IdVehiculo,NroCuentaProv=@NroCta,ProveedorNombre=@ProvNom,FechaServicio=@FecServ,KmServicio=@KmServ,IdTipoServicio=@IdTipoServ,NroFactura=@NroFac,NroComprobante=@NroCom,PuntoDeVenta=@PV,FechaFactura=@FecFac,Observaciones=@Obs,SubTotalItems=@SubTot,OtrosGastos=@OtrosG,TotalServicio=@Total,IdUsuarioMod=@UsrM,FechaHoraMod=@FecM,IdSucursal=@idsucursal WHERE IdServicio=@Id"
                params = CmdParams(
                    "@IdVehiculo", idVehiculo,
                    "@NroCta", If(nroCuentaProv.HasValue, CObj(nroCuentaProv.Value), DBNull.Value),
                    "@ProvNom", If(nroCuentaProv.HasValue, txtNombreProv.Text, DBNull.Value),
                    "@FecServ", dtpFechaServicio.Value.Date,
                    "@KmServ", kmServ,
                    "@IdTipoServ", If(idTipoServicio = 0, DBNull.Value, CObj(idTipoServicio)),
                    "@NroFac", If(String.IsNullOrWhiteSpace(txtNroFactura.Text), DBNull.Value, txtNroFactura.Text),
                    "@NroCom", DBNull.Value,
                    "@PV", If(String.IsNullOrWhiteSpace(txtPuntoDeVenta.Text), DBNull.Value, txtPuntoDeVenta.Text),
                    "@FecFac", If(fecFact.HasValue, CObj(fecFact.Value), DBNull.Value),
                    "@Obs", If(String.IsNullOrWhiteSpace(txtObservaciones.Text), DBNull.Value, txtObservaciones.Text),
                    "@SubTot", subtotalI,
                    "@OtrosG", otrosG,
                    "@Total", totalS,
                    "@UsrM", usuarioId,
                    "@FecM", Date.Now,
                    "@Id", idServicioActual
                )
                DSM.Execute(DSM.Proveedores, sqlCabe, params, True)
            End If

            For Each r As DataRow In dt.Rows
                If r.RowState = DataRowState.Deleted Then Continue For
                Dim m = CStr(r("ModoItem"))
                If m = "BAJA" Then
                    If CInt(r("IdItem")) > 0 Then
                        DSM.Execute(DSM.Proveedores, "DELETE FROM ServiciosItems WHERE IdItem = @Id", CmdParams("@Id", CInt(r("IdItem"))), True)
                    End If
                    Continue For
                End If
                Dim tipo = CStr(r("TipoItem"))
                Dim desc = r("DescripcionItem").ToString().Trim()
                Dim cant As Decimal = 0, prec As Decimal = 0
                Decimal.TryParse(r("Cantidad").ToString(), cant)
                Decimal.TryParse(r("PrecioUnitario").ToString(), prec)
                Dim idTs = CInt(r("IdTipoServicio"))
                Dim idRp = CInt(r("IdRepuesto"))
                If String.IsNullOrEmpty(desc) AndAlso idTs = 0 AndAlso idRp = 0 Then Continue For
                If cant = 0 Then Continue For
                Dim idItem = CInt(r("IdItem"))
                Dim nroR = CInt(r("NroRenglon"))
                Dim garKm = If(r.IsNull("GarantiaKm"), 0, CInt(r("GarantiaKm")))
                Dim garMeses = If(r.IsNull("GarantiaMeses"), 0, CInt(r("GarantiaMeses")))
                Dim sqlIt As String
                If m = "ALTA" OrElse idItem = 0 Then
                    sqlIt = "INSERT INTO ServiciosItems (IdServicio,NroRenglon,TipoItem,IdTipoServicio,IdRepuesto,DescripcionItem,Cantidad,PrecioUnitario,SubtotalItem,GarantiaKm,GarantiaMeses) VALUES (@IdS,@NR,@TI,@ITS,@IR,@DI,@C,@PU,@STI,@GK,@GM)"
                    DSM.Execute(DSM.Proveedores, sqlIt, CmdParams(
                        "@IdS", idServicioActual,
                        "@NR", nroR,
                        "@TI", tipo,
                        "@ITS", If(tipo = "S" AndAlso idTs > 0, CObj(idTs), DBNull.Value),
                        "@IR", If(tipo = "R" AndAlso idRp > 0, CObj(idRp), DBNull.Value),
                        "@DI", desc,
                        "@C", cant,
                        "@PU", prec,
                        "@STI", cant * prec,
                        "@GK", If(garKm > 0, CObj(garKm), DBNull.Value),
                        "@GM", If(garMeses > 0, CObj(garMeses), DBNull.Value)
                    ), True)
                ElseIf m = "MODIF" Then
                    sqlIt = "UPDATE ServiciosItems SET TipoItem=@TI,IdTipoServicio=@ITS,IdRepuesto=@IR,DescripcionItem=@DI,Cantidad=@C,PrecioUnitario=@PU,SubtotalItem=@STI,GarantiaKm=@GK,GarantiaMeses=@GM,NroRenglon=@NR WHERE IdItem=@Id"
                    DSM.Execute(DSM.Proveedores, sqlIt, CmdParams(
                        "@TI", tipo,
                        "@ITS", If(tipo = "S" AndAlso idTs > 0, CObj(idTs), DBNull.Value),
                        "@IR", If(tipo = "R" AndAlso idRp > 0, CObj(idRp), DBNull.Value),
                        "@DI", desc,
                        "@C", cant,
                        "@PU", prec,
                        "@STI", cant * prec,
                        "@GK", If(garKm > 0, CObj(garKm), DBNull.Value),
                        "@GM", If(garMeses > 0, CObj(garMeses), DBNull.Value),
                        "@NR", nroR,
                        "@Id", idItem
                    ), True)
                End If
            Next

            If kmServ > 0 Then
                Dim sqlActualizaKm As String = "UPDATE Vehiculos SET KmActual = CASE WHEN @Km > KmActual THEN @Km ELSE KmActual END, IdUsuarioMod = @Usr, FechaHoraMod = @Fec WHERE IdVehiculo = @Id"
                DSM.Execute(DSM.Proveedores, sqlActualizaKm, CmdParams("@Km", kmServ, "@Usr", usuarioId, "@Fec", Date.Now, "@Id", idVehiculo), True)
            End If

            MessageBox.Show("Servicio guardado correctamente (Id: " & idServicioActual & ")", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CambiarModo(FormMode.CONSULTA)
            BuscarServicios()
            For i = 0 To DgvListado.Rows.Count - 1
                If DgvListado.Rows(i).Cells("IdServicio").Value IsNot DBNull.Value AndAlso CInt(DgvListado.Rows(i).Cells("IdServicio").Value) = idServicioActual Then
                    DgvListado.ClearSelection()
                    DgvListado.Rows(i).Selected = True
                    DgvListado.CurrentCell = DgvListado.Rows(i).Cells(1)
                    Exit For
                End If
            Next
        Catch ex As Exception
            MessageBox.Show("Error al guardar servicio: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CmdBorrar_Click(sender As Object, e As EventArgs) Handles CmdBorrar.Click
        If idServicioActual <= 0 Then Exit Sub
        If MessageBox.Show("¿Confirma eliminar el servicio Nro. " & idServicioActual & "? Se eliminarán los ítems asociados.", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) <> DialogResult.Yes Then
            Exit Sub
        End If
        Try
            DSM.Execute(DSM.Proveedores, "DELETE FROM Servicios WHERE IdServicio = @Id", CmdParams("@Id", idServicioActual), True)
            MessageBox.Show("Servicio eliminado", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LimpiarCampos()
            BuscarServicios()
        Catch ex As Exception
            MessageBox.Show("Error al eliminar: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub lnkCopiar_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkCopiar.LinkClicked
        CopiarDataGrid(DgvItems, chkEncabezados.Checked)
    End Sub

    Private Sub DgvItems_KeyDown(sender As Object, e As KeyEventArgs) Handles DgvItems.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.C Then
            CopiarDataGrid(DgvItems, chkEncabezados.Checked)
            e.SuppressKeyPress = True
        End If
    End Sub

    Private Sub dtpBusqHasta_ValueChanged(sender As Object, e As EventArgs) Handles dtpBusqHasta.ValueChanged
        idServicioActual = 0
        BuscarServicios()
    End Sub

    Private Sub dtpBusqDesde_ValueChanged(sender As Object, e As EventArgs) Handles dtpBusqDesde.ValueChanged
        idServicioActual = 0
        BuscarServicios()
    End Sub

    Private Sub cboBusqVehiculo_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboBusqVehiculo.SelectedValueChanged
        idServicioActual = 0
        BuscarServicios()
    End Sub

    Private Sub cboSucursal_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboSucursal.SelectedValueChanged
        idServicioActual = 0
        BuscarServicios()
    End Sub
End Class
