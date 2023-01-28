using System.ComponentModel.DataAnnotations.Schema;

namespace Lumia.Models
{
    public class TeamMember
    {
        public int Id { get; set; }
        public int PositionId { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string About { get; set; }

        public string? TwitterLink { get; set; }
        public string? FacebookLink { get; set; }
        public string? InstagramLink { get; set; }
        public string? LinkedInLink { get; set; }

        public string? ImageName { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        public Position? Position { get; set; }
    }
}
