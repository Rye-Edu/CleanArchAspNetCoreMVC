using MediatR;
using Microsoft.AspNetCore.Mvc;
using Northwind_App.Features.PurchaseRequest_CommQuery.Commands;
using Northwind_App.Features.PurchaseRequest_CommQuery.Queries;
using Northwind_App.Interfaces.IRepositories;
using Northwind_App.ViewModels.ProductVM;
using Northwind_App.ViewModels.PurchaseVM;
using Northwind_Core.Domain.Entities;

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
        [HttpGet("[controller]/purchase-request/create/{productID?}", Name ="GetPurchaseRequest")]
        public async Task <IActionResult> GetPurchaseRequest(int productID) {
          
            var productDetail =await _mediator.Send(new ProductDetailQuery(productID, new PurchaseRequestDetailVM()));

            return View("PurchaseRequest", productDetail);
        }

        [HttpPost("[controller]/purchase-request/create/{productID}", Name = "PurchaseRequest")]
        public async Task<IActionResult> CreatePurchaseRequest(PurchaseRequestDetailVM? productDetail, int productID)
        {
            var createPurchaseRequest = await _mediator.Send(new CreatePurchaseRequest(productDetail));

            return RedirectToAction("Products", "ProductCatalog", new { page = 1 });
        }
    }
}
