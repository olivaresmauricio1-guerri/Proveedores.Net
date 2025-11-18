<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainForm
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
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        MenuStrip1 = New MenuStrip()
        MnuConfiguracion = New ToolStripMenuItem()
        MnuConImp = New ToolStripMenuItem()
        ToolStripSeparator1 = New ToolStripSeparator()
        mncalcu = New ToolStripMenuItem()
        MnuConAlm = New ToolStripMenuItem()
        ToolStripSeparator2 = New ToolStripSeparator()
        MnuSalir = New ToolStripMenuItem()
        MnuSto = New ToolStripMenuItem()
        MnuStoMan = New ToolStripMenuItem()
        ToolStripSeparator3 = New ToolStripSeparator()
        MnuStoNov = New ToolStripMenuItem()
        mnlistanov = New ToolStripMenuItem()
        mnpaga = New ToolStripMenuItem()
        ToolStripSeparator4 = New ToolStripSeparator()
        mnopagox = New ToolStripMenuItem()
        mncandjdjs = New ToolStripMenuItem()
        ToolStripSeparator12 = New ToolStripSeparator()
        MnuStoAct = New ToolStripMenuItem()
        ToolStripSeparator13 = New ToolStripSeparator()
        MnuStoCie = New ToolStripMenuItem()
        MnuConsultas = New ToolStripMenuItem()
        MnuConCli = New ToolStripMenuItem()
        ToolStripSeparator5 = New ToolStripSeparator()
        MnuConStoCom = New ToolStripMenuItem()
        ToolStripSeparator6 = New ToolStripSeparator()
        mncosjhs = New ToolStripMenuItem()
        ToolStripSeparator7 = New ToolStripSeparator()
        MnuConStoCob = New ToolStripMenuItem()
        ToolStripSeparator9 = New ToolStripSeparator()
        MnuConStoTot = New ToolStripMenuItem()
        ToolStripSeparator14 = New ToolStripSeparator()
        mnconsul = New ToolStripMenuItem()
        mnmarcaop = New ToolStripMenuItem()
        ToolStripSeparator10 = New ToolStripSeparator()
        mncpt = New ToolStripMenuItem()
        MnuNom = New ToolStripMenuItem()
        MnuNomBco = New ToolStripMenuItem()
        MnuNomCon = New ToolStripMenuItem()
        MnuNomEmp = New ToolStripMenuItem()
        MnuNomImp = New ToolStripMenuItem()
        MnuNomPro = New ToolStripMenuItem()
        mnrubro = New ToolStripMenuItem()
        MnuNomSuc = New ToolStripMenuItem()
        MnuNomIva = New ToolStripMenuItem()
        mnnro = New ToolStripMenuItem()
        MnuSeg = New ToolStripMenuItem()
        MnuSegInf = New ToolStripMenuItem()
        ToolStripSeparator8 = New ToolStripSeparator()
        MnuSegSes = New ToolStripMenuItem()
        ToolStripSeparator11 = New ToolStripSeparator()
        MnuSegINI = New ToolStripMenuItem()
        MnuVen = New ToolStripMenuItem()
        MnuAceVen = New ToolStripMenuItem()
        MnuVenCer = New ToolStripMenuItem()
        MnuVenTod = New ToolStripMenuItem()
        ToolStripSeparator15 = New ToolStripSeparator()
        MnuVenCas = New ToolStripMenuItem()
        MnuVenVer = New ToolStripMenuItem()
        MnuVenHor = New ToolStripMenuItem()
        ToolStripSeparator16 = New ToolStripSeparator()
        MnuVenIco = New ToolStripMenuItem()
        MnuVenImp = New ToolStripMenuItem()
        MnuAce = New ToolStripMenuItem()
        MnuAceAce = New ToolStripMenuItem()
        MnuAceAyu = New ToolStripMenuItem()
        Timer1 = New Timer(components)
        StatusBar1 = New StatusStrip()
        Panel1 = New ToolStripStatusLabel()
        Panel2 = New ToolStripStatusLabel()
        Panel3 = New ToolStripStatusLabel()
        Panel4 = New ToolStripStatusLabel()
        MenuStrip1.SuspendLayout()
        StatusBar1.SuspendLayout()
        SuspendLayout()
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.Items.AddRange(New ToolStripItem() {MnuConfiguracion, MnuSto, MnuConsultas, MnuNom, MnuSeg, MnuVen, MnuAce})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.MdiWindowListItem = MnuAceVen
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Padding = New Padding(7, 2, 0, 2)
        MenuStrip1.Size = New Size(1095, 24)
        MenuStrip1.TabIndex = 0
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' MnuConfiguracion
        ' 
        MnuConfiguracion.DropDownItems.AddRange(New ToolStripItem() {MnuConImp, ToolStripSeparator1, mncalcu, MnuConAlm, ToolStripSeparator2, MnuSalir})
        MnuConfiguracion.Name = "MnuConfiguracion"
        MnuConfiguracion.Size = New Size(95, 20)
        MnuConfiguracion.Text = "&Configuración"
        ' 
        ' MnuConImp
        ' 
        MnuConImp.Name = "MnuConImp"
        MnuConImp.Size = New Size(186, 22)
        MnuConImp.Text = "Especificar &Impresora"
        ' 
        ' ToolStripSeparator1
        ' 
        ToolStripSeparator1.Name = "ToolStripSeparator1"
        ToolStripSeparator1.Size = New Size(183, 6)
        ' 
        ' mncalcu
        ' 
        mncalcu.Name = "mncalcu"
        mncalcu.Size = New Size(186, 22)
        mncalcu.Text = "&Calculadora"
        ' 
        ' MnuConAlm
        ' 
        MnuConAlm.Name = "MnuConAlm"
        MnuConAlm.Size = New Size(186, 22)
        MnuConAlm.Text = "Control Cajas"
        ' 
        ' ToolStripSeparator2
        ' 
        ToolStripSeparator2.Name = "ToolStripSeparator2"
        ToolStripSeparator2.Size = New Size(183, 6)
        ' 
        ' MnuSalir
        ' 
        MnuSalir.Name = "MnuSalir"
        MnuSalir.Size = New Size(186, 22)
        MnuSalir.Text = "&Salir"
        ' 
        ' MnuSto
        ' 
        MnuSto.DropDownItems.AddRange(New ToolStripItem() {MnuStoMan, ToolStripSeparator3, MnuStoNov, mnlistanov, mnpaga, ToolStripSeparator4, mnopagox, mncandjdjs, ToolStripSeparator12, MnuStoAct, ToolStripSeparator13, MnuStoCie})
        MnuSto.Name = "MnuSto"
        MnuSto.Size = New Size(101, 20)
        MnuSto.Text = "&Actualizaciones"
        ' 
        ' MnuStoMan
        ' 
        MnuStoMan.Name = "MnuStoMan"
        MnuStoMan.Size = New Size(218, 22)
        MnuStoMan.Text = "Mantenimiento de &Cuentas"
        ' 
        ' ToolStripSeparator3
        ' 
        ToolStripSeparator3.Name = "ToolStripSeparator3"
        ToolStripSeparator3.Size = New Size(215, 6)
        ' 
        ' MnuStoNov
        ' 
        MnuStoNov.Name = "MnuStoNov"
        MnuStoNov.Size = New Size(218, 22)
        MnuStoNov.Text = "&Novedades"
        ' 
        ' mnlistanov
        ' 
        mnlistanov.Name = "mnlistanov"
        mnlistanov.Size = New Size(218, 22)
        mnlistanov.Text = "&Listar Novedades"
        ' 
        ' mnpaga
        ' 
        mnpaga.Name = "mnpaga"
        mnpaga.Size = New Size(218, 22)
        mnpaga.Text = "Pago a Proveedores Galicia"
        ' 
        ' ToolStripSeparator4
        ' 
        ToolStripSeparator4.Name = "ToolStripSeparator4"
        ToolStripSeparator4.Size = New Size(215, 6)
        ' 
        ' mnopagox
        ' 
        mnopagox.Name = "mnopagox"
        mnopagox.Size = New Size(218, 22)
        mnopagox.Text = "Emisión &Ordenes de Pago"
        ' 
        ' mncandjdjs
        ' 
        mncandjdjs.Name = "mncandjdjs"
        mncandjdjs.Size = New Size(218, 22)
        mncandjdjs.Text = "&Cancelar Facturas"
        ' 
        ' ToolStripSeparator12
        ' 
        ToolStripSeparator12.Name = "ToolStripSeparator12"
        ToolStripSeparator12.Size = New Size(215, 6)
        ' 
        ' MnuStoAct
        ' 
        MnuStoAct.Name = "MnuStoAct"
        MnuStoAct.Size = New Size(218, 22)
        MnuStoAct.Text = "&Actualizacion de Saldos"
        ' 
        ' ToolStripSeparator13
        ' 
        ToolStripSeparator13.Name = "ToolStripSeparator13"
        ToolStripSeparator13.Size = New Size(215, 6)
        ' 
        ' MnuStoCie
        ' 
        MnuStoCie.Name = "MnuStoCie"
        MnuStoCie.Size = New Size(218, 22)
        MnuStoCie.Text = "&Cierre de Mes"
        ' 
        ' MnuConsultas
        ' 
        MnuConsultas.DropDownItems.AddRange(New ToolStripItem() {MnuConCli, ToolStripSeparator5, MnuConStoCom, ToolStripSeparator6, mncosjhs, ToolStripSeparator7, MnuConStoCob, ToolStripSeparator9, MnuConStoTot, ToolStripSeparator14, mnconsul, mnmarcaop, ToolStripSeparator10, mncpt})
        MnuConsultas.Name = "MnuConsultas"
        MnuConsultas.Size = New Size(71, 20)
        MnuConsultas.Text = "C&onsultas"
        ' 
        ' MnuConCli
        ' 
        MnuConCli.Name = "MnuConCli"
        MnuConCli.Size = New Size(232, 22)
        MnuConCli.Text = "&Consulta Gral. de Proveedores"
        ' 
        ' ToolStripSeparator5
        ' 
        ToolStripSeparator5.Name = "ToolStripSeparator5"
        ToolStripSeparator5.Size = New Size(229, 6)
        ' 
        ' MnuConStoCom
        ' 
        MnuConStoCom.Name = "MnuConStoCom"
        MnuConStoCom.Size = New Size(232, 22)
        MnuConStoCom.Text = "Resumen de Cuentas"
        ' 
        ' ToolStripSeparator6
        ' 
        ToolStripSeparator6.Name = "ToolStripSeparator6"
        ToolStripSeparator6.Size = New Size(229, 6)
        ' 
        ' mncosjhs
        ' 
        mncosjhs.Name = "mncosjhs"
        mncosjhs.Size = New Size(232, 22)
        mncosjhs.Text = "Resumen de Cuenta Histórico"
        ' 
        ' ToolStripSeparator7
        ' 
        ToolStripSeparator7.Name = "ToolStripSeparator7"
        ToolStripSeparator7.Size = New Size(229, 6)
        ' 
        ' MnuConStoCob
        ' 
        MnuConStoCob.Name = "MnuConStoCob"
        MnuConStoCob.Size = New Size(232, 22)
        MnuConStoCob.Text = "&Listado de Proveedores"
        ' 
        ' ToolStripSeparator9
        ' 
        ToolStripSeparator9.Name = "ToolStripSeparator9"
        ToolStripSeparator9.Size = New Size(229, 6)
        ' 
        ' MnuConStoTot
        ' 
        MnuConStoTot.Name = "MnuConStoTot"
        MnuConStoTot.Size = New Size(232, 22)
        MnuConStoTot.Text = "&Sub Diarios de IVA"
        ' 
        ' ToolStripSeparator14
        ' 
        ToolStripSeparator14.Name = "ToolStripSeparator14"
        ToolStripSeparator14.Size = New Size(229, 6)
        ' 
        ' mnconsul
        ' 
        mnconsul.Name = "mnconsul"
        mnconsul.Size = New Size(232, 22)
        mnconsul.Text = "Consultar Factura"
        ' 
        ' mnmarcaop
        ' 
        mnmarcaop.Name = "mnmarcaop"
        mnmarcaop.Size = New Size(232, 22)
        mnmarcaop.Text = "Marcar Ordenes de Pago"
        ' 
        ' ToolStripSeparator10
        ' 
        ToolStripSeparator10.Name = "ToolStripSeparator10"
        ToolStripSeparator10.Size = New Size(229, 6)
        ' 
        ' mncpt
        ' 
        mncpt.Name = "mncpt"
        mncpt.Size = New Size(232, 22)
        mncpt.Text = "Listar Comprobantes"
        ' 
        ' MnuNom
        ' 
        MnuNom.DropDownItems.AddRange(New ToolStripItem() {MnuNomBco, MnuNomCon, MnuNomEmp, MnuNomImp, MnuNomPro, mnrubro, MnuNomSuc, MnuNomIva, mnnro})
        MnuNom.Name = "MnuNom"
        MnuNom.Size = New Size(103, 20)
        MnuNom.Text = "&Nomencladores"
        ' 
        ' MnuNomBco
        ' 
        MnuNomBco.Name = "MnuNomBco"
        MnuNomBco.Size = New Size(209, 22)
        MnuNomBco.Text = "&Bancos"
        ' 
        ' MnuNomCon
        ' 
        MnuNomCon.Name = "MnuNomCon"
        MnuNomCon.Size = New Size(209, 22)
        MnuNomCon.Text = "&Condiciones de Compra"
        ' 
        ' MnuNomEmp
        ' 
        MnuNomEmp.Name = "MnuNomEmp"
        MnuNomEmp.Size = New Size(209, 22)
        MnuNomEmp.Text = "&Empresas"
        ' 
        ' MnuNomImp
        ' 
        MnuNomImp.Name = "MnuNomImp"
        MnuNomImp.Size = New Size(209, 22)
        MnuNomImp.Text = "Porcentajes de &Impuestos"
        ' 
        ' MnuNomPro
        ' 
        MnuNomPro.Name = "MnuNomPro"
        MnuNomPro.Size = New Size(209, 22)
        MnuNomPro.Text = "&Provincias"
        ' 
        ' mnrubro
        ' 
        mnrubro.Name = "mnrubro"
        mnrubro.Size = New Size(209, 22)
        mnrubro.Text = "&Rubro del Proveedor"
        ' 
        ' MnuNomSuc
        ' 
        MnuNomSuc.Name = "MnuNomSuc"
        MnuNomSuc.Size = New Size(209, 22)
        MnuNomSuc.Text = "&Sucursales"
        ' 
        ' MnuNomIva
        ' 
        MnuNomIva.Name = "MnuNomIva"
        MnuNomIva.Size = New Size(209, 22)
        MnuNomIva.Text = "Tipos de &Iva"
        ' 
        ' mnnro
        ' 
        mnnro.Name = "mnnro"
        mnnro.Size = New Size(209, 22)
        mnnro.Text = "&Nros.Comprobantes"
        ' 
        ' MnuSeg
        ' 
        MnuSeg.DropDownItems.AddRange(New ToolStripItem() {MnuSegInf, ToolStripSeparator8, MnuSegSes, ToolStripSeparator11, MnuSegINI})
        MnuSeg.Name = "MnuSeg"
        MnuSeg.Size = New Size(72, 20)
        MnuSeg.Text = "&Seguridad"
        ' 
        ' MnuSegInf
        ' 
        MnuSegInf.Name = "MnuSegInf"
        MnuSegInf.Size = New Size(195, 22)
        MnuSegInf.Text = "&Información Reservada"
        ' 
        ' ToolStripSeparator8
        ' 
        ToolStripSeparator8.Name = "ToolStripSeparator8"
        ToolStripSeparator8.Size = New Size(192, 6)
        ' 
        ' MnuSegSes
        ' 
        MnuSegSes.Name = "MnuSegSes"
        MnuSegSes.Size = New Size(195, 22)
        MnuSegSes.Text = "&Iniciar Sesión"
        ' 
        ' ToolStripSeparator11
        ' 
        ToolStripSeparator11.Name = "ToolStripSeparator11"
        ToolStripSeparator11.Size = New Size(192, 6)
        ' 
        ' MnuSegINI
        ' 
        MnuSegINI.Name = "MnuSegINI"
        MnuSegINI.Size = New Size(195, 22)
        MnuSegINI.Text = "&Editar .INI"
        ' 
        ' MnuVen
        ' 
        MnuVen.DropDownItems.AddRange(New ToolStripItem() {MnuAceVen, MnuVenCer, MnuVenTod, ToolStripSeparator15, MnuVenCas, MnuVenVer, MnuVenHor, ToolStripSeparator16, MnuVenIco, MnuVenImp})
        MnuVen.Name = "MnuVen"
        MnuVen.Size = New Size(66, 20)
        MnuVen.Text = "&Ventanas"
        ' 
        ' MnuAceVen
        ' 
        MnuAceVen.Name = "MnuAceVen"
        MnuAceVen.Size = New Size(174, 22)
        MnuAceVen.Text = "Ventanas &Activas"
        ' 
        ' MnuVenCer
        ' 
        MnuVenCer.Name = "MnuVenCer"
        MnuVenCer.Size = New Size(174, 22)
        MnuVenCer.Text = "&Cerrar"
        ' 
        ' MnuVenTod
        ' 
        MnuVenTod.Name = "MnuVenTod"
        MnuVenTod.Size = New Size(174, 22)
        MnuVenTod.Text = "Cerrar &Todas"
        ' 
        ' ToolStripSeparator15
        ' 
        ToolStripSeparator15.Name = "ToolStripSeparator15"
        ToolStripSeparator15.Size = New Size(171, 6)
        ' 
        ' MnuVenCas
        ' 
        MnuVenCas.Name = "MnuVenCas"
        MnuVenCas.Size = New Size(174, 22)
        MnuVenCas.Text = "&Cascada"
        ' 
        ' MnuVenVer
        ' 
        MnuVenVer.Name = "MnuVenVer"
        MnuVenVer.Size = New Size(174, 22)
        MnuVenVer.Text = "&Vertical"
        ' 
        ' MnuVenHor
        ' 
        MnuVenHor.Name = "MnuVenHor"
        MnuVenHor.Size = New Size(174, 22)
        MnuVenHor.Text = "&Horizontal"
        ' 
        ' ToolStripSeparator16
        ' 
        ToolStripSeparator16.Name = "ToolStripSeparator16"
        ToolStripSeparator16.Size = New Size(171, 6)
        ' 
        ' MnuVenIco
        ' 
        MnuVenIco.Name = "MnuVenIco"
        MnuVenIco.Size = New Size(174, 22)
        MnuVenIco.Text = "Reorganizar &Iconos"
        ' 
        ' MnuVenImp
        ' 
        MnuVenImp.Name = "MnuVenImp"
        MnuVenImp.Size = New Size(174, 22)
        MnuVenImp.Text = "&Imprimir Ventana"
        ' 
        ' MnuAce
        ' 
        MnuAce.DropDownItems.AddRange(New ToolStripItem() {MnuAceAce, MnuAceAyu})
        MnuAce.Name = "MnuAce"
        MnuAce.Size = New Size(24, 20)
        MnuAce.Text = "&?"
        ' 
        ' MnuAceAce
        ' 
        MnuAceAce.Name = "MnuAceAce"
        MnuAceAce.Size = New Size(135, 22)
        MnuAceAce.Text = "&Acerca de..."
        ' 
        ' MnuAceAyu
        ' 
        MnuAceAyu.Name = "MnuAceAyu"
        MnuAceAyu.Size = New Size(135, 22)
        MnuAceAyu.Text = "A&yuda"
        ' 
        ' Timer1
        ' 
        Timer1.Interval = 1000
        ' 
        ' StatusBar1
        ' 
        StatusBar1.ImageScalingSize = New Size(20, 20)
        StatusBar1.Items.AddRange(New ToolStripItem() {Panel1, Panel2, Panel3, Panel4})
        StatusBar1.Location = New Point(0, 567)
        StatusBar1.Name = "StatusBar1"
        StatusBar1.Padding = New Padding(1, 0, 16, 0)
        StatusBar1.Size = New Size(1095, 22)
        StatusBar1.TabIndex = 3
        StatusBar1.Text = "StatusStrip1"
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.Silver
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(932, 17)
        Panel1.Spring = True
        Panel1.Text = "Sistema de Proveedores"
        Panel1.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.Silver
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(65, 17)
        Panel2.Text = "01/01/2024"
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = Color.Silver
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(34, 17)
        Panel3.Text = "00:00"
        ' 
        ' Panel4
        ' 
        Panel4.BackColor = Color.Silver
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(47, 17)
        Panel4.Text = "Usuario"
        ' 
        ' MainForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1095, 589)
        Controls.Add(StatusBar1)
        Controls.Add(MenuStrip1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        IsMdiContainer = True
        MainMenuStrip = MenuStrip1
        Margin = New Padding(4, 3, 4, 3)
        Name = "MainForm"
        Text = "Sistema de Proveedores"
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        StatusBar1.ResumeLayout(False)
        StatusBar1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents MnuConfiguracion As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuConImp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mncalcu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuConAlm As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MnuSalir As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuSto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuStoMan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MnuStoNov As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnlistanov As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnpaga As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnopagox As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mncandjdjs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator12 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MnuStoAct As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator13 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MnuStoCie As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuConsultas As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuConCli As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MnuConStoCom As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mncosjhs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MnuConStoCob As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MnuConStoTot As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator14 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnconsul As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnmarcaop As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mncpt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuNom As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuNomBco As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuNomCon As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuNomEmp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuNomImp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuNomPro As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnrubro As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuNomSuc As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuNomIva As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnnro As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuSeg As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuSegInf As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MnuSegSes As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator11 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MnuSegINI As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuVen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuAceVen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuVenCer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuVenTod As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator15 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MnuVenCas As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuVenVer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuVenHor As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator16 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MnuVenIco As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuVenImp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuAce As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuAceAce As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuAceAyu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents StatusBar1 As StatusStrip
    Friend WithEvents Panel1 As ToolStripStatusLabel
    Friend WithEvents Panel2 As ToolStripStatusLabel
    Friend WithEvents Panel3 As ToolStripStatusLabel
    Friend WithEvents Panel4 As ToolStripStatusLabel


End Class
