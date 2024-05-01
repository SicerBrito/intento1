using Dominio.Entities;
using Dominio.Entities.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class CandidatoConfiguration : IEntityTypeConfiguration<Candidato>
{
    public void Configure(EntityTypeBuilder<Candidato> builder)
    {

        builder.ToTable("Candidato");

        builder.Property(p => p.Id)
            .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
            .HasColumnName("IdCandidato")
            .HasColumnType("int")
            .IsRequired();
        
        builder.Property(p => p.NombreCompleto)
            .HasColumnName("NombreCompleto")
            .HasColumnType("varchar")
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(p => p.FechaNacimiento)
            .HasColumnName("FechaNacimiento")
            .HasColumnType("DateTime")
            .IsRequired();

        builder.Property(p => p.Genero)
            .HasColumnName("Genero")
            .HasColumnType("varchar")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.Telefono)
            .HasColumnName("Telefono")
            .HasColumnType("varchar")
            .HasMaxLength(13)
            .IsRequired();

        builder.Property(p => p.CiudadRecidencia)
            .HasColumnName("CiudadRecidencia")
            .HasColumnType("varchar")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.Email)
            .HasColumnName("Email")
            .HasColumnType("varchar")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(p => p.DescripcionHabilidades)
            .HasColumnName("DescripcionHabilidades")
            .HasColumnType("varchar")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(p => p.Experiencia)
            .HasColumnName("Experiencia")
            .HasColumnType("varchar")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.UrlLinkedIn)
            .HasColumnName("UrlLinkedIn")
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();
        
        
        
        
        builder.HasData(
            new Candidato
            {
                Id = 1,
                NombreCompleto = "Juan Pérez",
                FechaNacimiento = new DateTime(1990, 5, 15),
                Genero = "Masculino",
                Telefono = "123456789",
                CiudadRecidencia = "Bogotá",
                Email = "juan@example.com",
                DescripcionHabilidades = "Desarrollo de software",
                Experiencia = "5 años",
                UrlLinkedIn = "https://www.linkedin.com/in/juanperez"
            },
            new Candidato
            {
                Id = 2,
                NombreCompleto = "María López",
                FechaNacimiento = new DateTime(1985, 10, 20),
                Genero = "Femenino",
                Telefono = "987654321",
                CiudadRecidencia = "Medellín",
                Email = "maria@example.com",
                DescripcionHabilidades = "Gestión de proyectos",
                Experiencia = "8 años",
                UrlLinkedIn = "https://www.linkedin.com/in/marialopez"
            },
            new Candidato
            {
                Id = 3,
                NombreCompleto = "Alex García",
                FechaNacimiento = new DateTime(1988, 8, 8),
                Genero = "Otro",
                Telefono = "456789123",
                CiudadRecidencia = "Cali",
                Email = "alex@example.com",
                DescripcionHabilidades = "Diseño gráfico",
                Experiencia = "3 años",
                UrlLinkedIn = "https://www.linkedin.com/in/alexgarcia"
            },
            new Candidato
            {
                Id = 4,
                NombreCompleto = "Ana Torres",
                FechaNacimiento = new DateTime(1992, 4, 25),
                Genero = "Femenino",
                Telefono = "741852963",
                CiudadRecidencia = "Barranquilla",
                Email = "ana@example.com",
                DescripcionHabilidades = "Marketing digital",
                Experiencia = "6 años",
                UrlLinkedIn = "https://www.linkedin.com/in/anatorres"
            },
            new Candidato
            {
                Id = 5,
                NombreCompleto = "David Martínez",
                FechaNacimiento = new DateTime(1987, 12, 12),
                Genero = "Masculino",
                Telefono = "369258147",
                CiudadRecidencia = "Cartagena",
                Email = "david@example.com",
                DescripcionHabilidades = "Desarrollo web",
                Experiencia = "7 años",
                UrlLinkedIn = "https://www.linkedin.com/in/davidmartinez"
            },
            new Candidato
            {
                Id = 6,
                NombreCompleto = "Laura Ramírez",
                FechaNacimiento = new DateTime(1993, 9, 18),
                Genero = "Femenino",
                Telefono = "654987321",
                CiudadRecidencia = "Bucaramanga",
                Email = "laura@example.com",
                DescripcionHabilidades = "Diseño UX/UI",
                Experiencia = "4 años",
                UrlLinkedIn = "https://www.linkedin.com/in/lauraramirez"
            },
            new Candidato
            {
                Id = 7,
                NombreCompleto = "Diego Sánchez",
                FechaNacimiento = new DateTime(1984, 7, 10),
                Genero = "Masculino",
                Telefono = "852369741",
                CiudadRecidencia = "Santa Marta",
                Email = "diego@example.com",
                DescripcionHabilidades = "Administración de bases de datos",
                Experiencia = "9 años",
                UrlLinkedIn = "https://www.linkedin.com/in/diegosanchez"
            },
            new Candidato
            {
                Id = 8,
                NombreCompleto = "Carolina Martín",
                FechaNacimiento = new DateTime(1991, 3, 27),
                Genero = "Femenino",
                Telefono = "987123654",
                CiudadRecidencia = "Cúcuta",
                Email = "carolina@example.com",
                DescripcionHabilidades = "Desarrollo móvil",
                Experiencia = "6 años",
                UrlLinkedIn = "https://www.linkedin.com/in/carolinamartin"
            },
            new Candidato
            {
                Id = 9,
                NombreCompleto = "Javier Fernández",
                FechaNacimiento = new DateTime(1989, 6, 8),
                Genero = "Masculino",
                Telefono = "369741852",
                CiudadRecidencia = "Pereira",
                Email = "javier@example.com",
                DescripcionHabilidades = "Gestión de proyectos",
                Experiencia = "7 años",
                UrlLinkedIn = "https://www.linkedin.com/in/javierfernandez"
            },
            new Candidato
            {
                Id = 10,
                NombreCompleto = "Valentina Ruiz",
                FechaNacimiento = new DateTime(1995, 11, 30),
                Genero = "Femenino",
                Telefono = "258147369",
                CiudadRecidencia = "Manizales",
                Email = "valentina@example.com",
                DescripcionHabilidades = "Desarrollo de aplicaciones móviles",
                Experiencia = "3 años",
                UrlLinkedIn = "https://www.linkedin.com/in/valentinaruiz"
            },
            new Candidato
            {
                Id = 11,
                NombreCompleto = "Gabriel Gómez",
                FechaNacimiento = new DateTime(1987, 2, 14),
                Genero = "Masculino",
                Telefono = "147258369",
                CiudadRecidencia = "Bogotá",
                Email = "gabriel@example.com",
                DescripcionHabilidades = "Análisis de datos",
                Experiencia = "5 años",
                UrlLinkedIn = "https://www.linkedin.com/in/gabrielgomez"
            },
            new Candidato
            {
                Id = 12,
                NombreCompleto = "Paula Herrera",
                FechaNacimiento = new DateTime(1990, 8, 22),
                Genero = "Femenino",
                Telefono = "123789456",
                CiudadRecidencia = "Medellín",
                Email = "paula@example.com",
                DescripcionHabilidades = "Marketing digital",
                Experiencia = "4 años",
                UrlLinkedIn = "https://www.linkedin.com/in/paulaherrera"
            },
            new Candidato
            {
                Id = 13,
                NombreCompleto = "Alejandro Torres",
                FechaNacimiento = new DateTime(1986, 5, 5),
                Genero = "Masculino",
                Telefono = "987654321",
                CiudadRecidencia = "Cali",
                Email = "alejandro@example.com",
                DescripcionHabilidades = "Desarrollo web",
                Experiencia = "8 años",
                UrlLinkedIn = "https://www.linkedin.com/in/alejandrotorres"
            },
            new Candidato
            {
                Id = 14,
                NombreCompleto = "Natalia Díaz",
                FechaNacimiento = new DateTime(1994, 12, 10),
                Genero = "Femenino",
                Telefono = "369852147",
                CiudadRecidencia = "Barranquilla",
                Email = "natalia@example.com",
                DescripcionHabilidades = "Diseño gráfico",
                Experiencia = "3 años",
                UrlLinkedIn = "https://www.linkedin.com/in/nataliadiaz"
            },
            new Candidato
            {
                Id = 15,
                NombreCompleto = "Andrés López",
                FechaNacimiento = new DateTime(1983, 6, 25),
                Genero = "Masculino",
                Telefono = "741258963",
                CiudadRecidencia = "Cartagena",
                Email = "andres@example.com",
                DescripcionHabilidades = "Administración de sistemas",
                Experiencia = "10 años",
                UrlLinkedIn = "https://www.linkedin.com/in/andreslopez"
            },
            new Candidato
            {
                Id = 16,
                NombreCompleto = "Sofía García",
                FechaNacimiento = new DateTime(1993, 7, 20),
                Genero = "Femenino",
                Telefono = "852369741",
                CiudadRecidencia = "Bucaramanga",
                Email = "sofia@example.com",
                DescripcionHabilidades = "Desarrollo de software",
                Experiencia = "6 años",
                UrlLinkedIn = "https://www.linkedin.com/in/sofiagarcia"
            },
            new Candidato
            {
                Id = 17,
                NombreCompleto = "Carlos Hernández",
                FechaNacimiento = new DateTime(1988, 4, 15),
                Genero = "Masculino",
                Telefono = "123456789",
                CiudadRecidencia = "Santa Marta",
                Email = "carlos@example.com",
                DescripcionHabilidades = "Gestión de proyectos",
                Experiencia = "7 años",
                UrlLinkedIn = "https://www.linkedin.com/in/carloshernandez"
            },
            new Candidato
            {
                Id = 18,
                NombreCompleto = "Isabella Martínez",
                FechaNacimiento = new DateTime(1990, 9, 25),
                Genero = "Femenino",
                Telefono = "987654321",
                CiudadRecidencia = "Cúcuta",
                Email = "isabella@example.com",
                DescripcionHabilidades = "Diseño UX/UI",
                Experiencia = "5 años",
                UrlLinkedIn = "https://www.linkedin.com/in/isabellamartinez"
            },
            new Candidato
            {
                Id = 19,
                NombreCompleto = "Miguel Rodríguez",
                FechaNacimiento = new DateTime(1985, 11, 12),
                Genero = "Masculino",
                Telefono = "369258147",
                CiudadRecidencia = "Pereira",
                Email = "miguel@example.com",
                DescripcionHabilidades = "Desarrollo móvil",
                Experiencia = "8 años",
                UrlLinkedIn = "https://www.linkedin.com/in/miguelrodriguez"
            },
            new Candidato
            {
                Id = 20,
                NombreCompleto = "Valeria López",
                FechaNacimiento = new DateTime(1992, 3, 30),
                Genero = "Femenino",
                Telefono = "741852963",
                CiudadRecidencia = "Manizales",
                Email = "valeria@example.com",
                DescripcionHabilidades = "Marketing digital",
                Experiencia = "6 años",
                UrlLinkedIn = "https://www.linkedin.com/in/valerialopez"
            }
        );



    }
}
