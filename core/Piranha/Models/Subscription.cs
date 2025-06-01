namespace Piranha.Models
{
    [Serializable]
    public class Subscription
    {
        public Guid Id { get; set; }
        public string EventStatus { get; set; }
        public string EventType { get; set; }
        public string Tags { get; set; }
        public string CallbackUrl { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}