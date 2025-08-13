using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NebrasProjectDomain.Models;
using NebrasProjectDTOs.DTOs.PhotoDTO;
using NebrasProjectModels.Models.Photos;
using NebrasProjectRepository.Repositories;
using NebrasProjectRepository.SheardRepository;

namespace NebrasProjectAPI.Controllers.photo
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly AppDBContext context;
        private readonly IRepository<SchoolPhoto> repository;
        private readonly PhotosRepository photosRepository;

        public PhotosController(AppDBContext context,
                                IRepository<SchoolPhoto> repository,
                                PhotosRepository photosRepository)
        {
            this.context = context;
            this.repository = repository;
            this.photosRepository = photosRepository;
        }
        [HttpPost]
        public async Task<ActionResult<SchoolPhoto>> CreateSchoolPhoto([FromForm] CreatePhoto dto)
        {
            if (dto.Photo == null || dto.Photo.Length == 0)
            {
                return BadRequest("No photo uploaded");
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(dto.Photo.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await dto.Photo.CopyToAsync(stream);
            }

            var photoUrl = $"/uploads/{uniqueFileName}";

            var schoolPhoto = new SchoolPhoto
            {
                SchoolId = dto.SchoolId,
                PhotoUrl = photoUrl,
                Description = dto.Description,
                IsCoverPhoto = dto.IsCoverPhoto
            };

            var newPhoto = repository.Add(schoolPhoto);
            repository.SaveChenges();

            //if (result <= 0)
            //{
            //    return StatusCode(500, "Failed to save the photo");
            //}

            return Ok(newPhoto);
        }

        [HttpGet("{id}", Name = "GetAllPhotos")]
        public ActionResult<List<SchoolPhoto>> GetAllForSpecificSchool(Guid id)
        {
            List<SchoolPhoto> schoolPhoto = repository.GetAll().Where(p => p.SchoolId == id).ToList();
            if (schoolPhoto == null)
            {
                return NotFound("There are no pictures of this school");
            }
            return Ok(schoolPhoto);
        }

        [HttpGet("{schoolId}/{photoId}", Name = "GetPhotoBySchoolAndId")]
        public ActionResult<string> GetById(Guid schoolId, Guid photoId)
        {
            if (schoolId == Guid.Empty || photoId == Guid.Empty)
            {
                return NotFound("Invalid identifiers.");
            }

            var photo = context.SchoolPhotos
                .FirstOrDefault(p => p.PhotoId == photoId && p.SchoolId == schoolId);

            if (photo == null) return NotFound();

            // مسار الصورة من الـ PhotoUrl المخزنة في قاعدة البيانات
            var filePath = Path.Combine("wwwroot/uploads", Path.GetFileName(photo.PhotoUrl));

            if (!System.IO.File.Exists(filePath)) return NotFound("File not found");

            // قراءة وتحويل الصورة إلى Base64
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var base64String = Convert.ToBase64String(fileBytes);

            return Ok(base64String);
        }



        [HttpDelete("{id}")]
        public ActionResult<SchoolPhoto> DeletePhotoById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid photo ID.");
            }
            var photo = context.SchoolPhotos.FirstOrDefault(p => p.PhotoId == id);

            if (photo == null)
            {
                return NotFound("This photo has been deleted or doesn't exist.");
            }

            context.SchoolPhotos.Remove(photo);
            context.SaveChanges();
            return NoContent();
        }
    }
}
