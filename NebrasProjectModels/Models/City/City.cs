using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NebrasProjectModels.Models.Governorates;
using NebrasProjectModels.Models.Schools;

namespace NebrasProjectModels.Models.Citys
{
    public class City
    {
        [Key]
        public Guid CityId { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }

        public Guid GovernorateId { get; set; }
        public int SchoolCount { get; set; }
        public ICollection<School> Schools { get; set; }
        public Governorate Governorate { get; set; }
        public string CityImage { get; set; } = string.Empty;

        public City()
        {
            CityId = Guid.NewGuid();
        }
    }
}
