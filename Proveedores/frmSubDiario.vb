Imports DSM = DataSourceManager.Lib.DataSourceManager
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
        ActualizarCierreIva()
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

    Private Sub CmdDecreto_Click(sender As Object, e As EventArgs) Handles CmdDecreto.Click
        If dbcMeses.SelectedIndex < 0 Then
            MessageBox.Show("DEBE SELECCIONAR UN MES")
            Exit Sub
        End If
        GenerarDecreto1361()
    End Sub

    Private Sub CmdLibroElectronico_Click(sender As Object, e As EventArgs) Handles CmdLibroElectronico.Click
        If dbcMeses.SelectedIndex < 0 Then
            MessageBox.Show("DEBE SELECCIONAR UN MES")
            Exit Sub
        End If
        GenerarLibroIvaDigital()
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



    Private Shared Function FechaAAAAMMDD(fecha As Date) As String
        Return fecha.ToString("yyyyMMdd")
    End Function

    Private Sub GenerarDecreto1361()
        Dim sql = "SELECT MaeCtaCte.NroCuenta, MaeCtaCte.Nombre, MaeCtaCte.Cuit, MaeCtaCte.IdTipoIva, WDetaCtaCte.NroFactura, WDetaCtaCte.NroComprobante, WDetaCtaCte.NombreComprobante, WDetaCtaCte.Fecha, WDetaCtaCte.IdImputacion, WDetaCtaCte.Monto, WDetaCtaCte.ComprasRNI, WDetaCtaCte.Neto105, WDetaCtaCte.Neto21, WDetaCtaCte.Neto27, WDetaCtaCte.Exento, WDetaCtaCte.IVA, WDetaCtaCte.Ganancias, WDetaCtaCte.Retenciva, WDetaCtaCte.IngresosB, WDetaCtaCte.IngresosB2, WDetaCtaCte.IngresosB3, WDetaCtaCte.IngresosB4 FROM WDetaCtaCte INNER JOIN MaeCtaCte ON WDetaCtaCte.NroCuenta = MaeCtaCte.NroCuenta where  (maectacte.nrocuenta <> 8100) and ((WDetaCtaCte.IdImputacion  = 1) or (WDetaCtaCte.IdImputacion = 11) or(WDetaCtaCte.IdImputacion = 6)or (WDetaCtaCte.IdImputacion = 2)  or (WDetaCtaCte.IdImputacion = 59))"
        Dim t = DSM.ExecuteQuery(DSM.Proveedores, sql)
        Dim nombre = "Compras_" & txtAno.Text.Trim() & Convert.ToInt32(dbcMeses.SelectedValue).ToString() & ".txt"
        Dim ruta = IO.Path.Combine("c:\DECRETOCOMPRAS", nombre)
        IO.Directory.CreateDirectory("c:\DECRETOCOMPRAS")
        Using fs As New IO.FileStream(ruta, IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.None)
            Using sw As New IO.StreamWriter(fs, Encoding.ASCII)
                Dim sumareg = 0
                Dim sumamonto As Decimal = 0
                Dim sumaRNI As Decimal = 0
                Dim sumang As Decimal = 0
                Dim sumaivari As Decimal = 0
                Dim sumae As Decimal = 0
                Dim sumaib As Decimal = 0
                Dim sumain As Decimal = 0
                Dim sumaga As Decimal = 0
                Dim fecho As Date = DateTime.Now
                For Each r As DataRow In t.Rows
                    Dim reg = New StringBuilder(New String(" "c, 369))
                    reg.Remove(0, 369)
                    Dim fecha = Convert.ToDateTime(r("Fecha"))
                    fecho = fecha
                    Dim idimp = Convert.ToInt32(r("IdImputacion"))
                    Dim idtipoiva = Convert.ToInt32(r("IdTipoIva"))
                    Dim nroComp = r("NroComprobante").ToString()
                    Dim cuit = r("Cuit").ToString()
                    Dim nombreProv = r("Nombre").ToString()
                    Dim monto As Decimal = Convert.ToDecimal(r("Monto"))
                    Dim comprasRNI As Decimal = Convert.ToDecimal(r("ComprasRNI"))
                    Dim neto105 As Decimal = Convert.ToDecimal(r("Neto105"))
                    Dim neto21 As Decimal = Convert.ToDecimal(r("Neto21"))
                    Dim neto27 As Decimal = Convert.ToDecimal(r("Neto27"))
                    Dim exento As Decimal = Convert.ToDecimal(r("Exento"))
                    Dim iva As Decimal = Convert.ToDecimal(r("IVA"))
                    Dim gan As Decimal = Convert.ToDecimal(r("Ganancias"))
                    Dim retIva As Decimal = Convert.ToDecimal(r("Retenciva"))
                    Dim ib As Decimal = Convert.ToDecimal(r("IngresosB")) + Convert.ToDecimal(r("IngresosB2")) + Convert.ToDecimal(r("IngresosB3")) + Convert.ToDecimal(r("IngresosB4"))
                    Dim linea As String = New String(" "c, 369)
                    Mid(linea, 1, 1) = "1"
                    Mid(linea, 2, 8) = FechaAAAAMMDD(fecha)
                    Dim tipo As String = "00"
                    If idimp = 6 Then tipo = "88"
                    If idimp = 11 Then tipo = "14"
                    If idimp = 1 AndAlso (idtipoiva = 1 Or idtipoiva = 2) Then tipo = "01"
                    If idimp = 1 AndAlso (idtipoiva = 6 Or idtipoiva = 4) Then tipo = "11"
                    If idimp = 2 AndAlso (idtipoiva = 6) Then tipo = "12"
                    If idimp = 2 AndAlso (idtipoiva = 1) Then tipo = "02"
                    If idimp = 2 AndAlso (idtipoiva = 4) Then tipo = "12"
                    If idimp = 59 AndAlso (idtipoiva = 1) Then tipo = "03"
                    If idimp = 59 AndAlso (idtipoiva = 6) Then tipo = "13"
                    Mid(linea, 10, 2) = tipo
                    Mid(linea, 13, 4) = "0001"
                    Mid(linea, 17, 20) = nroComp.PadLeft(20, "0"c)
                    Mid(linea, 37, 8) = FechaAAAAMMDD(fecha)
                    Mid(linea, 45, 3) = "000"
                    Dim nroCuenta = Convert.ToInt32(r("NroCuenta"))
                    If nroCuenta = 6288 Then
                        Mid(linea, 48, 4) = "IC04"
                        Dim jDesp = 6 - nroComp.Length
                        If jDesp < 0 Then jDesp = 0
                        Mid(linea, 52, 6) = New String("0"c, jDesp) & nroComp
                        Mid(linea, 58, 1) = "A"
                    Else
                        Mid(linea, 48, 4) = New String(" "c, 4)
                        Mid(linea, 52, 6) = New String("0"c, 6)
                        Mid(linea, 58, 1) = " "
                    End If
                    Mid(linea, 59, 2) = "80"
                    Dim cuitNum = cuit.Replace("-", "")
                    Mid(linea, 61, 11) = cuitNum
                    Mid(linea, 72, 30) = nombreProv.PadRight(30)
                    Dim campoMonto = Format(monto, "########.00")
                    Dim maniobraMonto As String = ""
                    For i = 1 To campoMonto.Length
                        If Mid(campoMonto, i, 1) <> "," Then
                            maniobraMonto &= Mid(campoMonto, i, 1)
                        End If
                    Next
                    Dim jMonto = 15 - maniobraMonto.Length
                    If jMonto < 0 Then jMonto = 0
                    Mid(linea, 102, 15) = New String("0"c, jMonto) & maniobraMonto
                    If idimp = 59 Then sumamonto -= monto Else sumamonto += monto
                    Dim campoRni = Format(comprasRNI, "########.00")
                    Dim maniobraRni As String = ""
                    For i = 1 To campoRni.Length
                        If Mid(campoRni, i, 1) <> "," Then
                            maniobraRni &= Mid(campoRni, i, 1)
                        End If
                    Next
                    Dim jRni = 15 - maniobraRni.Length
                    If jRni < 0 Then jRni = 0
                    Mid(linea, 117, 15) = New String("0"c, jRni) & maniobraRni
                    If idimp = 59 Then sumaRNI -= comprasRNI Else sumaRNI += comprasRNI
                    Dim ng = neto105 + neto21 + neto27
                    Dim campoNg = Format(ng, "########.00")
                    Dim maniobraNg As String = ""
                    For i = 1 To campoNg.Length
                        If Mid(campoNg, i, 1) <> "," Then
                            maniobraNg &= Mid(campoNg, i, 1)
                        End If
                    Next
                    Dim jNg = 15 - maniobraNg.Length
                    If jNg < 0 Then jNg = 0
                    Mid(linea, 132, 15) = New String("0"c, jNg) & maniobraNg
                    If idimp = 59 Then sumang -= ng Else sumang += ng
                    Dim alic As String = "0000"
                    If neto105 <> 0 AndAlso neto21 = 0 AndAlso neto27 = 0 Then alic = "1050"
                    If neto21 <> 0 AndAlso neto105 = 0 AndAlso neto27 = 0 Then alic = "2100"
                    If neto27 <> 0 AndAlso neto105 = 0 AndAlso neto21 = 0 Then alic = "2700"
                    If neto27 <> 0 AndAlso neto21 <> 0 Then alic = "2700"
                    If neto21 <> 0 AndAlso neto105 <> 0 Then alic = "2100"
                    Mid(linea, 147, 4) = alic
                    Dim campoIva = Format(iva, "########.00")
                    Dim maniobraIva As String = ""
                    For i = 1 To campoIva.Length
                        If Mid(campoIva, i, 1) <> "," Then
                            maniobraIva &= Mid(campoIva, i, 1)
                        End If
                    Next
                    Dim jIva = 15 - maniobraIva.Length
                    If jIva < 0 Then jIva = 0
                    Mid(linea, 151, 15) = New String("0"c, jIva) & maniobraIva
                    If idimp = 59 Then sumaivari -= iva Else sumaivari += iva
                    Dim campoEx = Format(exento, "########.00")
                    Dim maniobraEx As String = ""
                    For i = 1 To campoEx.Length
                        If Mid(campoEx, i, 1) <> "," Then
                            maniobraEx &= Mid(campoEx, i, 1)
                        End If
                    Next
                    Dim jEx = 15 - maniobraEx.Length
                    If jEx < 0 Then jEx = 0
                    Mid(linea, 166, 15) = New String("0"c, jEx) & maniobraEx
                    If idimp = 59 Then sumae -= exento Else sumae += exento
                    Dim campoRet = Format(retIva, "########.00")
                    Dim maniobraRet As String = ""
                    For i = 1 To campoRet.Length
                        If Mid(campoRet, i, 1) <> "," Then
                            maniobraRet &= Mid(campoRet, i, 1)
                        End If
                    Next
                    Dim jRet = 15 - maniobraRet.Length
                    If jRet < 0 Then jRet = 0
                    Mid(linea, 181, 15) = New String("0"c, jRet) & maniobraRet
                    If idimp = 59 Then sumain -= retIva Else sumain += retIva
                    Dim campoGan = Format(gan, "########.00")
                    Dim maniobraGan As String = ""
                    For i = 1 To campoGan.Length
                        If Mid(campoGan, i, 1) <> "," Then
                            maniobraGan &= Mid(campoGan, i, 1)
                        End If
                    Next
                    Dim jGan = 15 - maniobraGan.Length
                    If jGan < 0 Then jGan = 0
                    Mid(linea, 196, 15) = New String("0"c, jGan) & maniobraGan
                    If idimp = 59 Then sumaga -= gan Else sumaga += gan
                    Dim campoIb = Format(ib, "########.00")
                    Dim maniobraIb As String = ""
                    For i = 1 To campoIb.Length
                        If Mid(campoIb, i, 1) <> "," Then
                            maniobraIb &= Mid(campoIb, i, 1)
                        End If
                    Next
                    Dim jIb = 15 - maniobraIb.Length
                    If jIb < 0 Then jIb = 0
                    Mid(linea, 211, 15) = New String("0"c, jIb) & maniobraIb
                    If idimp = 59 Then sumaib -= ib Else sumaib += ib
                    Mid(linea, 226, 30) = New String("0"c, 30)
                    Mid(linea, 256, 1) = "0"
                    Mid(linea, 257, 1) = idtipoiva.ToString()
                    Mid(linea, 258, 3) = "PES"
                    Mid(linea, 261, 10) = New String("0"c, 10)
                    Mid(linea, 271, 1) = "1"
                    Mid(linea, 272, 1) = " "
                    Mid(linea, 273, 14) = New String("0"c, 14)
                    Mid(linea, 287, 8) = "00000000"
                    Mid(linea, 295, 75) = New String(" "c, 75)
                    sw.WriteLine(linea)
                    sumareg += 1
                Next
                Dim linea2 As String = New String(" "c, 369)
                Mid(linea2, 1, 1) = "2"
                Mid(linea2, 2, 6) = fecho.ToString("yyyyMM")
                Mid(linea2, 8, 10) = New String(" "c, 10)
                Mid(linea2, 18, 12) = sumareg.ToString().PadLeft(12, "0"c)
                Mid(linea2, 30, 31) = New String(" "c, 31)
                Mid(linea2, 61, 11) = "30677018816"
                Mid(linea2, 72, 30) = New String(" "c, 30)
                Dim campoSumMonto = Format(sumamonto, "########.00")
                Dim maniobraSumMonto As String = ""
                For i = 1 To campoSumMonto.Length
                    If Mid(campoSumMonto, i, 1) <> "," Then
                        maniobraSumMonto &= Mid(campoSumMonto, i, 1)
                    End If
                Next
                Dim jSumMonto = 15 - maniobraSumMonto.Length
                If jSumMonto < 0 Then jSumMonto = 0
                Mid(linea2, 102, 15) = New String("0"c, jSumMonto) & maniobraSumMonto
                Dim campoSumRni = Format(sumaRNI, "########.00")
                Dim maniobraSumRni As String = ""
                For i = 1 To campoSumRni.Length
                    If Mid(campoSumRni, i, 1) <> "," Then
                        maniobraSumRni &= Mid(campoSumRni, i, 1)
                    End If
                Next
                Dim jSumRni = 15 - maniobraSumRni.Length
                If jSumRni < 0 Then jSumRni = 0
                Mid(linea2, 117, 15) = New String("0"c, jSumRni) & maniobraSumRni
                Dim campoSumNg = Format(sumang, "########.00")
                Dim maniobraSumNg As String = ""
                For i = 1 To campoSumNg.Length
                    If Mid(campoSumNg, i, 1) <> "," Then
                        maniobraSumNg &= Mid(campoSumNg, i, 1)
                    End If
                Next
                Dim jSumNg = 15 - maniobraSumNg.Length
                If jSumNg < 0 Then jSumNg = 0
                Mid(linea2, 132, 15) = New String("0"c, jSumNg) & maniobraSumNg
                Mid(linea2, 147, 4) = New String(" "c, 4)
                Dim campoSumIva = Format(sumaivari, "########.00")
                Dim maniobraSumIva As String = ""
                For i = 1 To campoSumIva.Length
                    If Mid(campoSumIva, i, 1) <> "," Then
                        maniobraSumIva &= Mid(campoSumIva, i, 1)
                    End If
                Next
                Dim jSumIva = 15 - maniobraSumIva.Length
                If jSumIva < 0 Then jSumIva = 0
                Mid(linea2, 151, 15) = New String("0"c, jSumIva) & maniobraSumIva
                Dim campoSumE = Format(sumae, "########.00")
                Dim maniobraSumE As String = ""
                For i = 1 To campoSumE.Length
                    If Mid(campoSumE, i, 1) <> "," Then
                        maniobraSumE &= Mid(campoSumE, i, 1)
                    End If
                Next
                Dim jSumE = 15 - maniobraSumE.Length
                If jSumE < 0 Then jSumE = 0
                Mid(linea2, 166, 15) = New String("0"c, jSumE) & maniobraSumE
                Dim campoSumIn = Format(sumain, "########.00")
                Dim maniobraSumIn As String = ""
                For i = 1 To campoSumIn.Length
                    If Mid(campoSumIn, i, 1) <> "," Then
                        maniobraSumIn &= Mid(campoSumIn, i, 1)
                    End If
                Next
                Dim jSumIn = 15 - maniobraSumIn.Length
                If jSumIn < 0 Then jSumIn = 0
                Mid(linea2, 181, 15) = New String("0"c, jSumIn) & maniobraSumIn
                Dim campoSumGa = Format(sumaga, "########.00")
                Dim maniobraSumGa As String = ""
                For i = 1 To campoSumGa.Length
                    If Mid(campoSumGa, i, 1) <> "," Then
                        maniobraSumGa &= Mid(campoSumGa, i, 1)
                    End If
                Next
                Dim jSumGa = 15 - maniobraSumGa.Length
                If jSumGa < 0 Then jSumGa = 0
                Mid(linea2, 196, 15) = New String("0"c, jSumGa) & maniobraSumGa
                Dim campoSumIb = Format(sumaib, "########.00")
                Dim maniobraSumIb As String = ""
                For i = 1 To campoSumIb.Length
                    If Mid(campoSumIb, i, 1) <> "," Then
                        maniobraSumIb &= Mid(campoSumIb, i, 1)
                    End If
                Next
                Dim jSumIb = 15 - maniobraSumIb.Length
                If jSumIb < 0 Then jSumIb = 0
                Mid(linea2, 211, 15) = New String("0"c, jSumIb) & maniobraSumIb
                Mid(linea2, 226, 30) = New String("0"c, 30)
                Mid(linea2, 256, 114) = New String(" "c, 114)
                sw.WriteLine(linea2)
            End Using
        End Using
        MessageBox.Show("Archivo Decreto 1361 generado.")
    End Sub

    Private Sub GenerarLibroIvaDigital()
        Dim m = Convert.ToInt32(dbcMeses.SelectedValue)
        Dim y = Convert.ToInt32(txtAno.Text)
        DSM.Execute(DSM.Proveedores, "Update WDetactacte Set NRODESPACHO = 0 Where (NRODESPACHO Is Null);", Nothing, True)
        Dim sql = New StringBuilder()
        sql.Append("SELECT MaeCtaCte.NroCuenta, MaeCtaCte.Nombre, MaeCtaCte.Cuit, MaeCtaCte.IdTipoIva, WDetaCtaCte.NroFactura, WDetaCtaCte.NroComprobante, WDetaCtaCte.NombreComprobante, WDetaCtaCte.Fecha, WDetaCtaCte.IdImputacion, WDetaCtaCte.Monto, WDetaCtaCte.ComprasRNI, WDetaCtaCte.Neto105, WDetaCtaCte.Neto21, WDetaCtaCte.Neto27, WDetaCtaCte.Exento, WDetaCtaCte.IVA, WDetaCtaCte.Ganancias, WDetaCtaCte.Retenciva, WDetaCtaCte.IngresosB, WDetaCtaCte.IngresosB2, WDetaCtaCte.IngresosB3, WDetaCtaCte.IngresosB4, wdetactacte.nrodespacho FROM WDetaCtaCte INNER JOIN MaeCtaCte ON WDetaCtaCte.NroCuenta = MaeCtaCte.NroCuenta where (Datepart(m, [WDetaCtaCte].[fecha]) = ")
        sql.Append(m)
        sql.Append(") AND (Datepart(yyyy, [WDetaCtaCte].[fecha]) = ")
        sql.Append(y)
        sql.Append(") and (maectacte.nrocuenta <> 8100 and maectacte.nrocuenta <> 6647 and maectacte.nrocuenta <> 5654 and maectacte.nrocuenta <> 6112 and maectacte.nrocuenta <> 9930) and (WDetaCtaCte.IdImputacion  in(1,11, 2, 59)) and WDetaCtaCte.IVA > 0")
        Dim t = DSM.ExecuteQuery(DSM.Proveedores, sql.ToString())
        IO.Directory.CreateDirectory("c:\RG 3685")
        Dim nombreCbte = "LIBRO_IVA_DIGITAL_COMPRAS_CBTE" & txtAno.Text.Trim() & m.ToString() & ".txt"
        Dim rutaCbte = IO.Path.Combine("c:\RG 3685", nombreCbte)
        Dim sumang As Double = 0
        Dim sumareg As Double = 0
        Dim sumamonto As Double = 0
        Dim sumaRNI As Double = 0
        Dim sumaivari As Double = 0
        Dim sumae As Double = 0
        Dim sumaib As Double = 0
        Dim sumain As Double = 0
        Dim sumaga As Double = 0
        Using sw As New IO.StreamWriter(rutaCbte, False, Encoding.ASCII)
            For Each r As DataRow In t.Rows
                Dim reg As String = New String(" "c, 325)
                Dim fecha = Convert.ToDateTime(r("Fecha"))
                Dim idimp = Convert.ToInt32(r("IdImputacion"))
                Dim idtipoiva = Convert.ToInt32(r("IdTipoIva"))
                Dim tipo As String = "001"
                If idimp = 6 Then tipo = "089"
                If idimp = 11 Then tipo = "066"
                If idimp = 1 AndAlso (idtipoiva = 1 Or idtipoiva = 2) Then tipo = "001"
                If idimp = 1 AndAlso (idtipoiva = 6 Or idtipoiva = 4) Then tipo = "011"
                If idimp = 2 AndAlso idtipoiva = 6 Then tipo = "012"
                If idimp = 2 AndAlso idtipoiva = 1 Then tipo = "002"
                If idimp = 2 AndAlso idtipoiva = 4 Then tipo = "012"
                If idimp = 59 AndAlso idtipoiva = 1 Then tipo = "003"
                If idimp = 59 AndAlso idtipoiva = 6 Then tipo = "013"
                Mid(reg, 1, 8) = fecha.ToString("yyyyMMdd")
                Mid(reg, 9, 3) = tipo
                Dim nroCuenta = Convert.ToInt32(r("NroCuenta"))
                Dim nroComp = r("NroComprobante").ToString()
                If nroCuenta = 6288 Then
                    Mid(reg, 12, 5) = "00000"
                    Mid(reg, 17, 20) = New String("0"c, 20)
                Else
                    Mid(reg, 12, 5) = "00001"
                    Dim j As Integer = 20 - nroComp.Length
                    If j < 0 Then j = 0
                    Dim ceros As String = New String("0"c, j)
                    Mid(reg, 17, 20) = ceros & nroComp
                End If
                If nroCuenta = 6288 Then
                    Dim nroDesp = If(IsDBNull(r("nrodespacho")), "", r("nrodespacho").ToString())
                    Dim j As Integer = 16 - nroDesp.Length
                    If j < 0 Then j = 0
                    Dim ceros As String = New String("0"c, j)
                    Mid(reg, 37, 16) = ceros & nroDesp
                Else
                    Mid(reg, 37, 16) = New String(" "c, 16)
                End If
                Mid(reg, 53, 2) = "80"
                Mid(reg, 55, 20) = "000000000" & Mid(r("Cuit").ToString(), 1, 2) & Mid(r("Cuit").ToString(), 4, 8) & Mid(r("Cuit").ToString(), 13, 1)
                Mid(reg, 75, 30) = Mid(r("Nombre").ToString(), 1, 30)
                Dim iva = Convert.ToDecimal(r("IVA"))
                Dim monto = Convert.ToDecimal(r("Monto"))
                If iva = monto Then monto = iva / 0.21D + iva
                If nroCuenta = 6288 Then monto = iva / 0.21D + iva + Convert.ToDecimal(r("Ganancias")) + Convert.ToDecimal(r("Retenciva")) + Convert.ToDecimal(r("IngresosB")) + Convert.ToDecimal(r("ComprasRNI")) + Convert.ToDecimal(r("Exento"))
                Dim campo As String
                Dim maniobra As String = ""
                campo = Format(monto, "########.00")
                For i = 1 To campo.Length
                    If Mid(campo, i, 1) <> "," Then
                        maniobra &= Mid(campo, i, 1)
                    End If
                Next
                Dim jMonto As Integer = 15 - maniobra.Length
                If jMonto < 0 Then jMonto = 0
                Dim cerosMonto As String = New String("0"c, jMonto)
                Mid(reg, 105, 15) = cerosMonto & maniobra
                If idimp = 59 Then
                    sumamonto = sumamonto - Val(Mid(reg, 105, 15))
                Else
                    sumamonto = sumamonto + Val(Mid(reg, 105, 15))
                End If
                Dim comprasRNI As Decimal = If(New Integer() {2, 4, 6}.Contains(Convert.ToInt32(r("IdTipoIva"))), 0D, Convert.ToDecimal(r("ComprasRNI")))
                maniobra = ""
                campo = If(comprasRNI = 0D, "000000000000000", Format(comprasRNI, "########.00"))
                For i = 1 To campo.Length
                    If Mid(campo, i, 1) <> "," Then
                        maniobra &= Mid(campo, i, 1)
                    End If
                Next
                Dim jRni As Integer = 15 - maniobra.Length
                If jRni < 0 Then jRni = 0
                Mid(reg, 120, 15) = New String("0"c, jRni) & maniobra
                If idimp = 59 Then
                    sumaRNI = sumaRNI - Val(Mid(reg, 120, 15))
                Else
                    sumaRNI = sumaRNI + Val(Mid(reg, 120, 15))
                End If
                maniobra = ""
                campo = Format(iva, "########.00")
                For i = 1 To campo.Length
                    If Mid(campo, i, 1) <> "," Then
                        maniobra &= Mid(campo, i, 1)
                    End If
                Next
                Dim jIva As Integer = 15 - maniobra.Length
                If jIva < 0 Then jIva = 0
                Mid(reg, 240, 15) = New String("0"c, jIva) & maniobra
                If idimp = 59 Then
                    sumaivari = sumaivari - Val(Mid(reg, 240, 15))
                Else
                    sumaivari = sumaivari + Val(Mid(reg, 240, 15))
                End If
                maniobra = ""
                campo = Format(Convert.ToDecimal(r("Exento")), "########.00")
                For i = 1 To campo.Length
                    If Mid(campo, i, 1) <> "," Then
                        maniobra &= Mid(campo, i, 1)
                    End If
                Next
                Dim jEx As Integer = 15 - maniobra.Length
                If jEx < 0 Then jEx = 0
                Mid(reg, 135, 15) = New String("0"c, jEx) & maniobra
                maniobra = ""
                campo = Format(Convert.ToDecimal(r("Retenciva")), "########.00")
                For i = 1 To campo.Length
                    If Mid(campo, i, 1) <> "," Then
                        maniobra &= Mid(campo, i, 1)
                    End If
                Next
                Dim jRetIva As Integer = 15 - maniobra.Length
                If jRetIva < 0 Then jRetIva = 0
                Mid(reg, 150, 15) = New String("0"c, jRetIva) & maniobra
                If idimp = 59 Then
                    sumain = sumain - Val(Mid(reg, 150, 15))
                Else
                    sumain = sumain + Val(Mid(reg, 150, 15))
                End If
                maniobra = ""
                campo = Format(Convert.ToDecimal(r("Ganancias")), "########.00")
                For i = 1 To campo.Length
                    If Mid(campo, i, 1) <> "," Then
                        maniobra &= Mid(campo, i, 1)
                    End If
                Next
                Dim jGan As Integer = 15 - maniobra.Length
                If jGan < 0 Then jGan = 0
                Mid(reg, 165, 15) = New String("0"c, jGan) & maniobra
                If idimp = 59 Then
                    sumaga = sumaga - Val(Mid(reg, 165, 15))
                Else
                    sumaga = sumaga + Val(Mid(reg, 165, 15))
                End If
                Dim ib As Decimal = Convert.ToDecimal(r("IngresosB")) + Convert.ToDecimal(r("IngresosB2")) + Convert.ToDecimal(r("IngresosB3")) + Convert.ToDecimal(r("IngresosB4"))
                maniobra = ""
                campo = Format(ib, "########.00")
                For i = 1 To campo.Length
                    If Mid(campo, i, 1) <> "," Then
                        maniobra &= Mid(campo, i, 1)
                    End If
                Next
                Dim jIb As Integer = 15 - maniobra.Length
                If jIb < 0 Then jIb = 0
                Mid(reg, 180, 15) = New String("0"c, jIb) & maniobra
                If idimp = 59 Then
                    sumaib = sumaib - Val(Mid(reg, 180, 15))
                Else
                    sumaib = sumaib + Val(Mid(reg, 180, 15))
                End If
                Mid(reg, 195, 30) = New String("0"c, 30)
                Mid(reg, 225, 3) = "PES"
                Mid(reg, 228, 10) = "0001000000"
                Dim neto105 = Convert.ToDecimal(r("Neto105"))
                Dim neto21 = Convert.ToDecimal(r("Neto21"))
                Dim neto27 = Convert.ToDecimal(r("Neto27"))
                Dim flag As String = "0"
                If neto21 = 0 AndAlso neto105 = 0 AndAlso neto27 = 0 AndAlso Convert.ToInt32(r("IdTipoIva")) = 1 Then flag = "1" Else flag = "0"
                If neto21 = 0 AndAlso neto105 > 0 AndAlso neto27 = 0 Then flag = "1"
                If neto21 = 0 AndAlso neto105 = 0 AndAlso neto27 > 0 Then flag = "1"
                If neto21 > 0 AndAlso neto105 = 0 AndAlso neto27 = 0 Then flag = "1"
                If neto21 > 0 AndAlso neto105 > 0 AndAlso neto27 = 0 Then flag = "2"
                If neto21 > 0 AndAlso neto105 = 0 AndAlso neto27 > 0 Then flag = "2"
                If neto21 = 0 AndAlso neto105 > 0 AndAlso neto27 > 0 Then flag = "2"
                If neto21 > 0 AndAlso neto105 > 0 AndAlso neto27 > 0 Then flag = "3"
                If Convert.ToInt32(r("IdTipoIva")) > 3 AndAlso Convert.ToInt32(r("IdTipoIva")) < 7 Then flag = "0"
                If flag = "0" AndAlso Convert.ToInt32(r("IdTipoIva")) = 1 AndAlso Convert.ToDecimal(r("Exento")) > 0 Then flag = "1"
                If neto105 + neto21 + neto27 + iva + Convert.ToDecimal(r("Exento")) + comprasRNI = 0 Then flag = "1"
                Mid(reg, 238, 1) = If(neto105 + neto21 + neto27 = 0 OrElse Convert.ToInt32(r("NroCuenta")) = 6288, "E", " ")
                Mid(reg, 237, 1) = flag
                Mid(reg, 254, 26) = New String("0"c, 26)
                Mid(reg, 310, 15) = New String("0"c, 15)
                Mid(reg, 280, 30) = New String(" "c, 30)
                sw.WriteLine(reg)
            Next
        End Using
        Dim nombreAli = "LIBRO_IVA_DIGITAL_COMPRAS_ALICUOTAS" & txtAno.Text.Trim() & m.ToString() & ".txt"
        Dim rutaAli = IO.Path.Combine("c:\RG 3685", nombreAli)
        Using sw As New IO.StreamWriter(rutaAli, False, Encoding.ASCII)
            For Each r As DataRow In t.Rows
                Dim nroCuenta = Convert.ToInt32(r("NroCuenta"))
                If nroCuenta <> 6288 Then
                    Dim reg2 As String = New String(" "c, 84)
                    Dim idimp = Convert.ToInt32(r("IdImputacion"))
                    Dim idtipoiva = Convert.ToInt32(r("IdTipoIva"))
                    Dim tipo As String = "001"
                    If idimp = 6 Then tipo = "089"
                    If idimp = 11 Then tipo = "066"
                    If idimp = 1 AndAlso (idtipoiva = 1 Or idtipoiva = 2) Then tipo = "001"
                    If idimp = 1 AndAlso (idtipoiva = 6 Or idtipoiva = 4) Then tipo = "011"
                    If idimp = 2 AndAlso idtipoiva = 6 Then tipo = "012"
                    If idimp = 2 AndAlso idtipoiva = 1 Then tipo = "002"
                    If idimp = 2 AndAlso idtipoiva = 4 Then tipo = "012"
                    If idimp = 59 AndAlso idtipoiva = 1 Then tipo = "003"
                    If idimp = 59 AndAlso idtipoiva = 6 Then tipo = "013"
                    Mid(reg2, 1, 3) = tipo
                    Mid(reg2, 4, 5) = "00001"
                    Mid(reg2, 9, 20) = r("NroComprobante").ToString().PadLeft(20, "0"c)
                    Mid(reg2, 29, 2) = "80"
                    Dim cuit = r("Cuit").ToString().Replace("-", "")
                    Mid(reg2, 31, 20) = ("000000000" & cuit).Substring(0, 20)
                    Dim neto21 = Convert.ToDecimal(r("Neto21"))
                    Dim neto105 = Convert.ToDecimal(r("Neto105"))
                    Dim neto27 = Convert.ToDecimal(r("Neto27"))
                    Dim iva = Convert.ToDecimal(r("IVA"))
                    Dim exento = Convert.ToDecimal(r("Exento"))
                    Dim comprasRni = Convert.ToDecimal(r("ComprasRNI"))

                    If neto21 + neto105 + neto27 = 0 Then
                        Dim campoBi = Format(iva / 0.21D, "########.00")
                        Dim maniobraBi As String = ""
                        For i = 1 To campoBi.Length
                            If Mid(campoBi, i, 1) <> "," Then
                                maniobraBi &= Mid(campoBi, i, 1)
                            End If
                        Next
                        Dim jBi = 15 - maniobraBi.Length
                        If jBi < 0 Then jBi = 0
                        Mid(reg2, 51, 15) = New String("0"c, jBi) & maniobraBi

                        If exento > 0 OrElse (comprasRni > 0 AndAlso iva = 0) Then
                            Mid(reg2, 66, 4) = "0003"
                        Else
                            Mid(reg2, 66, 4) = "0005"
                        End If
                        If (neto105 + neto21 + neto27 + iva + exento + comprasRni) = 0 Then
                            Mid(reg2, 66, 4) = "0003"
                        End If

                        Dim campoIva2 = Format(iva, "########.00")
                        Dim maniobraIva2 As String = ""
                        For i = 1 To campoIva2.Length
                            If Mid(campoIva2, i, 1) <> "," Then
                                maniobraIva2 &= Mid(campoIva2, i, 1)
                            End If
                        Next
                        Dim jIva2 = 15 - maniobraIva2.Length
                        If jIva2 < 0 Then jIva2 = 0
                        Mid(reg2, 70, 15) = New String("0"c, jIva2) & maniobraIva2
                        sw.WriteLine(reg2)
                    End If

                    If neto21 > 0 Then
                        Dim reg21 As String = reg2
                        Dim campo21 = Format(neto21, "########.00")
                        Dim maniobra21 As String = ""
                        For i = 1 To campo21.Length
                            If Mid(campo21, i, 1) <> "," Then
                                maniobra21 &= Mid(campo21, i, 1)
                            End If
                        Next
                        Dim j21 = 15 - maniobra21.Length
                        If j21 < 0 Then j21 = 0
                        Mid(reg21, 51, 15) = New String("0"c, j21) & maniobra21
                        Mid(reg21, 66, 4) = "0005"
                        Dim campo21iva = Format(neto21 * 0.21D, "########.00")
                        Dim maniobra21iva As String = ""
                        For i = 1 To campo21iva.Length
                            If Mid(campo21iva, i, 1) <> "," Then
                                maniobra21iva &= Mid(campo21iva, i, 1)
                            End If
                        Next
                        Dim j21iva = 15 - maniobra21iva.Length
                        If j21iva < 0 Then j21iva = 0
                        Mid(reg21, 70, 15) = New String("0"c, j21iva) & maniobra21iva
                        If Mid(reg21, 66, 1) = " " Then
                            Mid(reg21, 66, 4) = "0005"
                        End If
                        sw.WriteLine(reg21)
                    End If

                    If neto105 > 0 Then
                        Dim reg105 As String = reg2
                        Dim campo105 = Format(neto105, "########.00")
                        Dim maniobra105 As String = ""
                        For i = 1 To campo105.Length
                            If Mid(campo105, i, 1) <> "," Then
                                maniobra105 &= Mid(campo105, i, 1)
                            End If
                        Next
                        Dim j105 = 15 - maniobra105.Length
                        If j105 < 0 Then j105 = 0
                        Mid(reg105, 51, 15) = New String("0"c, j105) & maniobra105
                        Mid(reg105, 66, 4) = "0004"
                        Dim campo105iva = Format(neto105 * 0.105D, "########.00")
                        Dim maniobra105iva As String = ""
                        For i = 1 To campo105iva.Length
                            If Mid(campo105iva, i, 1) <> "," Then
                                maniobra105iva &= Mid(campo105iva, i, 1)
                            End If
                        Next
                        Dim j105iva = 15 - maniobra105iva.Length
                        If j105iva < 0 Then j105iva = 0
                        Mid(reg105, 70, 15) = New String("0"c, j105iva) & maniobra105iva
                        If Mid(reg105, 66, 1) = " " Then
                            Mid(reg105, 66, 4) = "0005"
                        End If
                        sw.WriteLine(reg105)
                    End If

                    If neto27 > 0 Then
                        Dim reg27 As String = reg2
                        Dim campo27 = Format(neto27, "########.00")
                        Dim maniobra27 As String = ""
                        For i = 1 To campo27.Length
                            If Mid(campo27, i, 1) <> "," Then
                                maniobra27 &= Mid(campo27, i, 1)
                            End If
                        Next
                        Dim j27 = 15 - maniobra27.Length
                        If j27 < 0 Then j27 = 0
                        Mid(reg27, 51, 15) = New String("0"c, j27) & maniobra27
                        Mid(reg27, 66, 4) = "0006"
                        Dim campo27iva = Format(neto27 * 0.27D, "########.00")
                        Dim maniobra27iva As String = ""
                        For i = 1 To campo27iva.Length
                            If Mid(campo27iva, i, 1) <> "," Then
                                maniobra27iva &= Mid(campo27iva, i, 1)
                            End If
                        Next
                        Dim j27iva = 15 - maniobra27iva.Length
                        If j27iva < 0 Then j27iva = 0
                        Mid(reg27, 70, 15) = New String("0"c, j27iva) & maniobra27iva
                        If Mid(reg27, 66, 1) = " " Then
                            Mid(reg27, 66, 4) = "0005"
                        End If
                        sw.WriteLine(reg27)
                    End If
                End If
            Next
        End Using
        Dim nombreImp = "LIBRO_IVA_DIGITAL_COMPRAS_IMPORTACIONES" & txtAno.Text.Trim() & m.ToString() & ".txt"
        Dim rutaImp = IO.Path.Combine("c:\RG 3685", nombreImp)
        Using sw As New IO.StreamWriter(rutaImp, False, Encoding.ASCII)
            For Each r As DataRow In t.Rows
                Dim nroCuenta = Convert.ToInt32(r("NroCuenta"))
                If nroCuenta = 6288 Then
                    Dim reg3 As String = New String(" "c, 50)
                    Dim nroDesp = If(IsDBNull(r("nrodespacho")), "", r("nrodespacho").ToString())
                    Mid(reg3, 1, 16) = nroDesp.PadLeft(16, "0"c)
                    Dim iva = Convert.ToDecimal(r("IVA"))
                    Dim neto21 = iva / 0.21D
                    Dim campoN21 = Format(neto21, "########.00")
                    Dim maniobraN21 As String = ""
                    For i = 1 To campoN21.Length
                        If Mid(campoN21, i, 1) <> "," Then
                            maniobraN21 &= Mid(campoN21, i, 1)
                        End If
                    Next
                    Dim jN21 = 15 - maniobraN21.Length
                    If jN21 < 0 Then jN21 = 0
                    Mid(reg3, 17, 15) = New String("0"c, jN21) & maniobraN21
                    Mid(reg3, 32, 4) = "0005"
                    Dim campoIvaImp = Format(iva, "########.00")
                    Dim maniobraIvaImp As String = ""
                    For i = 1 To campoIvaImp.Length
                        If Mid(campoIvaImp, i, 1) <> "," Then
                            maniobraIvaImp &= Mid(campoIvaImp, i, 1)
                        End If
                    Next
                    Dim jIvaImp = 15 - maniobraIvaImp.Length
                    If jIvaImp < 0 Then jIvaImp = 0
                    Mid(reg3, 36, 15) = New String("0"c, jIvaImp) & maniobraIvaImp
                    sw.WriteLine(reg3)
                End If
            Next
        End Using
        MessageBox.Show("Archivos Libro IVA Digital generados.")
    End Sub

End Class
