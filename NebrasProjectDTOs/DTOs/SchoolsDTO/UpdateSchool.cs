using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NebrasProjectModels.Models.Governorates;

namespace NebrasProjectDTOs.DTOs.SchoolsDTO
{
    public class UpdateSchool
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int DamageLevel { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid GovernorateId { get; set; }

    }
}
