using AutoMapper;
using MediatR;
using Northwind_App.ViewModels.ProductVM;
using Northwind_App.Interfaces.IRepositories;
using Northwind_App.Interfaces.IServices.FileUploader;
using Northwind_Core.Domain.Entities;

namespace Northwind_App.Features.Product_Feature.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProduct, ProductViewModel?>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IUploaderService _uploaderService;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper, IUploaderService uploaderService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _uploaderService = uploaderService;
        }
        public async Task<ProductViewModel?> Handle(CreateProduct request, CancellationToken cancellationToken)
        {

            if (request?.Product?.FileData?.Length > 0 )
            {
              
                await _uploaderService.UploadFile(request.Product.FileData);
                request.Product.ImageFile = _uploaderService.FileNameUploaded;
                
            }
           var create = await _productRepository.AddEntityAsync(_mapper.Map<Product>(request?.Product));
            var created = _mapper.Map<ProductViewModel>(create);
            return Task.FromResult(created).Result;
        }

       
    }
}
