﻿using MediatR;
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
        public IActionResult Create()
        {
            return View("PurchaseRequest");
        }
       

        //Get product detail for purchase creation
       [HttpGet("[controller]/purchase-request/{requestAction?}/{id:int?}", Name = "GetPurchaseRequest")]
        [ActionName("RestockProduct")]
        public async Task <IActionResult> GetPurchaseRequest(string requestAction,int id) {

            
            ViewData["RequestAction"] = requestAction;
         
            var productDetail =await _mediator.Send(new ProductDetailQuery(id, new PurchaseRequestDetailVM()));

            return View("PurchaseRequest", productDetail);
        }
        [HttpPost("[controller]/purchase-request/{requestAction}/{id:int}", Name = "PurchaseRequest")]
        [ActionName("RestockProduct")]
        public async Task<IActionResult> CreatePurchaseRequest( int id, PurchaseRequestDetailVM? productDetail)
        {
            var createPurchaseRequest = await _mediator.Send(new CreatePurchaseRequest(productDetail!));
   
            return RedirectToAction("Products","ProductCatalog", new {list="list", productPage= 1});
        }


        //edit approve, decline purchase request
        [HttpGet("[controller]/{requestAction}/{id:int?}", Name = "SelectedPurchaseRequest")]
        [ActionName("RestockProduct")]
        public async Task<IActionResult> GetSelectedPurchaseRequest(int id, PurchaseRequestDetailVM requestDetailVM) {

            var requestDetail = await _mediator.Send(new SelectedPurchasRequestCommand(id));


            var s = "s";
            return View("PurchaseRequest", requestDetail);
        }
        [HttpGet("[controller]/{requestAction}/{id:int}/{requestDetailVM?}", Name = "ApproveSelectedPurchaseRequest")]
        [ActionName("RestockProduct")]
        public async Task<IActionResult> ApproveSelectedPurchaseRequest(int id, PurchaseRequestDetailVM requestDetailVM)
        {

            var requestDetail = await _mediator.Send(new SelectedPurchasRequestCommand(id));


            var s = "s";
            return RedirectToAction("Products", "ProductCatalog", new { list = "list", productPage = 1 });
        }


        [HttpGet("[controller]/{purchase?}/request/list", Name = "PurchaseRequestList")]
        public async Task<IActionResult> PurchaseRequestList(string purchase = "purchase")
        {

            ViewData["SelectedNav"] = purchase;
            var requestList = await _mediator.Send(new PurchaseRequestListQuery());
            PurchaseRequestDetailVM purchaseRequestDetailVM = new PurchaseRequestDetailVM();
            purchaseRequestDetailVM.PurchaseRequestDetailList = requestList.ToList();
            return View(requestList);
        }

        #region Process Purchase request
        // [HttpPost]
        // [HttpGet("[controller]/puchase-request/{requestID?}", Name ="RequestDetail")]
        //// [HttpGet]
        // public async Task<IActionResult> ProcessPurchaseRequest(int? requestID)
        // {
        //    // var purchaseRequest = await _mediator.Send(new ApprovePurchaseRequestCommand(requestID.GetValueOrDefault()));
        //     //var requestDetail = await _mediator.Send();
        //     //var x = "asdfasdf";
        //     var productDetail = await _mediator.Send(new ApprovePurchaseRequestCommand(requestID.GetValueOrDefault(), new PurchaseRequestDetailVM()));

        //     return View("PurchaseRequest", new PurchaseRequestDetailVM());
        //     //return View();
        // }

        #endregion
    }
}