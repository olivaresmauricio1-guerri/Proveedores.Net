Imports DSM = DataSourceManager.Lib.DataSourceManager
Imports System.Text
Imports System.IO
Imports System.Globalization

Public Class frmSifere
    Private Shared instancia As frmSifere
    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmSifere()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub
    Private Sub frmsifere_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub
    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        Cursor = Cursors.WaitCursor
        Try
            DSM.Execute(DSM.Proveedores, "Delete from sifere;", Nothing, True)

            Dim miSql As String = "INSERT INTO sifere ( NroComprobante, IdImputacion, Fecha, Brutos, Cuit, Juri, cbu ) "
            miSql &= "SELECT WDetaCtaCte.NroFactura, WDetaCtaCte.IdImputacion, WDetaCtaCte.Fecha, "
            miSql &= "WDetaCtaCte.IngresosB, MaeCtaCte.Cuit, Provincias.CodigoConv, MaeCtaCte.cbu "
            miSql &= "FROM (WDetaCtaCte INNER JOIN MaeCtaCte ON WDetaCtaCte.NroCuenta = MaeCtaCte.NroCuenta) "
            miSql &= "INNER JOIN Provincias ON MaeCtaCte.Provincia = Provincias.Descripcion "
            miSql &= "WHERE (WDetaCtaCte.IngresosB)<>0;"
            DSM.Execute(DSM.Proveedores, miSql, Nothing, True)

            Dim dt As DataTable = DSM.ExecuteQuery(DSM.Proveedores, "SELECT * from SIFERE;", Nothing)

            Dim outputDir = "c:\citi\"
            Directory.CreateDirectory(outputDir)
            Dim outputPath = Path.Combine(outputDir, "sifere.txt")

            Using fs As New FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None)
                For Each row As DataRow In dt.Rows
                    Dim reg As String = New String(" "c, 107)

                    Dim juri As String = Convert.ToString(row("juri"))
                    If juri Is Nothing Then juri = ""
                    juri = juri.Trim()
                    If juri.Length > 3 Then juri = juri.Substring(0, 3)
                    Mid(reg, 1, 3) = juri.PadRight(3, " "c)

                    Dim cuit As String = Convert.ToString(row("cuit"))
                    If cuit Is Nothing Then cuit = ""
                    cuit = cuit.Trim()
                    If cuit.Length > 13 Then cuit = cuit.Substring(0, 13)
                    Mid(reg, 4, 13) = cuit.PadRight(13, " "c)

                    Dim fechaStr As String
                    If row.IsNull("Fecha") Then
                        fechaStr = ""
                    Else
                        Dim fecha As DateTime = Convert.ToDateTime(row("Fecha"), CultureInfo.CurrentCulture)
                        fechaStr = fecha.ToString("dd/MM/yyyy")
                    End If
                    If fechaStr.Length > 10 Then fechaStr = fechaStr.Substring(0, 10)
                    Mid(reg, 18, 10) = fechaStr.PadRight(10, " "c)

                    Mid(reg, 28, 4) = "0001"

                    Dim nroComprobante As String = Convert.ToString(row("NroComprobante"))
                    If nroComprobante Is Nothing Then nroComprobante = ""
                    nroComprobante = nroComprobante.Trim()
                    Dim j As Integer = 16 - nroComprobante.Length
                    If j < 0 Then j = 0
                    Dim ceros As String = New String("0"c, j)
                    Dim nroComp16 As String = (ceros & nroComprobante)
                    If nroComp16.Length > 16 Then nroComp16 = nroComp16.Substring(nroComp16.Length - 16, 16)
                    Mid(reg, 32, 16) = nroComp16

                    Dim tipo As String = " "
                    If Not row.IsNull("IdImputacion") Then
                        Dim idImputacion As Integer = Convert.ToInt32(row("IdImputacion"))
                        Select Case idImputacion
                            Case 1
                                tipo = "F"
                            Case 2
                                tipo = "D"
                            Case 6
                                tipo = "R"
                            Case 59
                                tipo = "C"
                        End Select
                    End If
                    Mid(reg, 48, 1) = tipo

                    Mid(reg, 49, 1) = "A"
                    Mid(reg, 50, 20) = "0000" & nroComp16

                    Dim brutosTxt As String
                    If row.IsNull("Brutos") Then
                        brutosTxt = "0"
                    Else
                        brutosTxt = Convert.ToString(row("Brutos"))
                    End If
                    If brutosTxt Is Nothing Then brutosTxt = "0"
                    brutosTxt = brutosTxt.Trim()
                    j = 11 - brutosTxt.Length
                    If j < 0 Then j = 0
                    ceros = New String("0"c, j)
                    Dim brutos11 As String = ceros & brutosTxt
                    If brutos11.Length > 11 Then brutos11 = brutos11.Substring(brutos11.Length - 11, 11)
                    Mid(reg, 70, 11) = brutos11

                    Dim cbu As String = ""
                    If Not row.IsNull("cbu") Then
                        cbu = Convert.ToString(row("cbu"))
                    End If
                    If cbu Is Nothing Then cbu = ""
                    cbu = cbu.Trim()
                    If cbu.Length > 22 Then cbu = cbu.Substring(0, 22)
                    If String.IsNullOrEmpty(cbu) Then
                        cbu = New String("0"c, 22)
                    Else
                        cbu = cbu.PadRight(22, " "c)
                    End If
                    Mid(reg, 82, 22) = cbu

                    Mid(reg, 105, 2) = "CC"
                    Mid(reg, 107, 1) = "1"

                    Dim lineBytes = Encoding.Default.GetBytes(reg & vbCrLf)
                    fs.Write(lineBytes, 0, lineBytes.Length)
                Next
            End Using

            MessageBox.Show("Archivo generado Correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub


End Class
