using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using NebrasProjectModels.Models.Governorates;  

namespace NebrasProjectModels.Models.Schools
{
    public class School
    {

        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int DamageLevel { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid GovernorateId { get; set; }
        [JsonIgnore]  
         public virtual Governorate Governorate { get; set; }

        public School()
        {
            Id = Guid.NewGuid();
        }
    }
}
