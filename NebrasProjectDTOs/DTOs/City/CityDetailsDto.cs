using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NebrasProjectDTOs.DTOs.Shared;

namespace NebrasProjectDTOs.DTOs.City
{
    public class CityDetailsDto
    {
        public Guid CityId { get; set; }
        public string NameAr { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public int SchoolCount { get; set; }
        public Guid GovernorateId { get; set; }
        public string GovernorateNameAr { get; set; } = string.Empty;
        public string GovernorateNameEn { get; set; } = string.Empty;

        public FileData? CityImageUrl { get; set; }
    }
}
