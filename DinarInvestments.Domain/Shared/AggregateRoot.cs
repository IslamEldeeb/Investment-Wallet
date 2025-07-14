namespace DinarInvestments.Domain.Shared;

public abstract class AggregateRoot
{
    public long Id { get; protected set; }
}