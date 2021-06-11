using EventStore.ClientAPI;

namespace marketplace.infrastructure.CheckPoints
{
    public class checkpoint
    {
        public string Id { get; set; }
        public Position Position { get; set; }
    }
}