using API.FileProcessing.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

namespace API.FileProcessing.Service
{
    public class ManageImage : IManageImage
    {
        private readonly AppDbContext _context;

        public ManageImage(AppDbContext context)
        {
            _context = context;
        }
        private static string GetStaticContentDirectory()
        {
            var result = Path.Combine(Directory.GetCurrentDirectory(), "Uploads\\StaticContent\\");
            if (!Directory.Exists(result))
            {
                Directory.CreateDirectory(result);
            }
            return result;
        }

        public async Task<string> UploadFile(IFormFile _IFormFile )
        {
            string FileName = "";
            try
            {
                FileInfo _FileInfo = new FileInfo(_IFormFile.FileName);
                FileName = _IFormFile.FileName + "_" + DateTime.Now.Ticks.ToString() + _FileInfo.Extension;
                var _GetFilePath = Path.Combine(GetStaticContentDirectory(), FileName);
                using (var _FileStream = new FileStream(_GetFilePath, FileMode.Create))
                {
                    await _IFormFile.CopyToAsync(_FileStream);
                }

                Console.WriteLine("6666666666666666666666666666666666666666666" + _GetFilePath);
                return FileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<(byte[], string, string)> DownloadFile(string FileName)
        {
            try
            {
                var _GetFilePath = Path.Combine(GetStaticContentDirectory(), FileName);
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(_GetFilePath, out var _ContentType))
                {
                    _ContentType = "application/octet-stream";
                }
                var _ReadAllBytesAsync = await File.ReadAllBytesAsync(_GetFilePath);
                return (_ReadAllBytesAsync, _ContentType, Path.GetFileName(_GetFilePath));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<product>> getAllProducts()
        {
            var result = await _context.Products.ToListAsync();
            if(result is null)
            {
                return null;
            }
            return result;
        }


        public async Task<product> AddProduct( product productToAdd)
        {
            string FileName = "";
            try
            {
                FileInfo _FileInfo = new FileInfo(productToAdd.formFile.FileName);
                FileName = productToAdd.formFile.FileName + "_" + DateTime.Now.Ticks.ToString() + productToAdd.formFile;
                var _GetFilePath = Path.Combine(GetStaticContentDirectory(), FileName);
                using (var _FileStream = new FileStream(_GetFilePath, FileMode.Create))
                {
                    await productToAdd.formFile.CopyToAsync(_FileStream);
                }


                var product =  new product()
                {
                    Id = Guid.NewGuid(),
                    Name = FileName,
                    Description = _FileInfo.Extension,
                    Price = 55.5,
                    ImageUrl = _GetFilePath
                };

                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return product;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
