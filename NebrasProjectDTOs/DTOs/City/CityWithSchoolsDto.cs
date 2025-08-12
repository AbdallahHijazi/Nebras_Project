using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebrasProjectDTOs.DTOs.City
{
    public class CityWithSchoolsDto
    {
        public Guid CityId { get; set; }
        public string NameAr { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;

        public Guid GovernorateId { get; set; }
        public string GovernorateNameAr { get; set; } = string.Empty;
        public string GovernorateNameEn { get; set; } = string.Empty;
        public List<SchoolBasicDto> Schools { get; set; }
    }
    public class SchoolBasicDto
    {
        public Guid SchoolId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
    }
}
