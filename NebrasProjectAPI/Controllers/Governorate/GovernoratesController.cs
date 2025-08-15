using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NebrasProjectDomain.Models;
using NebrasProjectDTOs.DTOs.GovernorateDTO;
using NebrasProjectDTOs.DTOs.Shared;
using NebrasProjectModels.Models.Governorates;
using NebrasProjectModels.Models.Schools;
using NebrasProjectModels.Models.Users;
using NebrasProjectRepository.Repositories;
using NebrasProjectRepository.SheardRepository;

namespace NebrasProjectAPI.Controllers.Governorates
{
    [Route("api/[controller]")]
    [ApiController]
    public class GovernoratesController : ControllerBase
    {
        private readonly AppDBContext context;
        private readonly IRepository<Governorate> repository;
        private readonly GovernorateRepository governorateRepository;

        public GovernoratesController(AppDBContext context,
                                      IRepository<Governorate> repository,
                                      GovernorateRepository governorateRepository
                                       )
        {
            this.context = context;
            this.repository = repository;
            this.governorateRepository = governorateRepository;
        }


        [HttpGet]
        public ActionResult<List<GovernorateDetailsDTO>> GetAll()
        {
            var governoratesData = repository.GetAll().ToList();

            if (governoratesData == null || governoratesData.Count == 0)
                return NotFound("No data in the governorates");

            var governorates = new List<GovernorateDetailsDTO>();

            foreach (var g in governoratesData)
            {
                FileData? profileImage = null;

                if (!string.IsNullOrEmpty(g?.GovernorateImage))
                {
                    var safeFileName = Path.GetFileName(g.GovernorateImage);
                    var filePath = Path.Combine("wwwroot/uploads", safeFileName);

                    if (System.IO.File.Exists(filePath))
                    {
                        var fileBytes = System.IO.File.ReadAllBytes(filePath);
                        profileImage = new FileData
                        {
                            Base64String = Convert.ToBase64String(fileBytes),
                            ContentType = Path.GetExtension(filePath).ToLower() switch
                            {
                                ".jpg" or ".jpeg" => "image/jpeg",
                                ".png" => "image/png",
                                ".gif" => "image/gif",
                                _ => "application/octet-stream"
                            }
                        };
                    }
                }

                governorates.Add(new GovernorateDetailsDTO
                {
                    GovernorateId = g.GovernorateId,
                    NameAr = g.NameAr,
                    NameEn = g.NameEn,
                    Description = g.Description,
                    CityCount = g.Cities?.Count ?? 0,
                    SchoolCount = g.Cities?.SelectMany(c => c.Schools ?? Enumerable.Empty<School>()).Count() ?? 0,
                    GovernorateImageUrl = profileImage!
                });
            }


            return Ok(governorates);
        }


        //[HttpGet("{id}/schools-summary", Name = "GetGovernorateWithSchools")]
        //public async Task<ActionResult<GovernorateDetailsDTO>> GetGovernorateWithSchools(Guid id)
        //{
        //    var governorate = await governorateRepository.GetGovernorateDetails(id);
        //    if (governorate == null)
        //    {
        //        return NotFound("No data in the governorates");
        //    }
        //    else
        //    {
        //        return Ok(governorate);
        //    }
        //}

        [HttpGet("{id}", Name = "GetGovernorate")]
        public async Task<ActionResult<GovernorateDetailsDTO>> Get(Guid id)
        {
            var governorate = await governorateRepository.GetGovernorateDetails(id);
            if (governorate == null)
            {
                return NotFound("No data in the governorates");
            }
            else
            {

                return Ok(governorate);
            }
        }

        [HttpPost]
        public ActionResult<Governorate> Post(CreateGovernorate governorate)
        {

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(governorate.GovImageBase64!.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                governorate.GovImageBase64.CopyToAsync(stream);
            }

            var photoUrl = $"/uploads/{uniqueFileName}";
            var governorates = new Governorate
            {
                NameAr = governorate.NameAr,
                NameEn = governorate.NameEn,
                Description = governorate.Description,
                GovernorateImage = photoUrl

            };
            if (governorates is null)
            {
                return BadRequest("No data in the governorate");
            }
            else if (governorate.NameEn.IsNullOrEmpty() || governorate.NameAr.IsNullOrEmpty())
            {
                return BadRequest("Name most be not enpty.");
            }
            else
            {
                repository.Add(governorates);
                repository.SaveChenges();
                return CreatedAtRoute("GetGovernorate", new { id = governorates.GovernorateId }, governorates);
            }
        }

        [HttpPut]
        public ActionResult<Governorate> Put(UpdateGovernorate governorate)
        {
            var governorates = repository.Get(governorate.Id);
            if (governorates is null)
            {
                return NotFound("No data in the governorate");
            }
            else
            {
                governorates.NameAr = governorate.NameAr;
                governorates.NameEn = governorate.NameEn;
                if (governorate.NameEn.IsNullOrEmpty() || governorate.NameAr.IsNullOrEmpty())
                {
                    return BadRequest("Name most be not enpty.");
                }

                repository.Update(governorates);
                repository.SaveChenges();
                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Governorate> Delete(Guid id)
        {
            var governorate = repository.Get(id);
            if (governorate is null)
            {
                return NotFound("No data in the governorate");
            }
            else
            {
                repository.Delete(governorate);
                repository.SaveChenges();
                return NoContent();
            }
        }

        [HttpGet("{id}/summary", Name = "GetGovernorateSummaryId")]
        public async Task<ActionResult<GovernorateSummaryDTO>> GetGovernorateSummaryId(Guid id)
        {
            var governorateSummary = await governorateRepository.GetGovernorateSummary(id);

            if (governorateSummary == null)
            {
                return NotFound("No data in the governorate summary");
            }

            return Ok(governorateSummary);
        }

    }
}
