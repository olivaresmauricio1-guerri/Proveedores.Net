Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmCierreMes
    Private Shared instancia As frmCierreMes

    ' En VB6 estaba hardcodeada a 31/12/2022 antes de validar.
    ' Si después querés que sea dinámica, la cambiamos acá.
    Private _fechaCierre As Date = New Date(2022, 12, 31)

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmCierreMes()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmCierreMes_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Private Sub frmCierreMes_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Imagen1.Visible = False
        Imagen2.Visible = False
        Imagen3.Visible = False
        Imagen4.Visible = False

        If Me.MdiParent IsNot Nothing Then
            Dim parentClient = Me.MdiParent.ClientRectangle
            Me.Left = CInt((parentClient.Width - Me.Width) / 2)
            Me.Top = CInt((parentClient.Height - Me.Height) / 3)
        End If
    End Sub

    Private Sub cmdSalir_Click(sender As Object, e As EventArgs) Handles cmdSalir.Click
        Close()
    End Sub

    Private Sub cmdCierre_Click(sender As Object, e As EventArgs) Handles cmdCierre.Click

        If General.UsuarioActual <> "ADMIN" Then
            MessageBox.Show("PARA CERRAR MES DEBE SER ADMINISTRADOR....GRACIAS ")
            Return
        End If

        If _fechaCierre = Date.MinValue Then
            MessageBox.Show("Fecha Erronea")
            Return
        End If

        cmdCierre.Enabled = False

        Try
            MostrarPaso(Imagen1)

            ' 1) ¿Hay registros para procesar?
            Dim dtCount = DSM.ExecuteQuery(DSM.Proveedores, "SELECT COUNT(*) AS Cnt FROM DetaCtaCte", Nothing)
            Dim cnt = 0
            If dtCount.Rows.Count > 0 AndAlso Not IsDBNull(dtCount.Rows(0)("Cnt")) Then
                cnt = Convert.ToInt32(dtCount.Rows(0)("Cnt"))
            End If

            If cnt = 0 Then
                MessageBox.Show("No hay registros de Detalle de Proveedores para Procesar...")
                Close()
                Return
            End If

            ' 2) Insert a anual (equivale al loop AddNew del VB6, con las mismas condiciones)
            MostrarPaso(Imagen2)

            Dim sqlInsert As String =
              "INSERT INTO DetaCtaCteAnual (" &
              "  NroCuenta, NroFactura, NroComprobante, NombreComprobante, Condicion, Fecha, IdImputacion," &
              "  Monto, CtaMonto, ComprasRNI, CtaRNI, Neto21, Cta21, Neto105, CtaNeto105, Neto27, Cta27," &
              "  Exento, CtaExento, IVA, CtaIva, Ganancias, CtaGanancia, RetencIva, CtaRetencion," &
              "  IngresosB, CtaIB, IngresosB2, CtaIB2, IngresosB3, CtaIB3, IngresosB4, CtaIB4," &
              "  ACuenta, FechaVto, TipoValor, NroCheque, RegInterno, Sucursal, Cobrado, Anterior," &
              "  Comentario, Rubro, CAI, Dolar, NroDespacho, FondoFijo, PuntoDeVenta" &
              ") " &
              "SELECT " &
              "  NroCuenta, NroFactura, NroComprobante, NombreComprobante, Condicion, Fecha, IdImputacion," &
              "  Monto, CtaMonto, ComprasRNI, CtaRNI, Neto21, Cta21, Neto105, CtaNeto105, Neto27, Cta27," &
              "  Exento, CtaExento, IVA, CtaIva, Ganancias, CtaGanancia, RetencIva, CtaRetencion," &
              "  IngresosB, CtaIB, IngresosB2, CtaIB2, IngresosB3, CtaIB3, IngresosB4, CtaIB4," &
              "  ACuenta, FechaVto, TipoValor, NroCheque, RegInterno, Sucursal, Cobrado, Anterior," &
              "  Comentario, Rubro, CAI, Dolar, NroDespacho, FondoFijo, PuntoDeVenta " &
              "FROM DetaCtaCte " &
              "WHERE " &
              "  (IdImputacion > 50) " &
              "  OR (IdImputacion = 6) " &
              "  OR ((IdImputacion IN (1,2,10,11)) AND (Cobrado = 1))"

            DSM.Execute(DSM.Proveedores, sqlInsert, Nothing, True)

            ' 3) Borro los registros que en VB6 borraba (dos deletes)
            Dim sqlDelete1 As String = "DELETE FROM DetaCtaCte WHERE (IdImputacion > 50) OR (IdImputacion = 6)"
            DSM.Execute(DSM.Proveedores, sqlDelete1, Nothing, True)

            Dim sqlDelete2 As String = "DELETE FROM DetaCtaCte WHERE (IdImputacion IN (1,2,10,11)) AND (Cobrado = 1)"
            DSM.Execute(DSM.Proveedores, sqlDelete2, Nothing, True)

            ' 4) Recalculo UltimoResumen
            Dim sqlUpdateMae As String = "UPDATE MaeCtaCte SET UltimoResumen = SaldoActual"
            DSM.Execute(DSM.Proveedores, sqlUpdateMae, Nothing, True)

            ' 5) Marco los registros como del mes anterior (VB6: Update DetaCtaCte set Anterior=1)
            MostrarPaso(Imagen3)

            Dim sqlAnterior As String = "UPDATE DetaCtaCte SET Anterior = 1"
            DSM.Execute(DSM.Proveedores, sqlAnterior, Nothing, True)

            ' 6) “Configurando Detalles.” (en VB6 no hacía más que mostrar el tilde y luego chequeaba)
            MostrarPaso(Imagen4)

            ' 7) Chequeo equivalente al VB6 (si hay coincidencias en ambas tablas con esas claves, avisa)
            Dim sqlCheck As String =
              "SELECT COUNT(*) AS Cnt " &
              "FROM DetaCtaCte dc " &
              "INNER JOIN DetaCtaCteAnual da ON " &
              "  dc.NroCuenta = da.NroCuenta AND " &
              "  dc.NroFactura = da.NroFactura AND " &
              "  dc.Fecha = da.Fecha AND " &
              "  dc.NroComprobante = da.NroComprobante AND " &
              "  dc.IdImputacion = da.IdImputacion AND " &
              "  dc.Monto = da.Monto"

            Dim dtCheck = DSM.ExecuteQuery(DSM.Proveedores, sqlCheck, Nothing)
            Dim dup = 0
            If dtCheck.Rows.Count > 0 AndAlso Not IsDBNull(dtCheck.Rows(0)("Cnt")) Then
                dup = Convert.ToInt32(dtCheck.Rows(0)("Cnt"))
            End If

            If dup > 0 Then
                MessageBox.Show("CUIDADO !!!!!! No pasaron bien los movimientos al deta anual...")
            End If

            MessageBox.Show("Se ha realizado la tarea de Cierre en forma Satisfactoria.")
            cmdCierre.Enabled = False

        Catch ex As Exception
            MessageBox.Show("No se ha podido terminar con la tarea de cierre.")
            cmdCierre.Enabled = True
        End Try
    End Sub

    Private Sub MostrarPaso(pb As PictureBox)
        pb.Visible = True
        pb.Refresh()
        Application.DoEvents()
    End Sub
End Class
