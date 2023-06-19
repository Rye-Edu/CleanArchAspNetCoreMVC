using MediatR;
using Microsoft.AspNetCore.Mvc;
using Northwind_App.Features.PurchaseRequest_CommQuery.Commands;
using Northwind_App.Features.PurchaseRequest_CommQuery.Queries;
using Northwind_App.Interfaces.Common;
using Northwind_App.ViewModels.PurchaseVM;

namespace NorthwindMVC_UI.Controllers.Purchase
{
   

    public class PurchaseRequestController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IPaging<PurchaseRequestDetailVM> _paging;

        public PurchaseRequestController(IMediator mediator, IPaging<PurchaseRequestDetailVM> paging)
        {
            _mediator = mediator;
            _paging = paging;
        }
        public IActionResult Create()
        {
            return View("PurchaseRequest");
        }


        //Get product detail for purchase creation
        [HttpGet("[controller]/{requestAction}/productid-{id:int}", Name = "GetPurchaseRequest")]
        [ActionName("RestockProduct")]
        public async Task<IActionResult> GetPurchaseRequest(string requestAction, int id)
        {


            ViewData["RequestAction"] = requestAction;
            var productDetail = await _mediator.Send(new ProductDetailQuery(id, new PurchaseRequestDetailVM()));


            return View("~/Views/Purchase/PurchaseRequest/PurchaseRequest.cshtml", productDetail);
        }


        [HttpPost("[controller]/{requestAction}/productid-{id:int}", Name = "PurchaseRequest")]
        [ActionName("RestockProduct")]
        public async Task<IActionResult> CreatePurchaseRequest(int id, PurchaseRequestDetailVM? productDetail)
        {
            productDetail!.ProductID = id;
            var createPurchaseRequest = await _mediator.Send(new CreatePurchaseRequest(productDetail!));

            return RedirectToAction("Products", "ProductCatalog", new { list = "list", itemPage = 1 });
        }


        [HttpGet("[controller]/list/page-{itemPage:int}", Name = "PurchaseRequestList")]
        public async Task<IActionResult> PurchaseRequestList(int itemPage = 1)
        {

            ViewData["SelectedNav"] = "purchase";
            ViewData["SelectedLink"] = "purchase-request";
            ViewData["PageNumber"] = itemPage;
            var requestList = await _mediator.Send(new PurchaseRequestListQuery(itemPage));
            PurchaseRequestDetailVM purchaseRequestDetailVM = new PurchaseRequestDetailVM();
            ViewData["ButtonPages"] = requestList.Pages;

            return View("~/Views/Purchase/PurchaseRequest/PurchaseRequestList.cshtml", requestList);
        }


    }
}
