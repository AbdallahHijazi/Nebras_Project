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
        public ActionResult<CreatePhoto> CreateSchoolPhoto(CreatePhoto photo)
        {
            if (photo == null)
            {
                return BadRequest("No photo is add");
            }
            var schoolPhoto = new SchoolPhoto
            {
                SchoolId = photo.SchoolId,
                PhotoUrl = photo.PhotoUrl,
                Description = photo.Description,
                IsCoverPhoto = photo.IsCoverPhoto
            };
            var newPhoto = repository.Add(schoolPhoto);
            repository.SaveChenges();
            return CreatedAtRoute(nameof(CreateSchoolPhoto), photo);
        }
        [HttpGet("{id}",Name ="GetAllPhotos")]
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
        public ActionResult<SchoolPhoto> GetById(Guid schoolId, Guid photoId)
        {
            if (schoolId == Guid.Empty || photoId == Guid.Empty)
            {
                return NotFound("Information for these identifiers dose not exist");
            }
            var photo = context.SchoolPhotos
                                .FirstOrDefault(p => p.PhotoId == photoId && p.SchoolId == schoolId);

            return Ok(photo);
        }

        [HttpDelete("{id}")]
        public ActionResult<SchoolPhoto> DeletePhotoById(Guid id)
        {
            if(id == Guid.Empty)
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
