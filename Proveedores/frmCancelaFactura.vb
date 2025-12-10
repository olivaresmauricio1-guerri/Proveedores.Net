Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmCancelaFactura
    Private Shared instancia As frmCancelaFactura
    Private filaEncontrada As DataRow

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmCancelaFactura()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmCancelaFactura_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Private Sub frmCancelaFactura_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        StartPosition = FormStartPosition.CenterScreen
        KeyPreview = True
        CargarCombos(cmbSucursal, "Sucursales", "Descripcion", "Descripcion", "Codigo")
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        btnBuscar.Enabled = False

        Dim cuenta As Integer = Val(txtNroCuenta.Text)
        Dim nroFactura As Integer = Val(txtNroFactura.Text)
        Dim sucursal As String = cmbSucursal.Text?.Trim()

        Dim sql As String = "SELECT * FROM detactacte " &
                            " WHERE (idimputacion IN (1,2,10))" &
                            " AND (nrocuenta = @nrocuenta)" &
                            " AND (nroFACTURA = @nroFACTURA)" &
                            " AND (sucursal = @sucursal)" &
                            " ORDER BY Fecha DESC"

        Dim tabla = DSM.ExecuteQuery(DSM.Proveedores, sql,
                                     CmdParams("@nrocuenta", cuenta,
                                               "@nroFACTURA", nroFactura,
                                               "@sucursal", sucursal))

        If tabla Is Nothing OrElse tabla.Rows.Count < 1 Then
            MsgBox("Factura Inexistente", MsgBoxStyle.Exclamation)
            btnBuscar.Enabled = True
            Exit Sub
        End If

        filaEncontrada = tabla.Rows(0)

        txtOPago.Text = "0"
        Dim cobradoVal As Boolean = False
        If Not IsDBNull(filaEncontrada("cobrado")) Then cobradoVal = Convert.ToBoolean(filaEncontrada("cobrado"))
        chkCobrado.Checked = cobradoVal

        txtImporte.Text = If(IsDBNull(filaEncontrada("monto")), "", filaEncontrada("monto").ToString())
        txtACuenta.Text = If(IsDBNull(filaEncontrada("acuenta")), "", filaEncontrada("acuenta").ToString())

        If Not IsDBNull(filaEncontrada("Fecha")) Then
            Dim f As DateTime
            DateTime.TryParse(filaEncontrada("Fecha").ToString(), f)
            dtpFecha.Value = If(f = DateTime.MinValue, DateTime.Today, f)
        End If

        btnBuscar.Enabled = True
    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click

        Dim cuenta As Integer = Val(txtNroCuenta.Text)
        Dim nroFactura As Integer = Val(txtNroFactura.Text)
        Dim sucursal As String = cmbSucursal.Text?.Trim()
        Dim fechaSel As Object = If(filaEncontrada Is Nothing OrElse IsDBNull(filaEncontrada("Fecha")), dtpFecha.Value, filaEncontrada("Fecha"))

        Dim sql As String = "UPDATE detactacte SET cobrado = @cobrado " &
                            " WHERE (idimputacion IN (1,2,10))" &
                            " AND (nrocuenta = @nrocuenta)" &
                            " AND (nroFACTURA = @nroFACTURA)" &
                            " AND (sucursal = @sucursal)" &
                            " AND (Fecha = @fecha)"

        Dim p = CmdParams("@cobrado", chkCobrado.Checked,
                          "@nrocuenta", cuenta,
                          "@nroFACTURA", nroFactura,
                          "@sucursal", sucursal,
                          "@fecha", fechaSel)

        DSM.Execute(DSM.Proveedores, sql, p, True)

        MessageBox.Show("Datos actualizados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Close()
    End Sub
End Class
