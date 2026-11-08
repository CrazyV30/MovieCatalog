namespace backEnd.Contracts
{
    public class CommentRequest
    {
        public int MovieId { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
    }
}
