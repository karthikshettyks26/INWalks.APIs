using INWalks.API.Models.DTO;

namespace INWalks.APIs.Models.DTO
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        //public Guid DifficultyId { get; set; }
        //public Guid RegionId { get; set; }

        //nav Props
        public RegionDto Region { get; set; }
        public DifficultyDto Difficulty { get; set; }
        
    }
}
