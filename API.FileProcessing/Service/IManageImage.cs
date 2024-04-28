using API.FileProcessing.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.FileProcessing.Service
{
    public interface IManageImage
    {
        Task<string> UploadFile(IFormFile _IFormFile);
        Task<product> AddProduct(product productToAdd);
        public Task<List<product>> getAllProducts();
        Task<(byte[], string, string)> DownloadFile(string FileName);
    }
}
