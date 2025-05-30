using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebrasProjectDTOs.DTOs.SchoolStatus
{
    public class CreateSchoolStatusDto
    {
        public string StatusNameAr { get; set; }

        public string StatusNameEn { get; set; }
        public string StatusColor { get; set; }
    }
}
