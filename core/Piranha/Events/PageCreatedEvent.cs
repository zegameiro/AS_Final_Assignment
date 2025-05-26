public class PageCreatedEvent
{
    public Guid PageId { get; set; }
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; }
}