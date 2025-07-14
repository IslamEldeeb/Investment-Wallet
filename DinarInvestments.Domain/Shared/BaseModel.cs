namespace DinarInvestments.Domain.Shared;

public abstract class BaseModel<TKey>
    where TKey : struct
{
    public TKey Id { get; protected init; }

    public DateTime CreationDate { get; protected init; } = DateTime.UtcNow;
}
