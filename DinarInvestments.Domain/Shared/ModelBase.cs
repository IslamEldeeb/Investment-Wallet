namespace DinarInvestments.Domain.Shared;

public abstract class ModelBase
{
    public long Id { get; protected set; }

    public DateTime CreationDate { get; protected set; }
}