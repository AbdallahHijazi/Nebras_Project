using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NebrasProjectDTOs.DTOs.Shared;

namespace NebrasProjectDTOs.DTOs.GovernorateDTO
{
    public class GovernorateSummaryDTO
    {
        public Guid GovernorateId { get; set; }
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public int CoveredSchools { get; set; }
        public int TotalSchools { get; set; }
        public int CoveredCities { get; set; }
        public int TotalCities { get; set; }
        public int DamagePercentage { get; set; }
        public List<ChartDataDTO>? SchoolsCoverage { get; set; }
        public List<ChartDataDTO>? CitiesCoverage { get; set; }
        public List<ChartDataDTO>? DamageChart { get; set; }
    }


}
