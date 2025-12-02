Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmPlanCuentasSelector
    Private tabla As New DataTable()
    Private filaActual As DataGridViewRow
    Private filaActualIndice As Integer = -1

    Public Property Seleccion As Dictionary(Of String, Object)

    Private Sub frmPlanCuentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        GridBuscar()
        GridConfigurarColumnas()
    End Sub

    Private Sub TxtBuscar_TextChanged(sender As Object, e As EventArgs) Handles TxtBuscar.TextChanged
        GridBuscar()
    End Sub

    Private Sub DgvListado_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvListado.CellClick
        If DgvListado.SelectedRows.Count > 1 Or e.RowIndex < 0 Then
            filaActual = Nothing
            filaActualIndice = -1
            Return
        End If
        filaActualIndice = e.RowIndex
        filaActual = DgvListado.Rows(e.RowIndex)
    End Sub
    Private Sub DgvListado_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvListado.CellDoubleClick
        AceptarSeleccion()
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles CmdAceptar.Click
        AceptarSeleccion()
        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Close()
    End Sub

    Private Sub GridBuscar()
        Dim texto As String = TxtBuscar.Text.Trim()
        Dim sql As String = "SELECT * FROM PlanCuentas WHERE IDSucursal = @Sucursal"
        Dim parametros As New List(Of Object) From {"@Sucursal", General.SucursalActual}

        If Not String.IsNullOrEmpty(texto) Then
            sql &= " AND (Descripcion LIKE @Descripcion OR CodContable LIKE @CodContable)"
            parametros.AddRange(New Object() {"@Descripcion", $"%{texto}%", "@CodContable", $"{texto.Trim()}%"})
        End If

        Dim tabla = DSM.ExecuteQuery(DSM.Contabilidad, sql, CmdParams(parametros.ToArray()))
        DgvListado.DataSource = tabla

        ' si no hay resultados, limpiar la selección
        If tabla.Rows.Count = 0 Then
            filaActualIndice = -1
            filaActual = Nothing
            Return
        End If

        ' Si hay resultados, seleccionar la primera fila
        filaActualIndice = 0
        filaActual = DgvListado.Rows(0)
    End Sub

    Public Sub AceptarSeleccion()
        If filaActual IsNot Nothing Then
            Seleccion = Funciones.DataGridViewRowToDictionary(filaActual)
        End If
    End Sub

    Public Sub GridConfigurarColumnas()
        For Each col As DataGridViewColumn In DgvListado.Columns
            col.Visible = False
        Next

        DgvListado.Columns("CodContable").Visible = True
        DgvListado.Columns("CodContable").HeaderText = "Código Contable"
        DgvListado.Columns("CodContable").Width = 120

        DgvListado.Columns("Descripcion").Visible = True
        DgvListado.Columns("Descripcion").HeaderText = "Descripción"
        DgvListado.Columns("Descripcion").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

        DgvListado.Columns("SaldoHabitual").Visible = True
        DgvListado.Columns("SaldoHabitual").HeaderText = "Saldo Habitual"
        DgvListado.Columns("SaldoHabitual").Width = 100

        DgvListado.Columns("FechaApertura").Visible = True
        DgvListado.Columns("FechaApertura").HeaderText = "Fecha Apertura"
        DgvListado.Columns("FechaApertura").Width = 100
        DgvListado.Columns("FechaApertura").DefaultCellStyle.Format = "dd-MM-yyyy"

        ConfigurarEstiloGrid(DgvListado)

        DgvListado.MultiSelect = False
        DgvListado.SelectionMode = DataGridViewSelectionMode.FullRowSelect

    End Sub
End Class
