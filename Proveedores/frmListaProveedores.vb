Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmListaProveedores
    Inherits Form
    Private Shared instancia As frmListaProveedores

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmListaProveedores()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmListaProveedores_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Private Sub frmListaProveedores_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If String.Equals(General.UsuarioActual, "GUSTAVO", StringComparison.OrdinalIgnoreCase) OrElse String.Equals(General.UsuarioActual, "ADMIN", StringComparison.OrdinalIgnoreCase) Then
            Frame3.Enabled = True
        Else
            Frame3.Enabled = False
        End If
        Option7.Checked = True
        Option1.Checked = True
    End Sub

    Private Sub cmdSalir_Click(sender As Object, e As EventArgs) Handles cmdSalir.Click
        Close()
    End Sub

    Private Sub cmdVer_Click(sender As Object, e As EventArgs) Handles cmdVer.Click
        Cursor = Cursors.WaitCursor
        txtCuenta.Text = "0"
        DSM.Execute(DSM.Proveedores, "Update MaeCtaCte set recalculado = 0;", Nothing, True)
        DSM.Execute(DSM.Proveedores, "Update MaeCtaCte Set SaldoActual = 0 Where (SaldoActual < 1) And (SaldoActual > -1) And (SaldoActual <> 0)", Nothing, True)

        Dim Criterio As String = ""
        If Option3.Checked AndAlso Option1.Checked Then
            Criterio = "({MaeCtaCte.autoshop}=false)and({MaeCtaCte.lista}=false)"
        End If
        If Option4.Checked AndAlso Option1.Checked Then
            Criterio = "({MaeCtaCte.autoshop}=true)and({MaeCtaCte.lista}=false)"
        End If
        If Option6.Checked AndAlso Option1.Checked Then
            Criterio = "{MaeCtaCte.nrocuenta}<>false"
        End If
        If Option2.Checked AndAlso Option3.Checked Then
            Criterio &= "({MaeCtaCte.autoshop}=false)And({maectacte.saldoactual}<>0)"
        End If
        If Option4.Checked AndAlso Option2.Checked Then
            Criterio &= "({MaeCtaCte.autoshop}=true)And({maectacte.saldoactual}<>0)"
        End If
        If Option6.Checked AndAlso Option2.Checked Then
            Criterio &= "{maectacte.saldoactual}<>0"
        End If
        If Option5.Checked AndAlso Option3.Checked Then
            Criterio &= "({MaeCtaCte.autoshop}=false)and({maectacte.codcontable}='2.1.1')and({MaeCtaCte.lista}=false)And({maectacte.saldoactual}<>0)"
        End If
        If Option5.Checked AndAlso Option4.Checked Then
            Criterio &= "({MaeCtaCte.autoshop}=true)and({maectacte.codcontable}='2.1.1')and({MaeCtaCte.lista}=false)And({maectacte.saldoactual}<>0)"
        End If
        If Option5.Checked AndAlso Option6.Checked Then
            Criterio &= "({maectacte.codcontable}='2.1.1')and({MaeCtaCte.lista}=false)"
        End If
        If Option3.Checked AndAlso Option9.Checked Then
            Criterio &= "({MaeCtaCte.autoshop}=false)and({maectacte.codcontable}='2.1.1')and({MaeCtaCte.lista}=false)And({maectacte.recalculado}<>false)"
        End If
        If Option4.Checked AndAlso Option9.Checked Then
            Criterio &= "({MaeCtaCte.autoshop}=true)and({maectacte.codcontable}='2.1.1')and({MaeCtaCte.lista}=false)And({maectacte.recalculado}<>false)"
        End If
        If Option6.Checked AndAlso Option9.Checked Then
            Criterio &= "{maectacte.recalculado}<>false"
        End If

        If txtFecha.TextLength = 10 Then
            Dim d As Date
            If Not Date.TryParse(txtFecha.Text, d) Then
                MessageBox.Show("Fecha invalida......")
                Cursor = Cursors.Default
                Exit Sub
            End If

            Dim sqlMae As String
            If Option3.Checked Then
                sqlMae = "Select * from MaeCtaCte where autoshop = 0;"
            ElseIf Option4.Checked Then
                sqlMae = "Select * from MaeCtaCte where autoshop = 1;"
            Else
                sqlMae = "Select * from MaeCtaCte;"
            End If
            Dim dtMae = DSM.ExecuteQuery(DSM.Proveedores, sqlMae, Nothing)
            If dtMae IsNot Nothing AndAlso dtMae.Rows.Count > 0 Then
                For Each r As DataRow In dtMae.Rows
                    txtCuenta.Text = Convert.ToString(r("NroCuenta"))
                    Dim recalculado As Decimal = Convert.ToDecimal(r("SaldoActual"))
                    Dim sqlMov As String = "SELECT  NroCuenta, IdImputacion, Fecha, Monto, ctamonto From DetaCtaCte Where NroCuenta = @Nro and fecha > @Fecha Union ALL SELECT NroCuenta, IdImputacion, Fecha, Monto, ctamonto From DetaCtaCteAnual Where NroCuenta = @Nro and fecha > @Fecha"
                    Dim dtMov = DSM.ExecuteQuery(DSM.Proveedores, sqlMov, CmdParams("@Nro", Convert.ToInt32(r("NroCuenta")), "@Fecha", d))
                    If dtMov IsNot Nothing AndAlso dtMov.Rows.Count > 0 Then
                        For Each m As DataRow In dtMov.Rows
                            Dim idImp As Integer = Convert.ToInt32(m("IdImputacion"))
                            If idImp = 6 Then GoTo salta
                            Dim cta As String = Convert.ToString(m("ctamonto"))
                            If (idImp = 1 OrElse idImp = 2 OrElse idImp = 59) AndAlso cta <> "2.1.1" Then GoTo salta
                            If idImp > 50 Then
                                recalculado = recalculado + Convert.ToDecimal(m("Monto"))
                            Else
                                recalculado = recalculado - Convert.ToDecimal(m("Monto"))
                            End If
salta:
                        Next
                        DSM.Execute(DSM.Proveedores, "UPDATE MaeCtaCte SET recalculado = @val WHERE NroCuenta = @Nro", CmdParams("@val", recalculado, "@Nro", Convert.ToInt32(r("NroCuenta"))), True)
                    End If
                Next
            End If
        End If

        Dim rptPath As String = If(Option7.Checked, "Listadocli2", "Listadocli")
        Process.Start(General.ReportesPath, $"Proveedores {rptPath} RecordSelectionFormula " & Criterio)
        Cursor = Cursors.Default
    End Sub
End Class
