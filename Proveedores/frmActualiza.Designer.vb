<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmActualiza
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        lblControlando = New Label()
        lblActualizando = New Label()
        lblVolcando = New Label()
        lblBorrando = New Label()
        lblGenerando = New Label()
        btnSalir = New Button()
        btnActualizar = New Button()
        lblFechaHasta = New Label()
        dtpFechaHasta = New DateTimePicker()
        lblCancelando = New Label()
        SuspendLayout()
        ' 
        ' lblControlando
        ' 
        lblControlando.AutoSize = True
        lblControlando.Font = New Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblControlando.ForeColor = SystemColors.ControlText
        lblControlando.Location = New Point(12, 18)
        lblControlando.Name = "lblControlando"
        lblControlando.Size = New Size(217, 25)
        lblControlando.TabIndex = 0
        lblControlando.Text = "Controlando Novedades"
        ' 
        ' lblActualizando
        ' 
        lblActualizando.AutoSize = True
        lblActualizando.Font = New Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblActualizando.Location = New Point(12, 58)
        lblActualizando.Name = "lblActualizando"
        lblActualizando.Size = New Size(182, 25)
        lblActualizando.TabIndex = 1
        lblActualizando.Text = "Actualizando Saldos"
        ' 
        ' lblVolcando
        ' 
        lblVolcando.AutoSize = True
        lblVolcando.Font = New Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblVolcando.Location = New Point(12, 138)
        lblVolcando.Name = "lblVolcando"
        lblVolcando.Size = New Size(253, 25)
        lblVolcando.TabIndex = 2
        lblVolcando.Text = "Volcando registros al Detalle"
        ' 
        ' lblBorrando
        ' 
        lblBorrando.AutoSize = True
        lblBorrando.Font = New Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblBorrando.Location = New Point(12, 178)
        lblBorrando.Name = "lblBorrando"
        lblBorrando.Size = New Size(190, 25)
        lblBorrando.TabIndex = 3
        lblBorrando.Text = "Borrando Novedades"
        ' 
        ' lblGenerando
        ' 
        lblGenerando.AutoSize = True
        lblGenerando.Font = New Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblGenerando.Location = New Point(12, 98)
        lblGenerando.Name = "lblGenerando"
        lblGenerando.Size = New Size(182, 25)
        lblGenerando.TabIndex = 4
        lblGenerando.Text = "Generando Asientos"
        ' 
        ' btnSalir
        ' 
        btnSalir.BackColor = Color.IndianRed
        btnSalir.FlatStyle = FlatStyle.Flat
        btnSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnSalir.ForeColor = Color.White
        btnSalir.Location = New Point(196, 328)
        btnSalir.Name = "btnSalir"
        btnSalir.Size = New Size(90, 30)
        btnSalir.TabIndex = 14
        btnSalir.Text = "Salir"
        btnSalir.UseVisualStyleBackColor = False
        ' 
        ' btnActualizar
        ' 
        btnActualizar.FlatStyle = FlatStyle.Flat
        btnActualizar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnActualizar.Location = New Point(16, 328)
        btnActualizar.Name = "btnActualizar"
        btnActualizar.Size = New Size(168, 30)
        btnActualizar.TabIndex = 15
        btnActualizar.Text = "Actualizar Proveedores"
        btnActualizar.UseVisualStyleBackColor = True
        ' 
        ' lblFechaHasta
        ' 
        lblFechaHasta.AutoSize = True
        lblFechaHasta.Location = New Point(16, 271)
        lblFechaHasta.Name = "lblFechaHasta"
        lblFechaHasta.Size = New Size(199, 15)
        lblFechaHasta.TabIndex = 16
        lblFechaHasta.Text = "Actualizar movimientos anteriores a:"
        ' 
        ' dtpFechaHasta
        ' 
        dtpFechaHasta.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        dtpFechaHasta.Location = New Point(16, 289)
        dtpFechaHasta.Name = "dtpFechaHasta"
        dtpFechaHasta.Size = New Size(270, 25)
        dtpFechaHasta.TabIndex = 17
        ' 
        ' lblCancelando
        ' 
        lblCancelando.AutoSize = True
        lblCancelando.Font = New Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblCancelando.Location = New Point(12, 218)
        lblCancelando.Name = "lblCancelando"
        lblCancelando.Size = New Size(186, 25)
        lblCancelando.TabIndex = 18
        lblCancelando.Text = "Cancelando Facturas"
        ' 
        ' frmActualiza
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(298, 370)
        Controls.Add(lblCancelando)
        Controls.Add(dtpFechaHasta)
        Controls.Add(lblFechaHasta)
        Controls.Add(btnSalir)
        Controls.Add(btnActualizar)
        Controls.Add(lblGenerando)
        Controls.Add(lblBorrando)
        Controls.Add(lblVolcando)
        Controls.Add(lblActualizando)
        Controls.Add(lblControlando)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmActualiza"
        Text = "Actualizacion de Novedades Proveedores"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblControlando As Label
    Friend WithEvents lblActualizando As Label
    Friend WithEvents lblVolcando As Label
    Friend WithEvents lblBorrando As Label
    Friend WithEvents lblGenerando As Label
    Friend WithEvents btnSalir As Button
    Friend WithEvents btnActualizar As Button
    Friend WithEvents lblFechaHasta As Label
    Friend WithEvents dtpFechaHasta As DateTimePicker
    Friend WithEvents lblCancelando As Label
End Class
