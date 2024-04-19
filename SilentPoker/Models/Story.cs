using System.Diagnostics.Metrics;
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

        public void CaclulateVote(Room room)
        {
            int membersCastingVote = 0;
            int qualifiedMembers = 0;
            int votingMembers = 0;
            int sumOfVotes = 0;

            List<Vote> _votes = new List<Vote>();

            // Build a local list of votes to work with
            foreach(var vote in Votes)
                _votes.Add(new Vote { UserId = vote.UserId, VoteValue = vote.VoteValue });

            // Get the number of eligible voting members
            votingMembers = room.Members.Count(m => m.Role == MemberRole.Developer || m.Role == MemberRole.ScrumMaster);
                        
            // Remove any votes that are not from developers or scrum masters since only their votes will count
            _votes.RemoveAll(v => !room.Members.Any(m => m.UserId == v.UserId && (m.Role == MemberRole.Developer || m.Role == MemberRole.ScrumMaster)));

            // Trim the votes to remove the highest and lowest values
            _votes = (room.Trimming) ? _votes.OrderBy(v => v.VoteValue).Skip(1).Take(_votes.Count - 2).ToList() : _votes;

            // Calculate the average, variance, and participation
            foreach (var vote in _votes)
            {
                if (vote.VoteValue > -1)
                {
                    membersCastingVote++;
                    sumOfVotes += vote.VoteValue;
                }
            }

            // So we are not incorrectly showing a lower participation rate we need a list of the members who voted regardless of trimming.
            foreach (var vote in Votes)
            {
                // If there is an entry in the Votes object, they have voted.
                qualifiedMembers++;
            }

            // Nothing to calculate if there are no qualified members
            if (membersCastingVote == 0)
            {
                VoteAverage = 0;
                VoteVariance = 0;
                VoteParticipation = 0;
                return;
            }
            VoteAverage = sumOfVotes / membersCastingVote;
            VoteVariance = _votes.Sum(v => Math.Pow(v.VoteValue - VoteAverage, 2)) / membersCastingVote;
            VoteParticipation = 100 * ((qualifiedMembers * 1.0) / (votingMembers * 1.0));
        }

    }
}
