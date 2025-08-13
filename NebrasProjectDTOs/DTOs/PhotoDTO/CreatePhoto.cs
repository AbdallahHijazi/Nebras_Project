using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NebrasProjectDTOs.DTOs.PhotoDTO
{
    public class CreatePhoto
    {
        public Guid SchoolId { get; set; }
        public IFormFile Photo { get; set; }
        public string Description { get; set; }
        public bool IsCoverPhoto { get; set; }

    }
}
