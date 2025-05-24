using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NebrasProjectModels.Models.Schools;

namespace NebrasProjectDTOs.DTOs.GovernorateDTO
{
    public class UpdateGovernorate
    {

        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int DamageLevel { get; set; }
        public string Address { get; set; } = string.Empty;
        public ICollection<Guid> Schools { get; set; }
    }
}
