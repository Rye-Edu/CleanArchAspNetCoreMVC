using AutoMapper;
using MediatR;
using Northwind_App.ViewModels.ProductVM;
using Northwind_App.Interfaces.IRepositories;
using System.Reflection;
using Northwind_App.Interfaces.Common;
using Northwind_App.ServicesHandler.CommonServices.Filters;

namespace Northwind_App.Product_Feature.Queries
{
    public class ProductListQueryHandler : IRequestHandler<ProductListQuery, ProductViewModel>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IPaging<ProductViewModel> _productPages;

        public ProductListQueryHandler(IProductRepository productRepository, IMapper mapper, IPaging<ProductViewModel> productPages )
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _productPages = productPages;
        }
        public async Task<ProductViewModel> Handle(ProductListQuery? request, CancellationToken cancellationToken)
        {
            List<ProductViewModel> prods = new List<ProductViewModel>();
            if (GetPropertyValues(request!.ProductFilter!.GetType()))
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
            productViewModel.ProductList =  _productPages.PaginatedItems(request.ItemPage, prods.ToList()) ?? new List<ProductViewModel>();
            productViewModel.TotalPage = _productPages.TotalPage();
            
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
