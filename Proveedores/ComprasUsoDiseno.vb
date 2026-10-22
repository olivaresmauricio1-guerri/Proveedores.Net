Imports System.ComponentModel

Friend Module ComprasUsoDiseno
    Public Function EstaEnDiseno(control As Control) As Boolean
        Return LicenseManager.UsageMode = LicenseUsageMode.Designtime OrElse
               (control.Site IsNot Nothing AndAlso control.Site.DesignMode)
    End Function
End Module
