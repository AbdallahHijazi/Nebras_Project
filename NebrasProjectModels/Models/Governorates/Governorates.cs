using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NebrasProjectModels.Models.Citys;
using NebrasProjectModels.Models.Schools;
namespace NebrasProjectModels.Models.Governorates
{
    public class Governorate
    {
        [Key]
        public Guid GovernorateId { get; set; }
        public string NameAr { get; set; }=string.Empty;
        public string NameEn { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string GovernorateImage { get; set; } = string.Empty;
     
        public ICollection<City> Cities { get; set; }

        public Governorate()
        {
            GovernorateId = Guid.NewGuid();
        }
    }
}
