using AutoMapper;
using MediatR;
using Northwind_App.Interfaces.IRepositories;
using Northwind_App.ViewModels.ProductVM;
using Northwind_App.ViewModels.PurchaseVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind_App.Features.PurchaseRequest_CommQuery.Queries
{
    public class ProductDetailQuery : IRequest<PurchaseRequestDetailVM> {

        public int? ProductID { get; set; }
        public PurchaseRequestDetailVM ProductDetail { get; set; }
        public ProductDetailQuery(int productID, PurchaseRequestDetailVM productDetail)
        {
            ProductID = productID;
            ProductDetail = productDetail;

        }
    }
    public class ProductDetailQueryHandler : IRequestHandler<ProductDetailQuery, PurchaseRequestDetailVM>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductDetailQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<PurchaseRequestDetailVM> Handle(ProductDetailQuery? request, CancellationToken cancellationToken)
        {
            var purchaseRequest = new PurchaseRequestDetailVM();
            if (request!.ProductID != null) {

                var detail = await _productRepository.GetSingleProduct(request!.ProductID.GetValueOrDefault());
                if (detail != null) {
                   var product = _mapper.Map<ProductViewModel>(detail);
                    purchaseRequest.Product = product;    
                }
            }
            return Task.FromResult(purchaseRequest).Result;
        }
    }
}
