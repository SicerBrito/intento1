using Dominio.Entities.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration.Generic;
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario> {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.Property(p => p.Id)
                .HasAnnotation("MysqlValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasColumnName("IdUsuario")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.UserName)
                .HasColumnName("UserName")
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Email)
                .HasColumnName("Email")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder.HasIndex(p => p.UserName)
                .IsUnique();

            builder.HasIndex(p => p.Email)
                .IsUnique();
            
            builder.Property(p => p.Password)
                .HasColumnName("Password")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder.HasMany(p => p.RefreshTokens)
                .WithOne(p => p.Usuarios)
                .HasForeignKey(p => p.UsuarioId);

            // Define la configuracion de la entidad UsuariosRoles
            builder.HasMany(p => p.Roles)
                .WithMany(p => p.Usuarios)
                .UsingEntity<UsuarioRoles>(
                    j => j
                        .HasOne(p => p.Roles)
                        .WithMany(p => p.UsuarioRoles)
                        .HasForeignKey(p => p.RolId),

                    j => j
                        .HasOne(p => p.Usuarios)
                        .WithMany(p => p.UsuarioRoles)
                        .HasForeignKey(p => p.UsuarioId),

                    j => {
                        j.HasKey(p => new { p.UsuarioId, p.RolId});
                    }
                );

            builder.HasData(
                new {
                    Id = 1,
                    UserName = "Sicer Brito",
                    Email = "britodelgado514@gmail.com",
                    Password = "123456",
                    RolId = 1
                }
            );
        }
    }
