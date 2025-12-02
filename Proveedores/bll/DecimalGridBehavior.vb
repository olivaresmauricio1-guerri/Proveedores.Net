Imports System.Globalization
Imports System.Runtime.CompilerServices

Module DecimalGridBehavior

    ' Adjunta por tipo (Decimal/Double/Single). Nada de nombres.
    Public Sub Attach(dgv As DataGridView)
        AddHandler dgv.CellBeginEdit, AddressOf OnCellBeginEdit
        AddHandler dgv.CellParsing, AddressOf OnCellParsing
        AddHandler dgv.EditingControlShowing, AddressOf OnEditingControlShowing
        AddHandler dgv.Disposed, AddressOf OnGridDisposed

        ' Recomendado: asegurar ValueType correcto para cada columna numérica
        For Each col As DataGridViewColumn In dgv.Columns
            If IsNumericColumn(col) AndAlso String.IsNullOrEmpty(col.DefaultCellStyle.Format) Then
                col.DefaultCellStyle.Format = "N2"
            End If
        Next
    End Sub

    Public Sub Detach(dgv As DataGridView)
        RemoveHandler dgv.CellBeginEdit, AddressOf OnCellBeginEdit
        RemoveHandler dgv.CellParsing, AddressOf OnCellParsing
        RemoveHandler dgv.EditingControlShowing, AddressOf OnEditingControlShowing
        RemoveHandler dgv.Disposed, AddressOf OnGridDisposed
    End Sub

    Private Sub OnGridDisposed(sender As Object, e As EventArgs)
        Detach(DirectCast(sender, DataGridView))
    End Sub

    ' ---- 1) Entrar en edición: mostrar texto plano con punto ----
    Private Sub OnCellBeginEdit(sender As Object, e As DataGridViewCellCancelEventArgs)
        Dim dgv = DirectCast(sender, DataGridView)
        Dim col = dgv.Columns(e.ColumnIndex)
        If Not IsNumericColumn(col) Then Exit Sub

        dgv.BeginInvoke(Sub()
                            Dim tb = TryCast(dgv.EditingControl, DataGridViewTextBoxEditingControl)
                            If tb Is Nothing Then Return

                            Dim cur = dgv.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
                            If cur Is Nothing OrElse cur Is DBNull.Value Then
                                tb.Text = ""
                            Else
                                Dim d As Decimal
                                If Decimal.TryParse(cur.ToString(), NumberStyles.Any, CultureInfo.CurrentCulture, d) _
                                   OrElse Decimal.TryParse(cur.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, d) Then
                                    tb.Text = d.ToString("0.################", CultureInfo.InvariantCulture)
                                Else
                                    tb.Text = cur.ToString()
                                End If
                            End If
                            tb.SelectAll()
                        End Sub)
    End Sub

    ' ---- 2) Confirmar: parsear SIEMPRE con InvariantCulture ----
    Private Sub OnCellParsing(sender As Object, e As DataGridViewCellParsingEventArgs)
        Dim dgv = DirectCast(sender, DataGridView)
        Dim col = dgv.Columns(e.ColumnIndex)
        If Not IsNumericColumn(col) Then Exit Sub

        Dim txt = If(e.Value, "").ToString().Trim()
        If txt = "" Then
            e.Value = DBNull.Value
            e.ParsingApplied = True
            Return
        End If

        ' Normalizar coma a punto
        txt = txt.Replace(","c, "."c)

        Dim d As Decimal
        If Decimal.TryParse(txt, NumberStyles.Any, CultureInfo.InvariantCulture, d) Then
            ' Aseguramos tipo Decimal en la celda (evita que el grid la trate como string)
            e.Value = Convert.ChangeType(d, GetTargetNumericType(col))
            e.ParsingApplied = True
        End If
    End Sub

    ' ---- 3) Teclado (opcional pero práctico): permitir dígitos y un solo punto ----
    Private Sub OnEditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs)
        Dim dgv = DirectCast(sender, DataGridView)
        Dim col = dgv.Columns(dgv.CurrentCell.ColumnIndex)
        If Not IsNumericColumn(col) Then Return

        Dim tb = TryCast(e.Control, DataGridViewTextBoxEditingControl)
        If tb Is Nothing Then Return
        RemoveHandler tb.KeyPress, AddressOf DecimalOnly_KeyPress
        AddHandler tb.KeyPress, AddressOf DecimalOnly_KeyPress
    End Sub

    Private Sub DecimalOnly_KeyPress(sender As Object, e As KeyPressEventArgs)
        Dim tb = DirectCast(sender, DataGridViewTextBoxEditingControl)
        If Char.IsControl(e.KeyChar) OrElse Char.IsDigit(e.KeyChar) Then Return
        If e.KeyChar = "."c Then
            If tb.Text.Contains(".") Then e.Handled = True
            Return
        End If
        If e.KeyChar = "-"c Then
            If tb.SelectionStart <> 0 OrElse tb.Text.Contains("-") Then e.Handled = True
            Return
        End If
        e.Handled = True ' bloquea coma y letras
    End Sub

    ' ---- Helpers de tipo ----
    Private Function IsNumericColumn(col As DataGridViewColumn) As Boolean
        Dim t = col.ValueType
        If t Is Nothing Then Return False
        Return t Is GetType(Decimal) OrElse t Is GetType(Double) OrElse t Is GetType(Single)
    End Function

    Private Function GetTargetNumericType(col As DataGridViewColumn) As Type
        ' Respetá el tipo de la columna (Decimal recomendado)
        Dim t = col.ValueType
        If t Is Nothing Then Return GetType(Decimal)
        If t Is GetType(Double) Then Return GetType(Double)
        If t Is GetType(Single) Then Return GetType(Single)
        Return GetType(Decimal)
    End Function

End Module
