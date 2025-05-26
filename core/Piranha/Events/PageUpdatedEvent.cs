public class PageUpdatedEvent
{
    public Guid PageId { get; set; }
    public string Title { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
