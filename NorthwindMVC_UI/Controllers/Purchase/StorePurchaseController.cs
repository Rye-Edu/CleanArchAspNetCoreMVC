using MediatR;
using Microsoft.AspNetCore.Mvc;
using Northwind_App.Features.PurchaseRequest_CommQuery.Commands;
using Northwind_App.Features.PurchaseRequest_CommQuery.Queries;
using Northwind_App.Interfaces.IRepositories;
using Northwind_App.ViewModels.ProductVM;
using Northwind_App.ViewModels.PurchaseVM;

namespace NorthwindMVC_UI.Controllers.Purchase
{
    public class StorePurchaseController : Controller
    {
        private readonly IMediator _mediator;

        public StorePurchaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("[controller]/purchase-request/create/{productID}", Name ="PurchaseRequest")]
        public async Task <IActionResult> PurchaseRequest(PurchaseRequestVM product, int productID) {
          
            var productDetail =await _mediator.Send(new ProductDetailQuery(productID, product));

            //var p = await _mediator.Send(new CreatePurchaseRequest())
            productDetail.UnitsInStock = productDetail?.ProductDetail?.UnitsInStock ?? 0;

            return View(productDetail);
        }
    }
}
