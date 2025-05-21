
using System.ComponentModel.DataAnnotations;
using AppTrackV2.Models;

namespace AppTrackV2.Models
{
    public class Application
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public required string Title { get; set; }
        public required string Company { get; set; }
        public required string Notes { get; set; }
        public required string Status { get; set; }

        public required string Link { get; set; }
        public DateTime DateAdded { get; set; } 

        public required string UserId { get; set; }
        public required ApplicationUser ApplicationUser { get; set; }
    }
}
