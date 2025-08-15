
using NebrasProjectDTOs.DTOs.Shared;

namespace NebrasProjectDTOs.DTOs.GovernorateDTO
{
    public class GovernorateDetailsDTO
    {
        public Guid GovernorateId { get; set; }
        public string NameAr { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public int CityCount { get; set; }
        public int SchoolCount { get; set; }
        public string Description { get; set; } = string.Empty;

        public FileData GovernorateImageUrl { get; set; }
    }

}
