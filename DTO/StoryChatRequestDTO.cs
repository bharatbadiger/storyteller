using Storyteller.Enums;

public class StoryChatRequestDTO
{
    public required StoryReference Story { get; set; }
    public string? Text { get; set; }
    public string? MediaUrl { get; set; }
    public MessageType MessageType { get; set; }
    public string? ReactionType { get; set; }
    public bool ReactionEnabled { get; set; }
    public ChatSender Sender { get; set; }
    public DateTime? ChatTimestamp { get; set; }
}

public class StoryReference
{
    public long Id { get; set; }
}
