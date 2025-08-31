using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NebrasProjectDomain.Models;
using NebrasProjectDTOs.DTOs.SchoolsDTO;
using NebrasProjectModels.Models.Schools;
using NebrasProjectRepository.Repositories;
using NebrasProjectRepository.SheardRepository;

namespace NebrasProjectAPI.Controllers.Schools
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly AppDBContext context;
        private readonly IRepository<School> repository;
        private readonly SchoolRepository schoolRepository;

        public SchoolsController(AppDBContext context,
            IRepository<School> repository,
            SchoolRepository schoolRepository)
        {
            this.context = context;
            this.repository = repository;
            this.schoolRepository = schoolRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<SchoolDetailsDto>>> GetAll()
        {
            var schools = await schoolRepository.GetAllSchools();

            if (schools == null)
            {
                return NotFound("No data in the schools");
            }
            else
            {
                return Ok(schools);
            }
        }

        [HttpGet("{id}", Name = "GetSchool")]
        public ActionResult<SchoolDetailsDto> Get(Guid id)
        {
            var school = repository.Get(id);
            if (school == null)
            {
                return NotFound("No data in the schools");
            }
            else
            {
                return Ok(school);
            }
        }

        [HttpPost]
        public async Task<ActionResult<SchoolDetailsDto>> Post(CreateSchool dto)
        {
            if (dto == null)
                return BadRequest("Invalid request payload.");

            if (string.IsNullOrWhiteSpace(dto.NameAr) ||
               string.IsNullOrWhiteSpace(dto.NameEn) ||
               string.IsNullOrWhiteSpace(dto.City) ||
               string.IsNullOrWhiteSpace(dto.HeadTeacherName) ||
               string.IsNullOrWhiteSpace(dto.HeadTeacherNumber))
            {
                return BadRequest("Missing required fields: NameAr, NameEn, City, HeadTeacherName, HeadTeacherNumber.");
            }

            if (dto.UserId == Guid.Empty || dto.GovernorateId == Guid.Empty)
            {
                return BadRequest("Invalid UserId or GovernorateId.");
            }

            var user = await context.Users.FindAsync(dto.UserId);
            if (user == null)
                return NotFound($"User with ID {dto.UserId} not found.");

            var governorate = await context.Governorates.FindAsync(dto.GovernorateId);
            if (governorate == null)
                return NotFound($"Governorate with ID {dto.GovernorateId} not found.");

            string? photoUrl = null;
            if (dto.SchoolImageBase64 != null)
            {
                photoUrl = schoolRepository.SaveFile(dto.SchoolImageBase64, "schools");
            }
            var school = new School
            {
                NameAr = dto.NameAr.Trim(),
                NameEn = dto.NameEn.Trim(),
                City = dto.City.Trim(),
                Description = dto.Description?.Trim()!,
                EstimatedRenovationCost = dto.EstimatedRenovationCost,
                GovernorateId = dto.GovernorateId,
                HeadTeacherName = dto.HeadTeacherName.Trim(),
                HeadTeacherNumber = dto.HeadTeacherNumber.Trim(),
                AddedByUserId = dto.UserId,
                IsApproved = false,
                IsRequirementsMet = false,
                Needs = dto.Needs ?? new List<string>(),
                SchoolImageUrl = photoUrl!,
            };

            try
            {
                await context.Schools.AddAsync(school);
                await context.SaveChangesAsync();

                var result = new SchoolDetailsDto
                {
                    SchoolId = school.SchoolId,
                    NameAr = school.NameAr,
                    NameEn = school.NameEn,
                    City = school.City,
                    Description = school.Description!,
                    EstimatedRenovationCost = school.EstimatedRenovationCost,
                    GovernorteId = school.GovernorateId,
                    HeadTeacherName = school.HeadTeacherName,
                    HeadTeacherNumber = school.HeadTeacherNumber,
                    AddedByUserId = school.AddedByUserId,
                    IsApproved = school.IsApproved,
                    GovernortesName = governorate.NameAr,
                    IsRequirementsMet = school.IsRequirementsMet,
                    Needs = school.Needs,
                    ProfileImageUrl = photoUrl != null ? repository.GetFileAsBase64(photoUrl, "schools") : null
                };

                return CreatedAtAction(nameof(Get), new { id = school.SchoolId }, result);
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, $"Database error: {dbEx.InnerException?.Message ?? dbEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateSchool dto)
        {
            if (dto == null)
                return BadRequest("Invalid request payload or ID mismatch.");

            if (string.IsNullOrWhiteSpace(dto.NameAr) ||
                string.IsNullOrWhiteSpace(dto.NameEn) ||
                string.IsNullOrWhiteSpace(dto.City) ||
                string.IsNullOrWhiteSpace(dto.HeadTeacherName) ||
                string.IsNullOrWhiteSpace(dto.HeadTeacherNumber))
            {
                return BadRequest("Missing required fields.");
            }

            var school = await context.Schools.FindAsync(dto.SchoolId);
            if (school == null)
                return NotFound($"School with ID {dto.SchoolId} not found.");

            if (!await context.Governorates.AnyAsync(g => g.GovernorateId == dto.GovernorateId))
                return NotFound($"Governorate with ID {dto.GovernorateId} not found.");

            try
            {
                school.NameAr = dto.NameAr.Trim();
                school.NameEn = dto.NameEn.Trim();
                school.City = dto.City.Trim();
                school.Description = dto.Description?.Trim()!;
                school.EstimatedRenovationCost = dto.EstimatedRenovationCost;
                school.GovernorateId = dto.GovernorateId;
                school.HeadTeacherName = dto.HeadTeacherName.Trim();
                school.HeadTeacherNumber = dto.HeadTeacherNumber.Trim();
                school.UpdatedAt = DateTime.UtcNow;
                school.IsRequirementsMet = dto.IsRequirementsMet;
                school.Needs = dto.Needs ?? new List<string>();
                school.SchoolId = dto.SchoolId;
                repository.Update(school);
                await context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Database update error: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var school = repository.Get(id);


            if (school == null)
            {
                return NotFound("No data in the schools");
            }
            else
            {
                var deletedSchool = repository.Delete(school);
                repository.SaveChenges();
                return NoContent();
            }
        }

        //[HttpGet("governorate/{governorateId}")]
        //public async Task<IActionResult> GetSchoolsByGovernorate(Guid governorateId)
        //{
        //    try
        //    {
        //        if (governorateId == Guid.Empty)
        //        {
        //            return BadRequest("Governorate ID is required.");
        //        }

        //        var governorate = await context.Governorates.FindAsync(governorateId);
        //        if (governorate == null)
        //        {
        //            return NotFound($"Governorate with ID {governorateId} not found.");
        //        }

        //        var schools = await schoolRepository.GetSchoolsByGovernorateId(governorateId);

        //        if (schools == null || !schools.Any())
        //        {
        //            return NotFound("No schools found in the specified governorate.");
        //        }

        //        return Ok(schools);
        //    }
        //    catch (DbUpdateException dbEx)
        //    {
        //        return StatusCode(500, $"Database error: {dbEx.Message}");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
        //    }
        //}
    }
}