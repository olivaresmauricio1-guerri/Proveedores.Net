Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmNroComp
    Private tabla As New DataTable()
    Private filaActual As DataGridViewRow
    Private filaActualIndice As Integer = -1
    Private Shared instancia As frmNroComp

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmNroComp()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmNroComp_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Private Sub frmNroComp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            TxtIdComprob.Text = filaActual.Cells("IdComprob").Value?.ToString()
            TxtNroComprob.Text = filaActual.Cells("NroComprob").Value?.ToString()
            TxtDescripcion.Text = filaActual.Cells("Descripcion").Value?.ToString()
            TxtSRC.Text = filaActual.Cells("SRC").Value?.ToString()
            ChkAlCierre.Checked = Convert.ToBoolean(If(filaActual.Cells("AlCierre").Value, False))
            txtImputaCC.Text = filaActual.Cells("ImputaCC").Value?.ToString()
            txtImputaStk.Text = filaActual.Cells("ImputaStk").Value?.ToString()
        End If
    End Sub

    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles CmdAgregar.Click
        FormLimpiarSeleccionado()
        FormModoEdicion()
        TxtIdComprob.Focus()
    End Sub

    Private Sub CmdBorrar_Click(sender As Object, e As EventArgs) Handles CmdBorrar.Click
        If filaActual Is Nothing Then
            Return
        End If
        Dim idStr = filaActual.Cells("IdComprob").Value?.ToString()
        Dim id As Integer
        If Integer.TryParse(idStr, id) Then
            Dim sql = "DELETE FROM NumerosComprobantes WHERE IdComprob = @IdComprob"
            Dim parametros = CmdParams("@IdComprob", id)
            DSM.Execute(DSM.Proveedores, sql, parametros, True)
            FormModoConsulta()
            GridBuscar()
        End If
    End Sub

    Private Sub cmdAceptar_Click(sender As Object, e As EventArgs) Handles cmdAceptar.Click
        Dim idStr = TxtIdComprob.Text.Trim()
        Dim nroStr = TxtNroComprob.Text.Trim()
        Dim descripcion = TxtDescripcion.Text.Trim()
        Dim src = TxtSRC.Text.Trim()
        Dim alCierre = ChkAlCierre.Checked
        Dim imputaStk = txtImputaStk.Text.Trim()
        Dim imputaCC = txtImputaCC.Text.Trim()

        If String.IsNullOrEmpty(idStr) OrElse String.IsNullOrEmpty(nroStr) Then
            TxtIdComprob.Focus()
            Return
        End If
        Dim id As Integer
        Dim nro As Integer
        Dim okId = Integer.TryParse(idStr, id)
        Dim okNro = Integer.TryParse(nroStr, nro)
        If Not okId OrElse Not okNro Then
            TxtIdComprob.Focus()
            Return
        End If

        If filaActual Is Nothing Then
            Dim sqlExiste = "SELECT COUNT(*) FROM NumerosComprobantes WHERE IdComprob = @IdComprob AND NroComprob = @NroComprob"
            Dim t = DSM.ExecuteQuery(DSM.Proveedores, sqlExiste, CmdParams("@IdComprob", id, "@NroComprob", nro))
            Dim existe = Convert.ToInt32(t.Rows(0)(0))
            If existe > 0 Then
                TxtNroComprob.Focus()
                Return
            End If
            Dim sql = "INSERT INTO NumerosComprobantes (IdComprob, NroComprob, Descripcion, SRC, AlCierre, ImputaStk, ImputaCC) VALUES (@IdComprob, @NroComprob, @Descripcion, @SRC, @AlCierre, @ImputaStk, @ImputaCC)"
            Dim p = CmdParams(
                "@IdComprob", id,
                "@NroComprob", nro,
                "@Descripcion", If(String.IsNullOrWhiteSpace(descripcion), DBNull.Value, descripcion),
                "@SRC", If(String.IsNullOrWhiteSpace(src), DBNull.Value, src),
                "@AlCierre", alCierre,
                "@ImputaStk", imputaStk,
                "@ImputaCC", imputaCC
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
        Dim idStr = r.Cells("IdComprob").Value?.ToString()
        Dim nroStr = r.Cells("NroComprob").Value?.ToString()
        Dim descripcion = r.Cells("Descripcion").Value?.ToString()
        Dim src = r.Cells("SRC").Value?.ToString()
        Dim alCierre = Convert.ToBoolean(If(r.Cells("AlCierre").Value, False))
        Dim imputaStk = r.Cells("imputaStk").Value?.ToString()
        Dim imputaCC = r.Cells("imputaCC").Value?.ToString()

        Dim id As Integer
        Dim nro As Integer
        Dim okId = Integer.TryParse(idStr, id)
        Dim okNro = Integer.TryParse(nroStr, nro)

        Dim sql = "UPDATE NumerosComprobantes SET NroComprob = @NroComprob, Descripcion = @Descripcion, SRC = @SRC, AlCierre = @AlCierre, ImputaStk = @ImputaStk, ImputaCC = @ImputaCC WHERE IdComprob = @IdComprob"
        Dim p = CmdParams(
            "@NroComprob", If(okNro, CType(nro, Object), DBNull.Value),
            "@Descripcion", If(String.IsNullOrWhiteSpace(descripcion), DBNull.Value, descripcion),
            "@SRC", If(String.IsNullOrWhiteSpace(src), DBNull.Value, src),
            "@AlCierre", alCierre,
            "@ImputaStk", imputaStk,
            "@ImputaCC", imputaCC,
            "@IdComprob", If(okId, CType(id, Object), DBNull.Value)
        )
        DSM.Execute(DSM.Proveedores, sql, p, True)
    End Sub

    Private Sub lnkCopiar_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkCopiar.LinkClicked
        CopiarDataGrid(DgvListado, chkEncabezados.Checked)
    End Sub

    Private Sub GridBuscar()
        Dim texto = TxtBuscar.Text.Trim()
        Dim sql = "SELECT IdComprob, NroComprob, Descripcion, SRC, AlCierre, ImputaStk, ImputaCC FROM NumerosComprobantes WHERE (@t = '' OR Descripcion LIKE '%' + @t + '%' OR CAST(NroComprob AS VARCHAR(20)) LIKE '%' + @t + '%' OR SRC LIKE '%' + @t + '%') ORDER BY Descripcion"
        tabla = DSM.ExecuteQuery(DSM.Proveedores, sql, CmdParams("@t", texto))
        DgvListado.DataSource = tabla
    End Sub

    Private Sub GridConfigurarColumnas()

        If DgvListado Is Nothing OrElse DgvListado.Columns.Count = 0 Then Return

        For Each col As DataGridViewColumn In DgvListado.Columns
            col.Visible = False
        Next


        If DgvListado.Columns.Contains("IdComprob") Then
            DgvListado.Columns("IdComprob").Visible = True
            DgvListado.Columns("IdComprob").HeaderText = "IdComprob"
            DgvListado.Columns("IdComprob").Width = 60
            DgvListado.Columns("IdComprob").ReadOnly = True
        End If

        If DgvListado.Columns.Contains("NroComprob") Then
            DgvListado.Columns("NroComprob").Visible = True
            DgvListado.Columns("NroComprob").HeaderText = "NroComprob"
            DgvListado.Columns("NroComprob").Width = 100
        End If

        If DgvListado.Columns.Contains("Descripcion") Then
            DgvListado.Columns("Descripcion").Visible = True
            DgvListado.Columns("Descripcion").HeaderText = "Descripción"
            DgvListado.Columns("Descripcion").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End If

        If DgvListado.Columns.Contains("SRC") Then
            DgvListado.Columns("SRC").Visible = True
            DgvListado.Columns("SRC").HeaderText = "SRC"
            DgvListado.Columns("SRC").Width = 100
        End If

        If DgvListado.Columns.Contains("AlCierre") Then
            DgvListado.Columns("AlCierre").Visible = True
            DgvListado.Columns("AlCierre").HeaderText = "AlCierre"
            DgvListado.Columns("AlCierre").Width = 80
        End If

        If DgvListado.Columns.Contains("ImputaStk") Then
            DgvListado.Columns("ImputaStk").Visible = True
            DgvListado.Columns("ImputaStk").HeaderText = "ImputaStk"
            DgvListado.Columns("ImputaStk").Width = 80
        End If

        If DgvListado.Columns.Contains("ImputaCC") Then
            DgvListado.Columns("ImputaCC").Visible = True
            DgvListado.Columns("ImputaCC").HeaderText = "ImputaCC"
            DgvListado.Columns("ImputaCC").Width = 80
        End If

        ConfigurarEstiloGrid(DgvListado)

        DgvListado.EditMode = DataGridViewEditMode.EditOnF2
        DgvListado.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect

    End Sub

    Private Sub FormModoConsulta()
        SetControlesEnabled(False, TxtIdComprob, TxtNroComprob, TxtDescripcion, TxtSRC, ChkAlCierre, txtImputaStk, txtImputaCC, cmdAceptar, CmdCancelar)
        SetControlesEnabled(True, CmdAgregar, CmdBorrar)
    End Sub

    Private Sub FormModoEdicion()
        SetControlesEnabled(True, TxtIdComprob, TxtNroComprob, TxtDescripcion, TxtSRC, ChkAlCierre, txtImputaStk, txtImputaCC, cmdAceptar, CmdCancelar)
        SetControlesEnabled(False, CmdAgregar, CmdBorrar)
    End Sub

    Private Sub FormLimpiarSeleccionado()
        filaActualIndice = -1
        filaActual = Nothing
        TxtIdComprob.Text = ""
        TxtNroComprob.Text = ""
        TxtDescripcion.Text = ""
        TxtSRC.Text = ""
        ChkAlCierre.Checked = False
        txtImputaCC.Text = ""
        txtImputaStk.Text = ""
    End Sub
End Class