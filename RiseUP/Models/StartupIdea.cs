using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiseUp.Models
{
    public class StartupIdea
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Tagline { get; set; } = string.Empty;

        [Required]
        public string DetailedDescription { get; set; } = string.Empty;

        public string Category { get; set; } = "General";
        public string? PitchDeckUrl { get; set; }
        public string? MediaUrl { get; set; }
        public decimal FundingGoal { get; set; }

        // Flagged to showcase on the main landing dashboard
        public bool IsFeatured { get; set; } = false;
        public int ViewsCount { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Key linking the idea to its Founder
        [Required]
        public string FounderId { get; set; } = string.Empty;

        [ForeignKey("FounderId")]
        public ApplicationUser? Founder { get; set; }
    }
}