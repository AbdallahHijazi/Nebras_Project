using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NebrasProjectModels.Models.Schools;

namespace NebrasProjectModels.Models.Photos
{
    public class SchoolPhoto
    {
        [Key]
        public Guid PhotoId { get; set; }

        public Guid SchoolId { get; set; }
        public School School { get; set; }

        public string PhotoUrl { get; set; }
        public string Description { get; set; }
        public bool IsCoverPhoto { get; set; }
        public DateTime UploadedAt { get; set; }
        public SchoolPhoto()
        {
            PhotoId = Guid.NewGuid();
            UploadedAt = DateTime.UtcNow;
        }
    }
}
