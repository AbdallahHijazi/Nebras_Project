using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebrasProjectDTOs.DTOs.SchoolsDTO
{
    public class CreateSchool
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string AddressDetails { get; set; }

        public Guid CityId { get; set; }
        public Guid SchoolTypeId { get; set; }
        public Guid SchoolStatusId { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string ConditionDescription { get; set; }
        public decimal EstimatedRenovationCost { get; set; }

        public int StudentCapacity { get; set; }
        public int NumberOfClassrooms { get; set; }
        public int YearEstablished { get; set; }

        public Guid AddedByUserId { get; set; }

        public List<RenovationRequestDto> RequiredRenovations { get; set; }
    }

    public class RenovationRequestDto
    {
        public Guid RenovationTypeId { get; set; }
        public string Notes { get; set; }
    }
}
