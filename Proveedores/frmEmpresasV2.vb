Public Class frmEmpresasV2

    Private ReadOnly _service As New EmpresaService()
    Private _seleccionada As Empresa
    Private Shared _instancia As frmEmpresasV2

    ' --- Singleton ---
    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If _instancia Is Nothing OrElse _instancia.IsDisposed Then
            _instancia = New frmEmpresasV2()
            _instancia.MdiParent = mdiParent
        End If
        _instancia.Show()
        _instancia.BringToFront()
        _instancia.Focus()
    End Sub

    Private Sub frmEmpresasV2_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        _instancia = Nothing
    End Sub

    ' --- Carga ---
    Private Sub frmEmpresasV2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ModoConsulta()
        Refrescar()
    End Sub

    ' --- Búsqueda en tiempo real ---
    Private Sub TxtBuscar_TextChanged(sender As Object, e As EventArgs) Handles TxtBuscar.TextChanged
        Refrescar()
    End Sub

    Private Sub Refrescar()
        DgvListado.DataSource = _service.Buscar(TxtBuscar.Text.Trim())
        ConfigurarColumnas()
    End Sub

    ' --- Selección en grilla ---
    Private Sub DgvListado_SelectionChanged(sender As Object, e As EventArgs) Handles DgvListado.SelectionChanged
        If DgvListado.CurrentRow Is Nothing Then Return
        _seleccionada = TryCast(DgvListado.CurrentRow.DataBoundItem, Empresa)
        If _seleccionada IsNot Nothing Then MostrarEnCampos(_seleccionada)
    End Sub

    ' --- Botones ---
    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles CmdAgregar.Click
        _seleccionada = Nothing
        LimpiarCampos()
        ModoEdicion()
        TxtCodigo.Focus()
    End Sub

    Private Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles CmdAceptar.Click
        Dim codigoNum As Short
        Short.TryParse(TxtCodigo.Text.Trim(), codigoNum)

        If _seleccionada Is Nothing Then
            ' ALTA
            If Not String.IsNullOrWhiteSpace(codigoNum) AndAlso _service.CodigoExiste(codigoNum) Then
                MessageBox.Show("Ya existe una empresa con ese código.", "Atención",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TxtCodigo.Focus()
                Return
            End If
            _service.Agregar(New Empresa With {
                .Codigo = codigoNum,
                .Descripcion = TxtDescripcion.Text.Trim(),
                .CUIT = TxtCUIT.Text.Trim(),
                .ConMulti = TxtConMulti.Text.Trim()
            })
        Else
            ' EDICIÓN
            _seleccionada.Codigo = codigoNum
            _seleccionada.Descripcion = TxtDescripcion.Text.Trim()
            _seleccionada.CUIT = TxtCUIT.Text.Trim()
            _seleccionada.ConMulti = TxtConMulti.Text.Trim()
            _service.Actualizar(_seleccionada)
        End If

        ModoConsulta()
        Refrescar()
    End Sub

    Private Sub CmdBorrar_Click(sender As Object, e As EventArgs) Handles CmdBorrar.Click
        If _seleccionada Is Nothing Then Return
        Dim resp = MessageBox.Show("¿Eliminar esta empresa?", "Confirmar",
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp = DialogResult.No Then Return
        _service.Eliminar(_seleccionada.IdEmpresa)
        ModoConsulta()
        Refrescar()
    End Sub

    Private Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles CmdCancelar.Click
        ModoConsulta()
        Refrescar()
        If DgvListado.Rows.Count > 0 Then
            Dim primeraColumnaVisible = DgvListado.Columns.Cast(Of DataGridViewColumn)() _
                                    .FirstOrDefault(Function(c) c.Visible)
            If primeraColumnaVisible IsNot Nothing Then
                DgvListado.CurrentCell = DgvListado.Rows(0).Cells(primeraColumnaVisible.Index)
            End If
        End If
    End Sub

    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Close()
    End Sub

    Private Sub lnkCopiar_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkCopiar.LinkClicked
        CopiarDataGrid(DgvListado, chkEncabezados.Checked)
    End Sub

    ' --- Helpers UI ---
    Private Sub ModoConsulta()
        TxtCodigo.Enabled = False
        TxtDescripcion.Enabled = False
        TxtCUIT.Enabled = False
        TxtConMulti.Enabled = False
        CmdAceptar.Enabled = False
        CmdAgregar.Enabled = True
        CmdBorrar.Enabled = True
    End Sub

    Private Sub ModoEdicion()
        TxtCodigo.Enabled = True
        TxtDescripcion.Enabled = True
        TxtCUIT.Enabled = True
        TxtConMulti.Enabled = True
        CmdAceptar.Enabled = True
        CmdAgregar.Enabled = False
        CmdBorrar.Enabled = False
    End Sub

    Private Sub LimpiarCampos()
        TxtCodigo.Text = String.Empty
        TxtDescripcion.Text = String.Empty
        TxtCUIT.Text = String.Empty
        TxtConMulti.Text = String.Empty
    End Sub

    Private Sub MostrarEnCampos(e As Empresa)
        TxtCodigo.Text = If(e.Codigo = 0, String.Empty, e.Codigo.ToString())
        TxtDescripcion.Text = If(e.Descripcion, String.Empty)
        TxtCUIT.Text = If(e.CUIT, String.Empty)
        TxtConMulti.Text = If(e.ConMulti, String.Empty)
    End Sub

    Private Sub ConfigurarColumnas()
        If DgvListado.Columns.Count = 0 Then Return
        For Each col As DataGridViewColumn In DgvListado.Columns
            col.Visible = False
        Next
        With DgvListado.Columns
            If .Contains("Codigo") Then
                .Item("Codigo").Visible = True
                .Item("Codigo").HeaderText = "Código"
                .Item("Codigo").Width = 60
            End If
            If .Contains("Descripcion") Then
                .Item("Descripcion").Visible = True
                .Item("Descripcion").HeaderText = "Descripción"
                .Item("Descripcion").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End If
            If .Contains("CUIT") Then
                .Item("CUIT").Visible = True
                .Item("CUIT").HeaderText = "CUIT"
                .Item("CUIT").Width = 120
            End If
            If .Contains("ConMulti") Then
                .Item("ConMulti").Visible = True
                .Item("ConMulti").HeaderText = "ConMulti"
                .Item("ConMulti").Width = 80
            End If
        End With
    End Sub

End Class