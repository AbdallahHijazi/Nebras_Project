using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;
using NebrasProjectRepository.SheardRepository;
using NebrasProjectModels.Models.Users;
using NebrasProjectDomain.Models;
using NebrasProjectDTOs.DTOs.UsersDTO;

namespace NebrasProject.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<Users> repository;
        private readonly AppDBContext context;
        private readonly IConfiguration configuration;


        public UserController
            (
            AppDBContext context,
            IConfiguration configuration,
            IRepository<Users> repository
            )
        {
            this.repository = repository;
            this.context = context;
            this.configuration = configuration;
        }

        [HttpGet]
        public ActionResult<List<Users>> GetAll()
        {
            Users[] users = repository.GetAll().ToArray();
            if (users == null)
            {
                return NotFound("No data in the users");
            }
            else
            {
                return Ok(users);
            }
        }

        [HttpGet("{id}", Name = "GetUser")]
        public ActionResult<Users> Get(string id)
        {
            var user = repository.Get(id);
            if (user == null)
            {
                return NotFound("No data in the users");
            }
            else
            {
                return Ok(user);
            }
        }

        [HttpPost]
        public ActionResult<Users> Post(CreateUser user)
        {
            if (user == null)
            {
                return BadRequest("No data in the users");
            }

            var users = new Users
            {
                Id = Guid.NewGuid().ToString(),
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                Governorate = user.Governorate,
                City = user.City,
                Address = user.Address
            };
            var newUser = repository.Add(users);
            repository.SaveChenges();
            return CreatedAtRoute("GetUser", new { id = newUser.Id }, newUser);
        }

        [HttpPut]
        public ActionResult<Users> UpdateUser(UpdateUser user)
        {
            if (user == null)
            {
                return BadRequest("No data in the users");
            }
            var oldUser = repository.Get(user.Id);
            if (oldUser == null)
            {
                return NotFound("this user not found");
            }
            else
            {
                oldUser.UserName = user.UserName;
                oldUser.Email = user.Email;
                oldUser.PhoneNumber = user.PhoneNumber;
                oldUser.Governorate = user.Governorate;
                oldUser.City = user.City;
                oldUser.Address = user.Address;

                repository.Update(oldUser);
                repository.SaveChenges();
                return NoContent();
            }
        }
        [HttpDelete("{id}")]
        public ActionResult<Users> Delete(string id)
        {
            var user = repository.Get(id);
            if (user == null)
            {
                return NotFound("this user not found");
            }
            else
            {
                repository.Delete(user);
                repository.SaveChenges();
                return NoContent();
            }
        }
    }
}
