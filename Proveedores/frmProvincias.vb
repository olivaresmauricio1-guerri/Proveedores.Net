Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmProvincias
    Private tabla As New DataTable()
    Private filaActual As DataGridViewRow
    Private filaActualIndice As Integer = -1
    Private Shared instancia As frmProvincias

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmProvincias()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmProvincias_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Public Sub frmProvincias_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FormModoConsulta()
        GridBuscar()
        GridConfigurarColumnas()
        Me.KeyPreview = True
    End Sub

    Private Sub TxtBuscar_TextChanged(sender As Object, e As EventArgs) Handles TxtBuscar.TextChanged
        FormModoConsulta()
        FormLimpiarSeleccionado()
        GridBuscar()
    End Sub

    Private Sub DgvListado_KeyDown(sender As Object, e As KeyEventArgs) Handles DgvListado.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.C Then
            CopiarDataGrid(DgvListado, chkEncabezados.Checked)
            e.Handled = True
        End If
    End Sub

    Private Sub DgvListado_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvListado.CellClick
        If e.RowIndex < 0 Then
            filaActualIndice = -1
            filaActual = Nothing
            FormLimpiarSeleccionado()
            Return
        End If
        'AplicarSeleccionActual()
    End Sub

    Private Sub DgvListado_SelectionChanged(sender As Object, e As EventArgs) Handles DgvListado.SelectionChanged
        AplicarSeleccionActual()
    End Sub

    Private Sub chkEncabezados_CheckedChanged(sender As Object, e As EventArgs) Handles chkEncabezados.CheckedChanged
        DgvListado.Focus()
    End Sub

    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles CmdAgregar.Click
        filaActual = Nothing
        filaActualIndice = -1
        FormModoEdicion()
        FormLimpiarSeleccionado()
    End Sub

    Private Sub CmdBorrar_Click(sender As Object, e As EventArgs) Handles CmdBorrar.Click
        If filaActual Is Nothing Then Return

        If MessageBox.Show("¿Está seguro de que desea eliminar esta provincia?", "Confirmar borrado", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Dim id = Convert.ToInt32(filaActual.Cells("Id").Value)
            Dim sql = "DELETE FROM Provincias WHERE IdProvincia = @IdProvincia"
            Dim parametros = CmdParams("@IdProvincia", id)
            DSM.Execute(DSM.Proveedores, sql, parametros, True)

            FormModoConsulta()
            GridBuscar()
        End If
    End Sub

    Public Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles cmdAceptar.Click
        Dim descripcion = TxtDescripcion.Text.Trim()
        Dim codigoConv = TxtCodigoConv.Text.Trim()

        If filaActual Is Nothing Then
            Dim sql = "INSERT INTO Provincias (Descripcion, CodigoConv) VALUES (@Descripcion, @CodigoConv)"
            Dim parametros = CmdParams(
                "@Descripcion", If(String.IsNullOrWhiteSpace(descripcion), DBNull.Value, descripcion),
                "@CodigoConv", If(String.IsNullOrWhiteSpace(codigoConv), DBNull.Value, codigoConv)
            )
            DSM.Execute(DSM.Proveedores, sql, parametros, True)
        End If

        FormModoConsulta()
        GridBuscar()
    End Sub

    Private Sub DgvListado_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DgvListado.CellEndEdit
        Dim row = DgvListado.Rows(e.RowIndex)
        Dim idStr = row.Cells("Id").Value?.ToString().Trim()
        Dim descripcionStr = row.Cells("Descripcion").Value?.ToString().Trim()
        Dim codigoConvStr = row.Cells("CodigoConv").Value?.ToString().Trim()

        Dim idVal As Integer
        If Not Integer.TryParse(idStr, idVal) Then Return

        Dim sql = "UPDATE Provincias SET Descripcion = @Descripcion, CodigoConv = @CodigoConv WHERE IdProvincia = @IdProvincia"
        Dim parametros = CmdParams(
            "@Descripcion", If(String.IsNullOrWhiteSpace(descripcionStr), DBNull.Value, descripcionStr),
            "@CodigoConv", If(String.IsNullOrWhiteSpace(codigoConvStr), DBNull.Value, codigoConvStr),
            "@IdProvincia", idVal
        )
        DSM.Execute(DSM.Proveedores, sql, parametros, True)
    End Sub

    Public Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Close()
    End Sub

    Private Sub lnkCopiar_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkCopiar.LinkClicked
        CopiarDataGrid(DgvListado, chkEncabezados.Checked)
    End Sub

    Private Sub GridBuscar()
        Dim texto As String = TxtBuscar.Text.Trim()
        Dim sql As String = "SELECT IdProvincia AS Id, Descripcion, CodigoConv FROM Provincias"

        Dim parametros As New List(Of Object)

        If Not String.IsNullOrEmpty(texto) Then
            sql &= " WHERE (Descripcion LIKE @Descripcion"
            parametros.Add("@Descripcion")
            parametros.Add($"%{texto}%")

            Dim numVal As Integer
            If Integer.TryParse(texto, numVal) Then
                sql &= " OR IdProvincia = @IdProvincia"
                parametros.Add("@IdProvincia")
                parametros.Add(numVal)
            End If
            sql &= ")"
        End If

        sql &= " ORDER BY IdProvincia"

        Dim dt = DSM.ExecuteQuery(DSM.Proveedores, sql, CmdParams(parametros.ToArray()))
        DgvListado.DataSource = dt

        If dt.Rows.Count = 0 Then
            filaActualIndice = -1
            filaActual = Nothing
            FormLimpiarSeleccionado()
            Return
        End If

        filaActualIndice = 0
        filaActual = DgvListado.Rows(0)
        FormObtenerSeleccionado()
    End Sub

    Private Sub AplicarSeleccionActual()
        If DgvListado Is Nothing OrElse DgvListado.CurrentRow Is Nothing Then Return

        If DgvListado.SelectedRows.Count > 1 Then
            filaActualIndice = -1
            filaActual = Nothing
            FormLimpiarSeleccionado()
            Return
        End If

        Dim idx = DgvListado.CurrentRow.Index
        If idx < 0 OrElse idx = filaActualIndice Then Return

        FormModoConsulta()
        FormLimpiarSeleccionado()

        filaActualIndice = idx
        filaActual = DgvListado.CurrentRow
        FormObtenerSeleccionado()
    End Sub

    Public Sub GridConfigurarColumnas()
        If DgvListado Is Nothing OrElse DgvListado.Columns.Count = 0 Then Return

        For Each col As DataGridViewColumn In DgvListado.Columns
            col.Visible = False
        Next

        If DgvListado.Columns.Contains("Descripcion") Then
            DgvListado.Columns("Descripcion").Visible = True
            DgvListado.Columns("Descripcion").HeaderText = "Descripción"
            DgvListado.Columns("Descripcion").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End If

        If DgvListado.Columns.Contains("CodigoConv") Then
            DgvListado.Columns("CodigoConv").Visible = True
            DgvListado.Columns("CodigoConv").HeaderText = "Código Conv"
            DgvListado.Columns("CodigoConv").Width = 100
        End If

        ConfigurarEstiloGrid(DgvListado)

        DgvListado.EditMode = DataGridViewEditMode.EditOnF2
        DgvListado.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect


    End Sub

    Private Sub FormLimpiarSeleccionado()
        TxtId.Text = String.Empty
        TxtDescripcion.Text = String.Empty
        TxtCodigoConv.Text = String.Empty
    End Sub

    Private Sub FormObtenerSeleccionado()
        If filaActual IsNot Nothing Then
            TxtId.Text = If(filaActual.Cells("Id").Value IsNot DBNull.Value, filaActual.Cells("Id").Value.ToString(), String.Empty)
            TxtDescripcion.Text = If(filaActual.Cells("Descripcion").Value IsNot DBNull.Value, filaActual.Cells("Descripcion").Value.ToString(), String.Empty)
            TxtCodigoConv.Text = If(filaActual.Cells("CodigoConv").Value IsNot DBNull.Value, filaActual.Cells("CodigoConv").Value.ToString(), String.Empty)
        End If
    End Sub

    Public Sub FormModoConsulta()
        SetControlesEnabled(True, CmdAgregar, CmdBorrar)
        SetControlesEnabled(False, TxtDescripcion, TxtCodigoConv, cmdAceptar)
    End Sub

    Public Sub FormModoEdicion()
        SetControlesEnabled(True, TxtDescripcion, TxtCodigoConv, cmdAceptar)
        SetControlesEnabled(False, CmdAgregar, CmdBorrar)
    End Sub

    Private Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles CmdCancelar.Click
        FormModoConsulta()
        FormLimpiarSeleccionado()
        GridBuscar()
    End Sub
End Class