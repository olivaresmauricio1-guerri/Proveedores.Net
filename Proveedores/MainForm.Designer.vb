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
        MnuCompras = New ToolStripMenuItem()
        SolicitudComprasToolStripMenuItem = New ToolStripMenuItem()
        ConsultarComprasToolStripMenuItem = New ToolStripMenuItem()
        AutorizarComprasToolStripMenuItem = New ToolStripMenuItem()
        ComprasInteligentesToolStripMenuItem = New ToolStripMenuItem()
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
        MnuVehiculos = New ToolStripMenuItem()
        MnuVehServicios = New ToolStripMenuItem()
        MnuVehVehiculos = New ToolStripMenuItem()
        MnuVehTipos = New ToolStripMenuItem()
        MnuVehRepuestos = New ToolStripMenuItem()
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
        SectoresToolStripMenuItem = New ToolStripMenuItem()
        MenuStrip1.SuspendLayout()
        StatusBar1.SuspendLayout()
        SuspendLayout()
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.Items.AddRange(New ToolStripItem() {MnuConfiguracion, MnuSto, MnuConsultas, MnuCompras, MnuNom, MnuVehiculos, MnuSeg, MnuVen, MnuAce})
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
        MnuConfiguracion.Tag = "PROVEEDORES_CONFIGURACION"
        MnuConfiguracion.Size = New Size(95, 20)
        MnuConfiguracion.Text = "&Configuración"
        ' 
        ' MnuConImp
        ' 
        MnuConImp.Name = "MnuConImp"
        MnuConImp.Tag = "PROVEEDORES_CONFIGURACION_ESPECIFICAR_IMPRESORA"
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
        mncalcu.Tag = "PROVEEDORES_CONFIGURACION_CALCULADORA"
        mncalcu.Size = New Size(186, 22)
        mncalcu.Text = "&Calculadora"
        ' 
        ' MnuConAlm
        ' 
        MnuConAlm.Name = "MnuConAlm"
        MnuConAlm.Tag = "PROVEEDORES_CONFIGURACION_CONTROL_CAJAS"
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
        MnuSalir.Tag = "PROVEEDORES_CONFIGURACION_SALIR"
        MnuSalir.Size = New Size(186, 22)
        MnuSalir.Text = "&Salir"
        ' 
        ' MnuSto
        ' 
        MnuSto.DropDownItems.AddRange(New ToolStripItem() {MnuStoMan, ToolStripSeparator3, MnuStoNov, mnlistanov, mnpaga, ToolStripSeparator4, mnopagox, mncandjdjs, ToolStripSeparator12, MnuStoAct, ToolStripSeparator13, MnuStoCie})
        MnuSto.Name = "MnuSto"
        MnuSto.Tag = "PROVEEDORES_ACTUALIZACIONES"
        MnuSto.Size = New Size(101, 20)
        MnuSto.Text = "&Actualizaciones"
        ' 
        ' MnuStoMan
        ' 
        MnuStoMan.Name = "MnuStoMan"
        MnuStoMan.Tag = "PROVEEDORES_ACTUALIZACIONES_MANTENIMIENTO_DE_CUENTAS"
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
        MnuStoNov.Tag = "PROVEEDORES_ACTUALIZACIONES_NOVEDADES"
        MnuStoNov.Size = New Size(218, 22)
        MnuStoNov.Text = "&Novedades"
        ' 
        ' mnlistanov
        ' 
        mnlistanov.Name = "mnlistanov"
        mnlistanov.Tag = "PROVEEDORES_ACTUALIZACIONES_LISTAR_NOVEDADES"
        mnlistanov.Size = New Size(218, 22)
        mnlistanov.Text = "&Listar Novedades"
        ' 
        ' mnpaga
        ' 
        mnpaga.Name = "mnpaga"
        mnpaga.Tag = "PROVEEDORES_ACTUALIZACIONES_PAGO_A_PROVEEDORES_GALICIA"
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
        mnopagox.Tag = "PROVEEDORES_ACTUALIZACIONES_EMISION_ORDENES_DE_PAGO"
        mnopagox.Size = New Size(218, 22)
        mnopagox.Text = "Emisión &Ordenes de Pago"
        ' 
        ' mncandjdjs
        ' 
        mncandjdjs.Name = "mncandjdjs"
        mncandjdjs.Tag = "PROVEEDORES_ACTUALIZACIONES_CANCELAR_FACTURAS"
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
        MnuStoAct.Tag = "PROVEEDORES_ACTUALIZACIONES_ACTUALIZACION_DE_SALDOS"
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
        MnuStoCie.Tag = "PROVEEDORES_ACTUALIZACIONES_CIERRE_DE_MES"
        MnuStoCie.Size = New Size(218, 22)
        MnuStoCie.Text = "&Cierre de Mes"
        ' 
        ' MnuConsultas
        ' 
        MnuConsultas.DropDownItems.AddRange(New ToolStripItem() {MnuConCli, ToolStripSeparator5, MnuConStoCom, ToolStripSeparator6, mncosjhs, ToolStripSeparator7, MnuConStoCob, ToolStripSeparator9, MnuConStoTot, ToolStripSeparator14, mnconsul, mnmarcaop, ToolStripSeparator10, mncpt})
        MnuConsultas.Name = "MnuConsultas"
        MnuConsultas.Tag = "PROVEEDORES_CONSULTAS"
        MnuConsultas.Size = New Size(71, 20)
        MnuConsultas.Text = "C&onsultas"
        ' 
        ' MnuConCli
        ' 
        MnuConCli.Name = "MnuConCli"
        MnuConCli.Tag = "PROVEEDORES_CONSULTAS_CONSULTA_GRAL_DE_PROVEEDORES"
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
        MnuConStoCom.Tag = "PROVEEDORES_CONSULTAS_RESUMEN_DE_CUENTAS"
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
        mncosjhs.Tag = "PROVEEDORES_CONSULTAS_RESUMEN_DE_CUENTA_HISTORICO"
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
        MnuConStoCob.Tag = "PROVEEDORES_CONSULTAS_LISTADO_DE_PROVEEDORES"
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
        MnuConStoTot.Tag = "PROVEEDORES_CONSULTAS_SUB_DIARIOS_DE_IVA"
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
        mnconsul.Tag = "PROVEEDORES_CONSULTAS_CONSULTAR_FACTURA"
        mnconsul.Size = New Size(232, 22)
        mnconsul.Text = "Consultar Factura"
        ' 
        ' mnmarcaop
        ' 
        mnmarcaop.Name = "mnmarcaop"
        mnmarcaop.Tag = "PROVEEDORES_CONSULTAS_MARCAR_ORDENES_DE_PAGO"
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
        mncpt.Tag = "PROVEEDORES_CONSULTAS_LISTAR_COMPROBANTES"
        mncpt.Size = New Size(232, 22)
        mncpt.Text = "Listar Comprobantes"
        ' 
        ' MnuCompras
        ' 
        MnuCompras.DropDownItems.AddRange(New ToolStripItem() {SolicitudComprasToolStripMenuItem, ConsultarComprasToolStripMenuItem, AutorizarComprasToolStripMenuItem, ComprasInteligentesToolStripMenuItem})
        MnuCompras.Name = "MnuCompras"
        MnuCompras.Tag = "PROVEEDORES_COMPRAS"
        MnuCompras.Size = New Size(67, 20)
        MnuCompras.Text = "Compras"
        ' 
        ' SolicitudComprasToolStripMenuItem
        ' 
        SolicitudComprasToolStripMenuItem.Name = "SolicitudComprasToolStripMenuItem"
        SolicitudComprasToolStripMenuItem.Tag = "PROVEEDORES_COMPRAS_SOLICITUD_DE_COMPRAS"
        SolicitudComprasToolStripMenuItem.Size = New Size(186, 22)
        SolicitudComprasToolStripMenuItem.Text = "Solicitud Compras"
        ' 
        ' ConsultarComprasToolStripMenuItem
        ' 
        ConsultarComprasToolStripMenuItem.Name = "ConsultarComprasToolStripMenuItem"
        ConsultarComprasToolStripMenuItem.Tag = "PROVEEDORES_COMPRAS_CONSULTAR_COMPRAS"
        ConsultarComprasToolStripMenuItem.Size = New Size(186, 22)
        ConsultarComprasToolStripMenuItem.Text = "Consultar Compras"
        ' 
        ' AutorizarComprasToolStripMenuItem
        ' 
        AutorizarComprasToolStripMenuItem.Name = "AutorizarComprasToolStripMenuItem"
        AutorizarComprasToolStripMenuItem.Tag = "PROVEEDORES_COMPRAS_AUTOIRIZAR_COMPRAS"
        AutorizarComprasToolStripMenuItem.Size = New Size(186, 22)
        AutorizarComprasToolStripMenuItem.Text = "Autorizar Compras"
        ' 
        ' ComprasInteligentesToolStripMenuItem
        ' 
        ComprasInteligentesToolStripMenuItem.Name = "ComprasInteligentesToolStripMenuItem"
        ComprasInteligentesToolStripMenuItem.Tag = "PROVEEDORES_COMPRAS_COMPRAS_INTELIGENTES"
        ComprasInteligentesToolStripMenuItem.Size = New Size(186, 22)
        ComprasInteligentesToolStripMenuItem.Text = "Compras Inteligentes"
        ' 
        ' MnuNom
        ' 
        MnuNom.DropDownItems.AddRange(New ToolStripItem() {MnuNomBco, MnuNomCon, MnuNomEmp, MnuNomImp, MnuNomPro, mnrubro, MnuNomSuc, MnuNomIva, mnnro, SectoresToolStripMenuItem})
        MnuNom.Name = "MnuNom"
        MnuNom.Tag = "PROVEEDORES_NOMENCLADORES"
        MnuNom.Size = New Size(103, 20)
        MnuNom.Text = "&Nomencladores"
        ' 
        ' MnuNomBco
        ' 
        MnuNomBco.Name = "MnuNomBco"
        MnuNomBco.Tag = "PROVEEDORES_NOMENCLADORES_BANCOS"
        MnuNomBco.Size = New Size(209, 22)
        MnuNomBco.Text = "&Bancos"
        ' 
        ' MnuNomCon
        ' 
        MnuNomCon.Name = "MnuNomCon"
        MnuNomCon.Tag = "PROVEEDORES_NOMENCLADORES_CONDICIONES_DE_COMPRA"
        MnuNomCon.Size = New Size(209, 22)
        MnuNomCon.Text = "&Condiciones de Compra"
        ' 
        ' MnuNomEmp
        ' 
        MnuNomEmp.Name = "MnuNomEmp"
        MnuNomEmp.Tag = "PROVEEDORES_NOMENCLADORES_EMPRESAS"
        MnuNomEmp.Size = New Size(209, 22)
        MnuNomEmp.Text = "&Empresas"
        ' 
        ' MnuNomImp
        ' 
        MnuNomImp.Name = "MnuNomImp"
        MnuNomImp.Tag = "PROVEEDORES_NOMENCLADORES_PORCENTAJES_DE_IMPUESTOS"
        MnuNomImp.Size = New Size(209, 22)
        MnuNomImp.Text = "Porcentajes de &Impuestos"
        ' 
        ' MnuNomPro
        ' 
        MnuNomPro.Name = "MnuNomPro"
        MnuNomPro.Tag = "PROVEEDORES_NOMENCLADORES_PROVINCIAS"
        MnuNomPro.Size = New Size(209, 22)
        MnuNomPro.Text = "&Provincias"
        ' 
        ' mnrubro
        ' 
        mnrubro.Name = "mnrubro"
        mnrubro.Tag = "PROVEEDORES_NOMENCLADORES_RUBRO_DEL_PROVEEDOR"
        mnrubro.Size = New Size(209, 22)
        mnrubro.Text = "&Rubro del Proveedor"
        ' 
        ' MnuNomSuc
        ' 
        MnuNomSuc.Name = "MnuNomSuc"
        MnuNomSuc.Tag = "PROVEEDORES_NOMENCLADORES_SUCURSALES"
        MnuNomSuc.Size = New Size(209, 22)
        MnuNomSuc.Text = "&Sucursales"
        ' 
        ' MnuNomIva
        ' 
        MnuNomIva.Name = "MnuNomIva"
        MnuNomIva.Tag = "PROVEEDORES_NOMENCLADORES_TIPOS_DE_IVA"
        MnuNomIva.Size = New Size(209, 22)
        MnuNomIva.Text = "Tipos de &Iva"
        ' 
        ' mnnro
        ' 
        mnnro.Name = "mnnro"
        mnnro.Tag = "PROVEEDORES_NOMENCLADORES_NROS_COMPROBANTES"
        mnnro.Size = New Size(209, 22)
        mnnro.Text = "&Nros.Comprobantes"
        ' 
        ' MnuVehiculos
        ' 
        MnuVehiculos.DropDownItems.AddRange(New ToolStripItem() {MnuVehServicios, MnuVehVehiculos, MnuVehTipos, MnuVehRepuestos})
        MnuVehiculos.Name = "MnuVehiculos"
        MnuVehiculos.Tag = "PROVEEDORES_MANTENIMIENTOVEHICULOS"
        MnuVehiculos.Size = New Size(154, 20)
        MnuVehiculos.Text = "Mantenimiento &Vehículos"
        ' 
        ' MnuVehServicios
        ' 
        MnuVehServicios.Name = "MnuVehServicios"
        MnuVehServicios.Size = New Size(163, 22)
        MnuVehServicios.Text = "&Servicios"
        ' 
        ' MnuVehVehiculos
        ' 
        MnuVehVehiculos.Name = "MnuVehVehiculos"
