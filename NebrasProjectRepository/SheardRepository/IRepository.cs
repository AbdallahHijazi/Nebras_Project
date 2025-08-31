using System;
using Microsoft.AspNetCore.Http;
using NebrasProjectDTOs.DTOs.Shared;

namespace NebrasProjectRepository.SheardRepository
{
    public interface IRepository<T>
    {
        T Add(T entity);

        T Update(T entity);

        T? Get(Guid id);

        IList<T> GetAll();

        T? Delete(T item);

        void SaveChenges();

        FileData GetFileAsBase64(string relativePath, string folderName);

        string SaveFile(IFormFile file, string folderName);

    }
}
