using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Northwind_App.Interfaces.IServices.FileUploader;

namespace Northwind_Infrastructure.Services.Uploader
{
    public class Uploader_Service : IUploaderService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;


        public string FileNameUploaded { get; private set; } = null!;

     
        public Uploader_Service(IWebHostEnvironment webHostEnvironment)
        {
            
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<bool?> UploadFile(IFormFile formFile)
        {
            string path = string.Empty;
            
            try
            {

                if (formFile.Length > 0)
                {
                    FileNameUploaded = $"{Guid.NewGuid()} -{formFile.FileName}";
                   // formFile.FileName = fileUploadedName;
                    if (CheckIfImage(formFile.FileName))
                    {
                        path = Path.GetFullPath(Path.Combine(_webHostEnvironment.WebRootPath + "\\UploadedFiles\\Images\\"));
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                           // var ext = Path.GetExtension(path);
                        }

                        using (var file = new FileStream(Path.Combine(path, FileNameUploaded),FileMode.Create)){

                            await formFile.CopyToAsync(file);
                        }                   
                        return await Task.FromResult(true);
                    }
                    else
                    {
                        return await Task.FromResult(false);
                        throw new Exception("Not a valid image type");

                    }
                }
                else
                {
                    return await Task.FromResult(false);
                }
            }
            catch (Exception ex)
            {

                throw new Exception("File copy failed " + ex.Message);
            }
        }

       
        private bool CheckIfImage(string file)
        {

            string[] exts = new string[] { ".jpg", ".jpeg", ".png" };
            var extensions = Path.GetExtension(file);
            if (exts.Contains(extensions))
            {
                return true;
            }
            return false;
        }

       
        public async Task<bool?> RemoveUploadedFile(string? uploadedFile)
        {
            string filePath = Path.GetFullPath(Path.Combine(_webHostEnvironment.WebRootPath + "\\UploadedFiles\\Images\\" + uploadedFile));
            if (File.Exists(filePath)) {
               var x =   Path.GetFileName(filePath);
               File.Delete(filePath);
              return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}
