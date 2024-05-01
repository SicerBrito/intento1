using Dominio.Entities;
using Dominio.Interfaces.Generic;
namespace Dominio.Interfaces;

    public interface ICandidato : IGenericRepository<Candidato> {

        // Consulta #1
        List<Candidato> ObtenerPerfilesPorCiudad(string ciudad);

    }
