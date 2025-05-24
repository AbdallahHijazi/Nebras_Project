using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NebrasProjectDomain.Models;

namespace NebrasProjectRepository.SheardRepository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly AppDBContext context;


        public GenericRepository(AppDBContext context)
        {
            this.context = context;
        }

        public T Add(T entity)
        {
            var newEintity = context.Add(entity);
            return newEintity.Entity;
        }

        public T? Delete(T item)
        {
            //var item = context.Set<T>().Find(id);
            if (item == null)
            {
                return null;
            }
            context.Remove(item);
            return item;
        }

        public T? Get(Guid id)
        {
            var item = context.Set<T>().Find(id);
            if (item == null)
            {
                return null;
            }
            return item;
        }

        public IList<T> GetAll()
        {
            var item = context.Set<T>().ToList();
            return item;
        }

        public void SaveChenges()
        {
            context.SaveChanges();
        }

        public T Update(T entity)
        {
            var item = context.Update(entity);
            return item.Entity;
        }

    }
}
