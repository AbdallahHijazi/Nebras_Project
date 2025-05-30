using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebrasProjectDTOs.DTOs.GovernorateDTO
{
    public class GovernorateDto
    {
        public Guid GovernorateId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }

        public List<CityBasicDto> Cities { get; set; }
    }

    public class CityBasicDto
    {
        public Guid CityId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
    }

}
