using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using NebrasProjectModels.Models.Citys;
using NebrasProjectModels.Models.Governorates;
using NebrasProjectModels.Models.Photos;
using NebrasProjectModels.Models.SchoolRequiredRenovations;
using NebrasProjectModels.Models.SchoolStatues;
using NebrasProjectModels.Models.SchoolTypes;

namespace NebrasProjectModels.Models.Schools
{
    public class School
    {

        [Key]
        public Guid SchoolId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string AddressDetails { get; set; }

        public Guid CityId { get; set; }
        public City City { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Guid SchoolTypeId { get; set; }
        public SchoolType SchoolType { get; set; }

        public Guid SchoolStatusId { get; set; }
        public SchoolStatus SchoolStatus { get; set; }

        public string ConditionDescription { get; set; }
        public decimal EstimatedRenovationCost { get; set; }

        public int StudentCapacity { get; set; }
        public int NumberOfClassrooms { get; set; }
        public int YearEstablished { get; set; }

        public Guid AddedByUserId { get; set; }
        public Guid? ApprovedByUserId { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }

        public ICollection<SchoolPhoto> Photos { get; set; }
        public ICollection<SchoolRequiredRenovation> RequiredRenovations { get; set; }
        public School()
        {
            SchoolId = Guid.NewGuid();
        }
    }
}
