Imports System.Diagnostics
Imports System.Windows.Forms
Imports DSM = DataSourceManager.Lib.DataSourceManager

Partial Public Class MainForm

    ' Todos los formularios (MDI hijos y diálogos) usan el icono del Main; se asigna en un solo lugar.
    Private Sub AplicarIconoFormularios(sender As Object, e As EventArgs)
        For Each f As Form In Application.OpenForms
            If f IsNot Me AndAlso f.Icon IsNot Icon Then f.Icon = Icon
        Next
    End Sub

    Private Sub QuitarIconoFormularios(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        RemoveHandler Application.Idle, AddressOf AplicarIconoFormularios
    End Sub

    Private Sub MainForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler Application.Idle, AddressOf AplicarIconoFormularios
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

        AplicarOpcionesHabilitadas()
    End Sub
    ' Seguridad nueva (igual que CtaCte): cada opción tiene en Tag su código de la tabla Menues
    ' (base Seguridad) y se muestra según Autorizaciones del usuario (ver Permisos.vb).
    Private ReadOnly visibilidadOriginal As New Dictionary(Of ToolStripMenuItem, Boolean)
    Private ReadOnly habilitadoOriginal As New Dictionary(Of ToolStripMenuItem, Boolean)

    Private Sub AplicarOpcionesHabilitadas()
<<<<<<< Updated upstream
        Dim autoriz As String = General.OpcionesHabilitadas
        Dim idx As Integer = 0
        AplicarEnColeccion(MenuStrip1.Items, autoriz, idx)
        MnuNom.Visible = True
        MnuCompras.Visible = True
        SolicitudComprasToolStripMenuItem.Visible = True
        ConsultarComprasToolStripMenuItem.Visible = True
        AutorizarComprasToolStripMenuItem.Visible = True
=======
        Dim errores As New List(Of String)
        AplicarEnColeccion(MenuStrip1.Items, True, errores)
        If errores.Count > 0 Then
            MessageBox.Show("Las siguientes opciones quedaron ocultas por falta de configuracion:" &
                            vbCrLf & String.Join(vbCrLf, errores),
                            "Configuracion de permisos", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
>>>>>>> Stashed changes
    End Sub

    Private Sub AplicarEnColeccion(items As ToolStripItemCollection, padrePermitido As Boolean, errores As List(Of String))
        For Each item As ToolStripItem In items
            If TypeOf item Is ToolStripMenuItem Then
                Dim menuItem = DirectCast(item, ToolStripMenuItem)
                If Not visibilidadOriginal.ContainsKey(menuItem) Then
                    visibilidadOriginal.Add(menuItem, item.Available)
                    habilitadoOriginal.Add(menuItem, item.Enabled)
                End If
                Dim codigo = TryCast(item.Tag, String)
                If String.IsNullOrWhiteSpace(codigo) Then
                    errores.Add(item.Text.Replace("&", "") & ": sin codigo en Tag.")
                ElseIf Not Permisos.ExisteEnCatalogo(codigo) Then
                    errores.Add(item.Text.Replace("&", "") & ": codigo no registrado (" & codigo & ").")
                End If

                ' Las opciones de Ventanas son funciones locales del MDI: disponibles para todos.
                Dim esOpcionVentanas = codigo IsNot Nothing AndAlso codigo.StartsWith("PROVEEDORES_VENTANAS", StringComparison.OrdinalIgnoreCase)
                Dim permitido = padrePermitido AndAlso visibilidadOriginal(menuItem) AndAlso
                    (esOpcionVentanas OrElse Permisos.Autorizado(codigo))
                item.Visible = permitido
                ' Deshabilitar además de ocultar: un ítem oculto pero habilitado sigue respondiendo a su atajo de teclado.
                item.Enabled = permitido AndAlso habilitadoOriginal(menuItem)
                AplicarEnColeccion(menuItem.DropDownItems, permitido AndAlso item.Enabled, errores)
            End If
        Next

        ' Ocultar separadores sobrantes (al principio, al final o duplicados).
        Dim hayOpcion As Boolean = False
        Dim separadorPendiente As ToolStripSeparator = Nothing
        For Each item As ToolStripItem In items
            If TypeOf item Is ToolStripSeparator Then
<<<<<<< Updated upstream
                Continue For
            End If

            If item Is SectoresToolStripMenuItem Then
                ' Sectores se libera temporalmente y no consume una posicion de permisos.
                item.Visible = True
                Continue For
            End If

            If TypeOf item Is ToolStripMenuItem Then
                idx += 1

                Dim habilitado As Boolean = False
                If idx <= autoriz.Length Then
                    habilitado = (autoriz(idx - 1) = "1"c)
                End If

                item.Visible = habilitado

                Dim menuItem = DirectCast(item, ToolStripMenuItem)
                If menuItem.DropDownItems IsNot Nothing AndAlso menuItem.DropDownItems.Count > 0 Then
                    AplicarEnColeccion(menuItem.DropDownItems, autoriz, idx)
=======
                item.Visible = False
                If hayOpcion AndAlso separadorPendiente Is Nothing Then
                    separadorPendiente = DirectCast(item, ToolStripSeparator)
>>>>>>> Stashed changes
                End If
            ElseIf item.Available Then
                If separadorPendiente IsNot Nothing Then separadorPendiente.Visible = True
                separadorPendiente = Nothing
                hayOpcion = True
            End If
        Next
    End Sub
    Private Sub MainForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim cargaOrdenEnProgreso = frmOrdenPago.InstanciaAbierta()
        If cargaOrdenEnProgreso Then
            frmOrdenPago.CerrarYSalir()
            frmOrdenPago.AbrirInstancia(Me)
            e.Cancel = True
            Return
        End If
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
        frmControlCaja.AbrirInstancia(Me)
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
        frmListadoNovedades.AbrirInstancia(Me)
    End Sub

    Private Sub mnpaga_Click(sender As Object, e As EventArgs) Handles mnpaga.Click
        MessageBox.Show("Pago a Proveedores Galicia no implementado", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub mnopagox_Click(sender As Object, e As EventArgs) Handles mnopagox.Click
        frmOrdenPago.AbrirInstancia(Me)
    End Sub

    Private Sub mncandjdjs_Click(sender As Object, e As EventArgs) Handles mncandjdjs.Click
        frmCancelaFactura.AbrirInstancia(Me)
    End Sub

    Private Sub MnuStoAct_Click(sender As Object, e As EventArgs) Handles MnuStoAct.Click
        frmActualiza.AbrirInstancia(Me)
    End Sub

    Private Sub MnuStoCie_Click(sender As Object, e As EventArgs) Handles MnuStoCie.Click
        frmCierreMes.AbrirInstancia(Me)
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
        frmSubDiario.AbrirInstancia(Me)
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

    Private Sub EmpresasV2ToolStripMenuItem_Click(sender As Object, e As EventArgs)
        frmEmpresasV2.AbrirInstancia(Me)
    End Sub

    Private Sub MenuStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub

    Private Sub ComprasInteligentesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ComprasInteligentesToolStripMenuItem.Click
        Dim frm As New frmComprasInteligentes()
        frm.Show()
    End Sub

    Private Sub MnuVehVehiculos_Click(sender As Object, e As EventArgs) Handles MnuVehVehiculos.Click
        frmVehiculos.AbrirInstancia(Me)
    End Sub

    Private Sub MnuVehTipos_Click(sender As Object, e As EventArgs) Handles MnuVehTipos.Click
        frmTipoServicio.AbrirInstancia(Me)
    End Sub

    Private Sub MnuVehRepuestos_Click(sender As Object, e As EventArgs) Handles MnuVehRepuestos.Click
        frmRepuestos.AbrirInstancia(Me)
    End Sub

    Private Sub MnuVehServicios_Click(sender As Object, e As EventArgs) Handles MnuVehServicios.Click
        frmServicios.AbrirInstancia(Me)
    End Sub

    Private Sub SectoresToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SectoresToolStripMenuItem.Click
        frmSectores.AbrirInstancia(Me)
    End Sub

    Private Sub SolicitudComprasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SolicitudComprasToolStripMenuItem.Click
        frmSolicitudCompraUso.AbrirInstancia(Me)
    End Sub

    Private Sub ConsultarComprasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConsultarComprasToolStripMenuItem.Click
        frmConsultaComprasUso.AbrirInstancia(Me)
    End Sub

    Private Sub AutorizarComprasToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AutorizarComprasToolStripMenuItem.Click
        frmAutorizarComprasUso.AbrirInstancia(Me)
    End Sub
End Class
