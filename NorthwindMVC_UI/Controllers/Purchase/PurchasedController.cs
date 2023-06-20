using MediatR;
using Microsoft.AspNetCore.Mvc;
using Northwind_App.Features.PurchaseRequest_CommQuery.Commands;
using Northwind_App.Features.PurchaseRequest_CommQuery.Queries;
using Northwind_App.ViewModels.PurchaseVM;
using Northwind_Core.Domain.Entities;

namespace NorthwindMVC_UI.Controllers.Purchase
{
    public class PurchasedController : Controller
    {
        private readonly IMediator _mediator;

        public PurchasedController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //edit approve, decline purchase request
        [HttpGet("[controller]/{requestAction}/{id:int}", Name = "SelectedPurchaseRequest")]
        [ActionName("RestockProduct")]
        public async Task<IActionResult> GetSelectedPurchaseRequest(int id, PurchaseRequestDetailVM requestDetailVM)
        {

            var requestDetail = await _mediator.Send(new SelectedPurchasRequestCommand(id));
      
            return View("~/Views/Purchase/PurchaseRequest/PurchaseRequest.cshtml", requestDetail);
           
        }
        [HttpPost("[controller]/{requestAction}/{id:int}/{requestDetailVM?}", Name = "ApproveSelectedPurchaseRequest")]
        [ActionName("RestockProduct")]
        public async Task<IActionResult> ApproveSelectedPurchaseRequest(int id, PurchaseRequestDetailVM requestDetailVM)
        {
           
            var requestDetail = await _mediator.Send(new ApprovePurchaseRequestCommand(id, requestDetailVM));
            return RedirectToRoute("PurchaseRequestList", new { itemPage=1});
        }
        [HttpGet("[controller]/{requestAction}/list/{itemPage:int}", Name = "ApprovedPurchases")]
        public async Task<IActionResult> GetApprovedPurchases(int itemPage = 1)
        {
            ViewData["SelectedNav"] = "purchase";
            ViewData["SelectedLink"] = "approved-purchases";
            ViewData["PageNumber"] = itemPage;
           
            var purchases = await _mediator.Send(new ApprovedRequestQuery(itemPage));
            ViewData["ButtonPages"] = purchases.TotalPage;
            return View("~/Views/Purchase/ApprovedPurchases/ApprovedPurchases.cshtml", purchases);
        }
    }
}
