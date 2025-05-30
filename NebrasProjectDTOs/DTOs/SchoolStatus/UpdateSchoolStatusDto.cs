using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebrasProjectDTOs.DTOs.SchoolStatus
{
    public class UpdateSchoolStatusDto
    {
        public Guid SchoolStatusId { get; set; }

        public string StatusNameAr { get; set; }

        public string StatusNameEn { get; set; }
        public string StatusColor { get; set; }

    }
}
