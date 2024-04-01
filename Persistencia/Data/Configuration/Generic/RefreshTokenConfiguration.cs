using Dominio.Entities;
using Dominio.Entities.GenericEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken> {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {

            builder.ToTable("RefreshToken");

            builder.Property(p => p.Id)
            .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
            .HasColumnName("IdRefreshToken")
            .HasColumnType("int")
            .IsRequired();
            
            
            
            
            
            
            
            
            
            
            
            
            builder.HasData(
                new {
                    
                }
            );


        }
    }
