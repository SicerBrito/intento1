using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;
using Microsoft.EntityFrameworkCore;
using Aplicacion.Repository.Generic;

namespace Aplicacion.Repository;
public class TareaRepository : GenericRepository<Tarea>, ITarea
{
    private readonly DbAppContext _Context;
    public TareaRepository(DbAppContext context) : base(context)
    {
        _Context = context;
    }
    public override async Task<IEnumerable<Tarea>> GetAllAsync()
    {
        return await _Context.Set<Tarea>()
            //.Include(p => p.foraneas)
            .ToListAsync();
    }
}
