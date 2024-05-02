using Dominio.Entities;
using Dominio.Interfaces.Generic;
namespace Dominio.Interfaces;

    public interface ICandidato : IGenericRepository<Candidato> {

        // Consulta #1
        List<Candidato> ObtenerPerfilesPorCiudad(string ciudad);

        // Consulta #2
        List<Candidato> BuscarPorNombre(string nombre);

        // Consulta #3
        List<Candidato> BuscarPorGenero(string genero);

        // Consulta #4
        List<Candidato> BuscarPorExperiencia(int experienciaMinima);

        // Consulta #5
        int ObtenerCantidadCandidatos();

        // Consulta #6
        IEnumerable<Candidato> FiltrarPorEdad(int edadMinima, int edadMaxima);

    }