<<<<<<< Updated upstream
        MnuVehVehiculos.Size = New Size(163, 22)
=======
        MnuVehVehiculos.Tag = "PROVEEDORES_MANTENIMIENTOVEHICULOS_VEHICULOS"
        MnuVehVehiculos.Size = New Size(180, 22)
>>>>>>> Stashed changes
        MnuVehVehiculos.Text = "&Vehículos"
        ' 
        ' MnuVehTipos
        ' 
        MnuVehTipos.Name = "MnuVehTipos"
<<<<<<< Updated upstream
        MnuVehTipos.Size = New Size(163, 22)
=======
        MnuVehTipos.Tag = "PROVEEDORES_MANTENIMIENTOVEHICULOS_TIPOSERVICIOS"
        MnuVehTipos.Size = New Size(180, 22)
>>>>>>> Stashed changes
        MnuVehTipos.Text = "Tipos de &Servicio"
        ' 
        ' MnuVehRepuestos
        ' 
        MnuVehRepuestos.Name = "MnuVehRepuestos"
<<<<<<< Updated upstream
        MnuVehRepuestos.Size = New Size(163, 22)
        MnuVehRepuestos.Text = "&Repuestos"
        ' 
=======
        MnuVehRepuestos.Tag = "PROVEEDORES_MANTENIMIENTOVEHICULOS_RESPUESTOS"
        MnuVehRepuestos.Size = New Size(180, 22)
        MnuVehRepuestos.Text = "&Repuestos"
        ' 
        ' MnuVehServicios
        ' 
        MnuVehServicios.Name = "MnuVehServicios"
        MnuVehServicios.Tag = "PROVEEDORES_MANTENIMIENTOVEHICULOS_SERVICIOS"
        MnuVehServicios.Size = New Size(180, 22)
        MnuVehServicios.Text = "&Servicios"
        ' 
