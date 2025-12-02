Imports System.Diagnostics
Imports System.Windows.Forms
Imports DSM = DataSourceManager.Lib.DataSourceManager

Partial Public Class MainForm

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = $"Proveedores V.{My.Application.Info.Version}"
        Me.WindowState = FormWindowState.Maximized

        Try
            Dim sucursales = DSM.ExecuteQuery(
               DSM.Stock,
                "SELECT Descripcion FROM Sucursales WHERE idSucursal = @Sucursal",
                CmdParams("@Sucursal", SucursalActual))

            If sucursales.Rows.Count > 0 Then
                General.DescripcionSucursal = sucursales.Rows(0)("Descripcion").ToString()
            End If

            Dim empresas = DSM.ExecuteQuery(
               DSM.Stock,
                "SELECT Descripcion FROM Empresas WHERE Codigo = @Empresa",
                CmdParams("@Empresa", 1))

            If empresas.Rows.Count > 0 Then
                General.EmpresaActual = empresas.Rows(0)("Descripcion").ToString()
            End If

        Catch ex As Exception
            MessageBox.Show($"Error al cargar la configuración: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


        Panel1.Text = $"Sistema de Proveedores - Sucursal: {DescripcionSucursal}"
        Panel2.Text = DateTime.Now.ToString("dd/MM/yyyy")
        Panel3.Text = DateTime.Now.ToString("HH:mm")
        Panel4.Text = UsuarioActual & " | " & Mid(General.Entorno, 1, 3).ToUpper()

        Timer1.Start()
    End Sub

    Private Sub MainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Application.Exit()
    End Sub

    Private Sub MainForm_Leave(sender As Object, e As EventArgs) Handles MyBase.Leave
        Timer1.Stop()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Panel2.Text = DateTime.Now.ToString("dd/MM/yyyy")
        Panel3.Text = DateTime.Now.ToString("HH:mm")
    End Sub

    ' ----- Menú Configuración -----
    Private Sub MnuConImp_Click(sender As Object, e As EventArgs) Handles MnuConImp.Click
        Try
            Using dlg As New PrintDialog()
                dlg.UseEXDialog = True
                dlg.ShowDialog(Me)
            End Using
        Catch ex As Exception
            MessageBox.Show("No se pudo abrir el diálogo de impresora", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mncalcu_Click(sender As Object, e As EventArgs) Handles mncalcu.Click
        Try
            Process.Start("calc.exe")
        Catch ex As Exception
            MessageBox.Show("No se pudo abrir la calculadora", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub MnuConAlm_Click(sender As Object, e As EventArgs) Handles MnuConAlm.Click
        MessageBox.Show("Control Cajas no implementado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MnuSalir_Click(sender As Object, e As EventArgs) Handles MnuSalir.Click
        Me.Close()
    End Sub

    ' ----- Menú Actualizaciones -----
    Private Sub MnuStoMan_Click(sender As Object, e As EventArgs) Handles MnuStoMan.Click
        frmProvee.AbrirInstancia(Me)
    End Sub

    Private Sub MnuStoNov_Click(sender As Object, e As EventArgs) Handles MnuStoNov.Click
        frmNoveProveedores.AbrirInstancia(Me)
    End Sub

    Private Sub mnlistanov_Click(sender As Object, e As EventArgs) Handles mnlistanov.Click
        MessageBox.Show("Listar Novedades no implementado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub mnpaga_Click(sender As Object, e As EventArgs) Handles mnpaga.Click
        MessageBox.Show("Pago a Proveedores Galicia no implementado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub mnopagox_Click(sender As Object, e As EventArgs) Handles mnopagox.Click
        MessageBox.Show("Emisión Ordenes de Pago no implementado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub mncandjdjs_Click(sender As Object, e As EventArgs) Handles mncandjdjs.Click
        MessageBox.Show("Cancelar Facturas no implementado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MnuStoAct_Click(sender As Object, e As EventArgs) Handles MnuStoAct.Click
        MessageBox.Show("Actualización de Saldos no implementado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MnuStoCie_Click(sender As Object, e As EventArgs) Handles MnuStoCie.Click
        MessageBox.Show("Cierre de Mes no implementado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' ----- Menú Consultas -----
    Private Sub MnuConCli_Click(sender As Object, e As EventArgs) Handles MnuConCli.Click
        MessageBox.Show("Consulta Gral. de Proveedores no implementado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MnuConStoCom_Click(sender As Object, e As EventArgs) Handles MnuConStoCom.Click
        frmResumen.AbrirInstancia(Me)
    End Sub

    Private Sub mncosjhs_Click(sender As Object, e As EventArgs) Handles mncosjhs.Click
        frmResumenHistorico.AbrirInstancia(Me)
    End Sub

    Private Sub MnuConStoCob_Click(sender As Object, e As EventArgs) Handles MnuConStoCob.Click
        frmListaProveedores.AbrirInstancia(Me)
    End Sub

    Private Sub MnuConStoTot_Click(sender As Object, e As EventArgs) Handles MnuConStoTot.Click
        MessageBox.Show("Sub Diarios de IVA no implementado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub mnconsul_Click(sender As Object, e As EventArgs) Handles mnconsul.Click
        frmBuscaFactura.AbrirInstancia(Me)
    End Sub

    Private Sub mnmarcaop_Click(sender As Object, e As EventArgs) Handles mnmarcaop.Click
        MessageBox.Show("Marcar Ordenes de Pago no implementado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub mncpt_Click(sender As Object, e As EventArgs) Handles mncpt.Click
        frmListaComprobantes.AbrirInstancia(Me)
    End Sub

    ' ----- Menú Nomencladores -----
    Private Sub MnuNomBco_Click(sender As Object, e As EventArgs) Handles MnuNomBco.Click
        frmBancos.AbrirInstancia(Me)
    End Sub

    Private Sub MnuNomCon_Click(sender As Object, e As EventArgs) Handles MnuNomCon.Click
        frmCondicionVenta.AbrirInstancia(Me)
    End Sub

    Private Sub MnuNomEmp_Click(sender As Object, e As EventArgs) Handles MnuNomEmp.Click
        frmEmpresas.AbrirInstancia(Me)
    End Sub

    Private Sub MnuNomImp_Click(sender As Object, e As EventArgs) Handles MnuNomImp.Click
        frmImpuestos.AbrirInstancia(Me)
    End Sub

    Private Sub MnuNomPro_Click(sender As Object, e As EventArgs) Handles MnuNomPro.Click
        frmProvincias.AbrirInstancia(Me)
    End Sub

    Private Sub mnrubro_Click(sender As Object, e As EventArgs) Handles mnrubro.Click
        frmRubro.AbrirInstancia(Me)
    End Sub

    Private Sub MnuNomSuc_Click(sender As Object, e As EventArgs) Handles MnuNomSuc.Click
        frmSucursales.AbrirInstancia(Me)
    End Sub

    Private Sub MnuNomIva_Click(sender As Object, e As EventArgs) Handles MnuNomIva.Click
        frmIva.AbrirInstancia(Me)
    End Sub

    Private Sub mnnro_Click(sender As Object, e As EventArgs) Handles mnnro.Click
        frmNroComp.AbrirInstancia(Me)
    End Sub

    ' ----- Menú Seguridad -----
    Private Sub MnuSegInf_Click(sender As Object, e As EventArgs) Handles MnuSegInf.Click
        MessageBox.Show("Información Reservada no implementado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MnuSegSes_Click(sender As Object, e As EventArgs) Handles MnuSegSes.Click
        MessageBox.Show("Iniciar Sesión no implementado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MnuSegINI_Click(sender As Object, e As EventArgs) Handles MnuSegINI.Click
        MessageBox.Show("Editar .INI no implementado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ' ----- Menú Ventanas -----
    Private Sub MnuVenCer_Click(sender As Object, e As EventArgs) Handles MnuVenCer.Click
        If Me.ActiveMdiChild IsNot Nothing Then
            Me.ActiveMdiChild.Close()
        End If
    End Sub

    Private Sub MnuVenTod_Click(sender As Object, e As EventArgs) Handles MnuVenTod.Click
        For Each child As Form In Me.MdiChildren
            child.Close()
        Next
    End Sub

    Private Sub MnuVenCas_Click(sender As Object, e As EventArgs) Handles MnuVenCas.Click
        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub MnuVenVer_Click(sender As Object, e As EventArgs) Handles MnuVenVer.Click
        Me.LayoutMdi(MdiLayout.TileVertical)
    End Sub

    Private Sub MnuVenHor_Click(sender As Object, e As EventArgs) Handles MnuVenHor.Click
        Me.LayoutMdi(MdiLayout.TileHorizontal)
    End Sub

    Private Sub MnuVenIco_Click(sender As Object, e As EventArgs) Handles MnuVenIco.Click
        Me.LayoutMdi(MdiLayout.ArrangeIcons)
    End Sub

    Private Sub MnuVenImp_Click(sender As Object, e As EventArgs) Handles MnuVenImp.Click
        If Me.ActiveMdiChild IsNot Nothing Then
            MessageBox.Show("Función de imprimir ventana no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    ' ----- Menú Ayuda -----
    Private Sub MnuAceAce_Click(sender As Object, e As EventArgs) Handles MnuAceAce.Click
        MessageBox.Show("Sistema de Proveedores" & vbCrLf & "Guerrini Neumáticos S.A" & vbCrLf & "Versión: " & Application.ProductVersion, "Acerca de", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MnuAceAyu_Click(sender As Object, e As EventArgs) Handles MnuAceAyu.Click
        MessageBox.Show("Función de ayuda no implementada", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

End Class
