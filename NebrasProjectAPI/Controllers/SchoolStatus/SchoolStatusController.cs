using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NebrasProjectDomain.Models;
using NebrasProjectDTOs.DTOs.SchoolStatus;
using NebrasProjectModels.Models.SchoolStatues;
using NebrasProjectRepository.Repositories;
using NebrasProjectRepository.SheardRepository;

namespace NebrasProjectAPI.Controllers.SchoolStatues
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolStatusController : ControllerBase
    {
        private readonly AppDBContext context;
        private readonly IRepository<SchoolStatus> repository;
        private readonly SchoolStatusRepository statusRepository;

        public SchoolStatusController(AppDBContext context,
            IRepository<SchoolStatus> repository,
            SchoolStatusRepository statusRepository)
        {
            this.context = context;
            this.repository = repository;
            this.statusRepository = statusRepository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var schoolStatuses = repository.GetAll();
            if (schoolStatuses == null || !schoolStatuses.Any())
            {
                return NotFound("No school statuses found.");
            }
            return Ok(schoolStatuses);
        }

        [HttpGet("{id}", Name = "GetSchoolStatus")]
        public ActionResult<SchoolStatus> Get(Guid id)
        {
            var schoolStatus = repository.Get(id);
            if (schoolStatus == null)
            {
                return NotFound("No school status found with the provided ID.");
            }
            return Ok(schoolStatus);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchoolStatus(CreateSchoolStatusDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var exists = await context.SchoolStatuses
               .AnyAsync(s => s.StatusNameAr == dto.StatusNameAr || s.StatusNameEn == dto.StatusNameEn);


            var status = new SchoolStatus
            {
                SchoolStatusId = Guid.NewGuid(),
                StatusNameAr = dto.StatusNameAr,
                StatusNameEn = dto.StatusNameEn,
                StatusColor = dto.StatusColor
            };

            context.SchoolStatuses.Add(status);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = status.SchoolStatusId }, status);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchoolStatus(UpdateSchoolStatusDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var status = repository.Get(dto.SchoolStatusId);

            if (status == null)
                return NotFound($"School status with ID {dto.SchoolStatusId} was not found.");

            // Check for duplicates (optional)
            bool isDuplicate = await context.SchoolStatuses
                .AnyAsync(s => s.SchoolStatusId != dto.SchoolStatusId &&
                               (s.StatusNameAr == dto.StatusNameAr || s.StatusNameEn == dto.StatusNameEn));

            if (isDuplicate)
                return Conflict("Another school status with the same Arabic or English name already exists.");

            // Update
            status.StatusNameAr = dto.StatusNameAr.Trim();
            status.StatusNameEn = dto.StatusNameEn.Trim();
            status.StatusColor = dto.StatusColor.Trim();

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
                return StatusCode(500, $"Unexpected error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchoolStatus(Guid id)
        {
            var status = repository.Get(id);
            if (status == null)
            {
                return NotFound($"School status with ID {id} was not found.");
            }
            try
            {
                repository.Delete(status);
                await context.SaveChangesAsync();
                return NoContent(); // 204
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Database update error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Unexpected error: {ex.Message}");
            }
        }

    }
}
