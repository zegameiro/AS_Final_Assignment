namespace Piranha.Models
{
    [Serializable]
    public class Subscription
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string EventType { get; set; }
        public string Filter { get; set; }
        public string CallbackUrl { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}