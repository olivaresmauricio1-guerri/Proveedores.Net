Imports System.Data.SqlClient
Imports System.Linq
Imports System.Collections.Generic
Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmProvee
    Inherits Form

    Private Shared instancia As frmProvee
    Private dtProveedores As DataTable
    Private filaActual As DataGridViewRow
    Private filaActualIndice As Integer = -1

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmProvee()
            instancia.MdiParent = mdiParent
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmProvee_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        instancia = Nothing
    End Sub

    Private Sub frmProvee_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LlenarCombos()
        FormModoConsulta()
        CargarProveedores()
        ConfigurarEstiloGrid(dgvProveedores)
        ConfigurarListadoProveedores()

    End Sub

    Private Sub txtBuscar_TextChanged(sender As Object, e As EventArgs) Handles txtBuscar.TextChanged
        FormModoConsulta()
        FormLimpiarSeleccionado()
        CargarProveedores(txtBuscar.Text.Trim())
    End Sub
    Private Sub LlenarCombos()

        CargarCombos(CmbRubros, "Rubro", "Descripcion", "Descripcion", "Codigo")
        CargarCombos(CmbIva, "TipoIva", "Descripcion", "Descripcion", "Codigo")
        CargarCombos(CmbJurisdiccion, "Provincias", "Descripcion", "Descripcion", "IdProvincia")
        CargarCombos(CmbProvincias, "Provincias", "Descripcion", "Descripcion", "IdProvincia")
        CargarCombos(CmbPagos, "Valorizacion", "Descripcion", "Descripcion", "idValor", "", True)
    End Sub

    Private Sub CargarProveedores(Optional filtro As String = "")
        Dim sql As String = "SELECT * FROM MaeCtaCte WHERE FechaBaja IS NULL "
        Dim prms As Dictionary(Of String, Object) = Nothing
        If filtro <> "" Then
            Dim n As Integer
            If Integer.TryParse(filtro, n) Then
                sql &= " AND NroCuenta = @n"
                prms = CmdParams("@n", n)
            Else
                sql &= " AND Nombre LIKE '%' + @t + '%'"
                prms = CmdParams("@t", filtro)
            End If
        End If
        sql &= " ORDER BY Nombre"

        dtProveedores = DSM.ExecuteQuery(DSM.Proveedores, sql, prms)
        dgvProveedores.DataSource = dtProveedores


        If dtProveedores.Rows.Count = 0 Then
            filaActualIndice = -1
            filaActual = Nothing
            FormLimpiarSeleccionado()
            Return
        End If

        filaActualIndice = 0
        filaActual = dgvProveedores.Rows(filaActualIndice)
        AplicarSeleccionActual()

    End Sub

    Private Sub btnBuscarCuenta_Click(sender As Object, e As EventArgs) Handles btnBuscarCuenta.Click
        Using frm As New frmPlanCuentasSelector()
            If frm.ShowDialog(Me) = DialogResult.OK Then
                Dim cuenta = frm.Seleccion
                txtCodContable.Text = cuenta("CodContable").ToString()
            End If
        End Using
    End Sub
    Private Sub cmdAgregar_Click(sender As Object, e As EventArgs) Handles cmdAgregar.Click
        filaActual = Nothing
        FormLimpiarSeleccionado()
        FormModoEdicion()

        ' Obtener nuevo numero
        Dim sql As String = "Select Max(NroCuenta) as UltimoNumero from MaeCtaCte"
        Dim dt As DataTable = DSM.ExecuteQuery(DSM.Proveedores, sql)
        Dim nuevoNro As Integer = 1
        If dt IsNot Nothing AndAlso dt.Rows.Count > 0 AndAlso Not IsDBNull(dt.Rows(0)("UltimoNumero")) Then
            nuevoNro = Convert.ToInt32(dt.Rows(0)("UltimoNumero")) + 1
        End If

        txtNroCuenta.Text = nuevoNro.ToString()
        txtTipo.Text = "00-00000000-0"
        txtCodContable.Text = "2.1.1"
        txtTipo.Focus()
    End Sub

    Private Sub cmdModificar_Click(sender As Object, e As EventArgs) Handles cmdModificar.Click
        'If dgvProveedores.CurrentRow Is Nothing Then Return
        If filaActual Is Nothing Then Return
        FormModoEdicion()
        txtNroCuenta.Enabled = False
        txtTipo.Focus()
    End Sub

    Private Sub cmdBorrar_Click(sender As Object, e As EventArgs) Handles cmdBorrar.Click
        If String.IsNullOrEmpty(txtNroCuenta.Text) Then Return

        Dim sql As String = "Select Count(*) from DetaCtaCte Where NroCuenta = " & txtNroCuenta.Text
        Dim count As Integer = Convert.ToInt32(DSM.ExecuteQuery(DSM.Proveedores, sql).Rows(0)(0))

        If count > 0 Then
            MessageBox.Show("El Proveedor que intenta borrar tiene movimientos", "Proveedor con movimientos", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End If

        If MessageBox.Show("¿Está seguro que desea borrar el Cliente " & txtNombre.Text & "?", "Atención", MessageBoxButtons.YesNo) = DialogResult.Yes Then
            DSM.ExecuteQuery(DSM.Proveedores, "DELETE FROM MaeCtaCte WHERE NroCuenta = " & txtNroCuenta.Text)
            CargarProveedores()
        End If
    End Sub

    Private Sub cmdAceptar_Click(sender As Object, e As EventArgs) Handles cmdAceptar.Click
        If Not ValidarDatos() Then Return

        Try
            Dim sql As String = ""
            Dim prms As New Dictionary(Of String, Object)

            prms.Add("@NroCuenta", Val(txtNroCuenta.Text))
            prms.Add("@Nombre", txtNombre.Text)
            prms.Add("@Cuit", txtTipo.Text)
            prms.Add("@CalleNro", txtCalleNro.Text)
            prms.Add("@Localidad", txtLocalidad.Text)
            prms.Add("@Provincia", CmbProvincias.Text)
            prms.Add("@Rubro", CmbRubros.Text)
            prms.Add("@Telefono", txtPMinimo.Text)
            prms.Add("@CorreoE", txtcorreo.Text)
            prms.Add("@Comentario", txtObsv.Text)
            prms.Add("@IdSucursal", 1)
            prms.Add("@IdTipoIva", If(CmbIva.SelectedValue Is Nothing, DBNull.Value, CmbIva.SelectedValue))
            prms.Add("@Jurisdiccion", CmbJurisdiccion.Text)
            prms.Add("@CodContable", txtCodContable.Text)
            prms.Add("@Departamento", txtDepartamento.Text)
            prms.Add("@Cbu", txtcbu.Text)
            prms.Add("@Lista", chkLista.Checked)
            prms.Add("@Bloqueado", chkAsiento.Checked)
            prms.Add("@Autoshop", chkAutoshop.Checked)
            prms.Add("@Exterior", chkExterior.Checked)
            prms.Add("@Ibrutos", chkIBrutos.Checked)

            If IsDate(txtPublico.Text) Then
                prms.Add("@FechaBaja", Convert.ToDateTime(txtPublico.Text))
            Else
                prms.Add("@FechaBaja", DBNull.Value)
            End If

            If filaActual Is Nothing Then
                sql = "INSERT INTO MaeCtaCte (NroCuenta, Nombre, Cuit, CalleNro, Localidad, Provincia, Rubro, Telefono, CorreoE, Comentario, IdSucursal, IdTipoIva, Jurisdiccion, CodContable, Departamento, FechaBaja, SALDOACTUAL, ULTIMORESUMEN, SALDODTO, Cbu, Lista, Bloqueado, Autoshop, Exterior, IngBrutos) " &
                      "VALUES (@NroCuenta, @Nombre, @Cuit, @CalleNro, @Localidad, @Provincia, @Rubro, @Telefono, @CorreoE, @Comentario, @IdSucursal, @IdTipoIva, @Jurisdiccion, @CodContable, @Departamento, @FechaBaja, 0, 0, 0, @Cbu, @Lista, @Bloqueado, @Autoshop, @Exterior, @Ibrutos)"
            Else
                sql = "UPDATE MaeCtaCte SET Nombre=@Nombre, Cuit=@Cuit, CalleNro=@CalleNro, Localidad=@Localidad, Provincia=@Provincia, Rubro=@Rubro, Telefono=@Telefono, CorreoE=@CorreoE, Comentario=@Comentario, IdSucursal=@IdSucursal, IdTipoIva=@IdTipoIva, Jurisdiccion=@Jurisdiccion, CodContable=@CodContable, Departamento=@Departamento, FechaBaja=@FechaBaja, Cbu=@Cbu, Lista=@Lista, Bloqueado=@Bloqueado, Autoshop=@Autoshop, Exterior=@Exterior, IngBrutos=@Ibrutos WHERE NroCuenta=@NroCuenta"
            End If

            DSM.Execute(DSM.Proveedores, sql, prms, True)

            FormModoConsulta()
            CargarProveedores(txtBuscar.Text.Trim())

        Catch ex As Exception
            MessageBox.Show("Error al guardar los datos " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End Try
    End Sub

    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Close()
    End Sub
    Public Sub FormModoConsulta()
        SetControlesEnabled(True, cmdAgregar, cmdModificar, cmdBorrar, fraCriterio, dgvProveedores)
        SetControlesEnabled(False, txtNroCuenta, txtNombre, txtTipo, txtCalleNro, txtLocalidad, txtDepartamento, txtCodContable, txtcorreo, txtObsv, txtPMinimo, txtPublico, txtcbu, txtCodigoPostal, CmbRubros, CmbIva, CmbJurisdiccion, CmbProvincias, CmbPagos, chkLista, chkAsiento, chkAutoshop, chkExterior, chkIBrutos, txtSaldoActual, txtSaldoDto, txtUltimoResumen, cmdAceptar, cmdCancelar, btnBuscarCuenta)
    End Sub

    Public Sub FormModoEdicion()
        SetControlesEnabled(False, cmdAgregar, cmdModificar, cmdBorrar, fraCriterio, dgvProveedores)
        SetControlesEnabled(True, txtNroCuenta, txtNombre, txtTipo, txtCalleNro, txtLocalidad, txtDepartamento, txtCodContable, txtcorreo, txtObsv, txtPMinimo, txtPublico, txtcbu, txtCodigoPostal, CmbRubros, CmbIva, CmbJurisdiccion, CmbProvincias, CmbPagos, chkLista, chkAsiento, chkAutoshop, chkExterior, chkIBrutos, cmdAceptar, cmdCancelar, btnBuscarCuenta)
        SetControlesEnabled(False, txtSaldoActual, txtSaldoDto, txtUltimoResumen)
    End Sub

    Public Sub FormObtenerSeleccionado()
        If filaActual Is Nothing Then Return

        ' Limpiar combos
        CmbRubros.SelectedIndex = -1
        CmbRubros.Text = String.Empty
        CmbIva.SelectedIndex = -1
        CmbIva.Text = String.Empty
        CmbJurisdiccion.SelectedIndex = -1
        CmbJurisdiccion.Text = String.Empty
        CmbProvincias.SelectedIndex = -1
        CmbProvincias.Text = String.Empty

        If filaActual IsNot Nothing Then
            txtNroCuenta.Text = If(filaActual.Cells("NroCuenta").Value IsNot DBNull.Value, filaActual.Cells("NroCuenta").Value.ToString(), String.Empty)
            txtNombre.Text = If(filaActual.Cells("Nombre").Value IsNot DBNull.Value, filaActual.Cells("Nombre").Value.ToString(), String.Empty)
            txtTipo.Text = If(filaActual.Cells("Cuit").Value IsNot DBNull.Value, filaActual.Cells("Cuit").Value.ToString(), String.Empty)
            txtCalleNro.Text = If(filaActual.Cells("CalleNro").Value IsNot DBNull.Value, filaActual.Cells("CalleNro").Value.ToString(), String.Empty)
            txtLocalidad.Text = If(filaActual.Cells("Localidad").Value IsNot DBNull.Value, filaActual.Cells("Localidad").Value.ToString(), String.Empty)
            txtDepartamento.Text = If(filaActual.Cells("Departamento").Value IsNot DBNull.Value, filaActual.Cells("Departamento").Value.ToString(), String.Empty)
            txtCodContable.Text = If(filaActual.Cells("CodContable").Value IsNot DBNull.Value, filaActual.Cells("CodContable").Value.ToString(), String.Empty)
            txtcorreo.Text = If(filaActual.Cells("CorreoE").Value IsNot DBNull.Value, filaActual.Cells("CorreoE").Value.ToString(), String.Empty)
            txtObsv.Text = If(filaActual.Cells("Comentario").Value IsNot DBNull.Value, filaActual.Cells("Comentario").Value.ToString(), String.Empty)
            txtPMinimo.Text = If(filaActual.Cells("Telefono").Value IsNot DBNull.Value, filaActual.Cells("Telefono").Value.ToString(), String.Empty)

            If filaActual.Cells("FechaBaja").Value IsNot DBNull.Value Then
                txtPublico.Text = Convert.ToDateTime(filaActual.Cells("FechaBaja").Value).ToString("dd/MM/yyyy")
            Else
                txtPublico.Text = ""
            End If

            CmbRubros.Text = If(filaActual.Cells("Rubro").Value IsNot DBNull.Value, filaActual.Cells("Rubro").Value.ToString(), String.Empty)
            CmbIva.SelectedValue = If(filaActual.Cells("IdTipoIva").Value IsNot DBNull.Value, filaActual.Cells("IdTipoIva").Value.ToString(), String.Empty)
            CmbJurisdiccion.Text = If(filaActual.Cells("Jurisdiccion").Value IsNot DBNull.Value, filaActual.Cells("Jurisdiccion").Value.ToString(), String.Empty)
            CmbProvincias.Text = If(filaActual.Cells("Provincia").Value IsNot DBNull.Value, filaActual.Cells("Provincia").Value.ToString(), String.Empty)

            chkLista.Checked = If(filaActual.Cells("Lista").Value IsNot DBNull.Value, Convert.ToBoolean(filaActual.Cells("Lista").Value), False)
            chkAsiento.Checked = If(filaActual.Cells("Bloqueado").Value IsNot DBNull.Value, Convert.ToBoolean(filaActual.Cells("Bloqueado").Value), False)
            chkAutoshop.Checked = If(filaActual.Cells("Autoshop").Value IsNot DBNull.Value, Convert.ToBoolean(filaActual.Cells("Autoshop").Value), False)
            chkExterior.Checked = If(filaActual.Cells("Exterior").Value IsNot DBNull.Value, Convert.ToBoolean(filaActual.Cells("Exterior").Value), False)
            chkIBrutos.Checked = If(filaActual.Cells("Ingbrutos").Value IsNot DBNull.Value, Convert.ToBoolean(filaActual.Cells("Ingbrutos").Value), False)
            txtcbu.Text = If(filaActual.Cells("Cbu").Value IsNot DBNull.Value, filaActual.Cells("Cbu").Value.ToString(), String.Empty)
            txtUltimoResumen.Text = If(filaActual.Cells("ULTIMORESUMEN").Value IsNot DBNull.Value, Convert.ToDecimal(filaActual.Cells("ULTIMORESUMEN").Value), 0)

            ' Saldos
            Dim saldoActual As Decimal = If(filaActual.Cells("SALDOACTUAL").Value IsNot DBNull.Value, Convert.ToDecimal(filaActual.Cells("SALDOACTUAL").Value), 0)
            Dim saldoDto As Decimal = If(filaActual.Cells("SALDODTO").Value IsNot DBNull.Value, Convert.ToDecimal(filaActual.Cells("SALDODTO").Value), 0)
            lblSinDoc.Text = (saldoActual - saldoDto).ToString("C2")
            txtSaldoActual.Text = saldoActual.ToString("C2")
            txtSaldoDto.Text = saldoDto.ToString("C2")
        End If
    End Sub

    Private Sub dgvProveedores_SelectionChanged(sender As Object, e As EventArgs) Handles dgvProveedores.SelectionChanged
        If dgvProveedores Is Nothing OrElse dgvProveedores.CurrentRow Is Nothing Then Return
        filaActualIndice = -1
        filaActual = Nothing
        AplicarSeleccionActual()
    End Sub

    Private Function ValidarDatos() As Boolean
        If CmbRubros.Text.Length < 4 Then
            MessageBox.Show("El campo Rubro es obligatorio", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If
        If CmbProvincias.Text.Length < 4 Then
            MessageBox.Show("El campo Provincia es obligatorio", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If
        If Val(txtNroCuenta.Text) < 1 Then
            MessageBox.Show("El Número de cuenta es obligatorio", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return False
        End If
        Return True
    End Function

    Private Sub cmdCancelar_Click(sender As Object, e As EventArgs) Handles cmdCancelar.Click
        FormModoConsulta()
        FormObtenerSeleccionado()
    End Sub


    Private Sub ConfigurarListadoProveedores()
        If dgvProveedores Is Nothing OrElse dgvProveedores.Columns.Count = 0 Then Return
        For Each col As DataGridViewColumn In dgvProveedores.Columns
            col.Visible = False
        Next
        If dgvProveedores.Columns.Contains("NroCuenta") Then
            dgvProveedores.Columns("NroCuenta").Visible = True
            dgvProveedores.Columns("NroCuenta").HeaderText = "NroCta"
            dgvProveedores.Columns("NroCuenta").Width = 70
        End If
        If dgvProveedores.Columns.Contains("Nombre") Then
            dgvProveedores.Columns("Nombre").Visible = True
            dgvProveedores.Columns("Nombre").HeaderText = "Proveedor"
            dgvProveedores.Columns("Nombre").AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        End If
        If dgvProveedores.Columns.Contains("CodContable") Then
            dgvProveedores.Columns("CodContable").Visible = True
            dgvProveedores.Columns("CodContable").HeaderText = "CodContable"
            dgvProveedores.Columns("CodContable").Width = 80
        End If
        If dgvProveedores.Columns.Contains("Rubro") Then
            dgvProveedores.Columns("Rubro").Visible = True
            dgvProveedores.Columns("Rubro").HeaderText = "Rubro"
            dgvProveedores.Columns("Rubro").Width = 120
        End If
        If dgvProveedores.Columns.Contains("Telefono") Then
            dgvProveedores.Columns("Telefono").Visible = True
            dgvProveedores.Columns("Telefono").HeaderText = "Teléfono"
            dgvProveedores.Columns("Telefono").Width = 80
        End If
        If dgvProveedores.Columns.Contains("Cuit") Then
            dgvProveedores.Columns("Cuit").Visible = True
            dgvProveedores.Columns("Cuit").HeaderText = "CUIT/CUIL"
            dgvProveedores.Columns("Cuit").Width = 120
        End If
        If dgvProveedores.Columns.Contains("Provincia") Then
            dgvProveedores.Columns("Provincia").Visible = True
            dgvProveedores.Columns("Provincia").HeaderText = "Provincia"
            dgvProveedores.Columns("Provincia").Width = 120
        End If
    End Sub

    Private Sub AplicarSeleccionActual()
        If dgvProveedores Is Nothing OrElse dgvProveedores.CurrentRow Is Nothing Then Return
        If dgvProveedores.SelectedRows.Count > 1 Then
            filaActualIndice = -1
            filaActual = Nothing
            FormLimpiarSeleccionado()
            Return
        End If
        Dim idx = dgvProveedores.CurrentRow.Index
        If idx < 0 OrElse idx = filaActualIndice Then Return
        FormModoConsulta()

        filaActualIndice = idx
        filaActual = dgvProveedores.CurrentRow
        FormObtenerSeleccionado()
    End Sub

    Public Sub FormLimpiarSeleccionado()
        txtNroCuenta.Text = ""
        txtNombre.Text = ""
        txtTipo.Text = ""
        txtCalleNro.Text = ""
        txtLocalidad.Text = ""
        txtDepartamento.Text = ""
        txtCodigoPostal.Text = ""
        txtCodContable.Text = ""
        txtcorreo.Text = ""
        txtObsv.Text = ""
        txtPMinimo.Text = ""
        txtPublico.Text = ""
        txtcbu.Text = ""

        CmbRubros.SelectedIndex = -1
        CmbIva.SelectedIndex = -1
        CmbJurisdiccion.SelectedIndex = -1
        CmbProvincias.SelectedIndex = -1
        CmbPagos.SelectedIndex = -1

        chkLista.Checked = False
        chkAsiento.Checked = False
        chkAutoshop.Checked = False
        chkExterior.Checked = False
        chkIBrutos.Checked = False

        lblSinDoc.Text = ""
    End Sub
    Private Sub txtTipo_LostFocus(sender As Object, e As EventArgs) Handles txtTipo.LostFocus
        If filaActual Is Nothing Then Return

        If txtTipo.Text = "" OrElse txtTipo.Text = "0" Then
            MessageBox.Show("Debe ingresar un CUIT válido")
            txtTipo.Focus()
            Return
        End If
        If txtTipo.Text = "00-00000000-0" Then Return

        Dim cuit As String = txtTipo.Text
        If cuit.Length < 13 Then Return

        Dim tabla(10, 2) As Integer
        tabla(1, 1) = 5 : tabla(2, 1) = 4 : tabla(3, 1) = 3 : tabla(4, 1) = 2
        tabla(5, 1) = 7 : tabla(6, 1) = 6 : tabla(7, 1) = 5 : tabla(8, 1) = 4
        tabla(9, 1) = 3 : tabla(10, 1) = 2

        tabla(1, 2) = Val(Mid(cuit, 1, 1))
        tabla(2, 2) = Val(Mid(cuit, 2, 1))
        tabla(3, 2) = Val(Mid(cuit, 4, 1))
        tabla(4, 2) = Val(Mid(cuit, 5, 1))
        tabla(5, 2) = Val(Mid(cuit, 6, 1))
        tabla(6, 2) = Val(Mid(cuit, 7, 1))
        tabla(7, 2) = Val(Mid(cuit, 8, 1))
        tabla(8, 2) = Val(Mid(cuit, 9, 1))
        tabla(9, 2) = Val(Mid(cuit, 10, 1))
        tabla(10, 2) = Val(Mid(cuit, 11, 1))

        Dim SUMA As Integer = 0
        For i = 1 To 10
            SUMA += Val(tabla(i, 2)) * Val(tabla(i, 1))
        Next i

        Dim resto As Integer = SUMA Mod 11
        resto = 11 - resto
        If resto = 11 Then resto = 0
        If resto = 10 Then resto = 9

        If Val(Mid(cuit, 13, 1)) <> resto Then
            MessageBox.Show("El Dígito verificador es Incorrecto", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtTipo.Focus()
        End If
    End Sub

    Private Sub dgvProveedores_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvProveedores.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.C Then
            CopiarDataGrid(dgvProveedores, chkEncabezados.Checked)
            e.Handled = True
        End If
    End Sub

End Class
