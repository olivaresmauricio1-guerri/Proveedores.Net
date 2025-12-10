<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmControlCaja
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmControlCaja))
        CmdSalir = New Button()
        dtpFecha = New DateTimePicker()
        Label1 = New Label()
        dgvListado = New DataGridView()
        lnkCopiar = New LinkLabel()
        chkEncabezados = New CheckBox()
        CmdImprimir = New Button()
        CType(dgvListado, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' CmdSalir
        ' 
        CmdSalir.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        CmdSalir.BackColor = Color.IndianRed
        CmdSalir.FlatStyle = FlatStyle.Flat
        CmdSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdSalir.ForeColor = Color.White
        CmdSalir.Location = New Point(900, 373)
        CmdSalir.Margin = New Padding(4, 3, 4, 3)
        CmdSalir.Name = "CmdSalir"
        CmdSalir.Size = New Size(75, 30)
        CmdSalir.TabIndex = 12
        CmdSalir.Text = "Salir"
        CmdSalir.UseVisualStyleBackColor = False
        ' 
        ' dtpFecha
        ' 
        dtpFecha.Format = DateTimePickerFormat.Short
        dtpFecha.Location = New Point(59, 9)
        dtpFecha.Name = "dtpFecha"
        dtpFecha.Size = New Size(101, 23)
        dtpFecha.TabIndex = 13
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(12, 13)
        Label1.Name = "Label1"
        Label1.Size = New Size(41, 15)
        Label1.TabIndex = 14
        Label1.Text = "Fecha:"
        ' 
        ' dgvListado
        ' 
        dgvListado.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvListado.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvListado.Location = New Point(3, 38)
        dgvListado.Name = "dgvListado"
        dgvListado.Size = New Size(980, 296)
        dgvListado.TabIndex = 15
        ' 
        ' lnkCopiar
        ' 
        lnkCopiar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        lnkCopiar.AutoSize = True
        lnkCopiar.LinkColor = Color.Black
        lnkCopiar.Location = New Point(764, 341)
        lnkCopiar.Name = "lnkCopiar"
        lnkCopiar.Size = New Size(94, 15)
        lnkCopiar.TabIndex = 16
        lnkCopiar.TabStop = True
        lnkCopiar.Text = "Copiar selección"
        lnkCopiar.VisitedLinkColor = Color.Black
        ' 
        ' chkEncabezados
        ' 
        chkEncabezados.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        chkEncabezados.AutoSize = True
        chkEncabezados.Location = New Point(864, 340)
        chkEncabezados.Name = "chkEncabezados"
        chkEncabezados.Size = New Size(119, 19)
        chkEncabezados.TabIndex = 17
        chkEncabezados.Text = "Con encabezados"
        chkEncabezados.UseVisualStyleBackColor = True
        ' 
        ' CmdImprimir
        ' 
        CmdImprimir.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        CmdImprimir.FlatStyle = FlatStyle.Flat
        CmdImprimir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdImprimir.Location = New Point(818, 373)
        CmdImprimir.Name = "CmdImprimir"
        CmdImprimir.Size = New Size(75, 30)
        CmdImprimir.TabIndex = 18
        CmdImprimir.Text = "Imprimir"
        CmdImprimir.UseVisualStyleBackColor = True
        ' 
        ' frmControlCaja
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(988, 415)
        Controls.Add(CmdImprimir)
        Controls.Add(lnkCopiar)
        Controls.Add(chkEncabezados)
        Controls.Add(dgvListado)
        Controls.Add(Label1)
        Controls.Add(dtpFecha)
        Controls.Add(CmdSalir)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "frmControlCaja"
        Text = "Control Caja"
        CType(dgvListado, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents CmdSalir As Button
    Friend WithEvents dtpFecha As DateTimePicker
    Friend WithEvents Label1 As Label
    Friend WithEvents dgvListado As DataGridView
    Friend WithEvents lnkCopiar As LinkLabel
    Friend WithEvents chkEncabezados As CheckBox
    Friend WithEvents CmdImprimir As Button
End Class
