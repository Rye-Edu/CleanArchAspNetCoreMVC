using AutoMapper;
using MediatR;
using Northwind_App.ViewModels.ProductVM;
using Northwind_App.Interfaces.IRepositories;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Northwind_App.Product_Feature.Queries
{
    public class ProductByIDQueryHandler : IRequestHandler<ProductQueryByID, ProductViewModel>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductByIDQueryHandler(IProductRepository productRepository, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<ProductViewModel> Handle(ProductQueryByID request, CancellationToken cancellationToken)
        {
          
            var product = await _productRepository.GetSingleProduct(request.ProductID);
         
            var singleProduct = _mapper.Map<ProductViewModel>(product);
            return Task.FromResult(singleProduct).Result;

        }

      
    }
}
