using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NebrasProjectModels.Models.SchoolRequiredRenovations;

namespace NebrasProjectModels.Models.RenovationTypes
{
    public class RenovationType
    {
        [Key]
        public Guid RenovationTypeId { get; set; }
        public string TypeNameAr { get; set; } = string.Empty;
        public string TypeNameEn { get; set; } = string.Empty;

        public ICollection<SchoolRequiredRenovation> SchoolRequiredRenovations { get; set; }
        public RenovationType()
        {
            RenovationTypeId = Guid.NewGuid();
        }
    }
}

