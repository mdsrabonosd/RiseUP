using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RiseUp.Models
{
    // User roles defined across the ecosystem
    public enum UserType
    {
        Founder = 1,
        Investor = 2,
        Mentor = 3,
    
    }

    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        public UserType RoleType { get; set; }

        public string? ProfilePictureUrl { get; set; }
        public string? Bio { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relationships for Ideas and Messages
        public ICollection<StartupIdea>? Ideas { get; set; }
        public ICollection<ChatMessage>? SentMessages { get; set; }
        public ICollection<ChatMessage>? ReceivedMessages { get; set; }
    }
}