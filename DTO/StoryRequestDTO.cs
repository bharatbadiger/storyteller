namespace Storyteller.DTO
{
    public class StoryRequestDTO
    {
        public required CategoryRequest Category { get; set; }
        public required AuthorRequest Author { get; set; }
        public string? Name { get; set; }
        public string? Image { get; set; }
        public string? Tags { get; set; }
    }

    public class CategoryRequest
    {
        public long Id { get; set; }
    }

    public class AuthorRequest
    {
        public long Id { get; set; }
    }
}