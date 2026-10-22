<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmSeleccionSolicitudCompraUso
    Inherits System.Windows.Forms.Form

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle7 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As DataGridViewCellStyle = New DataGridViewCellStyle()
        PanelPrincipal = New TableLayoutPanel()
        PanelFiltro = New FlowLayoutPanel()
        LblBuscar = New Label()
        TxtBuscar = New TextBox()
        BtnBuscar = New Button()
        GridSolicitudes = New DataGridView()
        BarraAcciones = New FlowLayoutPanel()
        BtnCancelar = New Button()
        BtnAbrir = New Button()
        PanelPrincipal.SuspendLayout()
        PanelFiltro.SuspendLayout()
        CType(GridSolicitudes, ComponentModel.ISupportInitialize).BeginInit()
        BarraAcciones.SuspendLayout()
        SuspendLayout()
        ' 
        ' PanelPrincipal
        ' 
        PanelPrincipal.ColumnCount = 1
        PanelPrincipal.ColumnStyles.Add(New ColumnStyle(SizeType.Absolute, 20F))
        PanelPrincipal.Controls.Add(PanelFiltro, 0, 0)
        PanelPrincipal.Controls.Add(GridSolicitudes, 0, 1)
        PanelPrincipal.Controls.Add(BarraAcciones, 0, 2)
        PanelPrincipal.Dock = DockStyle.Fill
        PanelPrincipal.Location = New Point(0, 0)
        PanelPrincipal.Name = "PanelPrincipal"
        PanelPrincipal.Padding = New Padding(8)
        PanelPrincipal.RowStyles.Add(New RowStyle())
        PanelPrincipal.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        PanelPrincipal.RowStyles.Add(New RowStyle())
        PanelPrincipal.Size = New Size(944, 441)
        PanelPrincipal.TabIndex = 0
        ' 
        ' PanelFiltro
        ' 
        PanelFiltro.AutoSize = True
        PanelFiltro.Controls.Add(LblBuscar)
        PanelFiltro.Controls.Add(TxtBuscar)
        PanelFiltro.Controls.Add(BtnBuscar)
        PanelFiltro.Dock = DockStyle.Fill
        PanelFiltro.Location = New Point(11, 11)
        PanelFiltro.Name = "PanelFiltro"
        PanelFiltro.Size = New Size(922, 36)
        PanelFiltro.TabIndex = 0
        ' 
        ' LblBuscar
        ' 
        LblBuscar.AutoSize = True
        LblBuscar.Location = New Point(3, 7)
        LblBuscar.Margin = New Padding(3, 7, 3, 3)
        LblBuscar.Name = "LblBuscar"
        LblBuscar.Size = New Size(185, 15)
        LblBuscar.TabIndex = 0
        LblBuscar.Text = "Número, motivo, sector o usuario"
        ' 
        ' TxtBuscar
        ' 
        TxtBuscar.Location = New Point(194, 3)
        TxtBuscar.Name = "TxtBuscar"
        TxtBuscar.Size = New Size(380, 23)
        TxtBuscar.TabIndex = 1
        ' 
        ' BtnBuscar
        ' 
        BtnBuscar.AutoSize = True
        BtnBuscar.FlatStyle = FlatStyle.Flat
        BtnBuscar.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnBuscar.Location = New Point(580, 3)
        BtnBuscar.Name = "BtnBuscar"
        BtnBuscar.Size = New Size(75, 30)
        BtnBuscar.TabIndex = 2
        BtnBuscar.Text = "Buscar"
        ' 
        ' GridSolicitudes
        ' 
        GridSolicitudes.AllowUserToAddRows = False
        GridSolicitudes.AllowUserToDeleteRows = False
        GridSolicitudes.AllowUserToResizeColumns = False
        GridSolicitudes.AllowUserToResizeRows = False
        DataGridViewCellStyle7.BackColor = Color.FloralWhite
        GridSolicitudes.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle7
        DataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = SystemColors.Control
        DataGridViewCellStyle8.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        DataGridViewCellStyle8.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = DataGridViewTriState.True
        GridSolicitudes.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle8
        DataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = Color.White
        DataGridViewCellStyle9.Font = New Font("Segoe UI", 8F)
        DataGridViewCellStyle9.ForeColor = Color.Black
        DataGridViewCellStyle9.SelectionBackColor = Color.SteelBlue
        DataGridViewCellStyle9.SelectionForeColor = Color.White
        DataGridViewCellStyle9.WrapMode = DataGridViewTriState.False
        GridSolicitudes.DefaultCellStyle = DataGridViewCellStyle9
        GridSolicitudes.Dock = DockStyle.Fill
        GridSolicitudes.EnableHeadersVisualStyles = False
        GridSolicitudes.Location = New Point(11, 53)
        GridSolicitudes.MultiSelect = False
        GridSolicitudes.Name = "GridSolicitudes"
        GridSolicitudes.ReadOnly = True
        GridSolicitudes.RowHeadersWidth = 20
        GridSolicitudes.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        GridSolicitudes.Size = New Size(922, 335)
        GridSolicitudes.TabIndex = 1
        ' 
        ' BarraAcciones
        ' 
        BarraAcciones.AutoSize = True
        BarraAcciones.Controls.Add(BtnCancelar)
        BarraAcciones.Controls.Add(BtnAbrir)
        BarraAcciones.Dock = DockStyle.Fill
        BarraAcciones.FlowDirection = FlowDirection.RightToLeft
        BarraAcciones.Location = New Point(11, 394)
        BarraAcciones.Name = "BarraAcciones"
        BarraAcciones.Size = New Size(922, 36)
        BarraAcciones.TabIndex = 2
        ' 
        ' BtnCancelar
        ' 
        BtnCancelar.AutoSize = True
        BtnCancelar.BackColor = Color.IndianRed
        BtnCancelar.DialogResult = DialogResult.Cancel
        BtnCancelar.FlatStyle = FlatStyle.Flat
        BtnCancelar.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnCancelar.ForeColor = Color.White
        BtnCancelar.Location = New Point(831, 3)
        BtnCancelar.Name = "BtnCancelar"
        BtnCancelar.Size = New Size(88, 30)
        BtnCancelar.TabIndex = 0
        BtnCancelar.Text = "Cancelar"
        BtnCancelar.UseVisualStyleBackColor = False
        ' 
        ' BtnAbrir
        ' 
        BtnAbrir.AutoSize = True
        BtnAbrir.FlatStyle = FlatStyle.Flat
        BtnAbrir.Font = New Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        BtnAbrir.Location = New Point(705, 3)
        BtnAbrir.Name = "BtnAbrir"
        BtnAbrir.Size = New Size(120, 30)
        BtnAbrir.TabIndex = 1
        BtnAbrir.Text = "Abrir seleccionada"
        ' 
        ' frmSeleccionSolicitudCompraUso
        ' 
        AcceptButton = BtnBuscar
        AutoScaleDimensions = New SizeF(96F, 96F)
        AutoScaleMode = AutoScaleMode.Dpi
        CancelButton = BtnCancelar
        ClientSize = New Size(944, 441)
        Controls.Add(PanelPrincipal)
        Font = New Font("Segoe UI", 9F)
        MinimizeBox = False
        MinimumSize = New Size(800, 400)
        Name = "frmSeleccionSolicitudCompraUso"
        StartPosition = FormStartPosition.CenterParent
        Text = "Abrir solicitud"
        PanelPrincipal.ResumeLayout(False)
        PanelPrincipal.PerformLayout()
        PanelFiltro.ResumeLayout(False)
        PanelFiltro.PerformLayout()
        CType(GridSolicitudes, ComponentModel.ISupportInitialize).EndInit()
        BarraAcciones.ResumeLayout(False)
        BarraAcciones.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents PanelPrincipal As TableLayoutPanel
    Friend WithEvents PanelFiltro As FlowLayoutPanel
    Friend WithEvents LblBuscar As Label
    Friend WithEvents TxtBuscar As TextBox
    Friend WithEvents BtnBuscar As Button
    Friend WithEvents GridSolicitudes As DataGridView
    Friend WithEvents GridSolicitudesColIdSolicitud As DataGridViewTextBoxColumn
    Friend WithEvents GridSolicitudesColFechaSolicitud As DataGridViewTextBoxColumn
    Friend WithEvents GridSolicitudesColSector As DataGridViewTextBoxColumn
    Friend WithEvents GridSolicitudesColUsuarioSolicitante As DataGridViewTextBoxColumn
    Friend WithEvents GridSolicitudesColEstado As DataGridViewTextBoxColumn
    Friend WithEvents GridSolicitudesColMotivo As DataGridViewTextBoxColumn
    Friend WithEvents BarraAcciones As FlowLayoutPanel
    Friend WithEvents BtnCancelar As Button
    Friend WithEvents BtnAbrir As Button
End Class
