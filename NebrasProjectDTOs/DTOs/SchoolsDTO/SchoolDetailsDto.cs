using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebrasProjectDTOs.DTOs.SchoolsDTO
{
    public class SchoolDetailsDto
    {
        public Guid SchoolId { get; set; }

        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string AddressDetails { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string ConditionDescription { get; set; }
        public decimal EstimatedRenovationCost { get; set; }

        public int StudentCapacity { get; set; }
        public int NumberOfClassrooms { get; set; }
        public int YearEstablished { get; set; }

        public Guid CityId { get; set; }
        public string CityName { get; set; } = string.Empty;

        public Guid SchoolTypeId { get; set; }
        public string SchoolTypeName { get; set; } = string.Empty;

        public Guid SchoolStatusId { get; set; }
        public string SchoolStatusName { get; set; }=string.Empty;

        public Guid AddedByUserId { get; set; }
        public string AddedByUserName { get; set; } = string.Empty;

        public bool IsApproved { get; set; }
        public Guid? ApprovedByUserId { get; set; }
        public string ApprovedByUserName { get; set; } = string.Empty;
        public List<RenovationRequestDto> RequiredRenovations { get; set; }

    }

}
