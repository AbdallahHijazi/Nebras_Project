using Microsoft.AspNetCore.Mvc;
using NebrasProjectRepository.SheardRepository;
using NebrasProjectModels.Models.Users;
using NebrasProjectDomain.Models;
using NebrasProjectDTOs.DTOs.UsersDTO;
using NebrasProjectModels.Profiles;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using NebrasProjectRepository.Repositories;

namespace NebrasProject.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> repository;
        private readonly UserRepository userRepository;
        private readonly AppDBContext context;
        private readonly IConfiguration configuration;

        public UserController
            (
            AppDBContext context,
            IConfiguration configuration,
            IRepository<User> repository,
            UserRepository userRepository
            )
        {
            this.repository = repository;
            this.userRepository = userRepository;
            this.context = context;
            this.configuration = configuration;
        }

        [HttpGet]
        public ActionResult<List<User>> GetAll()
        {
            User[] users = repository.GetAll().ToArray();
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
        public async Task<ActionResult<UserDTO>> Get(Guid id)
        {
            var user = await userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            return Ok(user);
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Authenticate(AuthenticationUser authenticationUser)
        {
            var user = ValidateUserInformation(authenticationUser.Email, authenticationUser.password);
            if (user == null)
            {
                return Unauthorized();
            }
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var secretKey = new SymmetricSecurityKey(
                                Encoding.ASCII.GetBytes(configuration["Authentication:SecretKey"]!));
            var signingCred = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var securityToken = new JwtSecurityToken(
                configuration["Authentication:Issuer"],
                configuration["Authentication:Audiance"],
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(9),
                signingCred
                );
            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return Ok(new { Token = token, UserId = user.UserId });
        }

        private User ValidateUserInformation(string email, string password)
        {
            var user = context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Post(CreateUser user)
        {
            try
            {
                if (user == null)
                    return BadRequest("No data provided.");

                if (string.IsNullOrWhiteSpace(user.Username) ||
                    string.IsNullOrWhiteSpace(user.FullName) ||
                    string.IsNullOrWhiteSpace(user.Password) ||
                    string.IsNullOrWhiteSpace(user.Email))
                {
                    return BadRequest("Missing required fields: Username, FullName, Password, Email.");
                }

                if (context.Users.Any(u => u.Email == user.Email))
                {
                    return Conflict("A user with the same email already exists.");
                }

                string? photoUrl = null;

                if (user.ProfileImageBase64 != null)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "users");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var extension = Path.GetExtension(user.ProfileImageBase64.FileName);
                    var uniqueFileName = Guid.NewGuid().ToString() + extension;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await user.ProfileImageBase64.CopyToAsync(stream);
                    }

                    photoUrl = $"/uploads/users/{uniqueFileName}";
                }

                var userEntity = new User
                {
                    Email = user.Email,
                    Username = user.Username,
                    Password = user.Password,
                    FullName = user.FullName,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    RoleId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    ProfileImageUrl = photoUrl
                };

                var newUser = repository.Add(userEntity);
                repository.SaveChenges();

                return CreatedAtRoute("GetUser", new { id = newUser.UserId }, newUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the user: {ex.Message}");
            }
        }

        [HttpPut]
        public ActionResult<User> UpdateUser(UpdateUser user)
        {
            if (user == null)
            {
                return BadRequest("No data in the users");
            }
            var oldUser = repository.Get(user.UserId);
            if (oldUser == null)
            {
                return NotFound("this user not found");
            }
            else
            {
                oldUser.Username = user.Username;
                oldUser.Email = user.Email;
                oldUser.Password = oldUser.Password;
                oldUser.FullName = user.FullName;
                oldUser.IsActive = oldUser.IsActive;
                oldUser.RoleId = oldUser.RoleId;
                oldUser.CreatedAt = oldUser.CreatedAt;
                oldUser.ProfileImageUrl = oldUser.ProfileImageUrl;
                repository.Update(oldUser);
                repository.SaveChenges();
                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<User> Delete(Guid id)
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

        [HttpPut("changePassword")]
        public ActionResult ChangePassword(string newPassword, Guid userId)
        {

            var oldUser = repository.Get(userId);
            if (oldUser == null)
            {
                return NotFound();
            }
            else
            {
                oldUser.Username = oldUser.Username;
                oldUser.Email = oldUser.Email;
                oldUser.Password = newPassword;
                oldUser.FullName = oldUser.FullName;
                oldUser.IsActive = oldUser.IsActive;
                oldUser.RoleId = oldUser.RoleId;
                oldUser.CreatedAt = oldUser.CreatedAt;
                repository.Update(oldUser);
                repository.SaveChenges();
                return NoContent();
            }
        }

        [HttpGet("isValidPassword")]
        public ActionResult<bool> IsValidPassword(string password, Guid userId)
        {
            var user = repository.Get(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            if (user.Password == password)
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }
    }
}