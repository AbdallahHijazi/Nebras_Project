using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebrasProjectDTOs.DTOs.City
{
    public class UpdateCityDto
    {
        public Guid CityId { get; set; }
        public string NameAr { get; set; }=string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public Guid GovernorateId { get; set; }
        public int SchoolCount { get; set; }

    }
}
