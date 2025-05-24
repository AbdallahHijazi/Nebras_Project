using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NebrasProjectModels.Models.Schools;
namespace NebrasProjectModels.Models.Governorates
{
    public class Governorate
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int DamageLevel { get; set; }
        public int Code { get; set; }
        public string Address { get; set; } = string.Empty;
        public ICollection<School> Schools { get; set; }
        public Governorate()
        {
            Id = Guid.NewGuid();
        }
    }
}
