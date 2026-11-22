namespace BuildingBlocks.Messaging.Events;

public record IntegrationEvents
{
    public Guid Id => Guid.NewGuid();
    public DateTime OccurredOn => DateTime.UtcNow;
    public string EventType => GetType().AssemblyQualifiedName;
}
