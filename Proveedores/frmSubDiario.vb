Imports DSM = DataSourceManager.Lib.DataSourceManager
Imports System.Globalization
Imports System.IO
Imports System.Text

Public Class frmSubDiario
    Private Shared instancia As frmSubDiario

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmSubDiario()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmSubDiario_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        'Marca los del mes del deta actual
        Dim Sql As String
        Sql = "UPDATE [DetaCtaCte] SET [DetaCtaCte].sale = 0 where "
        Sql &= " (Datepart(m, [DetaCtaCte].[fecha]) = @Meses) AND "
        Sql &= " (Datepart(yyyy, [DetaCtaCte].[fecha]) = @Anio) and "
        Sql &= "([DetaCtaCte].IdImputacion = 1 or [DetaCtaCte].IdImputacion = 6 "
        Sql &= " or [DetaCtaCte].IdImputacion = 59  or [DetaCtaCte].IdImputacion = 2) And "
        Sql &= "  ([DetaCtaCte].NroCuenta <> 8100) ;"
        DSM.Execute(DSM.Proveedores, Sql, CmdParams("@Meses", Convert.ToInt32(dbcMeses.SelectedValue), "@Anio", Convert.ToInt32(txtAno.Text)), True)

        DSM.Execute(DSM.Proveedores, "Delete from wDetaCtaCte Where marcado = 1;", Nothing, True)

        ActualizarCierreIva()

        instancia = Nothing
    End Sub

    Private Sub frmSubDiario_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtAno.Text = DateTime.Now.Year.ToString()
        GroupBoxCierre.Enabled = EsUsuarioAutorizado()
        CargarCombos()
        dtFecha.Value = DateTime.Now
        'txtFecha.Text = dtFecha.Value.ToString("dd/MM/yyyy")
        CargaCierreIva()
        cmdVer.Enabled = False
        CmdDecreto.Enabled = False
        CmdLibroElectronico.Enabled = False
        CmdSIFERE.Enabled = False
    End Sub

    Private Function EsUsuarioAutorizado() As Boolean
        Dim u = General.UsuarioActual.ToUpper()
        Return u = "GUSTAVO" OrElse u = "ADMIN" OrElse u = "JULIOM"
    End Function

    Private Sub dtFecha_ValueChanged(sender As Object, e As EventArgs) Handles dtFecha.ValueChanged
        txtFecha.Text = dtFecha.Value.ToString("dd/MM/yyyy")
    End Sub

    Private Sub CmdOKCierre_Click(sender As Object, e As EventArgs) Handles CmdOKCierre.Click
        If EsUsuarioAutorizado() Then
            GroupBoxCierre.Enabled = True
        Else
            MessageBox.Show("No autorizado para cambiar la fecha.")
        End If
    End Sub

    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        'ActualizarCierreIva()
        Close()
    End Sub

    Private Sub cmdVer_Click(sender As Object, e As EventArgs) Handles cmdVer.Click
        If String.IsNullOrWhiteSpace(txtNro.Text) Then
            MessageBox.Show("Debe indicar un Número de Libro.")
            Exit Sub
        End If
        If dbcEmpresa.SelectedIndex < 0 Then
            MessageBox.Show("Debe indicar una Empresa.")
            Exit Sub
        End If
        If dbcSucursal.SelectedIndex < 0 Then
            MessageBox.Show("Debe indicar una Sucursal.")
            Exit Sub
        End If
        If dbcMeses.SelectedIndex < 0 Then
            MessageBox.Show("Debe indicar un Mes.")
            Exit Sub
        End If
        If String.IsNullOrWhiteSpace(txtAno.Text) Then
            MessageBox.Show("Debe indicar un Año.")
            Exit Sub
        End If

        DSM.Execute(DSM.Proveedores, "Delete from Cabeceras;", Nothing, True)

        Dim empresaDesc = dbcEmpresa.Text
        Dim sucDesc = dbcSucursal.Text
        Dim mesesDesc = dbcMeses.Text
        Dim sqlEmp = "Select * from Empresas where Descripcion = @Desc"
        Dim tEmp = DSM.ExecuteQuery(DSM.Proveedores, sqlEmp, CmdParams("@Desc", empresaDesc))
        Dim cuit = If(tEmp.Rows.Count > 0, tEmp.Rows(0)("Cuit").ToString(), "")
        Dim conMulti = If(tEmp.Rows.Count > 0, Convert.ToString(tEmp.Rows(0)("ConMulti")), 0)
        Dim sqlSuc = "Select * from Sucursales where Descripcion = @Desc"
        Dim tSuc = DSM.ExecuteQuery(DSM.Proveedores, sqlSuc, CmdParams("@Desc", sucDesc))
        Dim establecimiento = If(tSuc.Rows.Count > 0, tSuc.Rows(0)("Establecimiento").ToString(), "")
        Dim timbrado = If(tSuc.Rows.Count > 0, tSuc.Rows(0)("Timbrado").ToString(), "")
        Dim domicilio = If(tSuc.Rows.Count > 0, tSuc.Rows(0)("Domicilio").ToString(), "")
        Dim provincia = If(tSuc.Rows.Count > 0, tSuc.Rows(0)("Provincia").ToString(), "")

        Dim ins = "INSERT INTO Cabeceras (RazonSocial, Cuit, ConMulti, Sucursal, Establecimiento, Timbrado, Domicilio, NroLibro) VALUES (@RazonSocial, @Cuit, @ConMulti, @Sucursal, @Establecimiento, @Timbrado, @Domicilio, @NroLibro)"
        Dim nroLibro = txtNro.Text.Trim() & " " & mesesDesc & " '" & txtAno.Text.Trim()
        DSM.Execute(DSM.Proveedores, ins, CmdParams("@RazonSocial", empresaDesc, "@Cuit", cuit, "@ConMulti", conMulti, "@Sucursal", sucDesc, "@Establecimiento", establecimiento, "@Timbrado", timbrado, "@Domicilio", domicilio & " " & provincia, "@NroLibro", nroLibro), True)

        Dim Criterio As String = "({wDetaCtaCte.IdImputacion} = 1 or {wDetaCtaCte.IdImputacion} = 11 or {wDetaCtaCte.IdImputacion} = 6 or {wDetaCtaCte.IdImputacion} = 59  or {wDetaCtaCte.IdImputacion} = 2) And {wDetaCtaCte.NroCuenta} <> 8100"
        Try
            Process.Start(General.ReportesPath, "Proveedores subdiarioProve RecordSelectionFormula """ & Criterio & """")
        Catch ex As Exception
            MessageBox.Show("No se pudo abrir el reporte: " & ex.Message)
        End Try
    End Sub

    Private Sub CmdArmaArchivo_Click(sender As Object, e As EventArgs) Handles CmdArmaArchivo.Click
        PrepararMes()
        cmdVer.Enabled = True
        CmdDecreto.Enabled = True
        CmdLibroElectronico.Enabled = True
        CmdSIFERE.Enabled = True
        VerificarDuplicados()
        MessageBox.Show("Datos del mes preparados.")
    End Sub

    Private Sub CmdSIFERE_Click(sender As Object, e As EventArgs) Handles CmdSIFERE.Click
        frmSifere.AbrirInstancia(Me.MdiParent)
    End Sub

    Private Sub CargarCombos()
        General.CargarCombos(dbcEmpresa, "Empresas", "Descripcion", "Descripcion", "IdEmpresa")
        General.CargarCombos(dbcSucursal, "Sucursales", "Descripcion", "Descripcion", "IdSucursal")
        General.CargarCombos(dbcMeses, "Meses", "IdMes", "Mes", "IdMes")
    End Sub
    Private Sub CargaCierreIva()
        Dim sql = "Select * from cierreiva"
        Dim t = DSM.ExecuteQuery(DSM.Proveedores, sql)
        If t.Rows.Count > 0 Then
            dtFecha.Value = Convert.ToDateTime(t.Rows(0)("cierre"))
        End If
    End Sub
    Private Sub ActualizarCierreIva()
        DSM.Execute(DSM.Proveedores, "Delete from wdetactacte;", Nothing, True)
        Dim sql = "Select * from cierreiva"
        Dim t = DSM.ExecuteQuery(DSM.Proveedores, sql)
        If t.Rows.Count > 0 Then
            DSM.Execute(DSM.Proveedores, "UPDATE cierreiva SET cierre = @cierre", CmdParams("@cierre", txtFecha.Text), True)
        End If
    End Sub

    Private Sub PrepararMes()
        DSM.Execute(DSM.Proveedores, "Delete from wdetactacte;", Nothing, True)
        DSM.Execute(DSM.Proveedores, "Update DetaCtaCteAnual Set IngresosB4 = 0 Where (IngresosB4 Is Null);", Nothing, True)
        Dim m = Convert.ToInt32(dbcMeses.SelectedValue)
        Dim y = Convert.ToInt32(txtAno.Text)
        Dim insAnual As String = ""
        insAnual = "INSERT INTO wDetaCtaCte ( "
        insAnual &= "Marcado, NroCuenta, NroFactura, NroComprobante"
        insAnual &= ", NombreComprobante, Condicion, Fecha, IdImputacion"
        insAnual &= ", Monto, CtaMonto, ComprasRNI, CtaRNI"
        insAnual &= ", Neto105, CtaNeto105, Neto21, Cta21"
        insAnual &= ", Neto27, Cta27, Exento, CtaExento"
        insAnual &= ", IVA, CtaIva, Ganancias, CtaGanancia"
        insAnual &= ", Retenciva, CtaRetencion, IngresosB, CtaIB"
        insAnual &= ", IngresosB2, CtaIB2, IngresosB3, CtaIB3"
        insAnual &= ", IngresosB4, CtaIB4, ACuenta, FechaVto"
        insAnual &= ", TipoValor, NroCheque, RegInterno, Sucursal"
        insAnual &= ", Cobrado, Anterior, nrodespacho ) "
        insAnual &= "SELECT [DetaCtaCteAnual].Marcado, [DetaCtaCteAnual].NroCuenta, [DetaCtaCteAnual].NroFactura, [DetaCtaCteAnual].NroComprobante"
        insAnual &= ", [DetaCtaCteAnual].NombreComprobante, [DetaCtaCteAnual].Condicion, [DetaCtaCteAnual].Fecha, [DetaCtaCteAnual].IdImputacion"
        insAnual &= ", [DetaCtaCteAnual].Monto, [DetaCtaCteAnual].CtaMonto, [DetaCtaCteAnual].ComprasRNI, [DetaCtaCteAnual].CtaRNI"
        insAnual &= ", [DetaCtaCteAnual].Neto105, [DetaCtaCteAnual].CtaNeto105, [DetaCtaCteAnual].Neto21, [DetaCtaCteAnual].Cta21"
        insAnual &= ", [DetaCtaCteAnual].Neto27, [DetaCtaCteAnual].Cta27, [DetaCtaCteAnual].Exento, [DetaCtaCteAnual].CtaExento"
        insAnual &= ", [DetaCtaCteAnual].IVA, [DetaCtaCteAnual].CtaIva, [DetaCtaCteAnual].Ganancias, [DetaCtaCteAnual].CtaGanancia"
        insAnual &= ", [DetaCtaCteAnual].Retenciva, [DetaCtaCteAnual].CtaRetencion, [DetaCtaCteAnual].IngresosB, [DetaCtaCteAnual].CtaIB"
        insAnual &= ", [DetaCtaCteAnual].IngresosB2, [DetaCtaCteAnual].CtaIB2, [DetaCtaCteAnual].IngresosB3, [DetaCtaCteAnual].CtaIB3"
        insAnual &= ", [DetaCtaCteAnual].IngresosB4, [DetaCtaCteAnual].CtaIB4, [DetaCtaCteAnual].ACuenta, [DetaCtaCteAnual].FechaVto"
        insAnual &= ", [DetaCtaCteAnual].tipovalor, [DetaCtaCteAnual].NroCheque, [DetaCtaCteAnual].reginterno, [DetaCtaCteAnual].Sucursal"
        insAnual &= ", [DetaCtaCteAnual].Cobrado, [DetaCtaCteAnual].anterior, [DetaCtaCteAnual].nrodespacho From [DetaCtaCteAnual] "
        insAnual &= " WHERE (Datepart(m, [DetaCtaCteAnual].[fecha]) = " & m & ") And (Datepart(yyyy, [DetaCtaCteAnual].[fecha]) = " & y & ") "
        insAnual &= " And ([DetaCtaCteAnual].IdImputacion in(1,11,6,59,2)) And ([DetaCtaCteAnual].NroCuenta <> 8100);"
        DSM.Execute(DSM.Proveedores, insAnual, Nothing, True)

        Dim insActual As String = ""
        insActual = "INSERT INTO wDetaCtaCte ( "
        insActual &= "Marcado, NroCuenta, NroFactura, NroComprobante"
        insActual &= ", NombreComprobante, Condicion, Fecha, IdImputacion"
        insActual &= ", Monto, CtaMonto, ComprasRNI, CtaRNI"
        insActual &= ", Neto105, CtaNeto105, Neto21, Cta21"
        insActual &= ", Neto27, Cta27, Exento, CtaExento"
        insActual &= ", IVA, CtaIva, Ganancias, CtaGanancia"
        insActual &= ", Retenciva, CtaRetencion, IngresosB, CtaIB"
        insActual &= ", IngresosB2, CtaIB2, IngresosB3, CtaIB3"
        insActual &= ", IngresosB4, CtaIB4, ACuenta, FechaVto"
        insActual &= ", TipoValor, NroCheque, RegInterno, Sucursal"
        insActual &= ", Cobrado, Anterior, nrodespacho ) "
        insActual &= "SELECT [DetaCtaCte].Marcado, [DetaCtaCte].NroCuenta, [DetaCtaCte].NroFactura, [DetaCtaCte].NroComprobante"
        insActual &= ", [DetaCtaCte].NombreComprobante, [DetaCtaCte].Condicion, [DetaCtaCte].Fecha, [DetaCtaCte].IdImputacion"
        insActual &= ", [DetaCtaCte].Monto, [DetaCtaCte].CtaMonto, [DetaCtaCte].ComprasRNI, [DetaCtaCte].CtaRNI"
        insActual &= ", [DetaCtaCte].Neto105, [DetaCtaCte].CtaNeto105, [DetaCtaCte].Neto21, [DetaCtaCte].Cta21"
        insActual &= ", [DetaCtaCte].Neto27, [DetaCtaCte].Cta27, [DetaCtaCte].Exento, [DetaCtaCte].CtaExento"
        insActual &= ", [DetaCtaCte].IVA, [DetaCtaCte].CtaIva, [DetaCtaCte].Ganancias, [DetaCtaCte].CtaGanancia"
        insActual &= ", [DetaCtaCte].Retenciva, [DetaCtaCte].CtaRetencion, [DetaCtaCte].IngresosB, [DetaCtaCte].CtaIB"
        insActual &= ", [DetaCtaCte].IngresosB2, [DetaCtaCte].CtaIB2, [DetaCtaCte].IngresosB3, [DetaCtaCte].CtaIB3"
        insActual &= ", [DetaCtaCte].IngresosB4, [DetaCtaCte].CtaIB4, [DetaCtaCte].ACuenta, [DetaCtaCte].FechaVto"
        insActual &= ", [DetaCtaCte].tipovalor, [DetaCtaCte].NroCheque, [DetaCtaCte].reginterno, [DetaCtaCte].Sucursal"
        insActual &= ", [DetaCtaCte].Cobrado, [DetaCtaCte].anterior, [DetaCtaCte].nrodespacho From [DetaCtaCte] "
        insActual &= " WHERE (Datepart(m, [DetaCtaCte].[fecha]) = " & m & ") And (Datepart(yyyy, [DetaCtaCte].[fecha]) = " & y & ") "
        insActual &= " And ([DetaCtaCte].IdImputacion in(1,11,6,59,2)) And ([DetaCtaCte].NroCuenta <> 8100);"
        DSM.Execute(DSM.Proveedores, insActual, Nothing, True)

        'Traer movimientos de Bancos
        Dim junto = DateTime.Now.Year.ToString() & Convert.ToInt32(dbcMeses.SelectedValue).ToString()
        junto = junto.Substring(2)
        Dim sqlBancos As String = ""
        sqlBancos = "SELECT  MaestroBancos.Proveedor, DetaBancos.Cuenta, DetaBancos.Fecha, DetaBancos.IdMovimiento,"
        sqlBancos &= " DetaBancos.Monto, DetaBancos.CuentaConta, DetaBancos.ImputaConta FROM  MaestroBancos INNER JOIN"
        sqlBancos &= " DetaBancos ON MaestroBancos.IdBanco = DetaBancos.IdBanco"
        sqlBancos &= " where DetaBancos.cuentaconta in('1.3.7','1.3.53','1.3.2','1.3.39','1.3.30','1.3.31') and"
        sqlBancos &= " (Datepart(m, DetaBancos.fecha) = " & m & ")  And  (Datepart(yyyy, [DetaBancos].[fecha]) = " & y & ") UNION ALL"
        sqlBancos &= " SELECT  MaestroBancos.Proveedor, DetaBancosAnual.Cuenta, DetaBancosAnual.Fecha, DetaBancosAnual.IdMovimiento,"
        sqlBancos &= " DetaBancosAnual.Monto, DetaBancosAnual.CuentaConta, DetaBancosAnual.ImputaConta  FROM  MaestroBancos INNER JOIN"
        sqlBancos &= " DetaBancosAnual ON MaestroBancos.IdBanco = DetaBancosAnual.IdBanco"
        sqlBancos &= " where DetaBancosAnual.cuentaconta in('1.3.7','1.3.53','1.3.2','1.3.39','1.3.30','1.3.31') and"
        sqlBancos &= " (Datepart(m, DetaBancosAnual.fecha) = " & m & ")  And  (Datepart(yyyy, [DetaBancosAnual].[fecha]) = " & y & ");"
        Dim tb = DSM.ExecuteQuery(DSM.Bancos, sqlBancos)
        If tb IsNot Nothing AndAlso tb.Rows.Count > 0 Then
            Dim cuenta As Integer = 0
            For Each r As DataRow In tb.Rows
                Dim monto = Convert.ToDecimal(r("Monto"))
                Dim esDebito = monto < 0
                Dim nombreComp = If(esDebito, "Nota de Débito", "Nota de Crédito")
                Dim idImp = If(esDebito, 2, 59)
                Dim nroCuenta = r("Proveedor").ToString()
                cuenta = cuenta + 1
                Dim nrofact = junto & cuenta.ToString()
                Dim cuentaconta = r("CuentaConta").ToString()
                Dim iva = If(cuentaconta = "1.3.7", Math.Abs(monto), 0D)
                Dim retIva = If(cuentaconta = "1.3.2", Math.Abs(monto), 0D)
                Dim ib = If(cuentaconta = "1.3.30" Or cuentaconta = "1.3.39" Or cuentaconta = "1.3.53", Math.Abs(monto), 0D)
                Dim insRow = "INSERT INTO wDetaCtaCte ( Marcado, NroCuenta, NroFactura, NroComprobante, NombreComprobante, Condicion, Fecha, IdImputacion, Monto, CtaMonto, ComprasRNI, CtaRNI, Neto105, CtaNeto105, Neto21, Cta21, Neto27, Cta27, Exento, CtaExento, IVA, CtaIva, Ganancias, CtaGanancia, Retenciva, CtaRetencion, IngresosB, CtaIB, FechaVto, TipoValor, NroCheque, RegInterno, Sucursal, Cobrado, Anterior, nrodespacho) VALUES (@Marcado, @NroCuenta, @NroFactura, @NroComprobante, @NombreComprobante, @Condicion, @Fecha, @IdImputacion, @Monto, @CtaMonto, @ComprasRNI, @CtaRNI, @Neto105, @CtaNeto105, @Neto21, @Cta21, @Neto27, @Cta27, @Exento, @CtaExento, @IVA, @CtaIva, @Ganancias, @CtaGanancia, @Retenciva, @CtaRetencion, @IngresosB, @CtaIB, @FechaVto, @TipoValor, @NroCheque, @RegInterno, @Sucursal, @Cobrado, @Anterior, @nrodespacho)"
                DSM.Execute(DSM.Proveedores, insRow, CmdParams("@Marcado", 1, "@NroCuenta", nroCuenta, "@NroFactura", nrofact, "@NroComprobante", nrofact, "@NombreComprobante", nombreComp, "@Condicion", 1, "@Fecha", Convert.ToDateTime(r("Fecha")), "@IdImputacion", idImp, "@Monto", Math.Abs(monto), "@CtaMonto", 1, "@ComprasRNI", 0, "@CtaRNI", 1, "@Neto105", 0, "@CtaNeto105", 1, "@Neto21", 0, "@Cta21", 1, "@Neto27", 0, "@Cta27", 1, "@Exento", 0, "@CtaExento", 1, "@IVA", iva, "@CtaIva", 1, "@Ganancias", 0, "@CtaGanancia", 1, "@Retenciva", retIva, "@CtaRetencion", 1, "@IngresosB", ib, "@CtaIB", 1, "@FechaVto", DBNull.Value, "@TipoValor", 0, "@NroCheque", 0, "@RegInterno", 0, "@Sucursal", 1, "@Cobrado", 0, "@Anterior", 0, "@nrodespacho", DBNull.Value), True)
            Next
        End If
    End Sub

    Private Sub VerificarDuplicados()
        Dim sql = "SELECT DISTINCT wDetaCtaCte.NroCuenta, wDetaCtaCte.NroFactura, wDetaCtaCte.IdImputacion, wDetaCtaCte.Monto, wDetaCtaCte.Fecha From wDetaCtaCte WHERE (((wDetaCtaCte.NroCuenta) In (SELECT [NroCuenta] FROM [wDetaCtaCte] As Tmp GROUP BY [NroCuenta],[NroFactura],[IdImputacion] HAVING Count(*)>1  And [NroFactura] = [wDetaCtaCte].[NroFactura] And [IdImputacion] = [wDetaCtaCte].[IdImputacion])) AND ((wDetaCtaCte.IdImputacion)=1)) ORDER BY wDetaCtaCte.NroCuenta, wDetaCtaCte.NroFactura, wDetaCtaCte.IdImputacion;"
        Dim t = DSM.ExecuteQuery(DSM.Proveedores, sql)
        If t.Rows.Count > 1 Then
            MessageBox.Show("Cuidado Registros Duplicados")
        End If
    End Sub

    Private Sub CmdDecreto_Click(sender As Object, e As EventArgs) Handles CmdDecreto.Click
        If dbcMeses.SelectedIndex < 0 Then
            MessageBox.Show("Debe indicar un Mes.")
            Exit Sub
        End If

        Dim MiSql As String = "SELECT MaeCtaCte.NroCuenta, MaeCtaCte.Nombre, MaeCtaCte.Cuit, MaeCtaCte.IdTipoIva, WDetaCtaCte.NroFactura, WDetaCtaCte.NroComprobante, "
        MiSql &= "WDetaCtaCte.NombreComprobante, WDetaCtaCte.Fecha, WDetaCtaCte.IdImputacion, WDetaCtaCte.Monto, WDetaCtaCte.ComprasRNI, WDetaCtaCte.Neto105, "
        MiSql &= "WDetaCtaCte.Neto21, WDetaCtaCte.Neto27, WDetaCtaCte.Exento, WDetaCtaCte.IVA, WDetaCtaCte.Ganancias, WDetaCtaCte.Retenciva, WDetaCtaCte.IngresosB, WDetaCtaCte.IngresosB2, WDetaCtaCte.IngresosB3, WDetaCtaCte.IngresosB4 "
        MiSql &= "FROM WDetaCtaCte INNER JOIN MaeCtaCte ON WDetaCtaCte.NroCuenta = MaeCtaCte.NroCuenta "
        MiSql &= "where  (maectacte.nrocuenta <> 8100) and ((WDetaCtaCte.IdImputacion  = 1) or (WDetaCtaCte.IdImputacion = 11) or(WDetaCtaCte.IdImputacion = 6)or (WDetaCtaCte.IdImputacion = 2)  or (WDetaCtaCte.IdImputacion = 59))  ;"
        Dim dtDecreto As DataTable = DSM.ExecuteQuery(DSM.Proveedores, MiSql)

        Dim Nombre = "Compras_" + txtAno.Text + dbcMeses.Text + ".txt"
        Dim outputDir = "c:\DECRETOCOMPRAS\"
        Directory.CreateDirectory(outputDir)
        Dim outputPath = Path.Combine(outputDir, Nombre)

        Dim sumang As Long
        Dim sumareg As Long
        Dim sumamonto As Long
        Dim sumaRNI As Long
        Dim sumaivari As Long
        Dim sumae As Long
        Dim sumaib As Long
        Dim sumain As Long
        Dim sumaga As Long
        Dim fecho As String = ""

        Using fs As New FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None)
            For Each row As DataRow In dtDecreto.Rows
                Dim reg As String = New String(" "c, 369)

                Dim fecha As DateTime = Convert.ToDateTime(row("Fecha"))
                Dim fechaTxt As String = fecha.ToString("dd/MM/yyyy")
                fecho = fechaTxt
                Dim yyyymmdd As String = fechaTxt.Substring(6, 4) & fechaTxt.Substring(3, 2) & fechaTxt.Substring(0, 2)

                Mid(reg, 1, 1) = "1"
                Mid(reg, 2, 8) = yyyymmdd

                Dim idImputacion As Integer = Convert.ToInt32(row("IdImputacion"))
                Dim idTipoIva As Integer = Convert.ToInt32(row("IdTipoIva"))
                Dim nroCuenta As Integer = Convert.ToInt32(row("NroCuenta"))

                Mid(reg, 10, 2) = "00"
                If idImputacion = 6 Then Mid(reg, 10, 2) = "88"
                If idImputacion = 11 Then Mid(reg, 10, 2) = "14"

                If idImputacion = 1 AndAlso idTipoIva = 1 Then Mid(reg, 10, 2) = "01"
                If idImputacion = 1 AndAlso idTipoIva = 2 Then Mid(reg, 10, 2) = "01"
                If idImputacion = 1 AndAlso idTipoIva = 6 Then Mid(reg, 10, 2) = "11"
                If idImputacion = 1 AndAlso idTipoIva = 4 Then Mid(reg, 10, 2) = "11"

                If idImputacion = 2 AndAlso idTipoIva = 6 Then Mid(reg, 10, 2) = "12"
                If idImputacion = 2 AndAlso idTipoIva = 1 Then Mid(reg, 10, 2) = "02"
                If idImputacion = 2 AndAlso idTipoIva = 4 Then Mid(reg, 10, 2) = "12"

                If idImputacion = 59 AndAlso idTipoIva = 1 Then Mid(reg, 10, 2) = "03"
                If idImputacion = 59 AndAlso idTipoIva = 6 Then Mid(reg, 10, 2) = "13"

                Mid(reg, 12, 1) = " "
                Mid(reg, 13, 4) = "0001"

                Dim nroComprobante As String = Convert.ToString(row("NroComprobante")).Trim()
                Dim j As Integer = 20 - nroComprobante.Length
                If j < 0 Then j = 0
                Dim ceros As String = New String("0"c, j)
                Mid(reg, 17, 20) = ceros & nroComprobante

                Mid(reg, 37, 8) = yyyymmdd
                Mid(reg, 45, 3) = "000"

                If nroCuenta = 6288 Then
                    Mid(reg, 48, 4) = "IC04"
                    j = 6 - nroComprobante.Length
                    If j < 0 Then j = 0
                    ceros = New String("0"c, j)
                    Mid(reg, 52, 6) = ceros & nroComprobante
                    Mid(reg, 58, 1) = "A"
                Else
                    Mid(reg, 48, 4) = New String(" "c, 4)
                    Mid(reg, 52, 6) = New String("0"c, 6)
                    Mid(reg, 58, 1) = " "
                End If

                Mid(reg, 59, 2) = "80"

                Dim cuit As String = Convert.ToString(row("Cuit"))
                Dim cuit11 As String = ""
                If cuit IsNot Nothing Then cuit = cuit.Trim()
                If Not String.IsNullOrEmpty(cuit) AndAlso cuit.Length >= 13 Then
                    cuit11 = cuit.Substring(0, 2) & cuit.Substring(3, 8) & cuit.Substring(12, 1)
                Else
                    Dim digits As String = ""
                    If Not String.IsNullOrEmpty(cuit) Then
                        For Each ch As Char In cuit
                            If Char.IsDigit(ch) Then digits &= ch
                        Next
                    End If
                    If digits.Length >= 11 Then
                        cuit11 = digits.Substring(0, 11)
                    Else
                        cuit11 = digits.PadLeft(11, "0"c)
                    End If
                End If
                Mid(reg, 61, 11) = cuit11

                Dim nombreProv As String = Convert.ToString(row("Nombre"))
                If nombreProv Is Nothing Then nombreProv = ""
                If nombreProv.Length > 30 Then nombreProv = nombreProv.Substring(0, 30)
                Mid(reg, 72, 30) = nombreProv

                Dim montoDec As Decimal = If(row.IsNull("Monto"), 0D, Convert.ToDecimal(row("Monto")))
                Dim comprasRniDec As Decimal = If(row.IsNull("ComprasRNI"), 0D, Convert.ToDecimal(row("ComprasRNI")))
                Dim neto105Dec As Decimal = If(row.IsNull("Neto105"), 0D, Convert.ToDecimal(row("Neto105")))
                Dim neto21Dec As Decimal = If(row.IsNull("Neto21"), 0D, Convert.ToDecimal(row("Neto21")))
                Dim neto27Dec As Decimal = If(row.IsNull("Neto27"), 0D, Convert.ToDecimal(row("Neto27")))
                Dim exentoDec As Decimal = If(row.IsNull("Exento"), 0D, Convert.ToDecimal(row("Exento")))
                Dim ivaDec As Decimal = If(row.IsNull("IVA"), 0D, Convert.ToDecimal(row("IVA")))
                Dim gananciasDec As Decimal = If(row.IsNull("Ganancias"), 0D, Convert.ToDecimal(row("Ganancias")))
                Dim retencivaDec As Decimal = If(row.IsNull("Retenciva"), 0D, Convert.ToDecimal(row("Retenciva")))

                Dim monto As Long = CLng(Decimal.Round(montoDec * 100D, 0, MidpointRounding.AwayFromZero))
                Dim comprasRni As Long = CLng(Decimal.Round(comprasRniDec * 100D, 0, MidpointRounding.AwayFromZero))
                Dim netoGravado As Long = CLng(Decimal.Round((neto105Dec + neto21Dec + neto27Dec) * 100D, 0, MidpointRounding.AwayFromZero))
                Dim iva As Long = CLng(Decimal.Round(ivaDec * 100D, 0, MidpointRounding.AwayFromZero))
                Dim exento As Long = CLng(Decimal.Round(exentoDec * 100D, 0, MidpointRounding.AwayFromZero))
                Dim retenciva As Long = CLng(Decimal.Round(retencivaDec * 100D, 0, MidpointRounding.AwayFromZero))
                Dim ganancias As Long = CLng(Decimal.Round(gananciasDec * 100D, 0, MidpointRounding.AwayFromZero))

                Dim maniobra As String = monto.ToString()
                j = 15 - maniobra.Length
                If j < 0 Then j = 0
                ceros = New String("0"c, j)
                Mid(reg, 102, 15) = ceros & maniobra
                If idImputacion = 59 Then
                    sumamonto -= monto
                Else
                    sumamonto += monto
                End If

                maniobra = comprasRni.ToString()
                j = 15 - maniobra.Length
                If j < 0 Then j = 0
                ceros = New String("0"c, j)
                Mid(reg, 117, 15) = ceros & maniobra
                If idImputacion = 59 Then
                    sumaRNI -= comprasRni
                Else
                    sumaRNI += comprasRni
                End If

                maniobra = netoGravado.ToString()
                j = 15 - maniobra.Length
                If j < 0 Then j = 0
                ceros = New String("0"c, j)
                Mid(reg, 132, 15) = ceros & maniobra
                If idImputacion = 59 Then
                    sumang -= netoGravado
                Else
                    sumang += netoGravado
                End If

                Mid(reg, 147, 4) = "0000"
                If (neto105Dec <> 0D) AndAlso (neto21Dec = 0D) AndAlso (neto27Dec = 0D) Then Mid(reg, 147, 4) = "1050"
                If (neto21Dec <> 0D) AndAlso (neto105Dec = 0D) AndAlso (neto27Dec = 0D) Then Mid(reg, 147, 4) = "2100"
                If (neto27Dec <> 0D) AndAlso (neto105Dec = 0D) AndAlso (neto21Dec = 0D) Then Mid(reg, 147, 4) = "2700"
                If (neto27Dec <> 0D) AndAlso (neto21Dec <> 0D) Then Mid(reg, 147, 4) = "2700"
                If (neto21Dec <> 0D) AndAlso (neto105Dec <> 0D) Then Mid(reg, 147, 4) = "2100"

                maniobra = iva.ToString()
                j = 15 - maniobra.Length
                If j < 0 Then j = 0
                ceros = New String("0"c, j)
                Mid(reg, 151, 15) = ceros & maniobra
                If idImputacion = 59 Then
                    sumaivari -= iva
                Else
                    sumaivari += iva
                End If

                maniobra = exento.ToString()
                j = 15 - maniobra.Length
                If j < 0 Then j = 0
                ceros = New String("0"c, j)
                Mid(reg, 166, 15) = ceros & maniobra
                If idImputacion = 59 Then
                    sumae -= exento
                Else
                    sumae += exento
                End If

                maniobra = retenciva.ToString()
                j = 15 - maniobra.Length
                If j < 0 Then j = 0
                ceros = New String("0"c, j)
                Mid(reg, 181, 15) = ceros & maniobra
                If idImputacion = 59 Then
                    sumain -= retenciva
                Else
                    sumain += retenciva
                End If

                maniobra = ganancias.ToString()
                j = 15 - maniobra.Length
                If j < 0 Then j = 0
                ceros = New String("0"c, j)
                Mid(reg, 196, 15) = ceros & maniobra
                If idImputacion = 59 Then
                    sumaga -= ganancias
                Else
                    sumaga += ganancias
                End If

                Dim ingresosBDec As Decimal = If(row.IsNull("IngresosB"), 0D, Convert.ToDecimal(row("IngresosB")))
                Dim ingresosB2Dec As Decimal = If(row.IsNull("IngresosB2"), 0D, Convert.ToDecimal(row("IngresosB2")))
                Dim ingresosB3Dec As Decimal = If(row.IsNull("IngresosB3"), 0D, Convert.ToDecimal(row("IngresosB3")))
                Dim ingresosB4Dec As Decimal = If(row.IsNull("IngresosB4"), 0D, Convert.ToDecimal(row("IngresosB4")))
                Dim ingresosBrutos As Long = CLng(Decimal.Round((ingresosBDec + ingresosB2Dec + ingresosB3Dec + ingresosB4Dec) * 100D, 0, MidpointRounding.AwayFromZero))

                maniobra = ingresosBrutos.ToString()
                j = 15 - maniobra.Length
                If j < 0 Then j = 0
                ceros = New String("0"c, j)
                Mid(reg, 211, 15) = ceros & maniobra
                If idImputacion = 59 Then
                    sumaib -= ingresosBrutos
                Else
                    sumaib += ingresosBrutos
                End If

                Mid(reg, 226, 30) = New String("0"c, 30)
                Mid(reg, 256, 1) = "0"
                Dim idTipoIvaTxt = idTipoIva.ToString()
                If idTipoIvaTxt.Length = 0 Then idTipoIvaTxt = "0"
                Mid(reg, 257, 1) = idTipoIvaTxt.Substring(0, 1)
                Mid(reg, 258, 3) = "PES"
                Mid(reg, 261, 10) = New String("0"c, 10)
                Mid(reg, 271, 1) = "1"
                If idTipoIva = 4 Then
                    Mid(reg, 272, 1) = "E"
                Else
                    Mid(reg, 272, 1) = " "
                End If
                Mid(reg, 273, 14) = New String("0"c, 14)
                Mid(reg, 287, 8) = "00000000"
                Mid(reg, 295, 75) = New String(" "c, 75)

                Dim lineBytes = Encoding.Default.GetBytes(reg & vbCrLf)
                fs.Write(lineBytes, 0, lineBytes.Length)

                sumareg += 1


                Dim reg2 As String = New String(" "c, 369)
                Mid(reg2, 1, 1) = "2"
                If String.IsNullOrEmpty(fecho) Then fecho = DateTime.Now.ToString("dd/MM/yyyy")
                Mid(reg2, 2, 6) = fecho.Substring(6, 4) & fecho.Substring(3, 2)
                Mid(reg2, 8, 10) = New String(" "c, 10)

                Dim maniobra2 As String = sumareg.ToString()


                j = 12 - maniobra2.Length
                If j < 0 Then j = 0
                ceros = New String("0"c, j)
                Mid(reg2, 18, 12) = ceros & maniobra2

                Mid(reg2, 30, 31) = New String(" "c, 31)
                Mid(reg2, 61, 11) = "30677018816"
                Mid(reg2, 72, 30) = New String(" "c, 30)

                maniobra2 = sumamonto.ToString()
                j = 15 - maniobra2.Length
                If j < 0 Then j = 0
                ceros = New String("0"c, j)
                Mid(reg2, 102, 15) = ceros & maniobra2

                maniobra2 = sumaRNI.ToString()
                j = 15 - maniobra2.Length
                If j < 0 Then j = 0
                ceros = New String("0"c, j)
                Mid(reg2, 117, 15) = ceros & maniobra2

                maniobra2 = sumang.ToString()
                j = 15 - maniobra2.Length
                If j < 0 Then j = 0
                ceros = New String("0"c, j)
                Mid(reg2, 132, 15) = ceros & maniobra2

                Mid(reg2, 147, 4) = New String(" "c, 4)

                maniobra2 = sumaivari.ToString()
                j = 15 - maniobra2.Length
                If j < 0 Then j = 0
                ceros = New String("0"c, j)
                Mid(reg2, 151, 15) = ceros & maniobra2

                maniobra2 = sumae.ToString()
                j = 15 - maniobra2.Length
                If j < 0 Then j = 0
                ceros = New String("0"c, j)
                Mid(reg2, 166, 15) = ceros & maniobra2

                maniobra2 = sumain.ToString()
                j = 15 - maniobra2.Length
                If j < 0 Then j = 0
                ceros = New String("0"c, j)
                Mid(reg2, 181, 15) = ceros & maniobra2

                maniobra2 = sumaga.ToString()
                j = 15 - maniobra2.Length
                If j < 0 Then j = 0
                ceros = New String("0"c, j)
                Mid(reg2, 196, 15) = ceros & maniobra2

                maniobra2 = sumaib.ToString()
                j = 15 - maniobra2.Length
                If j < 0 Then j = 0
                ceros = New String("0"c, j)
                Mid(reg2, 211, 15) = ceros & maniobra2

                Mid(reg2, 226, 30) = New String("0"c, 30)
                Mid(reg2, 256, 114) = New String(" "c, 114)

                Dim lineBytes2 = Encoding.Default.GetBytes(reg2 & vbCrLf)
                fs.Write(lineBytes2, 0, lineBytes2.Length)
            Next

        End Using

        MsgBox("Archivo generado THIS IS A Acounter....CON LA NUEVA VERSION SUPERSONICA ....")
    End Sub

End Class