>>>>>>> Stashed changes
        ' MnuSeg
        ' 
        MnuSeg.DropDownItems.AddRange(New ToolStripItem() {MnuSegInf, ToolStripSeparator8, MnuSegSes, ToolStripSeparator11, MnuSegINI})
        MnuSeg.Name = "MnuSeg"
        MnuSeg.Tag = "PROVEEDORES_SEGURIDAD"
        MnuSeg.Size = New Size(72, 20)
        MnuSeg.Text = "&Seguridad"
        ' 
        ' MnuSegInf
        ' 
        MnuSegInf.Name = "MnuSegInf"
        MnuSegInf.Tag = "PROVEEDORES_SEGURIDAD_INFORMACION_RESERVADA"
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
        MnuSegSes.Tag = "PROVEEDORES_SEGURIDAD_INICIAR_SESION"
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
        MnuSegINI.Tag = "PROVEEDORES_SEGURIDAD_EDITAR_INI"
        MnuSegINI.Size = New Size(195, 22)
        MnuSegINI.Text = "&Editar .INI"
        ' 
        ' MnuVen
        ' 
        MnuVen.DropDownItems.AddRange(New ToolStripItem() {MnuAceVen, MnuVenCer, MnuVenTod, ToolStripSeparator15, MnuVenCas, MnuVenVer, MnuVenHor, ToolStripSeparator16, MnuVenIco, MnuVenImp})
        MnuVen.Name = "MnuVen"
        MnuVen.Tag = "PROVEEDORES_VENTANAS"
        MnuVen.Size = New Size(66, 20)
        MnuVen.Text = "&Ventanas"
        ' 
        ' MnuAceVen
        ' 
        MnuAceVen.Name = "MnuAceVen"
        MnuAceVen.Tag = "PROVEEDORES_VENTANAS_VENTANAS_ACTIVAS"
        MnuAceVen.Size = New Size(174, 22)
        MnuAceVen.Text = "Ventanas &Activas"
        ' 
        ' MnuVenCer
        ' 
        MnuVenCer.Name = "MnuVenCer"
        MnuVenCer.Tag = "PROVEEDORES_VENTANAS_CERRAR"
        MnuVenCer.Size = New Size(174, 22)
        MnuVenCer.Text = "&Cerrar"
        ' 
        ' MnuVenTod
        ' 
        MnuVenTod.Name = "MnuVenTod"
        MnuVenTod.Tag = "PROVEEDORES_VENTANAS_CERRAR_TODAS"
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
        MnuVenCas.Tag = "PROVEEDORES_VENTANAS_CASCADA"
        MnuVenCas.Size = New Size(174, 22)
        MnuVenCas.Text = "&Cascada"
        ' 
        ' MnuVenVer
        ' 
        MnuVenVer.Name = "MnuVenVer"
        MnuVenVer.Tag = "PROVEEDORES_VENTANAS_VERTICAL"
        MnuVenVer.Size = New Size(174, 22)
        MnuVenVer.Text = "&Vertical"
        ' 
        ' MnuVenHor
        ' 
        MnuVenHor.Name = "MnuVenHor"
        MnuVenHor.Tag = "PROVEEDORES_VENTANAS_HORIZONTAL"
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
        MnuVenIco.Tag = "PROVEEDORES_VENTANAS_REORGANIZAR_ICONOS"
        MnuVenIco.Size = New Size(174, 22)
        MnuVenIco.Text = "Reorganizar &Iconos"
        ' 
        ' MnuVenImp
        ' 
        MnuVenImp.Name = "MnuVenImp"
        MnuVenImp.Tag = "PROVEEDORES_VENTANAS_IMPRIMIR_VENTANA"
        MnuVenImp.Size = New Size(174, 22)
        MnuVenImp.Text = "&Imprimir Ventana"
        ' 
        ' MnuAce
        ' 
        MnuAce.DropDownItems.AddRange(New ToolStripItem() {MnuAceAce, MnuAceAyu})
        MnuAce.Name = "MnuAce"
        MnuAce.Tag = "PROVEEDORES_"
        MnuAce.Size = New Size(24, 20)
        MnuAce.Text = "&?"
        ' 
        ' MnuAceAce
        ' 
        MnuAceAce.Name = "MnuAceAce"
        MnuAceAce.Tag = "PROVEEDORES__ACERCA_DE"
        MnuAceAce.Size = New Size(135, 22)
        MnuAceAce.Text = "&Acerca de..."
        ' 
        ' MnuAceAyu
        ' 
        MnuAceAyu.Name = "MnuAceAyu"
        MnuAceAyu.Tag = "PROVEEDORES__AYUDA"
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
        ' SectoresToolStripMenuItem
        ' 
        SectoresToolStripMenuItem.Name = "SectoresToolStripMenuItem"
        SectoresToolStripMenuItem.Size = New Size(209, 22)
        SectoresToolStripMenuItem.Text = "Sectores"
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
    Friend WithEvents MnuCompras As ToolStripMenuItem
    Friend WithEvents SolicitudComprasToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ConsultarComprasToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AutorizarComprasToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ComprasInteligentesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MnuVehiculos As ToolStripMenuItem
    Friend WithEvents MnuVehVehiculos As ToolStripMenuItem
    Friend WithEvents MnuVehTipos As ToolStripMenuItem
    Friend WithEvents MnuVehRepuestos As ToolStripMenuItem
    Friend WithEvents MnuVehServicios As ToolStripMenuItem
    Friend WithEvents SectoresToolStripMenuItem As ToolStripMenuItem


End Class
