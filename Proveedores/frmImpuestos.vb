Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmImpuestos
    Private tabla As New DataTable()
    Private filaActual As DataGridViewRow
    Private filaActualIndice As Integer = -1
    Private Shared instancia As frmImpuestos

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmImpuestos()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmImpuestos_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Public Sub frmImpuestos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

        If MessageBox.Show("¿Está seguro de que desea eliminar este impuesto?", "Confirmar borrado", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Dim id = Convert.ToInt32(filaActual.Cells("Id").Value)
            Dim sql = "DELETE FROM PorcentajeImpuestos WHERE IdImpuesto = @IdImpuesto"
            Dim parametros = CmdParams("@IdImpuesto", id)
            DSM.Execute(DSM.Proveedores, sql, parametros, True)

            FormModoConsulta()
            GridBuscar()
        End If
    End Sub

    Public Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles cmdAceptar.Click
        Dim nroStr = TxtNroImpuesto.Text.Trim()
        Dim porStr = TxtPorcentaje.Text.Trim()
        Dim descripcion = TxtDescripcion.Text.Trim()

        Dim nroVal As Integer
        Dim porVal As Decimal
        Dim tieneNro = Integer.TryParse(nroStr, nroVal)
        Decimal.TryParse(porStr, porVal)

        If filaActual Is Nothing Then
            If tieneNro Then
                Dim sqlExiste = "SELECT COUNT(*) FROM PorcentajeImpuestos WHERE NroImpuesto = @NroImpuesto"
                Dim tablaExiste = DSM.ExecuteQuery(DSM.Proveedores, sqlExiste, CmdParams("@NroImpuesto", nroVal))
                Dim existe = Convert.ToInt32(tablaExiste.Rows(0)(0))
                If existe > 0 Then
                    MessageBox.Show("Ya existe un registro con ese NroImpuesto.")
                    TxtNroImpuesto.Focus()
                    Return
                End If
            End If

            Dim sql = "INSERT INTO PorcentajeImpuestos (NroImpuesto, Porcentaje, Descripcion) VALUES (@NroImpuesto, @Porcentaje, @Descripcion)"
            Dim parametros = CmdParams(
                "@NroImpuesto", If(tieneNro, CType(nroVal, Object), DBNull.Value),
                "@Porcentaje", porVal,
                "@Descripcion", If(String.IsNullOrWhiteSpace(descripcion), DBNull.Value, descripcion)
            )
            DSM.Execute(DSM.Proveedores, sql, parametros, True)
        End If

        FormModoConsulta()
        GridBuscar()
    End Sub

    Private Sub DgvListado_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DgvListado.CellEndEdit
        Dim row = DgvListado.Rows(e.RowIndex)
        Dim idStr = row.Cells("Id").Value?.ToString().Trim()
        Dim nroStr = row.Cells("NroImpuesto").Value?.ToString().Trim()
        Dim porStr = row.Cells("Porcentaje").Value?.ToString().Trim()
        Dim descripcionStr = row.Cells("Descripcion").Value?.ToString().Trim()

        Dim idVal As Integer
        If Not Integer.TryParse(idStr, idVal) Then Return

        Dim nroVal As Integer
        Integer.TryParse(nroStr, nroVal)
        Dim porVal As Decimal
        Decimal.TryParse(porStr, porVal)

        Dim sql = "UPDATE PorcentajeImpuestos SET NroImpuesto = @NroImpuesto, Porcentaje = @Porcentaje, Descripcion = @Descripcion WHERE IdImpuesto = @IdImpuesto"
        Dim parametros = CmdParams(
            "@NroImpuesto", If(nroStr Is Nothing OrElse nroStr = "", DBNull.Value, nroVal),
            "@Porcentaje", porVal,
            "@Descripcion", If(String.IsNullOrWhiteSpace(descripcionStr), DBNull.Value, descripcionStr),
            "@IdImpuesto", idVal
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
        Dim sql As String = "SELECT IdImpuesto AS Id, NroImpuesto, Porcentaje, Descripcion FROM PorcentajeImpuestos"

        Dim parametros As New List(Of Object)

        If Not String.IsNullOrEmpty(texto) Then
            sql &= " WHERE (Descripcion LIKE @Descripcion"
            parametros.Add("@Descripcion")
            parametros.Add($"%{texto}%")

            Dim numVal As Integer
            If Integer.TryParse(texto, numVal) Then
                sql &= " OR NroImpuesto = @NroImpuesto OR IdImpuesto = @IdImpuesto"
                parametros.Add("@NroImpuesto")
                parametros.Add(numVal)
                parametros.Add("@IdImpuesto")
                parametros.Add(numVal)
            End If
            sql &= ")"
        End If

        sql &= " ORDER BY IdImpuesto"

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

        If DgvListado.Columns.Contains("NroImpuesto") Then
            DgvListado.Columns("NroImpuesto").Visible = True
            DgvListado.Columns("NroImpuesto").HeaderText = "Nro Impuesto"
            DgvListado.Columns("NroImpuesto").Width = 90
        End If

        If DgvListado.Columns.Contains("Porcentaje") Then
            DgvListado.Columns("Porcentaje").Visible = True
            DgvListado.Columns("Porcentaje").HeaderText = "Porcentaje"
            DgvListado.Columns("Porcentaje").Width = 80
        End If

        If DgvListado.Columns.Contains("Descripcion") Then
            DgvListado.Columns("Descripcion").Visible = True
            DgvListado.Columns("Descripcion").HeaderText = "Descripción"
            DgvListado.Columns("Descripcion").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End If

        ConfigurarEstiloGrid(DgvListado)

        DgvListado.EditMode = DataGridViewEditMode.EditOnF2
        DgvListado.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect
    End Sub

    Private Sub FormLimpiarSeleccionado()
        TxtNroImpuesto.Text = String.Empty
        TxtPorcentaje.Text = String.Empty
        TxtDescripcion.Text = String.Empty
    End Sub

    Private Sub FormObtenerSeleccionado()
        If filaActual IsNot Nothing Then
            TxtNroImpuesto.Text = If(filaActual.Cells("NroImpuesto").Value IsNot DBNull.Value, filaActual.Cells("NroImpuesto").Value.ToString(), String.Empty)
            TxtPorcentaje.Text = If(filaActual.Cells("Porcentaje").Value IsNot DBNull.Value, filaActual.Cells("Porcentaje").Value.ToString(), String.Empty)
            TxtDescripcion.Text = If(filaActual.Cells("Descripcion").Value IsNot DBNull.Value, filaActual.Cells("Descripcion").Value.ToString(), String.Empty)
        End If
    End Sub

    Public Sub FormModoConsulta()
        SetControlesEnabled(True, CmdAgregar, CmdBorrar)
        SetControlesEnabled(False, TxtNroImpuesto, TxtPorcentaje, TxtDescripcion, cmdAceptar)
    End Sub

    Public Sub FormModoEdicion()
        SetControlesEnabled(True, TxtNroImpuesto, TxtPorcentaje, TxtDescripcion, cmdAceptar)
        SetControlesEnabled(False, CmdAgregar, CmdBorrar)
    End Sub

    Private Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles CmdCancelar.Click
        FormModoConsulta()
        FormLimpiarSeleccionado()
        GridBuscar()
    End Sub
End Class