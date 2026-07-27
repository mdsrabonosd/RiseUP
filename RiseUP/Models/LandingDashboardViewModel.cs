namespace RiseUp.Models
{
    public class LandingDashboardViewModel
    {
        // Lists to showcase on the dashboard
        public IEnumerable<StartupIdea> FeaturedIdeas { get; set; } = new List<StartupIdea>();
        public IEnumerable<StartupIdea> RecentIdeas { get; set; } = new List<StartupIdea>();

        // Platform Summary Stats
        public int TotalIdeasCount { get; set; }
        public int TotalFoundersCount { get; set; }
        public int TotalInvestorsCount { get; set; }
        public int TotalMentorsCount { get; set; }

        // Selected category filter
        public string SelectedCategory { get; set; } = "All";
    }
}