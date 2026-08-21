Public Class EmpresaService

    Public Function Buscar(texto As String) As List(Of Empresa)
        Using ctx = New ProveedoresDbContext()
            Dim query = ctx.Empresas.AsQueryable()

            If Not String.IsNullOrWhiteSpace(texto) Then
                Dim numVal As Short
                If Short.TryParse(texto, numVal) Then
                    query = query.Where(Function(e) _
                    e.Descripcion.Contains(texto) OrElse
                    e.Codigo = numVal OrElse
                    e.IdEmpresa = CInt(numVal))
                Else
                    query = query.Where(Function(e) e.Descripcion.Contains(texto))
                End If
            End If

            Return query.OrderBy(Function(e) e.IdEmpresa).ToList()
        End Using
    End Function

    Public Function CodigoExiste(codigo As String, Optional excluirId As Integer = 0) As Boolean
        Dim numVal As Short
        If Not Short.TryParse(codigo, numVal) Then Return False
        Using ctx = New ProveedoresDbContext()
            Return ctx.Empresas.Any(Function(e) e.Codigo = numVal AndAlso e.IdEmpresa <> excluirId)
        End Using
    End Function

    Public Sub Agregar(empresa As Empresa)
        Using ctx = New ProveedoresDbContext()
            ctx.Empresas.Add(empresa)
            ctx.SaveChanges()
        End Using
    End Sub

    Public Sub Actualizar(empresa As Empresa)
        Using ctx = New ProveedoresDbContext()
            ctx.Empresas.Update(empresa)
            ctx.SaveChanges()
        End Using
    End Sub

    Public Sub Eliminar(id As Integer)
        Using ctx = New ProveedoresDbContext()
            Dim e = ctx.Empresas.Find(id)
            If e IsNot Nothing Then
                ctx.Empresas.Remove(e)
                ctx.SaveChanges()
            End If
        End Using
    End Sub

End Class