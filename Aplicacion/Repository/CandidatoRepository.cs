using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;
using Microsoft.EntityFrameworkCore;
using Aplicacion.Repository.Generic;

namespace Aplicacion.Repository;
public class CandidatoRepository : GenericRepository<Candidato>, ICandidato
{
    private readonly DbAppContext _Context;
    public CandidatoRepository(DbAppContext context) : base(context)
    {
        _Context = context;
    }

    public override async Task<IEnumerable<Candidato>> GetAllAsync()
    {
        return await _Context.Set<Candidato>()
            //.Include(p => p.foraneas)
            .ToListAsync();
    }


    // Consulta #1
    public List<Candidato> ObtenerPerfilesPorCiudad(string ciudad)
    {
        return _Context.Candidatos!
            .Where(c => c.CiudadRecidencia == ciudad)
            .ToList();
    }

    // Consulta #2
    public List<Candidato> BuscarPorNombre(string nombre)
    {
        return _Context.Candidatos!
            .Where(c => c.NombreCompleto!.Contains(nombre))
            .ToList();
    }

    // Consulta #3
    public List<Candidato> BuscarPorGenero(string genero)
    {
        return _Context.Candidatos!
            .Where(c => c.Genero!.ToLower() == genero.ToLower())
            .ToList();
    }

    // Consulta #4
    public List<Candidato> BuscarPorExperiencia(int experienciaMinima)
    {
        // Obtenemos todos los candidatos de la base de datos
        var candidatos = _Context.Candidatos!.ToList();

        // Filtramos los candidatos en memoria
        return candidatos
            .Where(c => ExtractExperiencia(c.Experiencia!) >= experienciaMinima)
            .ToList();
    }

    // Método para extraer la experiencia numérica de la cadena
    private int ExtractExperiencia(string experiencia)
    {
        // Extraemos la parte numérica de la cadena
        string experienciaNumerica = experiencia.Split(' ')[0];

        // Convertimos la parte numérica a entero
        return int.Parse(experienciaNumerica);
    }


    // Consulta #5
    public int ObtenerCantidadCandidatos()
    {
        return _Context.Candidatos!.Count();
    }


    // Consulta #6
    public IEnumerable<Candidato> FiltrarPorEdad(int edadMinima, int edadMaxima)
        {
            var fechaActual = DateTime.Today;
            var fechaNacimientoMinima = fechaActual.AddYears(-edadMaxima);
            var fechaNacimientoMaxima = fechaActual.AddYears(-edadMinima);

            return _Context.Candidatos!
                .Where(c => c.FechaNacimiento >= fechaNacimientoMinima && c.FechaNacimiento <= fechaNacimientoMaxima)
                .ToList();
        }

}
