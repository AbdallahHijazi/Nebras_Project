using NebrasProjectDomain.Models;
using NebrasProjectModels.Models.Photos;
using NebrasProjectRepository.SheardRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebrasProjectRepository.Repositories
{
    public class PhotosRepository : GenericRepository<SchoolPhoto>
    {
        private readonly AppDBContext context;

        public PhotosRepository(AppDBContext context) : base(context)
        {
            this.context = context;
        }
        public async Task<SchoolPhoto> GetSchoolPhotos(Guid id)
        {
            var photos=context.SchoolPhotos.Where( s => s.PhotoId == id).ToList();
            return null;
        }
     
    }
}
