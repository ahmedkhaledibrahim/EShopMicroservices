namespace BuildingBlocks.Messaging.Events
{
    public record OrderCreatedIntegrationEvent : IntegrationEvent
    {
        public string Username { get; set; }
    }
}
