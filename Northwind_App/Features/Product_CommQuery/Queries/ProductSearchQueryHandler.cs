using AutoMapper;
using MediatR;
//using Microsoft.IdentityModel.Tokens;
using Northwind_App.ViewModels.ProductVM;
using Northwind_App.Interfaces.IRepositories;
using Northwind_App.Product_Feature.Queries;

namespace Northwind_App.Product_CommQuery.Queries
{
    public class ProductSearch:IRequest<IEnumerable<ProductViewModel?>>{

        //public string? SearchPhrase  { get; set; }
        // public ProductViewModel? Product { get; set; }
        public ProductFilterVM ProductFilters { get; set; }


        public ProductSearch(ProductFilterVM productFilters)
        {
            ProductFilters = productFilters;
        }
    }
    public class ProductSearchQueryHandler : IRequestHandler<ProductSearch, IEnumerable<ProductViewModel?>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductSearchQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductViewModel?>> Handle(ProductSearch? request, CancellationToken cancellationToken)
        {
            //if (!string.IsNullOrEmpty(request?.ProductFilters))
            //{
            //    return null;
            //}
            //else {

                var product = await _productRepository.GetSearchProduct(request );
                if (product.GetEnumerator().MoveNext())
                {

                    return Task.FromResult(_mapper.Map<IEnumerable<ProductViewModel>>(product)).Result.ToList();
                }
            //else
            //{
            //return Task.FromResult();
            //}
            // }
            return Task.FromResult(_mapper.Map<IEnumerable<ProductViewModel>>(product)).Result.ToList();
            // throw new NotImplementedException();
        }
    }
}
