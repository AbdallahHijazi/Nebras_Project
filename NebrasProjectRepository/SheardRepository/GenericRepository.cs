using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NebrasProjectDomain.Models;
using NebrasProjectDTOs.DTOs.Shared;

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

        public FileData? GetFileAsBase64(string relativePath, string folderName)
        {
            if (string.IsNullOrEmpty(relativePath))
                return null;

            var safeFileName = Path.GetFileName(relativePath);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", folderName, safeFileName);

            if (!System.IO.File.Exists(filePath))
                return null;

            var fileBytes = System.IO.File.ReadAllBytes(filePath);

            var contentType = Path.GetExtension(filePath).ToLower() switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream"
            };

            return new FileData
            {
                Base64String = Convert.ToBase64String(fileBytes),
                ContentType = contentType
            };
        }

        public string SaveFile(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
                return null;

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", folderName);

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var extension = Path.GetExtension(file.FileName);
            var uniqueFileName = Guid.NewGuid().ToString() + extension;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyToAsync(stream);
            }

            return $"/uploads/{folderName}/{uniqueFileName}";
        }

    }
}
