using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NebrasProjectDomain.Models;
using NebrasProjectDTOs.DTOs.GovernorateDTO;
using NebrasProjectDTOs.DTOs.Shared;
using NebrasProjectModels.Models.Governorates;
using NebrasProjectRepository.Repositories;
using NebrasProjectRepository.SheardRepository;

namespace NebrasPhotoService.Controllers.Governorates
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
                    profileImage = repository.GetFileAsBase64(g.GovernorateImage, "governorates");

                }
                governorates.Add(new GovernorateDetailsDTO
                {
                    GovernorateId = g.GovernorateId,
                    NameAr = g.NameAr,
                    NameEn = g.NameEn,
                    Description = g.Description,
                    CityCount = g.CityCount,
                    SchoolCount = g.SchoolCount,
                    GovernorateImageUrl = profileImage!
                });
            }

            return Ok(governorates);
        }

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
        public async Task<ActionResult<Governorate>> Post(CreateGovernorate governorate)
        {
            try
            {
                if (governorate == null)
                    return BadRequest("No data in the governorate");

                if (string.IsNullOrWhiteSpace(governorate.NameAr) || string.IsNullOrWhiteSpace(governorate.NameEn))
                    return BadRequest("NameAr and NameEn must not be empty.");

                string? photoUrl = null;

                if (governorate.GovImageBase64 != null)
                {
                    photoUrl = repository.SaveFile(governorate.GovImageBase64, "governorates");
                }

                var newGovernorate = new Governorate
                {
                    NameAr = governorate.NameAr,
                    NameEn = governorate.NameEn,
                    Description = governorate.Description ?? string.Empty,
                    GovernorateImage = photoUrl!
                };

                repository.Add(newGovernorate);
                repository.SaveChenges();

                return CreatedAtRoute("GetGovernorate", new { id = newGovernorate.GovernorateId }, newGovernorate);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the governorate: {ex.InnerException?.Message ?? ex.Message}");
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