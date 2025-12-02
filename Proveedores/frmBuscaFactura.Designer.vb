<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBuscaFactura
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer
    Friend WithEvents DgvBusca As DataGridView
    Friend WithEvents optAnual As RadioButton
    Friend WithEvents optCorriente As RadioButton
    Friend WithEvents CmdSalir As Button
    Friend WithEvents CmdBuscar As Button
    Friend WithEvents lnkCopiar As LinkLabel
    Friend WithEvents chkEncabezados As CheckBox
    Friend WithEvents TxtBuscar As TextBox
    Friend WithEvents LblBuscar As Label

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        DgvBusca = New DataGridView()
        optAnual = New RadioButton()
        optCorriente = New RadioButton()
        CmdSalir = New Button()
        CmdBuscar = New Button()
        lnkCopiar = New LinkLabel()
        chkEncabezados = New CheckBox()
        TxtBuscar = New TextBox()
        LblBuscar = New Label()
        CType(DgvBusca, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' DgvBusca
        ' 
        DgvBusca.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DgvBusca.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DgvBusca.Location = New Point(12, 43)
        DgvBusca.Name = "DgvBusca"
        DgvBusca.ReadOnly = True
        DgvBusca.Size = New Size(964, 282)
        DgvBusca.TabIndex = 8
        ' 
        ' optAnual
        ' 
        optAnual.AutoSize = True
        optAnual.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        optAnual.Location = New Point(600, 15)
        optAnual.Name = "optAnual"
        optAnual.Size = New Size(56, 19)
        optAnual.TabIndex = 7
        optAnual.TabStop = True
        optAnual.Text = "Anual"
        optAnual.UseVisualStyleBackColor = True
        ' 
        ' optCorriente
        ' 
        optCorriente.AutoSize = True
        optCorriente.Checked = True
        optCorriente.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        optCorriente.Location = New Point(516, 15)
        optCorriente.Name = "optCorriente"
        optCorriente.Size = New Size(78, 19)
        optCorriente.TabIndex = 6
        optCorriente.TabStop = True
        optCorriente.Text = "Corriente"
        optCorriente.UseVisualStyleBackColor = True
        ' 
        ' CmdSalir
        ' 
        CmdSalir.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        CmdSalir.BackColor = Color.IndianRed
        CmdSalir.FlatStyle = FlatStyle.Flat
        CmdSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdSalir.ForeColor = Color.White
        CmdSalir.Location = New Point(887, 379)
        CmdSalir.Name = "CmdSalir"
        CmdSalir.Size = New Size(85, 33)
        CmdSalir.TabIndex = 2
        CmdSalir.Text = "Salir"
        CmdSalir.UseVisualStyleBackColor = False
        ' 
        ' CmdBuscar
        ' 
        CmdBuscar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        CmdBuscar.FlatStyle = FlatStyle.Flat
        CmdBuscar.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        CmdBuscar.Location = New Point(794, 379)
        CmdBuscar.Name = "CmdBuscar"
        CmdBuscar.Size = New Size(85, 33)
        CmdBuscar.TabIndex = 1
        CmdBuscar.Text = "Buscar"
        CmdBuscar.UseVisualStyleBackColor = True
        ' 
        ' lnkCopiar
        ' 
        lnkCopiar.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        lnkCopiar.AutoSize = True
        lnkCopiar.LinkColor = Color.Black
        lnkCopiar.Location = New Point(753, 332)
        lnkCopiar.Name = "lnkCopiar"
        lnkCopiar.Size = New Size(94, 15)
        lnkCopiar.TabIndex = 10
        lnkCopiar.TabStop = True
        lnkCopiar.Text = "Copiar selección"
        lnkCopiar.VisitedLinkColor = Color.Black
        ' 
        ' chkEncabezados
        ' 
        chkEncabezados.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        chkEncabezados.AutoSize = True
        chkEncabezados.Location = New Point(853, 331)
        chkEncabezados.Name = "chkEncabezados"
        chkEncabezados.Size = New Size(119, 19)
        chkEncabezados.TabIndex = 11
        chkEncabezados.Text = "Con encabezados"
        chkEncabezados.UseVisualStyleBackColor = True
        ' 
        ' TxtBuscar
        ' 
        TxtBuscar.Font = New Font("Segoe UI", 10F)
        TxtBuscar.Location = New Point(76, 12)
        TxtBuscar.Name = "TxtBuscar"
        TxtBuscar.Size = New Size(427, 25)
        TxtBuscar.TabIndex = 12
        ' 
        ' LblBuscar
        ' 
        LblBuscar.AutoSize = True
        LblBuscar.Location = New Point(8, 17)
        LblBuscar.Name = "LblBuscar"
        LblBuscar.Size = New Size(42, 15)
        LblBuscar.TabIndex = 13
        LblBuscar.Text = "Buscar"
        ' 
        ' frmBuscaFactura
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(988, 424)
        Controls.Add(DgvBusca)
        Controls.Add(optAnual)
        Controls.Add(optCorriente)
        Controls.Add(CmdSalir)
        Controls.Add(CmdBuscar)
        Controls.Add(lnkCopiar)
        Controls.Add(chkEncabezados)
        Controls.Add(TxtBuscar)
        Controls.Add(LblBuscar)
        MinimumSize = New Size(1004, 463)
        Name = "frmBuscaFactura"
        Text = "Buscar Comprobantes"
        CType(DgvBusca, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub
End Class
