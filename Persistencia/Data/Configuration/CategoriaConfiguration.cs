using Dominio.Entities;
using Dominio.Entities.GenericEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {

        builder.ToTable("Categoria");

        builder.Property(p => p.Id)
            .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
            .HasColumnName("IdCategoria")
            .HasColumnType("int")
            .IsRequired();
        
        builder.Property(p => p.Nombre)
            .HasColumnName("Nombre")
            .HasColumnType("varchar")
            .HasMaxLength(20)
            .IsRequired();
        
        
        
        
        
        
        
        
        
        
        builder.HasData(
            new {
                Id = 1,
                Nombre = "Taller"
            },
            new {
                Id = 2,
                Nombre = "Tarea"
            },
            new {
                Id = 3,
                Nombre = "Evaluaciuon"
            }
        );


    }
}
