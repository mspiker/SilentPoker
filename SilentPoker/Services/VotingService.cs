using SilentPoker.Models;

namespace SilentPoker.Services
{
    public static class VotingService
    {
        public static List<VoteDetail> VotingOptions = new List<VoteDetail>
        {
            new VoteDetail() { VoteValue = -1, VoteStyle = "badge rounded-pill bg-dark", VoteText = "Not Enough Info", VoteCard = "?" },
            new VoteDetail() { VoteValue = -2, VoteStyle = "badge rounded-pill bg-secondary", VoteText = "Pass Vote", VoteCard = "Pass" },
            new VoteDetail() { VoteValue = -3, VoteStyle = "badge rounded-pill bg-dark", VoteText = "No Vote", VoteCard = "Not Vote" },
            new VoteDetail() { VoteValue = 0, VoteStyle = "badge rounded-pill bg-success", VoteText = "0 Story Point", VoteCard = "0" },
            new VoteDetail() { VoteValue = 1, VoteStyle = "badge rounded-pill bg-success", VoteText = "1 Story Point", VoteCard = "1" },
            new VoteDetail() { VoteValue = 2, VoteStyle = "badge rounded-pill bg-success", VoteText = "2 Story Points", VoteCard = "2" },
            new VoteDetail() { VoteValue = 3, VoteStyle = "badge rounded-pill bg-success", VoteText = "3 Story Points", VoteCard = "3" },
            new VoteDetail() { VoteValue = 5, VoteStyle = "badge rounded-pill bg-primary", VoteText = "5 Story Points", VoteCard = "5" },
            new VoteDetail() { VoteValue = 8, VoteStyle = "badge rounded-pill bg-warning text-dark", VoteText = "8 Story Points", VoteCard = "8" },
            new VoteDetail() { VoteValue = 13, VoteStyle = "badge rounded-pill bg-warning text-dark", VoteText = "13 Story Points", VoteCard = "13" },
            new VoteDetail() { VoteValue = 20, VoteStyle = "badge rounded-pill bg-danger", VoteText = "20 Story Points", VoteCard = "20" },
            new VoteDetail() { VoteValue = 40, VoteStyle = "badge rounded-pill bg-danger", VoteText = "40 Story Points", VoteCard = "40" }
        };

        public static VoteDetail Vote(int VoteValue)
        {
            return VotingOptions.FirstOrDefault(x => x.VoteValue == VoteValue);
        }

    };
}
