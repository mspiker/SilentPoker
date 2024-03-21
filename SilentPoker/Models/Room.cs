namespace SilentPoker.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Sprint { get; set; } = "";
        public string Filter { get; set; } = "";
        public bool AllowPass { get; set; }
        public bool OpenVoting { get; set; }
        public List<Member> Members { get; set; } = new List<Member>();

    }
}
