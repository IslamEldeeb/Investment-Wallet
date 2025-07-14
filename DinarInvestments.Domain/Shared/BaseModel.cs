namespace DinarInvestments.Domain.Shared;

public abstract class BaseModel<TKey>
    where TKey : struct
{
    public TKey Id { get; protected set; }

    public DateTime CreationDate { get; protected set; }
}
