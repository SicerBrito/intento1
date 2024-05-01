using Aplicacion.Repository;
using Aplicacion.Repository.Generic;
using Dominio.Interfaces;
using Dominio.Interfaces.Generic;
using Dominio.Interfaces.IUnitOfWork;
using Persistencia.Data;

namespace Aplicacion.UnitOfWork;
    public class UnitOfWork : IUnitOfWork, IDisposable{

        private readonly DbAppContext ? _Context;
        public UnitOfWork(DbAppContext context){
            _Context = context;
        }



        private RolRepository ? _Rol;
        private UsuarioRepository ? _Usuario;
        private UsuarioRolesRepository ? _UsuariosRoles;
        private CandidatoRepository ? _Candidato;





        public IUsuario? Usuarios => _Usuario ??= new UsuarioRepository( _Context! );
        public IRol? Roles => _Rol ??= new RolRepository(_Context!);
        public IUsuarioRoles? UsuarioRoles => _UsuariosRoles ??= new UsuarioRolesRepository(_Context!);
        public ICandidato? Candidatos => _Candidato ??= new CandidatoRepository(_Context!);





        public void Dispose()
        {
            _Context!.Dispose();
            GC.SuppressFinalize(this);
        }

        public Task<int> SaveAsync()
        {
            return _Context!.SaveChangesAsync();
        }

    }