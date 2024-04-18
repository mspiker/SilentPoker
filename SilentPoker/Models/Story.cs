using static System.Net.WebRequestMethods;

namespace SilentPoker.Models
{
    public class Story
    {
        public string? Short_description { get; set; }
        public string? Description { get; set; }
        public string? Product { get; set; }
        public string? Story_points { get; set; }
        public string? Number { get; set; }
        public string? Sys_id { get; set; }
        public int MyVote { get; set; }
        public string? Priority { get; set; }
        public List<Vote>? Votes { get; set; }
        public double VoteAverage { get; set; }
        public double VoteVariance { get; set; }
        public double VoteParticipation { get; set; }
        public string? Url { get => $"/now/nav/ui/classic/params/target/rm_story.do%3Fsys_id%3D{Sys_id}%26sysparm_stack%3D%26sysparm_view%3D"; }

        public void CaclulateVote(List<Member> roomMembers)
        {
            // Determine qualifying votes
            int qualifyingMembers = 0;
            int votingMembers = 0;
            int sumOfVotes = 0;
            foreach (var member in roomMembers)
            {
                if (member.Role == MemberRole.Developer || member.Role == MemberRole.ScrumMaster)
                {
                    qualifyingMembers++;
                    if (Votes.Any(v => v.UserId == member.UserId))
                    {
                        votingMembers++;
                        sumOfVotes += Votes.First(v => v.UserId == member.UserId).VoteValue;
                    }
                }
            }

            if (Votes == null || Votes.Count == 0 || qualifyingMembers == 0)
            {
                
            } else
            {
                if (votingMembers == 0)
                {
                    this.VoteAverage = 0;
                    this.VoteParticipation = 0;
                    this.VoteVariance = 0;
                }
                else
                {
                    this.VoteAverage = sumOfVotes / votingMembers;
                    this.VoteParticipation = (votingMembers * 1.0) / (qualifyingMembers * 1.0);
                    this.VoteVariance = Votes.Sum(v => Math.Pow(v.VoteValue - VoteAverage, 2));
                }
            }

            
        }

    }
}
