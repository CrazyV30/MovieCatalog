namespace backEnd.Contracts
{
    public class CommentResponse
    {
        public int Id { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string CreatedAt { get; set; } = string.Empty;
    }
}
