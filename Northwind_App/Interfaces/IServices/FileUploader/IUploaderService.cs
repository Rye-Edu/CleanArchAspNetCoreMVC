using Microsoft.AspNetCore.Http;

namespace Northwind_App.Interfaces.IServices.FileUploader
{
    public interface IUploaderService
    {
        Task<bool?> UploadFile(IFormFile formFile);
        Task<bool?> RemoveUploadedFile(string? uploadedFile);
        public string? FileNameUploaded { get; }
      
    }
}
