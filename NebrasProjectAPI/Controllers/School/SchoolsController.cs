using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<School> Get(Guid id)
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
        public ActionResult<School> Post(CreateSchool school)
        {
            var governorate = context.Governorates.FirstOrDefault(g => g.Id == school.GovernorateId);
            if (governorate is null)
            {
                return BadRequest("No data in the governorate");
            }
            if (school.DamageLevel > 5)
            {
                return BadRequest("max value for DamageLevel 5");

            }
            var schools = new School
            {
                Name = school.Name,
                Address = school.Address,
                DamageLevel = school.DamageLevel,
                Description = school.Description,
                GovernorateId = school.GovernorateId,
            };

            if (schools == null)
            {
                return BadRequest("No data in the schools");
            }
            else
            {
                var newSchool = repository.Add(schools);
                repository.SaveChenges();
                return CreatedAtRoute("GetSchool", new { id = newSchool.Id }, newSchool);
            }
        }

        [HttpPut]
        public ActionResult<School> Put(UpdateSchool school)
        {
            var governorate = context.Governorates.FirstOrDefault(g => g.Id == school.GovernorateId);
            if (governorate is null)
            {
                return BadRequest("No data in the governorate");
            }
            var schools = repository.Get(school.Id);
            if (schools == null)
            {
                return BadRequest("No data in the schools");
            }

            else if (school.DamageLevel > 5)
            {
                return BadRequest("max value for DamageLevel 5");

            }
            else
            {
                schools!.Name = school.Name;
                schools.Address = school.Address;
                schools.DamageLevel = school.DamageLevel;
                schools.Description = school.Description;
                schools.GovernorateId = school.GovernorateId;
                var updatedSchool = repository.Update(schools);
                repository.SaveChenges();
                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<School> Delete(Guid id)
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
        [HttpGet("Governorate/{id}")]
        public ActionResult<List<School>> GetSchoolsByGovernorateId(Guid id)
        {
            var schools = schoolRepository.GetSchoolsByGovernorateId(id);
            if (schools == null )
            {
                return NotFound("No schools found for this governorate");
            }
            else
            {
                return Ok(schools);
            }
        }
        [HttpGet("Names")]
        public ActionResult<IList<SchoolsNameDTO>> GetSchoolNameAndIdByGovernorateId(Guid id)
        {
            var schools = schoolRepository.GetSchoolNameAndId(id).Result;
            if (schools == null || !schools.Any())
            {
                return NotFound("No schools found");
            }
            else
            {
                return Ok(schools);
            }
        }

        //[HttpGet("by-damage")]
        //public ActionResult<List<School>> GetSchoolsByDamageLevel(int min, int max)
        //{
        //    var schools = schoolRepository.GetSchoolsByDamageLevel(min, max);
        //    if (schools == null)
        //    {
        //        return NotFound("No schools found with the specified damage level");
        //    }
        //    else
        //    {
        //        return Ok(schools);
        //    }
        //}
    }
}
