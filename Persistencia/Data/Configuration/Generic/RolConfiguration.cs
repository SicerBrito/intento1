using Dominio.Entities.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration.Generic;
    public class RolConfiguration : IEntityTypeConfiguration<Rol> {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.ToTable("Rol");

            builder.Property(p => p.Id)
                .HasAnnotation("MysqlValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasColumnName("IdRol")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.Nombre)
                .HasColumnName("Nombre")
                .HasColumnType("varchar")
                .HasMaxLength(25)
                .IsRequired();

            builder.HasData(
                new {
                    Id = 1,
                    Nombre = "Administrador"
                },
                new {
                    Id = 2,
                    Nombre = "Gerente"
                },
                new {
                    Id = 3,
                    Nombre = "Empleado"
                },
                new {
                    Id = 4,
                    Nombre = "Cliente"
                }            
            );
        }
    }
