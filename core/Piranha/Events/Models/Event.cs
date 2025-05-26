
public class Event
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public EventStatus Status { get; set; }
    public EventType Type { get; set; }
    public Guid ContentId { get; set; }
}