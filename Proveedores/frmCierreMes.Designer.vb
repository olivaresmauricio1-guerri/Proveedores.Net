<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmCierreMes
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        lblVolcando = New Label()
        lblCalculando = New Label()
        lblConfigurando = New Label()
        lblBorrando = New Label()
        btnSalir = New Button()
        btnCerrar = New Button()
        lblFechaHasta = New Label()
        dtpFechaHasta = New DateTimePicker()
        SuspendLayout()
        ' 
        ' lblVolcando
        ' 
        lblVolcando.AutoSize = True
        lblVolcando.Font = New Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblVolcando.ForeColor = SystemColors.ControlText
        lblVolcando.Location = New Point(12, 18)
        lblVolcando.Name = "lblVolcando"
        lblVolcando.Size = New Size(229, 25)
        lblVolcando.TabIndex = 0
        lblVolcando.Text = "Volcando el Detalle Anual"
        ' 
        ' lblCalculando
        ' 
        lblCalculando.AutoSize = True
        lblCalculando.Font = New Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblCalculando.Location = New Point(12, 58)
        lblCalculando.Name = "lblCalculando"
        lblCalculando.Size = New Size(327, 25)
        lblCalculando.TabIndex = 1
        lblCalculando.Text = "Calculando saldos al Ultimo Resumen"
        ' 
        ' lblConfigurando
        ' 
        lblConfigurando.AutoSize = True
        lblConfigurando.Font = New Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblConfigurando.Location = New Point(12, 138)
        lblConfigurando.Name = "lblConfigurando"
        lblConfigurando.Size = New Size(201, 25)
        lblConfigurando.TabIndex = 2
        lblConfigurando.Text = "Configurando Detalles"
        ' 
        ' lblBorrando
        ' 
        lblBorrando.AutoSize = True
        lblBorrando.Font = New Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblBorrando.Location = New Point(12, 98)
        lblBorrando.Name = "lblBorrando"
        lblBorrando.Size = New Size(172, 25)
        lblBorrando.TabIndex = 4
        lblBorrando.Text = "Borrando Registros"
        ' 
        ' btnSalir
        ' 
        btnSalir.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        btnSalir.BackColor = Color.IndianRed
        btnSalir.FlatStyle = FlatStyle.Flat
        btnSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnSalir.ForeColor = Color.White
        btnSalir.Location = New Point(249, 242)
        btnSalir.Name = "btnSalir"
        btnSalir.Size = New Size(90, 30)
        btnSalir.TabIndex = 14
        btnSalir.Text = "Salir"
        btnSalir.UseVisualStyleBackColor = False
        ' 
        ' btnCerrar
        ' 
        btnCerrar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        btnCerrar.FlatStyle = FlatStyle.Flat
        btnCerrar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        btnCerrar.Location = New Point(16, 242)
        btnCerrar.Name = "btnCerrar"
        btnCerrar.Size = New Size(225, 30)
        btnCerrar.TabIndex = 15
        btnCerrar.Text = "Cerrar el Mes"
        btnCerrar.UseVisualStyleBackColor = True
        ' 
        ' lblFechaHasta
        ' 
        lblFechaHasta.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        lblFechaHasta.AutoSize = True
        lblFechaHasta.Location = New Point(16, 185)
        lblFechaHasta.Name = "lblFechaHasta"
        lblFechaHasta.Size = New Size(199, 15)
        lblFechaHasta.TabIndex = 16
        lblFechaHasta.Text = "Actualizar movimientos anteriores a:"
        ' 
        ' dtpFechaHasta
        ' 
        dtpFechaHasta.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dtpFechaHasta.Font = New Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        dtpFechaHasta.Location = New Point(16, 203)
        dtpFechaHasta.Name = "dtpFechaHasta"
        dtpFechaHasta.Size = New Size(323, 25)
        dtpFechaHasta.TabIndex = 17
        ' 
        ' frmCierreMes
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(354, 284)
        Controls.Add(dtpFechaHasta)
        Controls.Add(lblFechaHasta)
        Controls.Add(btnSalir)
        Controls.Add(btnCerrar)
        Controls.Add(lblBorrando)
        Controls.Add(lblConfigurando)
        Controls.Add(lblCalculando)
        Controls.Add(lblVolcando)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmCierreMes"
        Text = "Cierre de Mes"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblVolcando As Label
    Friend WithEvents lblCalculando As Label
    Friend WithEvents lblConfigurando As Label
    Friend WithEvents lblBorrando As Label
    Friend WithEvents btnSalir As Button
    Friend WithEvents btnCerrar As Button
    Friend WithEvents lblFechaHasta As Label
    Friend WithEvents dtpFechaHasta As DateTimePicker
End Class
