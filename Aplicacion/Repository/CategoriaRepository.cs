using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;
using Microsoft.EntityFrameworkCore;
using Aplicacion.Repository.Generic;

namespace Aplicacion.Repository;
public class CategoriaRepository : GenericRepository<Categoria>, ICategoria
{
    private readonly DbAppContext _Context;
    public CategoriaRepository(DbAppContext context) : base(context)
    {
        _Context = context;
    }
    public override async Task<IEnumerable<Categoria>> GetAllAsync()
    {
        return await _Context.Set<Categoria>()
            //.Include(p => p.foraneas)
            .ToListAsync();
    }
}
