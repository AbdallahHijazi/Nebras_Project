using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NebrasProjectRepository.SheardRepository
{
    public interface IRepository<T>
    {
        T Add(T entity);

        T Update(T entity);

        T? Get(string id);

        IList<T> GetAll();

        T? Delete(T item);

        void SaveChenges();

    }
}
