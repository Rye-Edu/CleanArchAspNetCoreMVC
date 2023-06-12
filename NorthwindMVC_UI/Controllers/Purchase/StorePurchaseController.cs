using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using Northwind_App.Features.PurchaseRequest_CommQuery.Commands;
using Northwind_App.Features.PurchaseRequest_CommQuery.Queries;
using Northwind_App.Interfaces.IRepositories;
using Northwind_App.ViewModels.ProductVM;
using Northwind_App.ViewModels.PurchaseVM;
using Northwind_Core.Domain.Entities;
using System.Collections;

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
        [HttpGet("[controller]/{purchase?}/request/list", Name = "PurchaseRequestList")]
        public async Task<IActionResult> PurchaseRequestList(string purchase = "purchase") {

            ViewData["SelectedNav"] = purchase;
            var requestList = await _mediator.Send(new PurchaseRequestListQuery());
          PurchaseRequestDetailVM purchaseRequestDetailVM = new PurchaseRequestDetailVM();
            purchaseRequestDetailVM.PurchaseRequestDetailList = requestList.ToList();
            return View(requestList);
        }

        #region Process Purchase request
        // [HttpPost]
        [HttpGet("[controller]/puchase-request/{requestID?}", Name ="RequestDetail")]
       // [HttpGet]
        public async Task<IActionResult> ProcessPurchaseRequest(int? requestID)
        {
            //var purchaseRequest = await _mediator.Send(new ApprovePurchaseRequestCommand(requestID.GetValueOrDefault()));
            //var requestDetail = await _mediator.Send();
            //var x = "asdfasdf";
            var productDetail = await _mediator.Send(new ApprovePurchaseRequestCommand(requestID.GetValueOrDefault(), new PurchaseRequestDetailVM()));

            return View("PurchaseRequest", productDetail);
            //return View();
        }

        #endregion
    }
}
