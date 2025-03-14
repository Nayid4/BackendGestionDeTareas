namespace Dominio.Primitivos;

public abstract class AggregateRoot
{
    private readonly List<EventoDeDominio> _eventosDeDominio = new();

    public ICollection<EventoDeDominio> GetDomainEvents() => _eventosDeDominio;

    protected void Raise(EventoDeDominio eventoDeDominio)
    {
        _eventosDeDominio.Add(eventoDeDominio);
    }
}
