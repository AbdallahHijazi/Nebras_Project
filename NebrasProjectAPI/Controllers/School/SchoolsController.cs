using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NebrasProjectDomain.Models;
using NebrasProjectDTOs.DTOs.SchoolsDTO;
using NebrasProjectModels.Models.SchoolRequiredRenovations;
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
        public ActionResult<List<School>> GetAll()
        {
            School[] schools = repository.GetAll().ToArray();
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
            try
            {
                if (dto == null)
                    return BadRequest("School data is required.");

                if (string.IsNullOrWhiteSpace(dto.NameAr) || string.IsNullOrWhiteSpace(dto.NameEn))
                    return BadRequest("School name (Arabic and English) is required.");

                var city = await context.Cities.FindAsync(dto.CityId);
                if (city == null)
                    return NotFound("City not found.");

                var schoolType = await context.SchoolTypes.FindAsync(dto.SchoolTypeId);
                if (schoolType == null)
                    return NotFound("School type not found.");

                var schoolStatus = await context.SchoolStatuses.FindAsync(dto.SchoolStatusId);
                if (schoolStatus == null)
                    return NotFound("School status not found.");

                var addedBy = await context.Users.FindAsync(dto.AddedByUserId);
                if (addedBy == null)
                    return NotFound("User who added the school was not found.");

                var school = new School
                {
                    NameAr = dto.NameAr.Trim(),
                    NameEn = dto.NameEn.Trim(),
                    AddressDetails = dto.AddressDetails?.Trim()!,
                    CityId = dto.CityId,
                    SchoolTypeId = dto.SchoolTypeId,
                    SchoolStatusId = dto.SchoolStatusId,
                    Latitude = dto.Latitude,
                    Longitude = dto.Longitude,
                    ConditionDescription = dto.ConditionDescription,
                    EstimatedRenovationCost = dto.EstimatedRenovationCost,
                    StudentCapacity = dto.StudentCapacity,
                    NumberOfClassrooms = dto.NumberOfClassrooms,
                    YearEstablished = dto.YearEstablished,
                    AddedByUserId = dto.AddedByUserId,
                    IsApproved = false,
                    IsActive = true
                };

                if (dto.RequiredRenovations?.Any() == true)
                {
                    school.RequiredRenovations = dto.RequiredRenovations?.Select(r => new SchoolRequiredRenovation
                    {
                        RenovationTypeId = r.RenovationTypeId,
                        Notes = r.Notes
                    }).ToList()!;

                }

                await context.Schools.AddAsync(school);
                await context.SaveChangesAsync();

                var result = new SchoolDetailsDto
                {
                    SchoolId = school.SchoolId,
                    NameAr = school.NameAr,
                    NameEn = school.NameEn,
                    AddressDetails = school.AddressDetails,
                    Latitude = school.Latitude,
                    Longitude = school.Longitude,
                    ConditionDescription = school.ConditionDescription,
                    EstimatedRenovationCost = school.EstimatedRenovationCost,
                    StudentCapacity = school.StudentCapacity,
                    NumberOfClassrooms = school.NumberOfClassrooms,
                    YearEstablished = school.YearEstablished,
                    CityId = school.CityId,
                    CityName = city.NameAr,
                    SchoolTypeId = school.SchoolTypeId,
                    SchoolStatusId = school.SchoolStatusId,
                    AddedByUserId = school.AddedByUserId,
                    AddedByUserName = addedBy.FullName,
                    IsApproved = school.IsApproved,
                    ApprovedByUserId = null,
                    ApprovedByUserName = null,
                    RequiredRenovations = school.RequiredRenovations?.Select(r => new RenovationRequestDto
                    {
                        RenovationTypeId = r.RenovationTypeId,
                        Notes = r.Notes
                    }).ToList()!
                };

                return CreatedAtAction(nameof(Get), new { id = school.SchoolId }, result);
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, $"Database error: {dbEx.Message}");
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
                return BadRequest("Invalid school data or ID mismatch.");

            var school = await context.Schools
                .Include(s => s.RequiredRenovations)
                .FirstOrDefaultAsync(s => s.SchoolId == dto.SchoolId);

            if (school == null)
                return NotFound($"School with ID {dto.SchoolId} not found.");

            if (!await context.Cities.AnyAsync(c => c.CityId == dto.CityId))
                return NotFound("City not found.");

            if (!await context.SchoolTypes.AnyAsync(t => t.SchoolTypeId == dto.SchoolTypeId))
                return NotFound("School type not found.");

            if (!await context.SchoolStatuses.AnyAsync(s => s.SchoolStatusId == dto.SchoolStatusId))
                return NotFound("School status not found.");

            school.NameAr = dto.NameAr.Trim();
            school.NameEn = dto.NameEn.Trim();
            school.AddressDetails = dto.AddressDetails?.Trim()!;
            school.CityId = dto.CityId;
            school.SchoolTypeId = dto.SchoolTypeId;
            school.SchoolStatusId = dto.SchoolStatusId;
            school.Latitude = dto.Latitude;
            school.Longitude = dto.Longitude;
            school.ConditionDescription = dto.ConditionDescription;
            school.EstimatedRenovationCost = dto.EstimatedRenovationCost;
            school.StudentCapacity = dto.StudentCapacity;
            school.NumberOfClassrooms = dto.NumberOfClassrooms;
            school.YearEstablished = dto.YearEstablished;

            school.RequiredRenovations.Clear();

            if (dto.RequiredRenovations?.Any() == true)
            {
                school.RequiredRenovations = dto.RequiredRenovations.Select(r => new SchoolRequiredRenovation
                {
                    RenovationTypeId = r.RenovationTypeId,
                    Notes = r.Notes,
                    SchoolId = school.SchoolId
                }).ToList();
            }

            try
            {
                await context.SaveChangesAsync();
                return NoContent(); // 204
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Database update error: {ex.Message}");
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

        [HttpGet("governorate/{governorateId}")]
        public async Task<IActionResult> GetSchoolsByGovernorate(Guid governorateId)
        {
            try
            {
                if (governorateId == Guid.Empty)
                {
                    return BadRequest("Governorate ID is required.");
                }

                var governorate = await context.Governorates.FindAsync(governorateId);
                if (governorate == null)
                {
                    return NotFound($"Governorate with ID {governorateId} not found.");
                }

                var schools = await schoolRepository.GetSchoolsByGovernorateId(governorateId);

                if (schools == null || !schools.Any())
                {
                    return NotFound("No schools found in the specified governorate.");
                }

                return Ok(schools);
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, $"Database error: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
