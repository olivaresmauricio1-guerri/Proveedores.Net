Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmRubro
    Private tabla As New DataTable()
    Private filaActual As DataGridViewRow
    Private filaActualIndice As Integer = -1
    Private Shared instancia As frmRubro

    Private Function ObtenerSiguienteCodigo() As Integer
        Dim dt = DSM.ExecuteQuery(DSM.Proveedores, "SELECT ISNULL(MAX(Codigo), 0) + 1 AS Siguiente FROM Rubro", Nothing)
        If dt Is Nothing OrElse dt.Rows.Count = 0 OrElse IsDBNull(dt.Rows(0)("Siguiente")) Then Return 1
        Return Convert.ToInt32(dt.Rows(0)("Siguiente"))
    End Function

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmRubro()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmRubro_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Public Sub frmRubro_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        TxtCodigo.Text = ObtenerSiguienteCodigo().ToString()
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        If filaActual Is Nothing Then Return
        FormModoEdicion()
        FormObtenerSeleccionado()
    End Sub

    Private Sub CmdBorrar_Click(sender As Object, e As EventArgs) Handles CmdBorrar.Click
        If filaActual Is Nothing Then Return

        If MessageBox.Show("¿Está seguro de que desea eliminar este rubro?", "Confirmar borrado", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Dim codigoStr = filaActual.Cells("Codigo").Value?.ToString().Trim()
            Dim sql = "DELETE FROM Rubro WHERE Codigo = @Codigo"
            Dim parametros = CmdParams("@Codigo", codigoStr)
            DSM.Execute(DSM.Proveedores, sql, parametros, True)

            FormModoConsulta()
            GridBuscar()
        End If
    End Sub

    Public Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles cmdAceptar.Click
        Dim codigoStr = TxtCodigo.Text.Trim()
        Dim descripcion = TxtDescripcion.Text.Trim()
        Dim noGasto = ChkNoGasto.Checked

        If String.IsNullOrWhiteSpace(codigoStr) Then
            MessageBox.Show("El código es obligatorio.")
            Return
        End If

        If filaActual Is Nothing Then
            Dim sqlExiste = "SELECT COUNT(*) FROM Rubro WHERE Codigo = @Codigo"
            Dim tablaExiste = DSM.ExecuteQuery(DSM.Proveedores, sqlExiste, CmdParams("@Codigo", codigoStr))
            Dim existe = Convert.ToInt32(tablaExiste.Rows(0)(0))
            If existe > 0 Then
                MessageBox.Show("Ya existe un registro con ese Código.")
                TxtCodigo.Focus()
                Return
            End If

            Dim sql = "INSERT INTO Rubro (Codigo, NoGasto, Descripcion) VALUES (@Codigo, @NoGasto, @Descripcion)"
            Dim parametros = CmdParams(
                "@Codigo", codigoStr,
                "@NoGasto", noGasto,
                "@Descripcion", If(String.IsNullOrWhiteSpace(descripcion), DBNull.Value, descripcion)
            )
            DSM.Execute(DSM.Proveedores, sql, parametros, True)
        Else
            Dim codigoOriginal = filaActual.Cells("Codigo").Value?.ToString().Trim()
            Dim sql = "UPDATE Rubro SET NoGasto = @NoGasto, Descripcion = @Descripcion WHERE Codigo = @Codigo"
            Dim parametros = CmdParams(
                "@NoGasto", noGasto,
                "@Descripcion", If(String.IsNullOrWhiteSpace(descripcion), DBNull.Value, descripcion),
                "@Codigo", codigoOriginal
            )
            DSM.Execute(DSM.Proveedores, sql, parametros, True)
        End If

        FormModoConsulta()
        GridBuscar()
    End Sub

    Public Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Close()
    End Sub

    Private Sub lnkCopiar_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkCopiar.LinkClicked
        CopiarDataGrid(DgvListado, chkEncabezados.Checked)
    End Sub

    Private Sub GridBuscar()
        Dim texto As String = TxtBuscar.Text.Trim()
        Dim sql As String = "SELECT Codigo, NoGasto, Descripcion FROM Rubro"

        Dim parametros As New List(Of Object)

        If Not String.IsNullOrEmpty(texto) Then
            sql &= " WHERE (Descripcion LIKE @Descripcion"
            parametros.Add("@Descripcion")
            parametros.Add($"%{texto}%")

            Dim numVal As Integer
            If Integer.TryParse(texto, numVal) Then
                sql &= " OR Codigo = @Codigo"
                parametros.Add("@Codigo")
                parametros.Add(numVal)
            End If
            sql &= ")"
        End If

        sql &= " ORDER BY Descripcion"

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

        If DgvListado.Columns.Contains("Codigo") Then
            DgvListado.Columns("Codigo").Visible = True
            DgvListado.Columns("Codigo").HeaderText = "Código"
            DgvListado.Columns("Codigo").Width = 80
        End If

        If DgvListado.Columns.Contains("NoGasto") Then
            DgvListado.Columns("NoGasto").Visible = True
            DgvListado.Columns("NoGasto").HeaderText = "NoGasto"
            DgvListado.Columns("NoGasto").Width = 80
        End If

        If DgvListado.Columns.Contains("Descripcion") Then
            DgvListado.Columns("Descripcion").Visible = True
            DgvListado.Columns("Descripcion").HeaderText = "Descripción"
            DgvListado.Columns("Descripcion").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End If

        ConfigurarEstiloGrid(DgvListado)
        DgvListado.ReadOnly = True
        DgvListado.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect

    End Sub

    Private Sub FormLimpiarSeleccionado()
        TxtCodigo.Text = String.Empty
        ChkNoGasto.Checked = False
        TxtDescripcion.Text = String.Empty
    End Sub

    Private Sub FormObtenerSeleccionado()
        If filaActual IsNot Nothing Then
            TxtCodigo.Text = If(filaActual.Cells("Codigo").Value IsNot DBNull.Value, filaActual.Cells("Codigo").Value.ToString(), String.Empty)
            Dim ng = filaActual.Cells("NoGasto").Value
            ChkNoGasto.Checked = If(ng IsNot Nothing AndAlso Not IsDBNull(ng), Convert.ToBoolean(ng), False)
            TxtDescripcion.Text = If(filaActual.Cells("Descripcion").Value IsNot DBNull.Value, filaActual.Cells("Descripcion").Value.ToString(), String.Empty)
        End If
    End Sub

    Public Sub FormModoConsulta()
        SetControlesEnabled(True, CmdAgregar, btnModificar, CmdBorrar)
        SetControlesEnabled(False, TxtCodigo, ChkNoGasto, TxtDescripcion, cmdAceptar, CmdCancelar)
    End Sub

    Public Sub FormModoEdicion()
        SetControlesEnabled(True, ChkNoGasto, TxtDescripcion, cmdAceptar, CmdCancelar)
        SetControlesEnabled(False, TxtCodigo, CmdAgregar, btnModificar, CmdBorrar)
    End Sub

    Private Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles CmdCancelar.Click
        FormModoConsulta()
        FormLimpiarSeleccionado()
        GridBuscar()
    End Sub
End Class
