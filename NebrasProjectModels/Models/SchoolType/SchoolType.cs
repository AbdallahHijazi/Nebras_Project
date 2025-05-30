using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NebrasProjectModels.Models.Schools;

namespace NebrasProjectModels.Models.SchoolTypes
{
    public class SchoolType
    {
        [Key]
        public Guid SchoolTypeId { get; set; }
        public string TypeNameAr { get; set; }
        public string TypeNameEn { get; set; }

        public SchoolType()
        {
            SchoolTypeId = Guid.NewGuid();
        }
    }
}
