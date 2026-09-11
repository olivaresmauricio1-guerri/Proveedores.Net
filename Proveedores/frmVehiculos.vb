Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmVehiculos
    Private filaActual As DataGridViewRow
    Private filaActualIndice As Integer = -1
    Private Shared instancia As frmVehiculos
    Private _sucursalesTbl As DataTable

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmVehiculos()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmVehiculos_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Public Sub frmVehiculos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CargarSucursales()
        FormModoConsulta()
        GridBuscar()
        GridConfigurarColumnas()
        Me.KeyPreview = True
    End Sub

    Private Sub CargarSucursales()
        Try
            _sucursalesTbl = DSM.ExecuteQuery(DSM.Stock,
                "SELECT IdSucursal, Descripcion FROM Sucursales WHERE Codigo IN(1,3,10,13,14,15,16,21,23) ORDER BY Descripcion", Nothing)
            If _sucursalesTbl Is Nothing Then _sucursalesTbl = New DataTable()
            cboSucursal.BeginUpdate()
            cboSucursal.Items.Clear()
            cboSucursal.DisplayMember = "Descripcion"
            cboSucursal.ValueMember = "IdSucursal"
            For Each r As DataRow In _sucursalesTbl.Rows
                cboSucursal.Items.Add(New SucursalItem With {
                    .IdSucursal = Convert.ToInt32(r("IdSucursal")),
                    .Descripcion = Convert.ToString(r("Descripcion"))
                })
            Next
        Catch
        Finally
            cboSucursal.EndUpdate()
        End Try
    End Sub

    Private Class SucursalItem
        Public Property IdSucursal As Integer
        Public Property Descripcion As String
        Public Overrides Function ToString() As String
            Return Descripcion
        End Function
    End Class

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
        If e.RowIndex < 0 Then
            filaActualIndice = -1
            filaActual = Nothing
            FormLimpiarSeleccionado()
        End If
    End Sub

    Private Sub DgvListado_SelectionChanged(sender As Object, e As EventArgs) Handles DgvListado.SelectionChanged
        AplicarSeleccionActual()
    End Sub

    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles CmdAgregar.Click
        filaActual = Nothing
        filaActualIndice = -1
        FormModoEdicion()
        FormLimpiarSeleccionado()
        dtpFechaAlta.Value = Date.Now
        chkActivo.Checked = True
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        If filaActual Is Nothing Then Return
        FormModoEdicion()
        FormObtenerSeleccionado()
    End Sub

    Private Sub CmdBorrar_Click(sender As Object, e As EventArgs) Handles CmdBorrar.Click
        If filaActual Is Nothing Then Return
        If MessageBox.Show("¿Está seguro de eliminar este vehículo?" & vbCrLf & _
                           "Si tiene servicios asociados no se podrá borrar.", "Confirmar borrado",
                           MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return

        Dim idVehiculo = Convert.ToInt32(filaActual.Cells("IdVehiculo").Value)
        Try
            Dim sql = "DELETE FROM Vehiculos WHERE IdVehiculo = @IdVehiculo"
            DSM.Execute(DSM.Proveedores, sql, CmdParams("@IdVehiculo", idVehiculo), True)
        Catch ex As Exception
            MessageBox.Show("No se pudo eliminar: " & ex.Message)
            Return
        End Try

        FormModoConsulta()
        GridBuscar()
    End Sub

    Public Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles cmdAceptar.Click
        Dim patente = txtPatente.Text.Trim().ToUpper()
        If String.IsNullOrWhiteSpace(patente) Then
            MessageBox.Show("La Patente es obligatoria.")
            txtPatente.Focus()
            Return
        End If

        Dim marca = txtMarca.Text.Trim()
        Dim modelo = txtModelo.Text.Trim()
        Dim comentario = txtComentario.Text.Trim()

        Dim anioValue As Short = 0
        If Not String.IsNullOrWhiteSpace(txtAnio.Text) AndAlso
           Not Short.TryParse(txtAnio.Text, anioValue) Then
            MessageBox.Show("El Año no es válido.")
            txtAnio.Focus()
            Return
        End If

        Dim kmValue As Integer = 0
        If Not String.IsNullOrWhiteSpace(txtKmActual.Text) AndAlso
           Not Integer.TryParse(txtKmActual.Text, kmValue) Then
            MessageBox.Show("El Km Actual no es válido.")
            txtKmActual.Focus()
            Return
        End If

        Dim idSucursalSel As Integer = 0
        If cboSucursal.SelectedItem IsNot Nothing Then
            idSucursalSel = DirectCast(cboSucursal.SelectedItem, SucursalItem).IdSucursal
        End If

        If filaActual Is Nothing Then
            Dim sqlExiste = "SELECT COUNT(*) FROM Vehiculos WHERE Patente = @Patente"
            Dim existe = Convert.ToInt32(DSM.ExecuteQuery(DSM.Proveedores, sqlExiste,
                CmdParams("@Patente", patente)).Rows(0)(0))
            If existe > 0 Then
                MessageBox.Show("Ya existe un vehículo con esa patente.")
                txtPatente.Focus()
                Return
            End If

            Dim sql = "INSERT INTO Vehiculos (Patente, Marca, Modelo, Anio, KmActual, " &
                      "IdSucursal, FechaAlta, Activo, Comentario) " &
                      "VALUES (@Patente, @Marca, @Modelo, @Anio, @KmActual, " &
                      "@IdSucursal, @FechaAlta, @Activo, @Comentario)"
            DSM.Execute(DSM.Proveedores, sql, CmdParams(
                "@Patente", patente,
                "@Marca", If(String.IsNullOrWhiteSpace(marca), DBNull.Value, marca),
                "@Modelo", If(String.IsNullOrWhiteSpace(modelo), DBNull.Value, modelo),
                "@Anio", If(anioValue <> 0, CType(anioValue, Object), DBNull.Value),
                "@KmActual", kmValue,
                "@IdSucursal", If(idSucursalSel <> 0, CType(idSucursalSel, Object), DBNull.Value),
                "@FechaAlta", dtpFechaAlta.Value.Date,
                "@Activo", chkActivo.Checked,
                "@Comentario", If(String.IsNullOrWhiteSpace(comentario), DBNull.Value, comentario)
            ), True)
        Else
            Dim idVehiculo = Convert.ToInt32(filaActual.Cells("IdVehiculo").Value)
            Dim sqlExiste = "SELECT COUNT(*) FROM Vehiculos WHERE Patente = @Patente AND IdVehiculo <> @Id"
            Dim existe = Convert.ToInt32(DSM.ExecuteQuery(DSM.Proveedores, sqlExiste,
                CmdParams("@Patente", patente, "@Id", idVehiculo)).Rows(0)(0))
            If existe > 0 Then
                MessageBox.Show("Ya existe otro vehículo con esa patente.")
                txtPatente.Focus()
                Return
            End If

            Dim sql = "UPDATE Vehiculos SET Patente = @Patente, Marca = @Marca, " &
                      "Modelo = @Modelo, Anio = @Anio, KmActual = @KmActual, " &
                      "IdSucursal = @IdSucursal, FechaAlta = @FechaAlta, " &
                      "Activo = @Activo, Comentario = @Comentario " &
                      "WHERE IdVehiculo = @IdVehiculo"
            DSM.Execute(DSM.Proveedores, sql, CmdParams(
                "@Patente", patente,
                "@Marca", If(String.IsNullOrWhiteSpace(marca), DBNull.Value, marca),
                "@Modelo", If(String.IsNullOrWhiteSpace(modelo), DBNull.Value, modelo),
                "@Anio", If(anioValue <> 0, CType(anioValue, Object), DBNull.Value),
                "@KmActual", kmValue,
                "@IdSucursal", If(idSucursalSel <> 0, CType(idSucursalSel, Object), DBNull.Value),
                "@FechaAlta", dtpFechaAlta.Value.Date,
                "@Activo", chkActivo.Checked,
                "@Comentario", If(String.IsNullOrWhiteSpace(comentario), DBNull.Value, comentario),
                "@IdVehiculo", idVehiculo
            ), True)
        End If

        FormModoConsulta()
        GridBuscar()
    End Sub

    Public Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Close()
    End Sub

    Private Sub lnkCopiar_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkCopiar.LinkClicked
        CopiarDataGrid(DgvListado, chkEncabezados.Checked)
    End Sub

    Private Sub GridBuscar()
        Dim texto As String = TxtBuscar.Text.Trim()
        Dim sql As String = "SELECT IdVehiculo, Patente, Marca, Modelo, Anio, " &
                            "ISNULL(KmActual,0) AS KmActual, IdSucursal, " &
                            "CONVERT(VARCHAR, FechaAlta, 103) AS FechaAlta, Activo, Comentario " &
                            "FROM Vehiculos WHERE 1=1"
        Dim parametros As New List(Of Object)

        If Not String.IsNullOrEmpty(texto) Then
            sql &= " AND (Patente LIKE @Patente"
            parametros.Add("@Patente")
            parametros.Add($"%{texto.ToUpper()}%")

            sql &= " OR Marca LIKE @Marca OR Modelo LIKE @Modelo"
            parametros.Add("@Marca")
            parametros.Add($"%{texto}%")
            parametros.Add("@Modelo")
            parametros.Add($"%{texto}%")

            Dim numVal As Integer
            If Integer.TryParse(texto, numVal) Then
                sql &= " OR IdVehiculo = @Id OR KmActual = @Km"
                parametros.Add("@Id")
                parametros.Add(numVal)
                parametros.Add("@Km")
                parametros.Add(numVal)
            End If
            sql &= ")"
        End If
        sql &= " ORDER BY Patente"

        Dim dt = DSM.ExecuteQuery(DSM.Proveedores, sql, CmdParams(parametros.ToArray()))
        DgvListado.DataSource = dt

        If dt.Rows.Count = 0 Then
            filaActualIndice = -1
            filaActual = Nothing
            FormLimpiarSeleccionado()
            Return
        End If

        filaActualIndice = 0
        filaActual = DgvListado.Rows(0)
        FormObtenerSeleccionado()
    End Sub

    Private Sub AplicarSeleccionActual()
        If DgvListado Is Nothing OrElse DgvListado.CurrentRow Is Nothing Then Return
        If DgvListado.SelectedRows.Count > 1 Then
            filaActualIndice = -1
            filaActual = Nothing
            FormLimpiarSeleccionado()
            Return
        End If
        Dim idx = DgvListado.CurrentRow.Index
        If idx < 0 OrElse idx = filaActualIndice Then Return

        FormModoConsulta()
        FormLimpiarSeleccionado()
        filaActualIndice = idx
        filaActual = DgvListado.CurrentRow
        FormObtenerSeleccionado()
    End Sub

    Public Sub GridConfigurarColumnas()
        If DgvListado Is Nothing OrElse DgvListado.Columns.Count = 0 Then Return
        For Each col As DataGridViewColumn In DgvListado.Columns
            col.Visible = False
        Next

        If DgvListado.Columns.Contains("IdVehiculo") Then
            DgvListado.Columns("IdVehiculo").Visible = False
            DgvListado.Columns("IdVehiculo").HeaderText = "Cód."
            DgvListado.Columns("IdVehiculo").Width = 50
        End If
        If DgvListado.Columns.Contains("Patente") Then
            DgvListado.Columns("Patente").Visible = True
            DgvListado.Columns("Patente").HeaderText = "Patente"
            DgvListado.Columns("Patente").Width = 70
        End If
        If DgvListado.Columns.Contains("Marca") Then
            DgvListado.Columns("Marca").Visible = True
            DgvListado.Columns("Marca").HeaderText = "Marca"
            DgvListado.Columns("Marca").Width = 90
        End If
        If DgvListado.Columns.Contains("Modelo") Then
            DgvListado.Columns("Modelo").Visible = True
            DgvListado.Columns("Modelo").HeaderText = "Modelo"
            DgvListado.Columns("Modelo").Width = 80
        End If
        If DgvListado.Columns.Contains("Anio") Then
            DgvListado.Columns("Anio").Visible = True
            DgvListado.Columns("Anio").HeaderText = "Año"
            DgvListado.Columns("Anio").Width = 40
            DgvListado.Columns("Anio").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If
        If DgvListado.Columns.Contains("KmActual") Then
            DgvListado.Columns("KmActual").Visible = True
            DgvListado.Columns("KmActual").HeaderText = "Km Actual"
            DgvListado.Columns("KmActual").Width = 70
            DgvListado.Columns("KmActual").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DgvListado.Columns("KmActual").DefaultCellStyle.Format = "N0"
        End If
        If DgvListado.Columns.Contains("FechaAlta") Then
            DgvListado.Columns("FechaAlta").Visible = True
            DgvListado.Columns("FechaAlta").HeaderText = "Fec. Alta"
            DgvListado.Columns("FechaAlta").Width = 70
        End If
        If DgvListado.Columns.Contains("Activo") Then
            DgvListado.Columns("Activo").Visible = True
            DgvListado.Columns("Activo").HeaderText = "Activo"
            DgvListado.Columns("Activo").Width = 60
        End If
        If DgvListado.Columns.Contains("Comentario") Then
            DgvListado.Columns("Comentario").Visible = True
            DgvListado.Columns("Comentario").HeaderText = "Comentario"
            DgvListado.Columns("Comentario").AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
        End If

        ConfigurarEstiloGrid(DgvListado)
        DgvListado.ReadOnly = True
        DgvListado.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect
    End Sub

    Private Sub FormLimpiarSeleccionado()
        TxtCodigo.Text = String.Empty
        txtPatente.Text = String.Empty
        txtMarca.Text = String.Empty
        txtModelo.Text = String.Empty
        txtAnio.Text = String.Empty
        txtKmActual.Text = String.Empty
        cboSucursal.SelectedIndex = -1
        dtpFechaAlta.Value = Date.Now
        chkActivo.Checked = True
        txtComentario.Text = String.Empty
    End Sub

    Private Sub FormObtenerSeleccionado()
        If filaActual IsNot Nothing Then
            TxtCodigo.Text = If(filaActual.Cells("IdVehiculo").Value IsNot DBNull.Value,
                                filaActual.Cells("IdVehiculo").Value.ToString(), String.Empty)
            txtPatente.Text = If(filaActual.Cells("Patente").Value IsNot DBNull.Value,
                                 filaActual.Cells("Patente").Value.ToString(), String.Empty)
            txtMarca.Text = If(filaActual.Cells("Marca").Value IsNot Nothing AndAlso Not IsDBNull(filaActual.Cells("Marca").Value),
                              filaActual.Cells("Marca").Value.ToString(), String.Empty)
            txtModelo.Text = If(filaActual.Cells("Modelo").Value IsNot Nothing AndAlso Not IsDBNull(filaActual.Cells("Modelo").Value),
                               filaActual.Cells("Modelo").Value.ToString(), String.Empty)
            Dim a = filaActual.Cells("Anio").Value
            txtAnio.Text = If(a IsNot Nothing AndAlso Not IsDBNull(a), Convert.ToInt16(a).ToString(), String.Empty)
            txtKmActual.Text = Convert.ToInt32(filaActual.Cells("KmActual").Value).ToString("N0")
            Dim idSuc As Object = DBNull.Value
            If filaActual.Cells("IdSucursal").Value IsNot Nothing AndAlso Not IsDBNull(filaActual.Cells("IdSucursal").Value) Then
                idSuc = filaActual.Cells("IdSucursal").Value
            End If
            If idSuc IsNot DBNull.Value Then
                Dim buscado = Convert.ToInt32(idSuc)
                For i As Integer = 0 To cboSucursal.Items.Count - 1
                    If DirectCast(cboSucursal.Items(i), SucursalItem).IdSucursal = buscado Then
                        cboSucursal.SelectedIndex = i
                        Exit For
                    End If
                Next
            Else
                cboSucursal.SelectedIndex = -1
            End If
            Dim f = filaActual.Cells("FechaAlta").Value
            If f IsNot Nothing AndAlso Not IsDBNull(f) Then dtpFechaAlta.Value = Convert.ToDateTime(f)
            chkActivo.Checked = Convert.ToBoolean(filaActual.Cells("Activo").Value)
            txtComentario.Text = If(filaActual.Cells("Comentario").Value IsNot Nothing AndAlso Not IsDBNull(filaActual.Cells("Comentario").Value),
                                   filaActual.Cells("Comentario").Value.ToString(), String.Empty)
        End If
    End Sub

    Public Sub FormModoConsulta()
        SetControlesEnabled(True, CmdAgregar, btnModificar, CmdBorrar)
        SetControlesEnabled(False, txtPatente, txtMarca, txtModelo, txtAnio, txtKmActual,
            cboSucursal, dtpFechaAlta, chkActivo, txtComentario, cmdAceptar, CmdCancelar)
    End Sub

    Public Sub FormModoEdicion()
        SetControlesEnabled(True, txtPatente, txtMarca, txtModelo, txtAnio, txtKmActual,
            cboSucursal, dtpFechaAlta, chkActivo, txtComentario, cmdAceptar, CmdCancelar)
        SetControlesEnabled(False, CmdAgregar, btnModificar, CmdBorrar)
        txtPatente.Focus()
    End Sub

    Private Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles CmdCancelar.Click
        FormModoConsulta()
        FormLimpiarSeleccionado()
        GridBuscar()
    End Sub
End Class
