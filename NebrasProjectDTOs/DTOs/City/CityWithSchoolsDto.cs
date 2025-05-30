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
        public string NameAr { get; set; }
        public string NameEn { get; set; }

        public List<SchoolBasicDto> Schools { get; set; }
    }

    public class SchoolBasicDto
    {
        public Guid SchoolId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
    }
}
