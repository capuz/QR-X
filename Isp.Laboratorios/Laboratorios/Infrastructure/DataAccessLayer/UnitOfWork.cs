using System;
using Isp.Laboratorios.Infrastructure.DataAccessLayer;
using Isp.Laboratorios.Models;

namespace Isp.Laboratorios.DataAccessLayer
{
    public partial class UnitOfWork : IDisposable
    {
        private LaboratorioEntities _dbContext = new LaboratorioEntities();

        private FuncionalidadesRepository funcionalidadesRepository;
        private SistemasRepository sistemasRepository;
        private UsuariosRepository usuariosRepository;
        private AmbientesRepository ambientesRepository;

        public FuncionalidadesRepository Funcionalidades
        {
            get
            {
                return funcionalidadesRepository ??
                       (funcionalidadesRepository = new FuncionalidadesRepository(_dbContext));
            }
        }
        public SistemasRepository Sistemas
        {
            get { return sistemasRepository ?? (sistemasRepository = new SistemasRepository(_dbContext)); }
        }
        public UsuariosRepository Usuarios
        {
            get { return usuariosRepository ?? (usuariosRepository = new UsuariosRepository(_dbContext)); }
        }
        public AmbientesRepository Ambientes
        {
            get { return ambientesRepository ?? (ambientesRepository = new AmbientesRepository(_dbContext)); }
        }

        public void GuardarCambios()
        {
            _dbContext.SaveChanges();
        }
        public LaboratorioEntities DbContext
        {
            get { return _dbContext; }
        }
        public bool LazyLoadingEnabled
        {
            get { return _dbContext.Configuration.LazyLoadingEnabled; }
            set { _dbContext.Configuration.LazyLoadingEnabled = value; }
        }
        public bool ProxyCreationEnabled
        {
            get { return _dbContext.Configuration.ProxyCreationEnabled; }
            set { _dbContext.Configuration.ProxyCreationEnabled = value; }
        }
        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                    _dbContext.Dispose();

            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }
    }
}