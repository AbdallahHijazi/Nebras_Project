
namespace NebrasProjectDTOs.DTOs.GovernorateDTO
{
    public class GovernorateDetailsDTO
    {
        public Guid GovernorateId { get; set; }
        public string NameAr { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public int CityCount { get; set; }

        public ICollection<CityDetails> Cities { get; set; }

    }
    public class CityDetails
    {
        public Guid CityId { get; set; }
        public string NameAr { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public int SchoolCount { get; set; }
        public ICollection<SchoolDetails> Schools { get; set; }

    }
    public class SchoolDetails
    {
        public Guid SchoolId { get; set; }
        public string NameAr { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public string AddressDetails { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int StudentCapacity { get; set; }
        public int NumberOfClassrooms { get; set; }
        public int YearEstablished { get; set; }
    }
}
