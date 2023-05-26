using AutoMapper;
using MediatR;
using Northwind_App.ViewModels.ProductVM;
using Northwind_App.Product_Feature.Queries;
using Northwind_App.Interfaces.IRepositories;
using Northwind_App.Interfaces.IServices.FileUploader;

namespace Northwind_App.Product_CommQuery.Commands
{
    public class RemoveProduct:IRequest<ProductViewModel?>
    {
        public RemoveProduct(int? productID,ProductViewModel product)
        {
            ProductID = productID;
            Product = product;
        }

        public int? ProductID { get; set; }
        public ProductViewModel? Product { get; set; }
    }

    public class RemoveProductCommandHandler : IRequestHandler<RemoveProduct, ProductViewModel?>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IUploaderService _uploaderService;

        public RemoveProductCommandHandler(IProductRepository productRepository, IMapper mapper, IUploaderService uploaderService)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _uploaderService = uploaderService;
        }
        public async Task<ProductViewModel?> Handle(RemoveProduct request, CancellationToken cancellationToken)
        {
            ProductViewModel productViewModel = request.Product;
            
            if (request.Product != null) { 
                var found = await _productRepository.GetSingleProduct(request.ProductID.GetValueOrDefault());
              
                var p = _mapper.Map<ProductViewModel>(request.Product);
                ProductQueryByID productQueryByID = new ProductQueryByID(request.Product.ProductId.GetValueOrDefault());
                if (found != null)
                {
                    await _productRepository.DeleteEntityAsync(found.ProductId);
                    await _uploaderService.RemoveUploadedFile(found.ImageFile);
                }
            }
             
            return Task.FromResult(productViewModel).Result;
        }
    }
}
