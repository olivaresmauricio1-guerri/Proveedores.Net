Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmSucursales
    Private tabla As New DataTable()
    Private filaActual As DataGridViewRow
    Private filaActualIndice As Integer = -1
    Private Shared instancia As frmSucursales

    Private Function ObtenerSiguienteCodigo() As Integer
        Dim dt = DSM.ExecuteQuery(DSM.Proveedores, "SELECT ISNULL(MAX(Codigo), 0) + 1 AS Siguiente FROM Sucursales", Nothing)
        If dt Is Nothing OrElse dt.Rows.Count = 0 OrElse IsDBNull(dt.Rows(0)("Siguiente")) Then Return 1
        Return Convert.ToInt32(dt.Rows(0)("Siguiente"))
    End Function

    Private Sub CargarFilaActualEnFormulario()
        If filaActual Is Nothing Then Return
        TxtCodigo.Text = filaActual.Cells("Codigo").Value?.ToString()
        TxtDeposito.Text = filaActual.Cells("Deposito").Value?.ToString()
        TxtDescripcion.Text = filaActual.Cells("Descripcion").Value?.ToString()
        TxtDomicilio.Text = filaActual.Cells("Domicilio").Value?.ToString()
        TxtProvincia.Text = filaActual.Cells("Provincia").Value?.ToString()
        TxtEstablecimiento.Text = filaActual.Cells("Establecimiento").Value?.ToString()
        TxtTimbrado.Text = filaActual.Cells("Timbrado").Value?.ToString()
    End Sub

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmSucursales()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmSucursales_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Private Sub frmSucursales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FormModoConsulta()
        GridBuscar()
        GridConfigurarColumnas()
        Me.KeyPreview = True
    End Sub

    Private Sub TxtBuscar_TextChanged(sender As Object, e As EventArgs) Handles TxtBuscar.TextChanged
        FormModoConsulta()
        FormLimpiarSeleccionado()
        GridBuscar()
    End Sub

    Private Sub DgvListado_KeyDown(sender As Object, e As KeyEventArgs) Handles DgvListado.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.C Then
            CopiarDataGrid(DgvListado, chkEncabezados.Checked)
            e.Handled = True
        End If
    End Sub

    Private Sub DgvListado_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DgvListado.CellClick
        If e.RowIndex >= 0 AndAlso e.RowIndex < DgvListado.Rows.Count Then
            filaActualIndice = e.RowIndex
            filaActual = DgvListado.Rows(filaActualIndice)
            CargarFilaActualEnFormulario()
        End If
    End Sub

    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles CmdAgregar.Click
        filaActual = Nothing
        filaActualIndice = -1
        FormLimpiarSeleccionado()
        FormModoEdicion()
        TxtCodigo.Text = ObtenerSiguienteCodigo().ToString()
        TxtDeposito.Focus()
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        If filaActual Is Nothing Then Return
        FormModoEdicion()
        CargarFilaActualEnFormulario()
    End Sub

    Private Sub CmdBorrar_Click(sender As Object, e As EventArgs) Handles CmdBorrar.Click
        If filaActual Is Nothing Then
            Return
        End If
        Dim codigoStr = filaActual.Cells("Codigo").Value?.ToString()
        Dim codigo As Integer
        If Integer.TryParse(codigoStr, codigo) Then
            Dim sql = "DELETE FROM Sucursales WHERE Codigo = @Codigo"
            Dim p = CmdParams("@Codigo", codigo)
            DSM.Execute(DSM.Proveedores, sql, p, True)
            FormModoConsulta()
            GridBuscar()
        End If
    End Sub

    Private Sub cmdAceptar_Click(sender As Object, e As EventArgs) Handles cmdAceptar.Click
        Dim codigoStr = TxtCodigo.Text.Trim()
        Dim depositoStr = TxtDeposito.Text.Trim()
        Dim descripcion = TxtDescripcion.Text.Trim()
        Dim domicilio = TxtDomicilio.Text.Trim()
        Dim provincia = TxtProvincia.Text.Trim()
        Dim establecimiento = TxtEstablecimiento.Text.Trim()
        Dim timbrado = TxtTimbrado.Text.Trim()

        Dim codigo As Integer
        Dim deposito As Integer
        Dim okCodigo = Integer.TryParse(codigoStr, codigo)
        Dim okDeposito = Integer.TryParse(depositoStr, deposito)

        If Not okCodigo Then
            MessageBox.Show("El código es obligatorio.")
            TxtCodigo.Focus()
            Return
        End If

        If filaActual Is Nothing Then
            Dim sqlExiste = "SELECT COUNT(*) FROM Sucursales WHERE Codigo = @Codigo"
            Dim t = DSM.ExecuteQuery(DSM.Proveedores, sqlExiste, CmdParams("@Codigo", codigo))
            Dim existe = Convert.ToInt32(t.Rows(0)(0))
            If existe > 0 Then
                TxtCodigo.Focus()
                Return
            End If
            Dim sql = "INSERT INTO Sucursales (Codigo, Deposito, Descripcion, Domicilio, Provincia, Establecimiento, Timbrado) VALUES (@Codigo, @Deposito, @Descripcion, @Domicilio, @Provincia, @Establecimiento, @Timbrado)"
            Dim p = CmdParams(
                "@Codigo", If(okCodigo, CType(codigo, Object), DBNull.Value),
                "@Deposito", If(okDeposito, CType(deposito, Object), DBNull.Value),
                "@Descripcion", If(String.IsNullOrWhiteSpace(descripcion), DBNull.Value, descripcion),
                "@Domicilio", If(String.IsNullOrWhiteSpace(domicilio), DBNull.Value, domicilio),
                "@Provincia", If(String.IsNullOrWhiteSpace(provincia), DBNull.Value, provincia),
                "@Establecimiento", If(String.IsNullOrWhiteSpace(establecimiento), DBNull.Value, establecimiento),
                "@Timbrado", If(String.IsNullOrWhiteSpace(timbrado), DBNull.Value, timbrado)
            )
            DSM.Execute(DSM.Proveedores, sql, p, True)
        Else
            Dim codigoOriginal As Integer
            If Not Integer.TryParse(filaActual.Cells("Codigo").Value?.ToString(), codigoOriginal) Then Return

            Dim sql = "UPDATE Sucursales SET Deposito = @Deposito, Descripcion = @Descripcion, Domicilio = @Domicilio, Provincia = @Provincia, Establecimiento = @Establecimiento, Timbrado = @Timbrado WHERE Codigo = @Codigo"
            Dim p = CmdParams(
                "@Deposito", If(okDeposito, CType(deposito, Object), DBNull.Value),
                "@Descripcion", If(String.IsNullOrWhiteSpace(descripcion), DBNull.Value, descripcion),
                "@Domicilio", If(String.IsNullOrWhiteSpace(domicilio), DBNull.Value, domicilio),
                "@Provincia", If(String.IsNullOrWhiteSpace(provincia), DBNull.Value, provincia),
                "@Establecimiento", If(String.IsNullOrWhiteSpace(establecimiento), DBNull.Value, establecimiento),
                "@Timbrado", If(String.IsNullOrWhiteSpace(timbrado), DBNull.Value, timbrado),
                "@Codigo", codigoOriginal
            )
            DSM.Execute(DSM.Proveedores, sql, p, True)
        End If

        FormModoConsulta()
        GridBuscar()
    End Sub

    Private Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles CmdCancelar.Click
        FormModoConsulta()
        FormLimpiarSeleccionado()
        GridBuscar()
    End Sub

    Private Sub lnkCopiar_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkCopiar.LinkClicked
        CopiarDataGrid(DgvListado, chkEncabezados.Checked)
    End Sub

    Private Sub GridBuscar()
        Dim texto = TxtBuscar.Text.Trim()
        Dim sql = "SELECT Codigo, Deposito, Descripcion, Domicilio, Provincia, Establecimiento, Timbrado FROM Sucursales WHERE (@t = '' OR Descripcion LIKE '%' + @t + '%' OR CAST(Codigo AS VARCHAR(20)) LIKE '%' + @t + '%') ORDER BY Descripcion"
        tabla = DSM.ExecuteQuery(DSM.Proveedores, sql, CmdParams("@t", texto))
        DgvListado.DataSource = tabla
    End Sub

    Private Sub GridConfigurarColumnas()

        If DgvListado Is Nothing OrElse DgvListado.Columns.Count = 0 Then Return

        For Each col As DataGridViewColumn In DgvListado.Columns
            col.Visible = False
        Next


        If DgvListado.Columns.Contains("Codigo") Then
            DgvListado.Columns("Codigo").Visible = True
            DgvListado.Columns("Codigo").HeaderText = "Codigo"
            DgvListado.Columns("Codigo").Width = 60
            DgvListado.Columns("Codigo").ReadOnly = True
        End If

        If DgvListado.Columns.Contains("Deposito") Then
            DgvListado.Columns("Deposito").Visible = True
            DgvListado.Columns("Deposito").HeaderText = "Deposito"
            DgvListado.Columns("Deposito").Width = 60
        End If

        If DgvListado.Columns.Contains("Descripcion") Then
            DgvListado.Columns("Descripcion").Visible = True
            DgvListado.Columns("Descripcion").HeaderText = "Descripci�n"
            DgvListado.Columns("Descripcion").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End If

        If DgvListado.Columns.Contains("Domicilio") Then
            DgvListado.Columns("Domicilio").Visible = True
            DgvListado.Columns("Domicilio").HeaderText = "Domicilio"
            DgvListado.Columns("Domicilio").Width = 150
        End If

        If DgvListado.Columns.Contains("Provincia") Then
            DgvListado.Columns("Provincia").Visible = True
            DgvListado.Columns("Provincia").HeaderText = "Provincia"
            DgvListado.Columns("Provincia").Width = 100
        End If

        If DgvListado.Columns.Contains("Establecimiento") Then
            DgvListado.Columns("Establecimiento").Visible = True
            DgvListado.Columns("Establecimiento").HeaderText = "Establecimiento"
            DgvListado.Columns("Establecimiento").Width = 80
        End If

        If DgvListado.Columns.Contains("Timbrado") Then
            DgvListado.Columns("Timbrado").Visible = True
            DgvListado.Columns("Timbrado").HeaderText = "Timbrado"
            DgvListado.Columns("Timbrado").Width = 80
        End If

        ConfigurarEstiloGrid(DgvListado)
        DgvListado.ReadOnly = True
        DgvListado.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect

    End Sub

    Private Sub FormModoConsulta()
        SetControlesEnabled(False, TxtCodigo, TxtDeposito, TxtDescripcion, TxtDomicilio, TxtProvincia, TxtEstablecimiento, TxtTimbrado, cmdAceptar, CmdCancelar)
        SetControlesEnabled(True, CmdAgregar, btnModificar, CmdBorrar)
    End Sub

    Private Sub FormModoEdicion()
        SetControlesEnabled(True, TxtDeposito, TxtDescripcion, TxtDomicilio, TxtProvincia, TxtEstablecimiento, TxtTimbrado, cmdAceptar, CmdCancelar)
        SetControlesEnabled(False, TxtCodigo, CmdAgregar, btnModificar, CmdBorrar)
    End Sub

    Private Sub FormLimpiarSeleccionado()
        filaActualIndice = -1
        filaActual = Nothing
        TxtCodigo.Text = ""
        TxtDeposito.Text = ""
        TxtDescripcion.Text = ""
        TxtDomicilio.Text = ""
        TxtProvincia.Text = ""
        TxtEstablecimiento.Text = ""
        TxtTimbrado.Text = ""
    End Sub
End Class
