using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NebrasProjectDomain.Models;
using NebrasProjectDTOs.DTOs.PhotoDTO;
using NebrasProjectModels.Models.Photos;
using NebrasProjectRepository.Repositories;
using NebrasProjectRepository.SheardRepository;

namespace NebrasPhotoService.Controllers.photo
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly AppDBContext context;
        private readonly IRepository<SchoolPhoto> repository;
        private readonly PhotosRepository photosRepository;
        private readonly IConfiguration config;
        private readonly IWebHostEnvironment env;

        public PhotosController(AppDBContext context,
                                IRepository<SchoolPhoto> repository,
                                PhotosRepository photosRepository,
                                IConfiguration config,
                                IWebHostEnvironment env)
        {
            this.context = context;
            this.repository = repository;
            this.photosRepository = photosRepository;
            this.config = config;
            this.env = env;
        }
        [HttpPost]
        public async Task<ActionResult<SchoolPhoto>> CreateSchoolPhoto([FromForm] CreatePhoto dto)
        {
            if (dto.Photo == null || dto.Photo.Length == 0)
            {
                return BadRequest("No photo uploaded");
            }
            var uploadsFolder = GetUploadsFolder();

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

            return Ok(newPhoto);
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
            var uploadsFolder = GetUploadsFolder();
            var filePath = Path.Combine(uploadsFolder, Path.GetFileName(photo.PhotoUrl));

            if (!System.IO.File.Exists(filePath)) return NotFound("File not found");

            // قراءة وتحويل الصورة إلى Base64
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var base64String = Convert.ToBase64String(fileBytes);

            return Ok(base64String);
        }
        private string GetUploadsFolder()
        {
            var root = config["FileStorage:RootPath"];
            if (string.IsNullOrWhiteSpace(root))
                throw new Exception("FileStorage:RootPath is not configured.");

            var sharedRoot = Path.GetFullPath(Path.Combine(env.ContentRootPath, root));
            var uploadsFolder = Path.Combine(sharedRoot, "uploads");

            Directory.CreateDirectory(uploadsFolder);
            Console.WriteLine("UPLOADS FOLDER => " + uploadsFolder);
            return uploadsFolder;
        }

    }
}
