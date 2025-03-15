
using Aplicacion.Datos;
using Dominio.ListasDeTareas;
using Dominio.Primitivos;
using Dominio.Tareas;

namespace Infraestructure.Persistencia
{
    public class AplicacionContextoDb : DbContext, IAplicacionContextoDb, IUnitOfWork
    {

        private readonly IPublisher _publisher;

        public DbSet<ListaDeTarea> ListaDeTareas { get; set; }
        public DbSet<Tarea> Tareas { get; set; }

        public AplicacionContextoDb(DbContextOptions options, IPublisher publisher) : base(options)
        {
            _publisher = publisher;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AplicacionContextoDb).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var eventosDeDominio = ChangeTracker.Entries<AggregateRoot>()
                .Select(e => e.Entity)
                .Where(e => e.GetDomainEvents().Any())
                .SelectMany(e => e.GetDomainEvents());

            var resultado = await base.SaveChangesAsync(cancellationToken);

            foreach (var evento in eventosDeDominio)
            {
                await _publisher.Publish(evento, cancellationToken);
            }

            return resultado;
        }
    }
}
