using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NebrasProjectDTOs.DTOs.City
{
    public class CreateCityDto
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public Guid GovernorateId { get; set; }
        public int SchoolCount { get; set; }
        public IFormFile? CityBase64 { get; set; }

    }
}
