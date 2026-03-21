namespace MyApp.Domain.Core.Models;

public abstract class BaseEntity<TKey>
    where TKey : notnull
{
    public TKey Id { get; protected set; } = default!;

}