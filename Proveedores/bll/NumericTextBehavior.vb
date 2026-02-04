Imports System.Globalization
Imports System.Runtime.CompilerServices

Module NumericTextBehavior
    ' Guardamos el valor numérico por TextBox (sin usar Tag)
    Private ReadOnly Values As New Dictionary(Of TextBox, Nullable(Of Decimal))(New RefComparer())

    ' ===== API pública =====
    Public Sub Attach(tb As TextBox, Optional initialValue As Nullable(Of Decimal) = Nothing)
        If Not Values.ContainsKey(tb) Then
            Values(tb) = initialValue
            AddHandler tb.Enter, AddressOf OnEnter
            AddHandler tb.Leave, AddressOf OnLeave
            AddHandler tb.KeyPress, AddressOf OnKeyPress
            AddHandler tb.Disposed, AddressOf OnDisposed
        Else
            Values(tb) = initialValue
        End If
        RenderFormatted(tb)
    End Sub

    Public Sub Detach(tb As TextBox)
        If Not Values.ContainsKey(tb) Then Return
        RemoveHandler tb.Enter, AddressOf OnEnter
        RemoveHandler tb.Leave, AddressOf OnLeave
        RemoveHandler tb.KeyPress, AddressOf OnKeyPress
        RemoveHandler tb.Disposed, AddressOf OnDisposed
        Values.Remove(tb)
    End Sub

    Public Function GetValue(tb As TextBox) As Nullable(Of Decimal)
        Dim v As Nullable(Of Decimal) = Nothing
        If Values.TryGetValue(tb, v) Then Return v
        Return Nothing
    End Function

    Public Sub SetValue(tb As TextBox, value As Nullable(Of Decimal))
        If Not Values.ContainsKey(tb) Then Attach(tb)
        Values(tb) = value
        RenderFormatted(tb)
    End Sub

    ' ===== Handlers =====
    Private Sub OnEnter(sender As Object, e As EventArgs)
        Dim tb = DirectCast(sender, TextBox)
        If Not Values.ContainsKey(tb) Then Return

        Dim v = Values(tb)
        If v.HasValue Then
            tb.Text = v.Value.ToString("0.################", CultureInfo.InvariantCulture) ' edición: texto plano con punto
        Else
            tb.Text = ""
        End If

        tb.BeginInvoke(New Action(Sub() tb.SelectAll()))
    End Sub

    Private Sub OnLeave(sender As Object, e As EventArgs)
        Dim tb = DirectCast(sender, TextBox)
        If Not Values.ContainsKey(tb) Then Return

        Dim txt As String = (If(tb.Text, "")).Trim()   ' <<-- reemplazo del ?? por If(...)
        txt = txt.Replace(","c, "."c)

        If txt = "" Then
            Values(tb) = Nothing
            tb.Text = ""
            Exit Sub
        End If

        Dim d As Decimal
        If Decimal.TryParse(txt, NumberStyles.Any, CultureInfo.InvariantCulture, d) Then
            Values(tb) = d
            RenderFormatted(tb)  ' vuelve a N2 para mostrar
        Else
            ' si no parsea, dejamos el texto tal cual (o acá podés mostrar mensaje)
        End If
    End Sub

    Private Sub OnKeyPress(sender As Object, e As KeyPressEventArgs)
        Dim tb = DirectCast(sender, TextBox)

        If Char.IsControl(e.KeyChar) OrElse Char.IsDigit(e.KeyChar) Then Return

        Dim ch As Char = If(e.KeyChar = ","c, "."c, e.KeyChar) ' normalizar coma → punto

        If ch = "."c Then
            ' permitir un solo punto (salvo que esté seleccionado y lo reemplaces)
            If tb.Text.Contains(".") AndAlso Not (If(tb.SelectedText, "").Contains(".")) Then
                e.Handled = True
            End If
            Return
        End If

        If ch = "-"c Then
            ' sólo al inicio y si no existe ya (salvo que lo estés reemplazando)
            If tb.SelectionStart <> 0 OrElse (tb.Text.Contains("-") AndAlso Not (If(tb.SelectedText, "").Contains("-"))) Then
                e.Handled = True
            End If
            Return
        End If

        e.Handled = True ' bloquear resto
    End Sub

    Private Sub OnDisposed(sender As Object, e As EventArgs)
        Dim tb = DirectCast(sender, TextBox)
        Detach(tb)
    End Sub

    ' ===== Render =====
    Private Sub RenderFormatted(tb As TextBox)
        Dim v As Nullable(Of Decimal) = Nothing
        If Not Values.TryGetValue(tb, v) Then Return

        If v.HasValue Then
            tb.Text = v.Value.ToString("N2") ' vista: N2 según cultura (miles + coma/punto)
        Else
            tb.Text = ""
        End If
    End Sub

    ' Comparador por referencia para usar TextBox como key
    Private NotInheritable Class RefComparer
        Implements IEqualityComparer(Of TextBox)
        Public Overloads Function Equals(x As TextBox, y As TextBox) As Boolean Implements IEqualityComparer(Of TextBox).Equals
            Return ReferenceEquals(x, y)
        End Function
        Public Overloads Function GetHashCode(obj As TextBox) As Integer Implements IEqualityComparer(Of TextBox).GetHashCode
            Return RuntimeHelpers.GetHashCode(obj)
        End Function
    End Class
End Module
