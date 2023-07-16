namespace be.DTOs
{
    public class PostDTO
    {
        public int PostId { get; set; }
        public int? SubjectId { get; set; }
        public int? AccountId { get; set; }
        public string? PostText { get; set; }
        public string? PostFile { get; set; }
        public string? Status { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
