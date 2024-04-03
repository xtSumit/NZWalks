using System.ComponentModel.DataAnnotations;

namespace NZWalks.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MaxLength(3, ErrorMessage = "Code has to be a maximum of 3 Characters!")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Code has to be a maximum of 100 Characters!")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
