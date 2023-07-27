using Isp.Laboratorios.DataAccessLayer.Repositories;

namespace Isp.Laboratorios.DataAccessLayer
{

    public partial class UnitOfWork
    {
        private SolicitudRepository _solicitudRepository;
        private MuestraRepository _muestraRepository;
        private ExamenRepository _examenRepository;
        private PrestacionRepository _prestacionRepository;
        private EstadoRepository _estadoRepository;
        private LaboratorioRepository _laboratorioRepository;
        //private PacientesRepository _pacienteRepository;
        private BitacoraExamenRepository _bitacoraExamenRepository;
        private MotivoRechazoRepository _motivoRechazoRepository;
        private SeccionRepository _seccionRepository;
        private RechazoRepository _rechazoRepository;
        private MuestraObservacionRepository _muestraObservacionRepository;
        private FeriadoRepository _feriadoRepository;
        private TipoObservacionRepository _tipoObservacionRepository;
        private AnalitoRepository _analitoRepository;
        private MetodoRepository _metodoRepository;
        private NormaRepository _normaRepository;
        private LimiteCuentificacionRepository _limiteCuentificacionRepository;
        private LimiteDeteccionRepository _limiteDeteccionRepository;
        private LimiteMaximoPermitidoRepository _limiteMaximoPermitivoRepository;
        private UnidadMedidaRepository _unidadMedidaRepository;
        private EstadoIncidenteRepository _estadoIncidenteRepository;
        private ResultadoAnalitoRepository _resultadoAnalitoRepository;
        private BitacoraResultadoAnalitoRepository _bitacoraResultadoAnalitoRepository;
        public SolicitudRepository Solicitudes
        {
            get { return _solicitudRepository ?? (_solicitudRepository = new SolicitudRepository(_dbContext)); }
        }
        public MuestraRepository Muestras
        {
            get { return _muestraRepository ?? (_muestraRepository = new MuestraRepository(_dbContext)); }
        }
        public ExamenRepository Examenes
        {
            get { return _examenRepository ?? (_examenRepository = new ExamenRepository(_dbContext)); }
        }
        public PrestacionRepository Prestaciones
        {
            get { return _prestacionRepository ?? (_prestacionRepository = new PrestacionRepository(_dbContext)); }
        }
        public EstadoRepository Estados
        {
            get { return _estadoRepository ?? (_estadoRepository = new EstadoRepository(_dbContext)); }
        }
        public LaboratorioRepository Laboratorios
        {
            get { return _laboratorioRepository ?? (_laboratorioRepository = new LaboratorioRepository(_dbContext)); }
        }
        //public PacientesRepository Pacientes
        //{
        //    get { return _pacienteRepository ?? (_pacienteRepository = new PacientesRepository(_dbContext)); }
        //}
        public BitacoraExamenRepository BitacoraExamenes
        {
            get { return _bitacoraExamenRepository ?? (_bitacoraExamenRepository = new BitacoraExamenRepository(_dbContext)); }
        }
        public MotivoRechazoRepository MotivosRechazo
        {
            get { return _motivoRechazoRepository ?? (_motivoRechazoRepository = new MotivoRechazoRepository(_dbContext)); }
        }
        public SeccionRepository Secciones
        {
            get { return _seccionRepository ?? (_seccionRepository = new SeccionRepository(_dbContext)); }
        }
        public RechazoRepository Rechazos
        {
            get { return _rechazoRepository ?? (_rechazoRepository = new RechazoRepository(_dbContext)); }
        }
        public MuestraObservacionRepository MuestraObservaciones
        {
            get { return _muestraObservacionRepository ?? (_muestraObservacionRepository = new MuestraObservacionRepository(_dbContext)); }
        }
        public FeriadoRepository Feriados
        {
            get { return _feriadoRepository ?? (_feriadoRepository = new FeriadoRepository(_dbContext)); }
        }
        public TipoObservacionRepository TiposObservaciones
        {
            get { return _tipoObservacionRepository ?? (_tipoObservacionRepository = new TipoObservacionRepository(_dbContext)); }
        }
        public AnalitoRepository Analitos
        {
            get { return _analitoRepository ?? (_analitoRepository = new AnalitoRepository(_dbContext)); }
        }
        public MetodoRepository Metodos
        {
            get { return _metodoRepository ?? (_metodoRepository = new MetodoRepository(_dbContext)); }
        }
        public NormaRepository Normas
        {
            get { return _normaRepository ?? (_normaRepository = new NormaRepository(_dbContext)); }
        }
        public LimiteCuentificacionRepository LimitesCuentificaciones
        {
            get { return _limiteCuentificacionRepository ?? (_limiteCuentificacionRepository = new LimiteCuentificacionRepository(_dbContext)); }
        }
        public LimiteDeteccionRepository LimitesDetecciones
        {
            get { return _limiteDeteccionRepository ?? (_limiteDeteccionRepository = new LimiteDeteccionRepository(_dbContext)); }
        }
        public LimiteMaximoPermitidoRepository LimitesMaximosPermitidos
        {
            get { return _limiteMaximoPermitivoRepository ?? (_limiteMaximoPermitivoRepository = new LimiteMaximoPermitidoRepository(_dbContext)); }
        }
        public UnidadMedidaRepository UnidadesMedidas
        {
            get { return _unidadMedidaRepository ?? (_unidadMedidaRepository = new UnidadMedidaRepository(_dbContext)); }
        }
        public EstadoIncidenteRepository EstadosIncidentes
        {
            get { return _estadoIncidenteRepository ?? (_estadoIncidenteRepository = new EstadoIncidenteRepository(_dbContext)); }
        }
        public ResultadoAnalitoRepository ResultadosAnalitos
        {
            get { return _resultadoAnalitoRepository ?? (_resultadoAnalitoRepository = new ResultadoAnalitoRepository(_dbContext)); }
        }
       public BitacoraResultadoAnalitoRepository BitacoraResultadosAnalitos
        {
            get { return _bitacoraResultadoAnalitoRepository ?? (_bitacoraResultadoAnalitoRepository = new BitacoraResultadoAnalitoRepository(_dbContext)); }
        }
    }
}