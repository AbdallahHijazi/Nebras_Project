using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebrasProjectDTOs.DTOs.GovernorateDTO
{
    public class GovernoratesInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int SchoolCount { get; set; } = 0;
    }
}
