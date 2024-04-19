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
        /// <summary>
        /// When you drop the highest and lowest values before averaging numbers, 
        /// it's commonly referred to as trimming or winsorizing. This technique 
        /// helps mitigate the impact of outliers on the overall average, 
        /// providing a more robust estimate.
        /// </summary>
        public bool Trimming { get; set; }

    }
}
