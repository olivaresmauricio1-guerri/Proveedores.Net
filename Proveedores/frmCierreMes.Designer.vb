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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCierreMes))
        lblVolcando = New Label()
        lblCalculando = New Label()
        lblConfigurando = New Label()
        lblBorrando = New Label()
        cmdSalir = New Button()
        cmdCierre = New Button()
        Imagen1 = New PictureBox()
        Imagen2 = New PictureBox()
        Imagen3 = New PictureBox()
        Imagen4 = New PictureBox()
        CType(Imagen1, ComponentModel.ISupportInitialize).BeginInit()
        CType(Imagen2, ComponentModel.ISupportInitialize).BeginInit()
        CType(Imagen3, ComponentModel.ISupportInitialize).BeginInit()
        CType(Imagen4, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' lblVolcando
        ' 
        lblVolcando.AutoSize = True
        lblVolcando.Font = New Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblVolcando.ForeColor = SystemColors.ControlText
        lblVolcando.Location = New Point(64, 22)
        lblVolcando.Name = "lblVolcando"
        lblVolcando.Size = New Size(229, 25)
        lblVolcando.TabIndex = 0
        lblVolcando.Text = "Volcando el Detalle Anual"
        ' 
        ' lblCalculando
        ' 
        lblCalculando.AutoSize = True
        lblCalculando.Font = New Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblCalculando.Location = New Point(64, 62)
        lblCalculando.Name = "lblCalculando"
        lblCalculando.Size = New Size(327, 25)
        lblCalculando.TabIndex = 1
        lblCalculando.Text = "Calculando saldos al Ultimo Resumen"
        ' 
        ' lblConfigurando
        ' 
        lblConfigurando.AutoSize = True
        lblConfigurando.Font = New Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblConfigurando.Location = New Point(64, 142)
        lblConfigurando.Name = "lblConfigurando"
        lblConfigurando.Size = New Size(201, 25)
        lblConfigurando.TabIndex = 2
        lblConfigurando.Text = "Configurando Detalles"
        ' 
        ' lblBorrando
        ' 
        lblBorrando.AutoSize = True
        lblBorrando.Font = New Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        lblBorrando.Location = New Point(64, 102)
        lblBorrando.Name = "lblBorrando"
        lblBorrando.Size = New Size(172, 25)
        lblBorrando.TabIndex = 4
        lblBorrando.Text = "Borrando Registros"
        ' 
        ' cmdSalir
        ' 
        cmdSalir.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        cmdSalir.BackColor = Color.IndianRed
        cmdSalir.FlatStyle = FlatStyle.Flat
        cmdSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        cmdSalir.ForeColor = Color.White
        cmdSalir.Location = New Point(314, 189)
        cmdSalir.Name = "cmdSalir"
        cmdSalir.Size = New Size(90, 30)
        cmdSalir.TabIndex = 14
        cmdSalir.Text = "Salir"
        cmdSalir.UseVisualStyleBackColor = False
        ' 
        ' cmdCierre
        ' 
        cmdCierre.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        cmdCierre.FlatStyle = FlatStyle.Flat
        cmdCierre.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        cmdCierre.Location = New Point(16, 189)
        cmdCierre.Name = "cmdCierre"
        cmdCierre.Size = New Size(225, 30)
        cmdCierre.TabIndex = 15
        cmdCierre.Text = "Cerrar el Mes"
        cmdCierre.UseVisualStyleBackColor = True
        ' 
        ' Imagen1
        ' 
        Imagen1.Image = CType(resources.GetObject("Imagen1.Image"), Image)
        Imagen1.Location = New Point(16, 20)
        Imagen1.Name = "Imagen1"
        Imagen1.Size = New Size(31, 31)
        Imagen1.SizeMode = PictureBoxSizeMode.Zoom
        Imagen1.TabIndex = 16
        Imagen1.TabStop = False
        ' 
        ' Imagen2
        ' 
        Imagen2.Image = CType(resources.GetObject("Imagen2.Image"), Image)
        Imagen2.Location = New Point(16, 60)
        Imagen2.Name = "Imagen2"
        Imagen2.Size = New Size(31, 31)
        Imagen2.SizeMode = PictureBoxSizeMode.Zoom
        Imagen2.TabIndex = 17
        Imagen2.TabStop = False
        ' 
        ' Imagen3
        ' 
        Imagen3.Image = CType(resources.GetObject("Imagen3.Image"), Image)
        Imagen3.Location = New Point(16, 100)
        Imagen3.Name = "Imagen3"
        Imagen3.Size = New Size(31, 31)
        Imagen3.SizeMode = PictureBoxSizeMode.Zoom
        Imagen3.TabIndex = 18
        Imagen3.TabStop = False
        ' 
        ' Imagen4
        ' 
        Imagen4.Image = CType(resources.GetObject("Imagen4.Image"), Image)
        Imagen4.Location = New Point(16, 140)
        Imagen4.Name = "Imagen4"
        Imagen4.Size = New Size(31, 31)
        Imagen4.SizeMode = PictureBoxSizeMode.Zoom
        Imagen4.TabIndex = 19
        Imagen4.TabStop = False
        ' 
        ' frmCierreMes
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(419, 231)
        Controls.Add(Imagen4)
        Controls.Add(Imagen3)
        Controls.Add(Imagen2)
        Controls.Add(Imagen1)
        Controls.Add(cmdSalir)
        Controls.Add(cmdCierre)
        Controls.Add(lblBorrando)
        Controls.Add(lblConfigurando)
        Controls.Add(lblCalculando)
        Controls.Add(lblVolcando)
        FormBorderStyle = FormBorderStyle.FixedSingle
        MaximizeBox = False
        MinimizeBox = False
        Name = "frmCierreMes"
        Text = "Cierre de Mes"
        CType(Imagen1, ComponentModel.ISupportInitialize).EndInit()
        CType(Imagen2, ComponentModel.ISupportInitialize).EndInit()
        CType(Imagen3, ComponentModel.ISupportInitialize).EndInit()
        CType(Imagen4, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblVolcando As Label
    Friend WithEvents lblCalculando As Label
    Friend WithEvents lblConfigurando As Label
    Friend WithEvents lblBorrando As Label
    Friend WithEvents cmdSalir As Button
    Friend WithEvents cmdCierre As Button
    Friend WithEvents Imagen1 As PictureBox
    Friend WithEvents Imagen2 As PictureBox
    Friend WithEvents Imagen3 As PictureBox
    Friend WithEvents Imagen4 As PictureBox
End Class
