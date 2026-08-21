Imports Microsoft.EntityFrameworkCore

Public Class ProveedoresDbContext
    Inherits DbContext

    Public Property Empresas As DbSet(Of Empresa)

    Protected Overrides Sub OnConfiguring(optionsBuilder As DbContextOptionsBuilder)
        optionsBuilder.UseSqlServer(General.ProveedoresConnectionString)
    End Sub

    Protected Overrides Sub OnModelCreating(modelBuilder As ModelBuilder)
        modelBuilder.Entity(Of Empresa)().ToTable("Empresas")
        modelBuilder.Entity(Of Empresa)().HasKey(Function(e) e.IdEmpresa)

    End Sub

End Class