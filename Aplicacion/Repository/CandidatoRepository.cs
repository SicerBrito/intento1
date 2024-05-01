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

    public List<Candidato> ObtenerPerfilesPorCiudad(string ciudad)
    {
        return _Context.Candidatos!
            .Where(c => c.CiudadRecidencia == ciudad)
            .ToList();
    }
}
