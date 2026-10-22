Imports DSM = DataSourceManager.Lib.DataSourceManager

Public Class frmSectores
    Private Shared instancia As frmSectores
    Private editando As Boolean
    Private cargando As Boolean
    Private idEdicion As Integer?
    Private descripcionOriginal As String
    Private activoOriginal As Boolean

    Public Shared Sub AbrirInstancia(mdiParent As Form)
        If instancia Is Nothing OrElse instancia.IsDisposed Then
            instancia = New frmSectores() With {.MdiParent = mdiParent}
        End If
        instancia.Show()
        instancia.BringToFront()
        instancia.Focus()
    End Sub

    Private Sub frmSectores_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If ComprasUsoDiseno.EstaEnDiseno(Me) Then Return
        DgvListado.ReadOnly = True
        DgvListado.MultiSelect = False
        DgvListado.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        ModoConsulta()
        CargarSectores()
    End Sub

    Private Sub frmSectores_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        If instancia Is Me Then instancia = Nothing
    End Sub

    Private Sub frmSectores_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If editando AndAlso MessageBox.Show(Me, "¿Desea salir sin guardar los cambios?", "Sectores",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then
            e.Cancel = True
        End If
    End Sub

    Private Sub TxtBuscar_TextChanged(sender As Object, e As EventArgs) Handles TxtBuscar.TextChanged
        If Not editando AndAlso Not cargando Then CargarSectores()
    End Sub

    Private Sub CargarSectores(Optional seleccionarId As Integer? = Nothing)
        Try
            Dim buscar = TxtBuscar.Text.Trim()
            Dim sql = "SELECT IdSector, Descripcion, Activo FROM dbo.Sectores " &
                      "WHERE (@Buscar = N'' OR Descripcion LIKE @Patron " &
                      "OR CONVERT(varchar(11), IdSector) = @Buscar) ORDER BY Descripcion, IdSector;"
            Dim datos = DSM.ExecuteQuery(DSM.Proveedores, sql,
                CmdParams("@Buscar", buscar, "@Patron", "%" & buscar & "%"), True)
            If datos Is Nothing Then Throw New InvalidOperationException("No se obtuvo respuesta al consultar los sectores.")

            cargando = True
            Try
                DgvListado.DataSource = datos
                If seleccionarId.HasValue Then
                    For Each fila As DataGridViewRow In DgvListado.Rows
                        If Convert.ToInt32(fila.Cells("IdSector").Value) = seleccionarId.Value Then
                            DgvListado.ClearSelection()
                            DgvListado.CurrentCell = fila.Cells("Descripcion")
                            fila.Selected = True
                            Exit For
                        End If
                    Next
                End If
            Finally
                cargando = False
            End Try
            MostrarSeleccion()
        Catch ex As Exception
            DgvListado.DataSource = Nothing
            MostrarSeleccion()
            MessageBox.Show(Me, "No se pudieron consultar los sectores. " & ex.Message,
                            "Sectores", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function FilaSeleccionada() As DataGridViewRow
        Dim fila = DgvListado.CurrentRow
        If fila Is Nothing OrElse fila.IsNewRow OrElse Not DgvListado.Columns.Contains("IdSector") Then Return Nothing
        Return fila
    End Function

    Private Sub DgvListado_SelectionChanged(sender As Object, e As EventArgs) Handles DgvListado.SelectionChanged
        If Not editando AndAlso Not cargando Then MostrarSeleccion()
    End Sub

    Private Sub MostrarSeleccion()
        If editando OrElse cargando Then Return
        Dim fila = FilaSeleccionada()
        TxtCodigo.Text = If(fila Is Nothing, "", Convert.ToString(fila.Cells("IdSector").Value))
        TxtDescripcion.Text = If(fila Is Nothing, "", Convert.ToString(fila.Cells("Descripcion").Value))
        ChkActivo.Checked = fila IsNot Nothing AndAlso Convert.ToBoolean(fila.Cells("Activo").Value)
        btnModificar.Enabled = fila IsNot Nothing
        CmdEstado.Enabled = fila IsNot Nothing
        CmdEstado.Text = If(fila IsNot Nothing AndAlso Not ChkActivo.Checked, "Activar", "Eliminar")
    End Sub

    Private Sub ModoConsulta()
        editando = False
        idEdicion = Nothing
        TxtBuscar.Enabled = True
        DgvListado.Enabled = True
        TxtDescripcion.Enabled = False
        ChkActivo.Enabled = False
        CmdAgregar.Enabled = True
        CmdAceptar.Enabled = False
        CmdCancelar.Enabled = False
        AcceptButton = Nothing
        MostrarSeleccion()
    End Sub

    Private Sub ModoEdicion()
        editando = True
        TxtBuscar.Enabled = False
        DgvListado.Enabled = False
        TxtDescripcion.Enabled = True
        ChkActivo.Enabled = True
        CmdAgregar.Enabled = False
        btnModificar.Enabled = False
        CmdEstado.Enabled = False
        CmdAceptar.Enabled = True
        CmdCancelar.Enabled = True
        AcceptButton = CmdAceptar
        TxtDescripcion.Focus()
        TxtDescripcion.SelectAll()
    End Sub

    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles CmdAgregar.Click
        idEdicion = Nothing
        TxtCodigo.Text = "Automático"
        TxtDescripcion.Clear()
        ChkActivo.Checked = True
        ModoEdicion()
    End Sub

    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        Dim fila = FilaSeleccionada()
        If fila Is Nothing Then Return
        idEdicion = Convert.ToInt32(fila.Cells("IdSector").Value)
        descripcionOriginal = Convert.ToString(fila.Cells("Descripcion").Value)
        activoOriginal = Convert.ToBoolean(fila.Cells("Activo").Value)
        ModoEdicion()
    End Sub

    Private Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles CmdAceptar.Click
        If Not editando Then Return
        Dim descripcion = TxtDescripcion.Text.Trim()
        If descripcion.Length = 0 Then
            MessageBox.Show(Me, "Ingrese la descripción del sector.", "Sectores", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtDescripcion.Focus()
            Return
        End If
        If descripcion.Length > 100 Then
            MessageBox.Show(Me, "La descripción admite hasta 100 caracteres.", "Sectores", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        Try
            CmdAceptar.Enabled = False
            Dim repetidos = DSM.ExecuteQuery(DSM.Proveedores,
                "SELECT IdSector FROM dbo.Sectores WHERE Descripcion = @Descripcion AND (@IdSector IS NULL OR IdSector <> @IdSector);",
                CmdParams("@Descripcion", descripcion, "@IdSector", If(idEdicion.HasValue, CObj(idEdicion.Value), DBNull.Value)), True)
            If repetidos Is Nothing Then Throw New InvalidOperationException("No se pudo validar la descripción.")
            If repetidos.Rows.Count > 0 Then
                MessageBox.Show(Me, "Ya existe un sector con esa descripción, incluso si está inactivo.", "Sectores", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TxtDescripcion.Focus()
                Return
            End If

            Dim resultado As DataTable
            If idEdicion.HasValue Then
                resultado = DSM.ExecuteQuery(DSM.Proveedores,
                    "UPDATE dbo.Sectores SET Descripcion = @Descripcion, Activo = @Activo " &
                    "OUTPUT INSERTED.IdSector WHERE IdSector = @IdSector " &
                    "AND Descripcion = @DescripcionOriginal AND Activo = @ActivoOriginal;",
                    CmdParams("@Descripcion", descripcion, "@Activo", ChkActivo.Checked, "@IdSector", idEdicion.Value,
                              "@DescripcionOriginal", descripcionOriginal, "@ActivoOriginal", activoOriginal), True)
            Else
                resultado = DSM.ExecuteQuery(DSM.Proveedores,
                    "INSERT INTO dbo.Sectores (Descripcion, Activo) OUTPUT INSERTED.IdSector VALUES (@Descripcion, @Activo);",
                    CmdParams("@Descripcion", descripcion, "@Activo", ChkActivo.Checked), True)
            End If
            If resultado Is Nothing OrElse resultado.Rows.Count <> 1 Then
                MessageBox.Show(Me, "El sector ya no está disponible o fue modificado por otro usuario. Cancele y vuelva a consultarlo.",
                                "Sectores", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If
            Dim idGuardado = Convert.ToInt32(resultado.Rows(0)("IdSector"))
            ModoConsulta()
            ' Al guardar se quita el filtro para mostrar siempre el registro guardado.
            cargando = True
            Try
                TxtBuscar.Clear()
            Finally
                cargando = False
            End Try
            CargarSectores(idGuardado)
        Catch ex As Exception
            Dim mensaje = If(ex.ToString().Contains("UQ_Sectores_Descripcion"),
                             "Ya existe un sector con esa descripción.", "No se pudo guardar el sector. " & ex.Message)
            MessageBox.Show(Me, mensaje, "Sectores", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            CmdAceptar.Enabled = editando
        End Try
    End Sub

    Private Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles CmdCancelar.Click
        Dim seleccionado = idEdicion
        ModoConsulta()
        CargarSectores(seleccionado)
    End Sub

    Private Sub CmdEstado_Click(sender As Object, e As EventArgs) Handles CmdEstado.Click
        Dim fila = FilaSeleccionada()
        If fila Is Nothing OrElse editando Then Return
        Dim id = Convert.ToInt32(fila.Cells("IdSector").Value)
        Dim descripcion = Convert.ToString(fila.Cells("Descripcion").Value)
        Dim activo = Convert.ToBoolean(fila.Cells("Activo").Value)
        Dim accion = If(activo, "dar de baja", "activar")
        If MessageBox.Show(Me, "¿Desea " & accion & " el sector " & descripcion & "?", "Sectores",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> DialogResult.Yes Then Return

        Try
            Dim resultado = DSM.ExecuteQuery(DSM.Proveedores,
                "UPDATE dbo.Sectores SET Activo = @NuevoActivo OUTPUT INSERTED.IdSector " &
                "WHERE IdSector = @IdSector AND Activo = @ActivoOriginal AND Descripcion = @DescripcionOriginal;",
                CmdParams("@NuevoActivo", Not activo, "@IdSector", id, "@ActivoOriginal", activo, "@DescripcionOriginal", descripcion), True)
            If resultado Is Nothing OrElse resultado.Rows.Count <> 1 Then
                MessageBox.Show(Me, "Otro usuario modificó el sector. Se actualizará la consulta.",
                                "Sectores", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
            CargarSectores(id)
        Catch ex As Exception
            MessageBox.Show(Me, "No se pudo cambiar el estado del sector. " & ex.Message,
                            "Sectores", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CmdSalir_Click(sender As Object, e As EventArgs) Handles CmdSalir.Click
        Close()
    End Sub

    Private Sub lnkCopiar_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkCopiar.LinkClicked
        CopiarDataGrid(DgvListado, chkEncabezados.Checked)
    End Sub

    Private Sub DgvListado_KeyDown(sender As Object, e As KeyEventArgs) Handles DgvListado.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.C Then
            CopiarDataGrid(DgvListado, chkEncabezados.Checked)
            e.SuppressKeyPress = True
        End If
    End Sub
End Class
