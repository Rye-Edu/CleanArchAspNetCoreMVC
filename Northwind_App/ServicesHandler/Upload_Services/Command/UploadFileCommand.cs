using MediatR;
using Microsoft.AspNetCore.Http;
using Northwind_App.Interfaces.IServices.FileUploader;

namespace Northwind_App.ServicesHandler.Upload_Services.Command
{
    public class UploadFileCommand:IRequest<string>
    {
        public IFormFile UploadedFile { get; set; }
        public UploadFileCommand(IFormFile formFile)
        {
            UploadedFile = formFile;   
        }
    }
    public class UploadFileCommandHandler : IRequestHandler<UploadFileCommand, string>
    {
        private readonly IUploaderService _uploaderService;

        public UploadFileCommandHandler(IUploaderService uploaderService)
        {
            _uploaderService = uploaderService;
        }
        public Task<string> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {

            string fileUploadedName = string.Empty;

            if (request.UploadedFile != null) { 
              // fileUploadedName = _uploaderService.UploadFile(request.UploadedFile).Result.FileName;
               /// fileUploaded = $"{Guid.NewGuid()}-{fileName.Result.FileName}";
            }

            return Task.FromResult(fileUploadedName);

        }
    }
}
