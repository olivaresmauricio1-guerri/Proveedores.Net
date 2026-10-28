Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Globalization
Imports System.Windows.Forms

''' <summary>Gráficos nativos de WinForms con datos validados, sin dependencias de gráficos externas.</summary>
Public Class ChatChartControl
    Inherits Control

    Private _datos As ChatChartData
    Private ReadOnly _tooltip As New ToolTip With {.InitialDelay = 200, .AutoPopDelay = 15000}
    Private ReadOnly _zonas As New List(Of Tuple(Of RectangleF, String))
    Private _textoTooltip As String = ""
    Private Shared ReadOnly Colores As Color() = {Color.FromArgb(37, 99, 180), Color.FromArgb(15, 136, 115), Color.FromArgb(207, 113, 33), Color.FromArgb(133, 76, 173)}

    Public Sub New()
        DoubleBuffered = True
        ResizeRedraw = True
        BackColor = Color.White
        Font = New Font("Segoe UI", 9.0F)
        AccessibleName = "Gráfico del resultado de la consulta"
    End Sub

    Public ReadOnly Property AlturaRecomendada As Integer
        Get
            Return If(_datos IsNot Nothing AndAlso _datos.Tipo = "barras", Math.Max(320, _datos.Categorias.Count * 32 + 180), 320)
        End Get
    End Property

    Public Sub Mostrar(datos As ChatChartData)
        _datos = datos
        _zonas.Clear()
        _tooltip.SetToolTip(Me, "")
        Invalidate()
    End Sub

    Public Sub GuardarPng(ruta As String)
        If _datos Is Nothing Then Throw New InvalidOperationException("No hay un gráfico para guardar.")
        Using imagen As New Bitmap(Math.Max(Width, 900), Math.Max(Height, AlturaRecomendada))
            imagen.SetResolution(96, 96)
            Using dibujo = Graphics.FromImage(imagen)
                Dibujar(dibujo, New Rectangle(0, 0, imagen.Width, imagen.Height), False)
            End Using
            imagen.Save(ruta, ImageFormat.Png)
        End Using
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        Dibujar(e.Graphics, ClientRectangle, True)
    End Sub

    Private Sub Dibujar(g As Graphics, espacio As Rectangle, guardarZonas As Boolean)
        g.Clear(Color.White)
        g.SmoothingMode = SmoothingMode.AntiAlias
        If guardarZonas Then _zonas.Clear()
        Using texto As New SolidBrush(Color.FromArgb(40, 48, 61)), tenue As New SolidBrush(Color.FromArgb(98, 109, 124)),
              titulo As New Font("Segoe UI", 13.0F, FontStyle.Bold), pequeno As New Font("Segoe UI", 8.0F),
              normal As New Font("Segoe UI", 9.0F), eje As New Pen(Color.FromArgb(156, 165, 178)), grilla As New Pen(Color.FromArgb(230, 235, 241))
            If _datos Is Nothing Then
                g.DrawString("Pedí un gráfico en el chat después de consultar datos.", normal, tenue, New PointF(24, 24))
                Return
            End If
            g.DrawString(_datos.Titulo, titulo, texto, New RectangleF(20, 12, espacio.Width - 40, 42))
            Dim anchoLeyenda As Single = CSng(Math.Max(110, (espacio.Width - 40) / Math.Max(1, _datos.Series.Count)))
            For i = 0 To _datos.Series.Count - 1
                Using color As New SolidBrush(Colores(i Mod Colores.Length))
                    Dim x = 20 + i * anchoLeyenda
                    g.FillRectangle(color, x, 59, 12, 12)
                    g.DrawString(_datos.Series(i).Nombre, pequeno, texto, New RectangleF(x + 18, 56, anchoLeyenda - 22, 30))
                End Using
            Next
            Dim valores = _datos.Series.SelectMany(Function(s) s.Valores).Where(Function(v) v.HasValue).Select(Function(v) CDbl(v.Value)).ToList()
            If valores.Count = 0 Then Return
            Dim minimo = Math.Min(0, valores.Min())
            Dim maximo = Math.Max(0, valores.Max())
            If maximo = minimo Then maximo = minimo + 1
            Dim horizontal = _datos.Tipo = "barras"
            Dim margen = If(horizontal, 180, 110)
            Dim area As New RectangleF(margen, 100, Math.Max(100, espacio.Width - margen - 32), Math.Max(120, espacio.Height - 190))
            Dim numero As Func(Of Double, String) = Function(v) v.ToString(If(Math.Abs(v) < 10, "N2", "N0"), CultureInfo.CurrentCulture)
            For i = 0 To 5
                Dim valor = minimo + (maximo - minimo) * i / 5
                If horizontal Then
                    Dim x = PosX(valor, minimo, maximo, area)
                    g.DrawLine(grilla, x, area.Top, x, area.Bottom)
                    TextoAlineado(g, numero(valor), pequeno, tenue, New RectangleF(x - 45, area.Bottom + 8, 90, 24), StringAlignment.Center)
                Else
                    Dim y = PosY(valor, minimo, maximo, area)
                    g.DrawLine(grilla, area.Left, y, area.Right, y)
                    TextoAlineado(g, numero(valor), pequeno, tenue, New RectangleF(4, y - 9, margen - 14, 24), StringAlignment.Far)
                End If
            Next
            If horizontal Then
                Dim cero = PosX(0, minimo, maximo, area)
                g.DrawLine(eje, cero, area.Top, cero, area.Bottom)
                DibujarBarras(g, area, minimo, maximo, normal, texto, guardarZonas)
            Else
                Dim cero = PosY(0, minimo, maximo, area)
                g.DrawLine(eje, area.Left, cero, area.Right, cero)
                DibujarVertical(g, area, minimo, maximo, pequeno, texto, guardarZonas)
            End If
            Dim nota = If(_datos.DetalleTruncado,
                $"Detalle limitado: se grafican {_datos.Categorias.Count} filas recibidas; no el total completo de la consulta.",
                "Cada punto representa una fila recibida. Los importes no se agrupan ni se recalculan.")
            g.DrawString(nota, pequeno, tenue, New RectangleF(20, espacio.Height - 34, espacio.Width - 40, 32))
        End Using
    End Sub

    Private Sub DibujarBarras(g As Graphics, area As RectangleF, minimo As Double, maximo As Double, fuente As Font, texto As Brush, zonas As Boolean)
        Dim paso = area.Height / _datos.Categorias.Count
        Dim alto = Math.Min(22, paso * 0.72F / _datos.Series.Count)
        Dim cero = PosX(0, minimo, maximo, area)
        For fila = 0 To _datos.Categorias.Count - 1
            Dim centro = area.Top + paso * (fila + 0.5F)
            TextoAlineado(g, _datos.Categorias(fila), fuente, texto, New RectangleF(12, centro - 13, area.Left - 22, 28), StringAlignment.Far)
            For s = 0 To _datos.Series.Count - 1
                Dim valor = _datos.Series(s).Valores(fila)
                If Not valor.HasValue Then Continue For
                Dim x = PosX(CDbl(valor.Value), minimo, maximo, area)
                Dim y = centro - alto * _datos.Series.Count / 2 + s * alto
                Dim rect As New RectangleF(Math.Min(cero, x), y, Math.Max(2, Math.Abs(x - cero)), Math.Max(2, alto - 2))
                Using color As New SolidBrush(Colores(s Mod Colores.Length))
                    g.FillRectangle(color, rect)
                End Using
                If zonas Then _zonas.Add(Tuple.Create(rect, Etiqueta(fila, s)))
            Next
        Next
    End Sub

    Private Sub DibujarVertical(g As Graphics, area As RectangleF, minimo As Double, maximo As Double, fuente As Font, texto As Brush, zonas As Boolean)
        Dim paso = area.Width / _datos.Categorias.Count
        Dim ancho = Math.Max(1, Math.Min(45, paso * 0.72F / _datos.Series.Count))
        Dim cada = Math.Max(1, CInt(Math.Ceiling(_datos.Categorias.Count / 8.0)))
        For fila = 0 To _datos.Categorias.Count - 1 Step cada
            Dim x = area.Left + paso * (fila + 0.5F)
            TextoAlineado(g, _datos.Categorias(fila), fuente, texto, New RectangleF(x - paso * cada / 2, area.Bottom + 8, paso * cada, 40), StringAlignment.Center)
        Next
        Dim cero = PosY(0, minimo, maximo, area)
        For s = 0 To _datos.Series.Count - 1
            Dim anterior As PointF? = Nothing
            Using color As New SolidBrush(Colores(s Mod Colores.Length)), linea As New Pen(Colores(s Mod Colores.Length), 2.5F)
                For fila = 0 To _datos.Categorias.Count - 1
                    Dim valor = _datos.Series(s).Valores(fila)
                    If Not valor.HasValue Then
                        anterior = Nothing
                        Continue For
                    End If
                    Dim x = area.Left + paso * (fila + 0.5F)
                    Dim y = PosY(CDbl(valor.Value), minimo, maximo, area)
                    If _datos.Tipo = "lineas" Then
                        Dim punto As New PointF(x, y)
                        If anterior.HasValue Then g.DrawLine(linea, anterior.Value, punto)
                        g.FillEllipse(color, x - 3, y - 3, 6, 6)
                        anterior = punto
                        If zonas Then _zonas.Add(Tuple.Create(New RectangleF(x - 8, y - 8, 16, 16), Etiqueta(fila, s)))
                    Else
                        Dim rect As New RectangleF(x - ancho * _datos.Series.Count / 2 + s * ancho, Math.Min(cero, y), Math.Max(1, ancho - 2), Math.Max(2, Math.Abs(cero - y)))
                        g.FillRectangle(color, rect)
                        If zonas Then _zonas.Add(Tuple.Create(rect, Etiqueta(fila, s)))
                    End If
                Next
            End Using
        Next
    End Sub

    Private Function Etiqueta(fila As Integer, serie As Integer) As String
        Return _datos.Categorias(fila) & Environment.NewLine & _datos.Series(serie).Nombre & ": " & _datos.Series(serie).Valores(fila).Value.ToString("#,0.####", CultureInfo.CurrentCulture)
    End Function
    Private Shared Function PosX(valor As Double, minimo As Double, maximo As Double, area As RectangleF) As Single
        Return CSng(area.Left + (valor - minimo) / (maximo - minimo) * area.Width)
    End Function
    Private Shared Function PosY(valor As Double, minimo As Double, maximo As Double, area As RectangleF) As Single
        Return CSng(area.Bottom - (valor - minimo) / (maximo - minimo) * area.Height)
    End Function
    Private Shared Sub TextoAlineado(g As Graphics, texto As String, fuente As Font, color As Brush, area As RectangleF, alineacion As StringAlignment)
        Using formato As New StringFormat With {.Alignment = alineacion, .Trimming = StringTrimming.EllipsisCharacter}
            g.DrawString(texto, fuente, color, area, formato)
        End Using
    End Sub
    Protected Overrides Sub OnMouseMove(e As MouseEventArgs)
        MyBase.OnMouseMove(e)
        Dim zona = _zonas.LastOrDefault(Function(z) z.Item1.Contains(e.Location))
        Dim texto = If(zona?.Item2, "")
        If texto <> _textoTooltip Then
            _textoTooltip = texto
            _tooltip.SetToolTip(Me, texto)
        End If
    End Sub
    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then _tooltip.Dispose()
        MyBase.Dispose(disposing)
    End Sub
End Class
