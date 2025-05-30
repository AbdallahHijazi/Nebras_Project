using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NebrasProjectModels.Models.RenovationTypes;
using NebrasProjectModels.Models.Schools;

namespace NebrasProjectModels.Models.SchoolRequiredRenovations
{
    public class SchoolRequiredRenovation
    {
        [Key]
        public Guid SchoolId { get; set; }
        public School School { get; set; }

        public Guid RenovationTypeId { get; set; }
        public RenovationType RenovationType { get; set; }

        public string Notes { get; set; }

    }
}
