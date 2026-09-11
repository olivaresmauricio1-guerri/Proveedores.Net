Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmTipoServicio
    Private filaActual As DataGridViewRow
    Private filaActualIndice As Integer = -1
    Private Shared instancia As frmTipoServicio

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmTipoServicio()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmTipoServicio_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Public Sub frmTipoServicio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        End If
    End Sub

    Private Sub DgvListado_SelectionChanged(sender As Object, e As EventArgs) Handles DgvListado.SelectionChanged
        AplicarSeleccionActual()
    End Sub

    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles CmdAgregar.Click
        filaActual = Nothing
        filaActualIndice = -1
        FormModoEdicion()
        FormLimpiarSeleccionado()
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        If filaActual Is Nothing Then Return
        FormModoEdicion()
        FormObtenerSeleccionado()
    End Sub

    Private Sub CmdBorrar_Click(sender As Object, e As EventArgs) Handles CmdBorrar.Click
        If filaActual Is Nothing Then Return
        If MessageBox.Show("¿Está seguro de que desea eliminar este tipo de servicio?", "Confirmar borrado",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return

        Dim idTipo = Convert.ToInt32(filaActual.Cells("IdTipoServicio").Value)
        Dim sql = "DELETE FROM TipoServicio WHERE IdTipoServicio = @IdTipoServicio"
        DSM.Execute(DSM.Proveedores, sql, CmdParams("@IdTipoServicio", idTipo), True)

        FormModoConsulta()
        GridBuscar()
    End Sub

    Public Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles cmdAceptar.Click
        Dim descripcion = TxtDescripcion.Text.Trim()
        If String.IsNullOrWhiteSpace(descripcion) Then
            MessageBox.Show("La descripción es obligatoria.")
            TxtDescripcion.Focus()
            Return
        End If

        Dim kmValue As Integer = 0
        If Not String.IsNullOrWhiteSpace(txtKmRecomendado.Text) AndAlso
           Not Integer.TryParse(txtKmRecomendado.Text, kmValue) Then
            MessageBox.Show("El Km recomendado no es válido.")
            txtKmRecomendado.Focus()
            Return
        End If

        If filaActual Is Nothing Then
            Dim sqlExiste = "SELECT COUNT(*) FROM TipoServicio WHERE Descripcion = @Descripcion"
            Dim existe = Convert.ToInt32(DSM.ExecuteQuery(DSM.Proveedores, sqlExiste,
                CmdParams("@Descripcion", descripcion)).Rows(0)(0))
            If existe > 0 Then
                MessageBox.Show("Ya existe un tipo de servicio con esa descripción.")
                TxtDescripcion.Focus()
                Return
            End If

            Dim sql = "INSERT INTO TipoServicio (Descripcion,  KmRecomendado, Activo) " &
                      "VALUES (@Descripcion,  @KmRecomendado, 1)"
            DSM.Execute(DSM.Proveedores, sql, CmdParams(
                "@Descripcion", descripcion,
                "@KmRecomendado", If(kmValue <> 0, CType(kmValue, Object), DBNull.Value)
            ), True)
        Else
            Dim idTipo = Convert.ToInt32(filaActual.Cells("IdTipoServicio").Value)
            Dim sql = "UPDATE TipoServicio SET Descripcion = @Descripcion, " &
                      "KmRecomendado = @KmRecomendado " &
                      "WHERE IdTipoServicio = @IdTipoServicio"
            DSM.Execute(DSM.Proveedores, sql, CmdParams(
                "@Descripcion", descripcion,
                "@KmRecomendado", If(kmValue <> 0, CType(kmValue, Object), DBNull.Value),
                "@IdTipoServicio", idTipo
            ), True)
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
        Dim sql As String = "SELECT IdTipoServicio, Descripcion,  KmRecomendado, Activo FROM TipoServicio WHERE Activo = 1"
        Dim parametros As New List(Of Object)

        If Not String.IsNullOrEmpty(texto) Then
            sql &= " AND (Descripcion LIKE @Descripcion"
            parametros.Add("@Descripcion")
            parametros.Add($"%{texto}%")

            Dim numVal As Integer
            If Integer.TryParse(texto, numVal) Then
                sql &= " OR IdTipoServicio = @Id"
                parametros.Add("@Id")
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

        If DgvListado.Columns.Contains("IdTipoServicio") Then
            DgvListado.Columns("IdTipoServicio").Visible = True
            DgvListado.Columns("IdTipoServicio").HeaderText = "Cód."
            DgvListado.Columns("IdTipoServicio").Width = 50
        End If
        If DgvListado.Columns.Contains("Descripcion") Then
            DgvListado.Columns("Descripcion").Visible = True
            DgvListado.Columns("Descripcion").HeaderText = "Descripción"
            DgvListado.Columns("Descripcion").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End If
        If DgvListado.Columns.Contains("KmRecomendado") Then
            DgvListado.Columns("KmRecomendado").Visible = True
            DgvListado.Columns("KmRecomendado").HeaderText = "Km Rec."
            DgvListado.Columns("KmRecomendado").Width = 90
            DgvListado.Columns("KmRecomendado").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvListado.Columns("KmRecomendado").DefaultCellStyle.Format = "N0"
        End If

        ConfigurarEstiloGrid(DgvListado)
        DgvListado.ReadOnly = True
        DgvListado.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect
    End Sub

    Private Sub FormLimpiarSeleccionado()
        TxtCodigo.Text = String.Empty
        TxtDescripcion.Text = String.Empty
        txtKmRecomendado.Text = String.Empty
    End Sub

    Private Sub FormObtenerSeleccionado()
        If filaActual IsNot Nothing Then
            TxtCodigo.Text = If(filaActual.Cells("IdTipoServicio").Value IsNot DBNull.Value,
                                filaActual.Cells("IdTipoServicio").Value.ToString(), String.Empty)
            TxtDescripcion.Text = If(filaActual.Cells("Descripcion").Value IsNot DBNull.Value,
                                     filaActual.Cells("Descripcion").Value.ToString(), String.Empty)
            Dim km = filaActual.Cells("KmRecomendado").Value
            txtKmRecomendado.Text = If(km IsNot Nothing AndAlso Not IsDBNull(km) AndAlso Convert.ToInt32(km) <> 0,
                                       Convert.ToInt32(km).ToString("N0"), String.Empty)
        End If
    End Sub

    Public Sub FormModoConsulta()
        SetControlesEnabled(True, CmdAgregar, btnModificar, CmdBorrar)
        SetControlesEnabled(False, TxtDescripcion, txtKmRecomendado, cmdAceptar, CmdCancelar)
    End Sub

    Public Sub FormModoEdicion()
        SetControlesEnabled(True, TxtDescripcion, txtKmRecomendado, cmdAceptar, CmdCancelar)
        SetControlesEnabled(False, CmdAgregar, btnModificar, CmdBorrar)
        TxtDescripcion.Focus()
    End Sub

    Private Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles CmdCancelar.Click
        FormModoConsulta()
        FormLimpiarSeleccionado()
        GridBuscar()
    End Sub
End Class
