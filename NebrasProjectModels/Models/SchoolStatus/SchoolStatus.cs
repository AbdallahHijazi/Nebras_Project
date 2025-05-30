using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NebrasProjectModels.Models.Schools;

namespace NebrasProjectModels.Models.SchoolStatues
{
    public class SchoolStatus
    {
        [Key]
        public Guid SchoolStatusId { get; set; }
        public string StatusNameAr { get; set; }
        public string StatusNameEn { get; set; }
        public string StatusColor { get; set; }

        public SchoolStatus()
        {
            SchoolStatusId = Guid.NewGuid();
        }
    }
}
