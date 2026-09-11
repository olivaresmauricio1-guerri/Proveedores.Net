Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmRepuestos
    Private filaActual As DataGridViewRow
    Private filaActualIndice As Integer = -1
    Private Shared instancia As frmRepuestos

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmRepuestos()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmRepuestos_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Public Sub frmRepuestos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        If MessageBox.Show("¿Está seguro de que desea eliminar este repuesto?", "Confirmar borrado",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return

        Dim idRepuesto = Convert.ToInt32(filaActual.Cells("IdRepuesto").Value)
        Dim sql = "DELETE FROM Repuestos WHERE IdRepuesto = @IdRepuesto"
        DSM.Execute(DSM.Proveedores, sql, CmdParams("@IdRepuesto", idRepuesto), True)

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


        Dim codigo = TxtCodigo.Text.Trim()
        Dim marca = txtMarcaArticulo.Text.Trim()

        If filaActual Is Nothing Then
            If Not String.IsNullOrWhiteSpace(codigo) Then
                Dim sqlExiste = "SELECT COUNT(*) FROM Repuestos WHERE Codigo = @Codigo"
                Dim existe = Convert.ToInt32(DSM.ExecuteQuery(DSM.Proveedores, sqlExiste,
                    CmdParams("@Codigo", codigo)).Rows(0)(0))
                If existe > 0 Then
                    MessageBox.Show("Ya existe un repuesto con ese código.")
                    TxtCodigo.Focus()
                    Return
                End If
            End If

            Dim sql = "INSERT INTO Repuestos (Codigo, Descripcion,  MarcaArticulo,  Activo) " &
                      "VALUES (@Codigo, @Descripcion, @MarcaArticulo, 1)"
            DSM.Execute(DSM.Proveedores, sql, CmdParams(
                "@Codigo", If(String.IsNullOrWhiteSpace(codigo), DBNull.Value, codigo),
                "@Descripcion", descripcion,
                "@MarcaArticulo", If(String.IsNullOrWhiteSpace(marca), DBNull.Value, marca)
            ), True)
        Else
            Dim idRepuesto = Convert.ToInt32(filaActual.Cells("IdRepuesto").Value)
            If Not String.IsNullOrWhiteSpace(codigo) Then
                Dim sqlExiste = "SELECT COUNT(*) FROM Repuestos WHERE Codigo = @Codigo AND IdRepuesto <> @Id"
                Dim existe = Convert.ToInt32(DSM.ExecuteQuery(DSM.Proveedores, sqlExiste,
                    CmdParams("@Codigo", codigo, "@Id", idRepuesto)).Rows(0)(0))
                If existe > 0 Then
                    MessageBox.Show("Ya existe otro repuesto con ese código.")
                    TxtCodigo.Focus()
                    Return
                End If
            End If

            Dim sql = "UPDATE Repuestos SET Codigo = @Codigo, Descripcion = @Descripcion, " &
                      "MarcaArticulo = @MarcaArticulo " &
                      "WHERE IdRepuesto = @IdRepuesto"
            DSM.Execute(DSM.Proveedores, sql, CmdParams(
                "@Codigo", If(String.IsNullOrWhiteSpace(codigo), DBNull.Value, codigo),
                "@Descripcion", descripcion,
                "@MarcaArticulo", If(String.IsNullOrWhiteSpace(marca), DBNull.Value, marca),
                "@IdRepuesto", idRepuesto
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
        Dim sql As String = "SELECT IdRepuesto, Codigo, Descripcion,  MarcaArticulo " &
                            " FROM Repuestos WHERE Activo = 1"
        Dim parametros As New List(Of Object)

        If Not String.IsNullOrEmpty(texto) Then
            sql &= " AND (Descripcion LIKE @Descripcion OR Codigo LIKE @Codigo)"
            parametros.Add("@Descripcion")
            parametros.Add($"%{texto}%")

            Dim numVal As Integer
            If Integer.TryParse(texto, numVal) Then
                sql &= " OR IdRepuesto = @Id"
                parametros.Add("@Id")
                parametros.Add(numVal)
            End If
            If Not String.IsNullOrWhiteSpace(texto) Then
                sql &= " OR Codigo LIKE @Codigo"
                parametros.Add("@Codigo")
                parametros.Add($"%{texto}%")
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

        If DgvListado.Columns.Contains("IdRepuesto") Then
            DgvListado.Columns("IdRepuesto").Visible = True
            DgvListado.Columns("IdRepuesto").HeaderText = "Id"
            DgvListado.Columns("IdRepuesto").Width = 50
        End If
        If DgvListado.Columns.Contains("Codigo") Then
            DgvListado.Columns("Codigo").Visible = True
            DgvListado.Columns("Codigo").HeaderText = "Código"
            DgvListado.Columns("Codigo").Width = 100
        End If
        If DgvListado.Columns.Contains("Descripcion") Then
            DgvListado.Columns("Descripcion").Visible = True
            DgvListado.Columns("Descripcion").HeaderText = "Descripción"
            DgvListado.Columns("Descripcion").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End If

        If DgvListado.Columns.Contains("MarcaArticulo") Then
            DgvListado.Columns("MarcaArticulo").Visible = True
            DgvListado.Columns("MarcaArticulo").HeaderText = "Marca"
            DgvListado.Columns("MarcaArticulo").Width = 110
        End If

        ConfigurarEstiloGrid(DgvListado)
        DgvListado.ReadOnly = True
        DgvListado.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect
    End Sub

    Private Sub FormLimpiarSeleccionado()
        TxtCodigo.Text = String.Empty
        TxtDescripcion.Text = String.Empty
        txtMarcaArticulo.Text = String.Empty
    End Sub

    Private Sub FormObtenerSeleccionado()
        If filaActual IsNot Nothing Then
            TxtCodigo.Text = If(filaActual.Cells("Codigo").Value IsNot Nothing AndAlso Not IsDBNull(filaActual.Cells("Codigo").Value),
                                filaActual.Cells("Codigo").Value.ToString(), String.Empty)
            TxtDescripcion.Text = If(filaActual.Cells("Descripcion").Value IsNot DBNull.Value,
                                     filaActual.Cells("Descripcion").Value.ToString(), String.Empty)
            txtMarcaArticulo.Text = If(filaActual.Cells("MarcaArticulo").Value IsNot Nothing AndAlso Not IsDBNull(filaActual.Cells("MarcaArticulo").Value),
                                       filaActual.Cells("MarcaArticulo").Value.ToString(), String.Empty)
        End If
    End Sub

    Public Sub FormModoConsulta()
        SetControlesEnabled(True, CmdAgregar, btnModificar, CmdBorrar)
        SetControlesEnabled(False, TxtCodigo, TxtDescripcion, txtMarcaArticulo, cmdAceptar, CmdCancelar)
    End Sub

    Public Sub FormModoEdicion()
        SetControlesEnabled(True, TxtCodigo, TxtDescripcion, txtMarcaArticulo, cmdAceptar, CmdCancelar)
        SetControlesEnabled(False, CmdAgregar, btnModificar, CmdBorrar)
        TxtDescripcion.Focus()
    End Sub

    Private Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles CmdCancelar.Click
        FormModoConsulta()
        FormLimpiarSeleccionado()
        GridBuscar()
    End Sub
End Class
