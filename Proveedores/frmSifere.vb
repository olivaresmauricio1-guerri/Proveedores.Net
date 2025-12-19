Imports DSM = DataSourceManager.Lib.DataSourceManager
Imports System.Text
Imports System.IO

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
            ' Limpiar archivo de salida
            DSM.Execute(DSM.Proveedores, "Delete from sifere", Nothing, True)

            Dim sqlInsert As String = "INSERT INTO sifere ( NroComprobante, IdImputacion, Fecha, Brutos, Cuit, Juri, cbu ) " &
                                      "SELECT WDetaCtaCte.NroFactura, WDetaCtaCte.IdImputacion, WDetaCtaCte.Fecha, " &
                                      "WDetaCtaCte.IngresosB, MaeCtaCte.Cuit, Provincias.CodigoConv, MaeCtaCte.cbu " &
                                      "FROM (WDetaCtaCte INNER JOIN MaeCtaCte ON WDetaCtaCte.NroCuenta = MaeCtaCte.NroCuenta) " &
                                      "INNER JOIN Provincias ON MaeCtaCte.Provincia = Provincias.Descripcion " &
                                      "WHERE (((WDetaCtaCte.IngresosB)<>0))"

            DSM.Execute(DSM.Proveedores, sqlInsert, Nothing, True)

            Dim sqlSelect As String = "SELECT * from SIFERE"
            Dim dt As DataTable = DSM.ExecuteQuery(DSM.Proveedores, sqlSelect)

            ' Crear directorio si no existe
            Dim filePath As String = "c:\citi\sifere.txt"
            Dim dir As String = Path.GetDirectoryName(filePath)
            If Not Directory.Exists(dir) Then
                Directory.CreateDirectory(dir)
            End If

            Using sw As New StreamWriter(filePath, False, Encoding.Default)
                For Each r As DataRow In dt.Rows
                    Dim reg As New StringBuilder(New String(" "c, 107))

                    ' Mid(reg, 1, 3) = MiRs!juri
                    PutStr(reg, 1, r("juri").ToString(), 3)

                    ' Mid(reg, 4, 13) = MiRs!cuit
                    PutStr(reg, 4, r("cuit").ToString(), 13)

                    ' Mid(reg, 18, 10) = MiRs!Fecha
                    Dim fecha As DateTime = Convert.ToDateTime(r("Fecha"))
                    PutStr(reg, 18, fecha.ToString("dd/MM/yyyy"), 10) 

                    ' Mid(reg, 28, 4) = "0001"
                    PutStr(reg, 28, "0001", 4)

                    ' j = 16 - Len(MiRs!NroComprobante) -> ceros & MiRs!NroComprobante
                    Dim nroComp As String = r("NroComprobante").ToString()
                    Dim nroCompPad As String = nroComp.PadLeft(16, "0"c)
                    If nroCompPad.Length > 16 Then nroCompPad = nroCompPad.Substring(nroCompPad.Length - 16)
                    PutStr(reg, 32, nroCompPad, 16)

                    ' Select Case MiRs!idimputacion
                    Dim idImp As Integer = Convert.ToInt32(r("idimputacion"))
                    Select Case idImp
                        Case 1
                            PutStr(reg, 48, "F", 1)
                        Case 2
                            PutStr(reg, 48, "D", 15)
                        Case 6
                            PutStr(reg, 48, "R", 15)
                        Case 59
                            PutStr(reg, 48, "C", 15)
                    End Select

                    ' Mid(reg, 49, 1) = "A"
                    PutStr(reg, 49, "A", 1)

                    ' Mid(reg, 50, 20) = "0000" + Mid(reg, 32, 16)
                    ' VB6: Mid(reg, 32, 16) lee lo que acabamos de escribir (nroCompPad)
                    Dim nroCompPadRead = reg.ToString().Substring(31, 16) 
                    Dim val50 = "0000" & nroCompPadRead
                    PutStr(reg, 50, val50, 20)

                    ' j = 11 - Len(MiRs!brutos) -> ceros & MiRs!brutos
                    Dim brutos As String = r("brutos").ToString()
                    Dim brutosPad As String = brutos.PadLeft(11, "0"c)
                    If brutosPad.Length > 11 Then brutosPad = brutosPad.Substring(brutosPad.Length - 11)
                    PutStr(reg, 70, brutosPad, 11)

                    ' If Not IsNull(MiRs!cbu) Then ...
                    If Not IsDBNull(r("cbu")) AndAlso Not String.IsNullOrEmpty(r("cbu").ToString()) Then
                        PutStr(reg, 82, r("cbu").ToString(), 22)
                    Else
                        PutStr(reg, 82, New String("0"c, 22), 22)
                    End If

                    ' Mid(reg, 105, 2) = "CC"
                    PutStr(reg, 105, "CC", 2)

                    ' Mid(reg, 107, 1) = "1"
                    PutStr(reg, 107, "1", 1)

                    sw.WriteLine(reg.ToString())
                Next
            End Using

            MessageBox.Show("Proceso Terminado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' Replica el comportamiento de Mid(reg, start, len) = value de VB6
    ''' </summary>
    Private Sub PutStr(sb As StringBuilder, start1Based As Integer, value As String, Optional length As Integer = -1)
        If value Is Nothing Then value = ""
        
        Dim writeLen As Integer = value.Length
        If length <> -1 AndAlso length < writeLen Then
            writeLen = length
        End If
        
        Dim index0 As Integer = start1Based - 1
        
        For i As Integer = 0 To writeLen - 1
            If index0 + i < sb.Length Then
                sb(index0 + i) = value(i)
            End If
        Next
    End Sub

End Class