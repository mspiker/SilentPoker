namespace SilentPoker.Models
{
    public class Member
    {
        public int RoomId { get; set; }
        public string UserId { get; set; } = "";
        public MemberRole Role { get; set; } = MemberRole.Observer;
        
    }

    public enum MemberRole
    {
        Observer = 1,
        ScrumMaster = 2,
        ProductOwner = 3,
        Developer = 4
    }
}
