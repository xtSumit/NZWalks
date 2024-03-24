using NZWalks.Api.Models.DTO;

namespace NZWalks.Models.DTO
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        //Difficult and Regions IDs removed because we already have them in DifficultyDto and RegionDto
        //public Guid DifficultyId { get; set; }
        //public Guid RegionId { get; set; }

        public DifficultyDto  Difficulty { get; set; }
        public RegionDto Region { get; set; }


    }
}
