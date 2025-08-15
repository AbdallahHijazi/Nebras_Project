using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NebrasProjectModels.Models.Citys;
using NebrasProjectModels.Models.Schools;

namespace NebrasProjectDTOs.DTOs.GovernorateDTO
{
    public class CreateGovernorate
    {

        public string NameAr { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IFormFile? GovImageBase64 { get; set; }
        public int CityCount { get; set; }
        public int SchoolCount { get; set; }

    }
}
