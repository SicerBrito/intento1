using Dominio.Entities;
using Dominio.Entities.GenericEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class TareaConfiguration : IEntityTypeConfiguration<Tarea>
{
    public void Configure(EntityTypeBuilder<Tarea> builder)
    {

        builder.ToTable("Tarea");

        builder.Property(p => p.Id)
            .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
            .HasColumnName("IdTarea")
            .HasColumnType("int")
            .IsRequired();
        
        builder.Property(p => p.UsuarioId)
            .HasColumnName("UsuarioId")
            .HasColumnType("int")
            .IsRequired();

        builder.HasOne(p => p.Usuarios)
            .WithMany(p => p.Tareas)
            .HasForeignKey(p => p.UsuarioId);

        builder.Property(p => p.Titulo)
            .HasColumnName("Titulo")
            .HasColumnType("varchar")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.Descripcion)
            .HasColumnName("Descripcion")
            .HasColumnType("varchar")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(p => p.FechaVencimiento)
            .HasColumnName("FechaVencimiento")
            .HasColumnType("DateTime")
            .IsRequired();

        builder.Property(p => p.CategoriaId)
            .HasColumnName("CategoriaId")
            .HasColumnType("int")
            .IsRequired();

        builder.HasOne(p => p.Categorias)
            .WithMany(p => p.Tareas)
            .HasForeignKey(p => p.CategoriaId);
        
        
        
        
        
        
        
        
        builder.HasData(
            new Tarea
            {
                Id = 1,
                UsuarioId = 1,
                Titulo = "Completar informe de ventas",
                Descripcion = "Completar el informe de ventas del último trimestre.",
                FechaVencimiento = new DateTime(2024, 04, 10),
                CategoriaId = 1
            }
        );
    }
}
