Public Class frmSubDiarioV2
    Private ReadOnly _service As New SubDiarioService()
    Private Shared _instancia As frmSubDiarioV2

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If _instancia Is Nothing OrElse _instancia.IsDisposed Then
            _instancia = New frmSubDiarioV2()
            _instancia.MdiParent = mdiParent
        End If
        _instancia.Show()
        _instancia.BringToFront()
        _instancia.Focus()
    End Sub

    Private Sub frmSubDiarioV2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtAno.Text = DateTime.Now.Year.ToString()
        GroupBoxCierre.Enabled = _service.EsUsuarioAutorizado()
        CargarCombos()
        Dim fechaCierre = _service.ObtenerFechaCierreIva()
        If fechaCierre.HasValue Then dtFecha.Value = fechaCierre.Value
        dtFecha_ValueChanged(Nothing, Nothing)
        DeshabilitarBotonesArchivo()
    End Sub

    Private Sub frmSubDiarioV2_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        If dbcMeses.SelectedValue IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(txtAno.Text) Then
            _service.LimpiarAlCerrar(
                Convert.ToInt32(dbcMeses.SelectedValue),
                Convert.ToInt32(txtAno.Text))
        End If
        _instancia = Nothing
    End Sub

    Private Sub CargarCombos()
        General.CargarCombos(dbcEmpresa, "Empresas", "Descripcion", "Descripcion", "IdEmpresa")
        General.CargarCombos(dbcSucursal, "Sucursales", "Descripcion", "Descripcion", "IdSucursal")
        General.CargarCombos(dbcMeses, "Meses", "IdMes", "Mes", "IdMes")
    End Sub

    Private Sub dtFecha_ValueChanged(sender As Object, e As EventArgs) Handles dtFecha.ValueChanged
        txtFecha.Text = dtFecha.Value.ToString("dd/MM/yyyy")
    End Sub

    Private Sub CmdOKCierre_Click(sender As Object, e As EventArgs) Handles CmdOKCierre.Click
        GroupBoxCierre.Enabled = _service.EsUsuarioAutorizado()
        If Not GroupBoxCierre.Enabled Then
            MessageBox.Show("No autorizado para cambiar la fecha.")
        End If
    End Sub

    Private Sub CmdArmaArchivo_Click(sender As Object, e As EventArgs) Handles CmdArmaArchivo.Click
        If Not ValidarParametrosBasicos() Then Return
        _service.PrepararMes(
            Convert.ToInt32(dbcMeses.SelectedValue),
            Convert.ToInt32(txtAno.Text))
        If _service.HayDuplicados() Then
            MessageBox.Show("Cuidado: Registros Duplicados")
        End If
        HabilitarBotonesArchivo()
        MessageBox.Show("Datos del mes preparados.")
    End Sub

    Private Sub cmdVer_Click(sender As Object, e As EventArgs) Handles cmdVer.Click
        If Not ValidarParametrosCompletos() Then Return
        Dim params = ObtenerParametros()
        _service.PrepararCabecera(params)
        Dim criterio = "({wDetaCtaCte.IdImputacion} = 1 or {wDetaCtaCte.IdImputacion} = 11 or {wDetaCtaCte.IdImputacion} = 6 or {wDetaCtaCte.IdImputacion} = 59 or {wDetaCtaCte.IdImputacion} = 2) And {wDetaCtaCte.NroCuenta} <> 8100"
        Try
            Process.Start(General.ReportesPath, "Proveedores subdiarioProve RecordSelectionFormula """ & criterio & """")
        Catch ex As Exception
            MessageBox.Show("No se pudo abrir el reporte: " & ex.Message)
        End Try
    End Sub

    Private Sub CmdDecreto_Click(sender As Object, e As EventArgs) Handles CmdDecreto.Click
        If Not ValidarMesAnio() Then Return
        Dim dt = _service.ObtenerDatosDecreto()
        _service.GenerarArchivoDecreto(dt, txtAno.Text, dbcMeses.Text)
        MessageBox.Show("Archivo generado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub CmdLibroElectronico_Click(sender As Object, e As EventArgs) Handles CmdLibroElectronico.Click
        If Not ValidarMesAnio() Then Return
        Dim mes = Convert.ToInt32(dbcMeses.SelectedValue)
        Dim anio = Convert.ToInt32(txtAno.Text)
        Dim mesTxt = mes.ToString().PadLeft(2, "0"c)
        Dim dt = _service.ObtenerDatosLibroElectronico(mes, anio)
        _service.GenerarArchivoLibroElectronico(dt, txtAno.Text, mesTxt)
        MessageBox.Show("Archivo generado correctamente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub CmdSIFERE_Click(sender As Object, e As EventArgs) Handles CmdSIFERE.Click
        frmSifere.AbrirInstancia(Me.MdiParent)
    End Sub

    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Close()
    End Sub

    ' ─── Helpers UI ──────────────────────────────────────────────────
    Private Sub HabilitarBotonesArchivo()
        cmdVer.Enabled = True
        CmdDecreto.Enabled = True
        CmdLibroElectronico.Enabled = True
        CmdSIFERE.Enabled = True
    End Sub

    Private Sub DeshabilitarBotonesArchivo()
        cmdVer.Enabled = False
        CmdDecreto.Enabled = False
        CmdLibroElectronico.Enabled = False
        CmdSIFERE.Enabled = False
    End Sub

    Private Function ObtenerParametros() As SubDiarioParametros
        Return New SubDiarioParametros With {
            .Mes = Convert.ToInt32(dbcMeses.SelectedValue),
            .Anio = Convert.ToInt32(txtAno.Text),
            .NroLibro = txtNro.Text.Trim(),
            .EmpresaDescripcion = dbcEmpresa.Text,
            .SucursalDescripcion = dbcSucursal.Text
        }
    End Function

    Private Function ValidarParametrosBasicos() As Boolean
        If dbcMeses.SelectedIndex < 0 Then
            MessageBox.Show("Debe indicar un Mes.") : Return False
        End If
        If String.IsNullOrWhiteSpace(txtAno.Text) Then
            MessageBox.Show("Debe indicar un Año.") : Return False
        End If
        Return True
    End Function

    Private Function ValidarParametrosCompletos() As Boolean
        If String.IsNullOrWhiteSpace(txtNro.Text) Then
            MessageBox.Show("Debe indicar un Número de Libro.") : Return False
        End If
        If dbcEmpresa.SelectedIndex < 0 Then
            MessageBox.Show("Debe indicar una Empresa.") : Return False
        End If
        If dbcSucursal.SelectedIndex < 0 Then
            MessageBox.Show("Debe indicar una Sucursal.") : Return False
        End If
        Return ValidarParametrosBasicos()
    End Function

    Private Function ValidarMesAnio() As Boolean
        If dbcMeses.SelectedIndex < 0 Then
            MessageBox.Show("Debe indicar un Mes.") : Return False
        End If
        If String.IsNullOrWhiteSpace(txtAno.Text) Then
            MessageBox.Show("Debe indicar un Año.") : Return False
        End If
        Return True
    End Function

End Class