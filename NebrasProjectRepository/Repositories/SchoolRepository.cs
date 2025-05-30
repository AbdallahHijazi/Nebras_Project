using Microsoft.EntityFrameworkCore;
using NebrasProjectDomain.Models;
using NebrasProjectDTOs.DTOs.SchoolsDTO;
using NebrasProjectModels.Models.Schools;
using NebrasProjectRepository.SheardRepository;

namespace NebrasProjectRepository.Repositories
{
    public class SchoolRepository : GenericRepository<School>
    {
        private readonly AppDBContext context;

        public SchoolRepository(AppDBContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IList<SchoolDetailsDto>> GetSchoolsByGovernorateId(Guid governorateId)
        {
            var governorate = await context.Governorates
                .FirstOrDefaultAsync(g => g.GovernorateId == governorateId);

            if (governorate == null)
                return new List<SchoolDetailsDto>(); // أو throw NotFoundException إذا كنت تستخدم service layer

            var schools = await context.Schools
               .Where(s => s.City.GovernorateId == governorateId)
               .Select(s => new SchoolDetailsDto
               {
                   SchoolId = s.SchoolId,
                   NameAr = s.NameAr,
                   NameEn = s.NameEn,
                   CityId = s.CityId,
                   AddressDetails = s.AddressDetails,
                   Latitude = s.Latitude,
                   Longitude = s.Longitude,
                   ConditionDescription = s.ConditionDescription,
                   EstimatedRenovationCost = s.EstimatedRenovationCost,
                   AddedByUserId = s.AddedByUserId,
                   StudentCapacity = s.StudentCapacity,
                   NumberOfClassrooms = s.NumberOfClassrooms,
                   YearEstablished = s.YearEstablished,
                   SchoolTypeId = s.SchoolTypeId,
                   SchoolStatusId = s.SchoolStatusId,
                   AddedByUserName = context.Users
                        .Where(u => u.UserId == s.AddedByUserId)
                        .Select(u => u.FullName)
                        .FirstOrDefault()!,
                   ApprovedByUserId = s.ApprovedByUserId,
                   ApprovedByUserName = context.Users
                        .Where(u => u.UserId == s.ApprovedByUserId)
                        .Select(u => u.FullName)
                        .FirstOrDefault()!,
                   IsApproved = s.IsApproved,
                   RequiredRenovations = s.RequiredRenovations.Select(r => new RenovationRequestDto
                   {
                       RenovationTypeId = r.RenovationTypeId,
                       Notes = r.Notes
                   }).ToList(),

                   SchoolStatusName = s.SchoolStatus.StatusNameAr,
                   SchoolTypeName = s.SchoolType.TypeNameAr,
                   CityName = s.City.NameAr,
               }).ToListAsync();

            return schools;
        }


        //public async Task<IList<SchoolsNameDTO>> GetSchoolNameAndId(Guid id)
        //{
        //    //var schools = await context.Schools.Where(s => s.GovernorateId == id)
        //    //                           .Select(s => new SchoolsNameDTO
        //    //                           {
        //    //                               Name = s.Name,
        //    //                               Id = s.Id.ToString()
        //    //                           }).
        //    //                           ToListAsync();

        //    return null;
        //}

        public async Task<School> GetSchoolsByDamageLevel(int min, int max)
        {
            //var school = await context.Schools
            //    .Where(s => s.DamageLevel <= min && s.DamageLevel >= max)
            //    .FirstOrDefaultAsync();

            return null;
        }
    }
}
