using AutoMapper;
using MediatR;
using Northwind_App.ViewModels.ProductVM;
using Northwind_App.Interfaces.IRepositories;
using Northwind_App.Interfaces.IServices.FileUploader;
using Northwind_Core.Domain.Entities;

namespace Northwind_App.Product_Feature.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProduct, ProductViewModel>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IUploaderService _uploaderService;

        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper, IUploaderService uploaderService)
        {
            _productRepository = productRepository;
         
            _mapper = mapper;
            _uploaderService = uploaderService;
        }



        public async Task<ProductViewModel> Handle(UpdateProduct request, CancellationToken cancellationToken)
        {

            if (request?.Product?.FileData?.Length > 0)
            {

                await _uploaderService.UploadFile(request.Product.FileData);
                if (string.IsNullOrEmpty(request.Product.ImageFile))
                {
                    request.Product.ImageFile = _uploaderService.FileNameUploaded;
                }               
                else if (!string.IsNullOrEmpty( _uploaderService.FileNameUploaded) && (!string.IsNullOrEmpty(request.Product.ImageFile))) {

                    await _uploaderService.RemoveUploadedFile(request.Product.ImageFile);
                   
                    request.Product.ImageFile = _uploaderService.FileNameUploaded;
                }
               

            }
            var updateProduct = _mapper.Map<Product>(request?.Product);
            await _productRepository.UpdateEntityAsync(updateProduct);
          

            return Task.FromResult(_mapper.Map<ProductViewModel>(updateProduct)).Result;
          
        }
    }
}
