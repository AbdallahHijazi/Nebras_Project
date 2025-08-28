using Microsoft.EntityFrameworkCore;
using NebrasProjectDomain.Models;
using NebrasProjectDTOs.DTOs.Shared;
using NebrasProjectDTOs.DTOs.UsersDTO;
using NebrasProjectModels.Models.Users;
using NebrasProjectRepository.SheardRepository;

namespace NebrasProjectRepository.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        private readonly AppDBContext context;

        public UserRepository(AppDBContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<UserDTO?> GetByIdAsync(Guid userId)
        {
            var user = await context.Users
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
                return null;

            FileData? profileImage = null;

            if (!string.IsNullOrEmpty(user.ProfileImageUrl))
            {
                var safeFileName = Path.GetFileName(user.ProfileImageUrl);
                var filePath = Path.Combine("wwwroot/uploads", safeFileName);

                if (System.IO.File.Exists(filePath))
                {
                    var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
                    var base64String = Convert.ToBase64String(fileBytes);

                    var extension = Path.GetExtension(filePath).ToLower();
                    var contentType = extension switch
                    {
                        ".jpg" or ".jpeg" => "image/jpeg",
                        ".png" => "image/png",
                        ".gif" => "image/gif",
                        _ => "application/octet-stream"
                    };

                    profileImage = new FileData
                    {
                        Base64String = base64String,
                        ContentType = contentType
                    };
                }
            }

            var schools = await context.Schools
                .Where(s => s.AddedByUserId == user.UserId)
                .Select(s => new SchoolDto
                {
                    //SchoolId = s.SchoolId,
                    //CityName = s.City != null ? s.City.NameAr : string.Empty,
                    //SchoolName = s.NameAr,
                    //GovernorateName = s.City!.Governorate != null ? s.City.Governorate.NameAr : string.Empty,
                    //CityId = s.City != null ? s.City.CityId : Guid.Empty,
                    //GovernorateId = s.City!.Governorate != null ? s.City.Governorate.GovernorateId : Guid.Empty
                })
                .ToListAsync();

            return new UserDTO
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                FullName = user.FullName,
                Role = "Administrator",
                CreatedAt = user.CreatedAt,
                IsActive = user.IsActive,
                ProfileImageUrl = profileImage,
                Schools = schools
            };
        }

    }
}
