﻿using AutoMapper;
using MediatR;
using Northwind_App.ViewModels.ProductVM;
using Northwind_App.Interfaces.IRepositories;
using System.Reflection;

namespace Northwind_App.Product_Feature.Queries
{
    public class ProductListQueryHandler : IRequestHandler<ProductListQuery,ProductFilterVM>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductListQueryHandler(IProductRepository productRepository, IMapper mapper )
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ProductFilterVM> Handle(ProductListQuery? request, CancellationToken cancellationToken)
        {
            if (request.ProductFilter == null) {
                var x = "asdfa";
                x.ToUpper();
            }
            GetPropertyValues(request?.ProductFilter);

            var productList = await _productRepository.GetProductDetails();

            ProductFilterVM productFilterVM = request.ProductFilter;
            var products = _mapper.Map<IEnumerable<ProductViewModel>>(productList);
            request.ProductFilter.ProductSearchList = products;
            //ProductViewModel productViewModel = new();
            //productViewModel.ProductList = products;
            return Task.FromResult(productFilterVM).Result;
        }

        private static bool GetPropertyValues(object obj) { 
            
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties) {
              //  if (property.GetValue(obj) == null || (string)property?.GetValue(obj) == string.Empty) {
                    var x = property.Name;
                    var y = property.GetValue(obj);
                   // return true;
                //}
            }
            return false;
        }
    }
}
