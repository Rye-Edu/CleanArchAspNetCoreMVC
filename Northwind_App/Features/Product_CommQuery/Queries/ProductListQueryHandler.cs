using AutoMapper;
using MediatR;
using Northwind_App.ViewModels.ProductVM;
using Northwind_App.Interfaces.IRepositories;
using System.Reflection;

namespace Northwind_App.Product_Feature.Queries
{
    public class ProductListQueryHandler : IRequestHandler<ProductListQuery, ProductViewModel>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductListQueryHandler(IProductRepository productRepository, IMapper mapper )
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ProductViewModel> Handle(ProductListQuery? request, CancellationToken cancellationToken)
        {
            List<ProductViewModel> prods = new List<ProductViewModel>();
            if (GetPropertyValues(request!.ProductFilter))
            {

                var searched = await _productRepository.GetSearchProduct(request);
                if (searched.GetEnumerator().MoveNext())
                {
                    prods = _mapper.Map<IEnumerable<ProductViewModel>>(searched).ToList();
                }

            }
            else {
                var productList = await _productRepository.GetProductDetails();
                prods = _mapper.Map<IEnumerable<ProductViewModel>>(productList).ToList();
            }

            ProductViewModel productViewModel = new();
            productViewModel.ProductList = prods;
            return Task.FromResult(productViewModel).Result;
        }

        private static bool GetPropertyValues(object obj) { 
            
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties) {


                if (property.Name != "SearchFilters" && property.GetValue(obj) != null)
                {
                 return true;
                }
            }
            return false;
        }
    }
}
