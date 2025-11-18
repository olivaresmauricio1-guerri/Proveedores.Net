Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmIva
    Private tabla As New DataTable()
    Private filaActual As DataGridViewRow
    Private filaActualIndice As Integer = -1
    Private Shared instancia As frmIva

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmIva()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmIva_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Private Sub frmIva_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        If e.RowIndex >= 0 AndAlso e.RowIndex < DgvListado.Rows.Count Then
            filaActualIndice = e.RowIndex
            filaActual = DgvListado.Rows(filaActualIndice)
            TxtCodigo.Text = filaActual.Cells("Codigo").Value?.ToString()
            TxtDescripcion.Text = filaActual.Cells("Descripcion").Value?.ToString()
            TxtIva.Text = filaActual.Cells("Iva").Value?.ToString()
            TxtNoInscripto.Text = filaActual.Cells("Noinscripto").Value?.ToString()
            TxtIBrutos.Text = filaActual.Cells("IBrutos").Value?.ToString()
        End If
    End Sub

    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles CmdAgregar.Click
        FormLimpiarSeleccionado()
        FormModoEdicion()
        TxtCodigo.Focus()
    End Sub

    Private Sub CmdBorrar_Click(sender As Object, e As EventArgs) Handles CmdBorrar.Click
        If filaActual Is Nothing Then
            Return
        End If
        Dim codigoStr = filaActual.Cells("Codigo").Value?.ToString()
        Dim codigo As Integer
        If Integer.TryParse(codigoStr, codigo) Then
            Dim sql = "DELETE FROM TipoIva WHERE Codigo = @Codigo"
            Dim p = CmdParams("@Codigo", codigo)
            DSM.Execute(DSM.Proveedores, sql, p, True)
            FormModoConsulta()
            GridBuscar()
        End If
    End Sub

    Private Sub cmdAceptar_Click(sender As Object, e As EventArgs) Handles cmdAceptar.Click
        Dim codigoStr = TxtCodigo.Text.Trim()
        Dim descripcion = TxtDescripcion.Text.Trim()
        Dim ivaStr = TxtIva.Text.Trim()
        Dim noinscripto = TxtNoInscripto.Text.Trim()
        Dim iBrutos = TxtIBrutos.Text.Trim()

        If String.IsNullOrEmpty(codigoStr) Then
            TxtCodigo.Focus()
            Return
        End If
        Dim codigo As Integer
        Dim okCodigo = Integer.TryParse(codigoStr, codigo)
        If Not okCodigo Then
            TxtCodigo.Focus()
            Return
        End If
        Dim ivaVal As Decimal
        Dim okIva = Decimal.TryParse(ivaStr, ivaVal)

        If filaActual Is Nothing Then
            Dim sqlExiste = "SELECT COUNT(*) FROM TipoIva WHERE Codigo = @Codigo"
            Dim t = DSM.ExecuteQuery(DSM.Proveedores, sqlExiste, CmdParams("@Codigo", codigo))
            Dim existe = Convert.ToInt32(t.Rows(0)(0))
            If existe > 0 Then
                TxtCodigo.Focus()
                Return
            End If
            Dim sql = "INSERT INTO TipoIva (Codigo, Descripcion, Iva, Noinscripto, IBrutos) VALUES (@Codigo, @Descripcion, @Iva, @Noinscripto, @IBrutos)"
            Dim p = CmdParams(
                "@Codigo", codigo,
                "@Descripcion", If(String.IsNullOrWhiteSpace(descripcion), DBNull.Value, descripcion),
                "@Iva", If(okIva, CType(ivaVal, Object), DBNull.Value),
                "@Noinscripto", noinscripto,
                "@IBrutos", iBrutos
            )
            DSM.Execute(DSM.Proveedores, sql, p, True)
        End If

        FormModoConsulta()
        GridBuscar()
    End Sub

    Private Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles CmdCancelar.Click
        FormModoConsulta()
        FormLimpiarSeleccionado()
        GridBuscar()
    End Sub

    Private Sub DgvListado_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DgvListado.CellEndEdit
        Dim r = DgvListado.Rows(e.RowIndex)
        Dim codigoStr = r.Cells("Codigo").Value?.ToString()
        Dim descripcion = r.Cells("Descripcion").Value?.ToString()
        Dim ivaStr = r.Cells("Iva").Value?.ToString()
        Dim noinscripto = Convert.ToBoolean(If(r.Cells("Noinscripto").Value, False))
        Dim iBrutos = Convert.ToBoolean(If(r.Cells("IBrutos").Value, False))

        Dim codigo As Integer
        Dim okCodigo = Integer.TryParse(codigoStr, codigo)
        Dim ivaVal As Decimal
        Dim okIva = Decimal.TryParse(ivaStr, ivaVal)

        Dim sql = "UPDATE TipoIva SET Descripcion = @Descripcion, Iva = @Iva, Noinscripto = @Noinscripto, IBrutos = @IBrutos WHERE Codigo = @Codigo"
        Dim p = CmdParams(
            "@Descripcion", If(String.IsNullOrWhiteSpace(descripcion), DBNull.Value, descripcion),
            "@Iva", If(okIva, CType(ivaVal, Object), DBNull.Value),
            "@Noinscripto", noinscripto,
            "@IBrutos", iBrutos,
            "@Codigo", If(okCodigo, CType(codigo, Object), DBNull.Value)
        )
        DSM.Execute(DSM.Proveedores, sql, p, True)
    End Sub

    Private Sub lnkCopiar_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkCopiar.LinkClicked
        CopiarDataGrid(DgvListado, chkEncabezados.Checked)
    End Sub

    Private Sub GridBuscar()
        Dim texto = TxtBuscar.Text.Trim()
        Dim sql = "SELECT Codigo, Descripcion, Iva, Noinscripto, IBrutos FROM TipoIva WHERE (@t = '' OR Descripcion LIKE '%' + @t + '%' OR CAST(Codigo AS VARCHAR(20)) LIKE '%' + @t + '%') ORDER BY Descripcion"
        tabla = DSM.ExecuteQuery(DSM.Proveedores, sql, CmdParams("@t", texto))
        DgvListado.DataSource = tabla
    End Sub

    Private Sub GridConfigurarColumnas()

        If DgvListado Is Nothing OrElse DgvListado.Columns.Count = 0 Then Return

        For Each col As DataGridViewColumn In DgvListado.Columns
            col.Visible = False
        Next

        If DgvListado.Columns.Contains("Codigo") Then
            DgvListado.Columns("Codigo").Visible = True
            DgvListado.Columns("Codigo").HeaderText = "Código"
            DgvListado.Columns("Codigo").Width = 60
        End If

        If DgvListado.Columns.Contains("Descripcion") Then
            DgvListado.Columns("Descripcion").Visible = True
            DgvListado.Columns("Descripcion").HeaderText = "Descripcion"
            DgvListado.Columns("Descripcion").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End If

        If DgvListado.Columns.Contains("Iva") Then
            DgvListado.Columns("Iva").Visible = True
            DgvListado.Columns("Iva").HeaderText = "IVA"
            DgvListado.Columns("Iva").Width = 80
        End If

        If DgvListado.Columns.Contains("Noinscripto") Then
            DgvListado.Columns("Noinscripto").Visible = True
            DgvListado.Columns("Noinscripto").HeaderText = "No Inscripto"
            DgvListado.Columns("Noinscripto").Width = 60
        End If

        If DgvListado.Columns.Contains("IBrutos") Then
            DgvListado.Columns("IBrutos").Visible = True
            DgvListado.Columns("IBrutos").HeaderText = "IBrutos"
            DgvListado.Columns("IBrutos").Width = 120
        End If

        ConfigurarEstiloGrid(DgvListado)

        DgvListado.EditMode = DataGridViewEditMode.EditOnF2
        DgvListado.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect

    End Sub

    Private Sub FormModoConsulta()
        SetControlesEnabled(False, TxtCodigo, TxtDescripcion, TxtIva, TxtNoInscripto, TxtIBrutos, cmdAceptar, CmdCancelar)
        SetControlesEnabled(True, CmdAgregar, CmdBorrar)
    End Sub

    Private Sub FormModoEdicion()
        SetControlesEnabled(True, TxtCodigo, TxtDescripcion, TxtIva, TxtNoInscripto, TxtIBrutos, cmdAceptar, CmdCancelar)
        SetControlesEnabled(False, CmdAgregar, CmdBorrar)
    End Sub

    Private Sub FormLimpiarSeleccionado()
        filaActualIndice = -1
        filaActual = Nothing
        TxtCodigo.Text = ""
        TxtDescripcion.Text = ""
        TxtIva.Text = ""
        TxtNoInscripto.Text = ""
        TxtIBrutos.Text = ""
    End Sub
End Class