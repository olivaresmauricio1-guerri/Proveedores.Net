Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmBuscaFactura
    Inherits Form
    Private Shared instancia As frmBuscaFactura
    Private dtResult As DataTable
    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmBuscaFactura()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmBuscaFactura_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Private Sub frmBuscaFactura_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        optCorriente.Checked = True
        BuscaFactura()
        ConfigurarEstiloGrid(DgvBusca)
        ConfigurarGrid()
    End Sub
    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Close()
    End Sub

    Private Sub lnkCopiar_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkCopiar.LinkClicked
        CopiarDataGrid(DgvBusca, chkEncabezados.Checked)
    End Sub

    Private Sub TxtBuscar_TextChanged(sender As Object, e As EventArgs) Handles TxtBuscar.TextChanged
        BuscaFactura(TxtBuscar.Text.Trim())
    End Sub
    Private Sub optCorriente_CheckedChanged(sender As Object, e As EventArgs) Handles optCorriente.CheckedChanged
        BuscaFactura(TxtBuscar.Text.Trim())
    End Sub

    Private Sub optAnual_CheckedChanged(sender As Object, e As EventArgs) Handles optAnual.CheckedChanged
        If TxtBuscar.Text.Trim() <> "" Then
            BuscaFactura(TxtBuscar.Text.Trim())
        Else
            BuscaFactura()
        End If
    End Sub
    Private Sub BuscaFactura(Optional filtro As String = "")

        Dim tabla As String = If(optCorriente.Checked, "DetaCtaCte", "DetaCtaCteAnual")
        Dim nFactura As Integer
        Dim fecha As DateTime
        Dim sql As String
        Dim parametros As Dictionary(Of String, Object) = Nothing

        sql = $"SELECT TOP (100) " &
              "FondoFijo, NroCuenta, NroFactura, Monto, NroComprobante, " &
              "NombreComprobante, Condicion, Fecha, IdImputacion, CtaMonto, " &
              "ComprasRNI, CtaRNI, Neto105, CtaNeto105, Neto21, Cta21, " &
              "Neto27, Cta27, Exento, CtaExento, IVA, CtaIva, Ganancias, " &
              "CtaGanancia, Retenciva, CtaRetencion, IngresosB, CtaIB, " &
              "IngresosB2, CtaIB2, IngresosB3, CtaIB3, IngresosB4, CtaIB4, " &
              "IngresosB5, CtaIB5, IngresosB6, CtaIB6, ACuenta, FechaVto, " &
              "TipoValor, NroCheque, RegInterno, Sucursal, Cobrado, Anterior, " &
              "Marcado, Rubro, Comentario " &
              $"FROM {tabla}"

        Dim condiciones As New List(Of String)

        If filtro <> "" Then

            ' Buscar por número de factura/comprobante
            If Integer.TryParse(filtro, nFactura) AndAlso nFactura > 0 Then

                condiciones.Add("(NroFactura = @val OR NroComprobante = @val)")
                parametros = CmdParams("@val", nFactura)

                ' Buscar por fecha
            ElseIf DateTime.TryParse(filtro, fecha) Then

                condiciones.Add("Fecha >= @fecha AND Fecha < DATEADD(DAY, 1, @fecha)")
                parametros = CmdParams("@fecha", fecha.Date)

                ' Buscar por tipo de comprobante
            Else

                condiciones.Add("NombreComprobante LIKE @val")
                parametros = CmdParams("@val", "%" & filtro & "%")

            End If

        End If

        If condiciones.Count > 0 Then
            sql &= " WHERE " & String.Join(" AND ", condiciones)
        End If

        sql &= " ORDER BY Fecha DESC"

        dtResult = DSM.ExecuteQuery(DSM.Proveedores, sql, parametros)

        DgvBusca.DataSource = dtResult
        ConfigurarGrid()

    End Sub
    Private Sub ConfigurarGrid()

        If DgvBusca.Columns.Count = 0 Then Return

        ' =========================================================
        ' DATOS PRINCIPALES
        ' =========================================================

        ConfigurarColumna("FondoFijo", "Fondo Fijo", 80)
        ConfigurarColumna("NroCuenta", "Nro Cuenta", 80)
        ConfigurarColumna("NroFactura", "Nro Factura", 90)
        ConfigurarColumna("Monto", "Monto", 100, True)
        ConfigurarColumna("NroComprobante", "Nro Comprob.", 100)
        ConfigurarColumna("NombreComprobante", "Comprobante", 130)
        ConfigurarColumna("Condicion", "Condición", 80)
        ConfigurarColumna("Fecha", "Fecha", 80)

        ' =========================================================
        ' IMPUTACIÓN
        ' =========================================================

        ConfigurarColumna("IdImputacion", "Imputación", 80)
        ConfigurarColumna("CtaMonto", "Cta. Monto", 80)

        ' =========================================================
        ' IVA / NETOS
        ' =========================================================

        ConfigurarColumna("ComprasRNI", "Compras RNI", 90, True)
        ConfigurarColumna("CtaRNI", "Cta. RNI", 75)

        ConfigurarColumna("Neto105", "Neto 10,5%", 90, True)
        ConfigurarColumna("CtaNeto105", "Cta. Neto 10,5", 90)

        ConfigurarColumna("Neto21", "Neto 21%", 90, True)
        ConfigurarColumna("Cta21", "Cta. Neto 21", 90)

        ConfigurarColumna("Neto27", "Neto 27%", 90, True)
        ConfigurarColumna("Cta27", "Cta. Neto 27", 90)

        ConfigurarColumna("Exento", "Exento", 90, True)
        ConfigurarColumna("CtaExento", "Cta. Exento", 90)

        ConfigurarColumna("IVA", "IVA", 90, True)
        ConfigurarColumna("CtaIva", "Cta. IVA", 80)

        ' =========================================================
        ' GANANCIAS / RETENCIONES
        ' =========================================================

        ConfigurarColumna("Ganancias", "Ganancias", 90, True)
        ConfigurarColumna("CtaGanancia", "Cta. Ganancia", 90)

        ConfigurarColumna("Retenciva", "Retenc. IVA", 90, True)
        ConfigurarColumna("CtaRetencion", "Cta. Retención", 90)

        ' =========================================================
        ' INGRESOS BRUTOS
        ' =========================================================

        ConfigurarColumna("IngresosB", "Ingresos B.", 90, True)
        ConfigurarColumna("CtaIB", "Cta. IB", 80)

        ConfigurarColumna("IngresosB2", "Ingresos B. 2", 90, True)
        ConfigurarColumna("CtaIB2", "Cta. IB 2", 80)

        ConfigurarColumna("IngresosB3", "Ingresos B. 3", 90, True)
        ConfigurarColumna("CtaIB3", "Cta. IB 3", 80)

        ConfigurarColumna("IngresosB4", "Ingresos B. 4", 90, True)
        ConfigurarColumna("CtaIB4", "Cta. IB 4", 80)

        ConfigurarColumna("IngresosB5", "Ingresos B. 5", 90, True)
        ConfigurarColumna("CtaIB5", "Cta. IB 5", 80)

        ConfigurarColumna("IngresosB6", "Ingresos B. 6", 90, True)
        ConfigurarColumna("CtaIB6", "Cta. IB 6", 80)

        ' =========================================================
        ' OTROS DATOS
        ' =========================================================

        ConfigurarColumna("ACuenta", "A Cuenta", 90, True)
        ConfigurarColumna("FechaVto", "Fecha Vto.", 80)
        ConfigurarColumna("TipoValor", "Tipo Valor", 80)
        ConfigurarColumna("NroCheque", "Nro. Cheque", 90)
        ConfigurarColumna("RegInterno", "Reg. Interno", 80)
        ConfigurarColumna("Sucursal", "Sucursal", 70)

        ConfigurarColumna("Cobrado", "Cobrado", 70)
        ConfigurarColumna("Anterior", "Anterior", 70)
        ConfigurarColumna("Marcado", "Marcado", 70)
        ConfigurarColumna("Rubro", "Rubro", 100)

        ' =========================================================
        ' COMENTARIO
        ' =========================================================

        If DgvBusca.Columns.Contains("Comentario") Then
            With DgvBusca.Columns("Comentario")
                .AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                .HeaderText = "Comentario"
                .MinimumWidth = 150
            End With
        End If

    End Sub

    Private Sub ConfigurarColumna(
        nombre As String,
        encabezado As String,
        ancho As Integer,
        Optional numerica As Boolean = False,
        Optional fecha As Boolean = False)

        If Not DgvBusca.Columns.Contains(nombre) Then Return

        With DgvBusca.Columns(nombre)

            .Width = ancho
            .HeaderText = encabezado

            If numerica Then
                .DefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleRight

                .DefaultCellStyle.Format = "N2"
            End If

            If fecha Then
                .DefaultCellStyle.Format = "dd/MM/yyyy"
            End If

        End With

    End Sub

End Class
