namespace SilentPoker.Models
{
    public class Vote
    {
        public string? StoryId { get; set; }
        public string? UserId { get; set; }
        public int VoteValue { get; set; }
    }
}
