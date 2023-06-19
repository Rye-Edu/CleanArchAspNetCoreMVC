using MediatR;
using Microsoft.AspNetCore.Mvc;
using Northwind_App.Features.PurchaseRequest_CommQuery.Commands;
using Northwind_App.Features.PurchaseRequest_CommQuery.Queries;
using Northwind_App.ViewModels.PurchaseVM;

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

            return View("PurchaseRequest", requestDetail);
        }
        [HttpPost("[controller]/{requestAction}/{id:int}/{requestDetailVM?}", Name = "ApproveSelectedPurchaseRequest")]
        [ActionName("RestockProduct")]
        public async Task<IActionResult> ApproveSelectedPurchaseRequest(int id, PurchaseRequestDetailVM requestDetailVM)
        {
            //To implement in NorthWind_App
            var requestDetail = await _mediator.Send(new ApprovePurchaseRequestCommand(id, requestDetailVM));
            return RedirectToRoute("PurchaseRequestList", new { purchase = "purchase" });
        }
        [HttpGet("[controller]/{requestAction}/list", Name = "ApprovedPurchases")]
        public async Task<IActionResult> GetApprovedPurchases()
        {
            ViewData["SelectedNav"] = "purchase";
            ViewData["SelectedLink"] = "approved-purchases";

            var purchases = await _mediator.Send(new ApprovedRequestQuery());
            return View("ApprovedPurchases", purchases);
        }
    }
}
