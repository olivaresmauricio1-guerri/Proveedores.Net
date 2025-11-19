Imports DSM = DataSourceManager.Lib.DataSourceManager

Partial Public Class frmResumen
    Inherits Form
    Private Shared instancia As frmResumen
    Private tablaProv As New DataTable()
    Private dtResumen As DataTable

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmResumen()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmResumen_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Private Sub frmResumen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Resumen de Proveedores"
        Me.WindowState = FormWindowState.Maximized
        Me.KeyPreview = True
        Funciones.ConfigurarEstiloGrid(DgvProveedores)
        Funciones.ConfigurarEstiloGrid(DBGdeta)
        CargarProveedores()
    End Sub

    Private Sub TxtBuscar_TextChanged(sender As Object, e As EventArgs) Handles TxtBuscar.TextChanged
        CargarProveedores(TxtBuscar.Text.Trim())
    End Sub

    Private Sub CargarProveedores(Optional filtro As String = "")
        Dim sql As String = "SELECT NroCuenta, Nombre, CodContable, Rubro, Telefono, Cuit, Provincia FROM MaeCtaCte"
        Dim prms As Dictionary(Of String, Object) = Nothing
        If filtro <> "" Then
            If Integer.TryParse(filtro, Nothing) Then
                sql &= " WHERE NroCuenta = @n"
                prms = CmdParams("@n", CInt(filtro))
            Else
                sql &= " WHERE Nombre LIKE '%' + @t + '%'"
                prms = CmdParams("@t", filtro)
            End If
        End If
        sql &= " ORDER BY Nombre"
        tablaProv = DSM.ExecuteQuery(DSM.Proveedores, sql, prms)
        DgvProveedores.DataSource = tablaProv
        ConfigurarListadoProveedores()
    End Sub

    Private Sub ConfigurarListadoProveedores()
        If DgvProveedores Is Nothing OrElse DgvProveedores.Columns.Count = 0 Then Return
        For Each col As DataGridViewColumn In DgvProveedores.Columns
            col.Visible = False
        Next
        If DgvProveedores.Columns.Contains("NroCuenta") Then
            DgvProveedores.Columns("NroCuenta").Visible = True
            DgvProveedores.Columns("NroCuenta").HeaderText = "NroCta"
            DgvProveedores.Columns("NroCuenta").Width = 80
        End If
        If DgvProveedores.Columns.Contains("Nombre") Then
            DgvProveedores.Columns("Nombre").Visible = True
            DgvProveedores.Columns("Nombre").HeaderText = "Proveedor"
            DgvProveedores.Columns("Nombre").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End If
        If DgvProveedores.Columns.Contains("CodContable") Then
            DgvProveedores.Columns("CodContable").Visible = True
            DgvProveedores.Columns("CodContable").HeaderText = "Cuenta"
            DgvProveedores.Columns("CodContable").Width = 100
        End If
        If DgvProveedores.Columns.Contains("Rubro") Then
            DgvProveedores.Columns("Rubro").Visible = True
            DgvProveedores.Columns("Rubro").HeaderText = "Rubro"
            DgvProveedores.Columns("Rubro").Width = 120
        End If
        If DgvProveedores.Columns.Contains("Telefono") Then
            DgvProveedores.Columns("Telefono").Visible = True
            DgvProveedores.Columns("Telefono").HeaderText = "Teléfono"
            DgvProveedores.Columns("Telefono").Width = 120
        End If
        If DgvProveedores.Columns.Contains("Cuit") Then
            DgvProveedores.Columns("Cuit").Visible = True
            DgvProveedores.Columns("Cuit").HeaderText = "CUIT/CUIL"
            DgvProveedores.Columns("Cuit").Width = 120
        End If
        If DgvProveedores.Columns.Contains("Provincia") Then
            DgvProveedores.Columns("Provincia").Visible = True
            DgvProveedores.Columns("Provincia").HeaderText = "Provincia"
            DgvProveedores.Columns("Provincia").Width = 120
        End If
    End Sub

    Private Sub DgvProveedores_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvProveedores.CellClick
        If e.RowIndex < 0 Then Return
        Dim row = DgvProveedores.Rows(e.RowIndex)
        If row Is Nothing OrElse row.Cells("NroCuenta").Value Is Nothing Then Return
        Dim nroCuenta As Integer = Convert.ToInt32(row.Cells("NroCuenta").Value)
        MostrarResumen(nroCuenta)
    End Sub

    Private Sub MostrarResumen(nroCuenta As Integer)
        Dim infoDt = DSM.ExecuteQuery(DSM.Proveedores, "SELECT ISNULL(ULTIMORESUMEN,0) AS SaldoAnterior FROM MaeCtaCte WHERE NroCuenta = @NroCuenta", CmdParams("@NroCuenta", nroCuenta))
        Dim saldoAnterior As Decimal = 0D
        If infoDt IsNot Nothing AndAlso infoDt.Rows.Count > 0 Then
            saldoAnterior = Convert.ToDecimal(infoDt.Rows(0)("SaldoAnterior"))
        End If
        Dim sqlResumen As String = "SELECT Fecha, NombreComprobante AS TipoMovimiento, " &
                                   "CASE WHEN Monto < 0 THEN ABS(Monto) ELSE 0 END AS Debitos, " &
                                   "CASE WHEN Monto > 0 THEN Monto ELSE 0 END AS Creditos, " &
                                   "@SaldoAnterior + SUM(Monto) OVER (ORDER BY Fecha, IDIMPUTACION, NroFactura ROWS UNBOUNDED PRECEDING) AS Saldo, " &
                                   "Comentario, Cobrado " &
                                   "FROM DetaCtaCte WHERE NroCuenta = @NroCuenta AND IDIMPUTACION NOT IN (5,56) " &
                                   "ORDER BY Fecha, IDIMPUTACION, NroFactura"
        dtResumen = DSM.ExecuteQuery(DSM.Proveedores, sqlResumen, CmdParams("@NroCuenta", nroCuenta, "@SaldoAnterior", saldoAnterior))
        DBGdeta.DataSource = dtResumen
        ConfiguraColListadoResumen()
    End Sub

    Private Sub ConfiguraColListadoResumen()
        If DBGdeta.Columns.Count = 0 Then Return
        For Each col As DataGridViewColumn In DBGdeta.Columns
            col.Visible = False
        Next
        If DBGdeta.Columns.Contains("Fecha") Then
            DBGdeta.Columns("Fecha").Visible = True
            DBGdeta.Columns("Fecha").HeaderText = "Fecha"
            DBGdeta.Columns("Fecha").Width = 80
            DBGdeta.Columns("Fecha").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DBGdeta.Columns("Fecha").DisplayIndex = 0
        End If
        If DBGdeta.Columns.Contains("TipoMovimiento") Then
            DBGdeta.Columns("TipoMovimiento").Visible = True
            DBGdeta.Columns("TipoMovimiento").HeaderText = "Tipo Movimiento"
            DBGdeta.Columns("TipoMovimiento").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            DBGdeta.Columns("TipoMovimiento").DisplayIndex = 1
        End If
        If DBGdeta.Columns.Contains("Debitos") Then
            DBGdeta.Columns("Debitos").Visible = True
            DBGdeta.Columns("Debitos").HeaderText = "Debe"
            DBGdeta.Columns("Debitos").Width = 100
            DBGdeta.Columns("Debitos").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DBGdeta.Columns("Debitos").DefaultCellStyle.Format = "N2"
            DBGdeta.Columns("Debitos").DisplayIndex = 2
        End If
        If DBGdeta.Columns.Contains("Creditos") Then
            DBGdeta.Columns("Creditos").Visible = True
            DBGdeta.Columns("Creditos").HeaderText = "Haber"
            DBGdeta.Columns("Creditos").Width = 100
            DBGdeta.Columns("Creditos").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DBGdeta.Columns("Creditos").DefaultCellStyle.Format = "N2"
            DBGdeta.Columns("Creditos").DisplayIndex = 3
        End If
        If DBGdeta.Columns.Contains("Saldo") Then
            DBGdeta.Columns("Saldo").Visible = True
            DBGdeta.Columns("Saldo").HeaderText = "Saldo"
            DBGdeta.Columns("Saldo").Width = 100
            DBGdeta.Columns("Saldo").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DBGdeta.Columns("Saldo").DefaultCellStyle.Format = "N2"
            DBGdeta.Columns("Saldo").DisplayIndex = 4
        End If
        If DBGdeta.Columns.Contains("Comentario") Then
            DBGdeta.Columns("Comentario").Visible = True
            DBGdeta.Columns("Comentario").HeaderText = "Comentario"
            DBGdeta.Columns("Comentario").Width = 250
            DBGdeta.Columns("Comentario").DisplayIndex = 5
        End If
        If DBGdeta.Columns.Contains("Cobrado") Then
            DBGdeta.Columns.Remove("Cobrado")
        End If
        Dim chk As New DataGridViewCheckBoxColumn()
        chk.Name = "Cobrado"
        chk.HeaderText = "Cobrado"
        chk.DataPropertyName = "Cobrado"
        chk.Width = 80
        chk.DisplayIndex = 6
        DBGdeta.Columns.Add(chk)
    End Sub
End Class