using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;
//using Microsoft.IdentityModel.Tokens;
using Northwind_App.ViewModels.ProductVM;
//using Northwind_App.Category_CommQuery.Queries;
using Northwind_App.Product_CommQuery.Commands;
using Northwind_App.Product_CommQuery.Queries;
using Northwind_App.Product_Feature.Commands;
using Northwind_App.Product_Feature.Queries;
using Northwind_App.Supplier_CommQuery.Queries;
using Northwind_App.Interfaces.Common;
using Northwind_App.ServicesHandler.CommonServices.Filters;
using Northwind_App.ServicesHandler.Upload_Services.Command;
using Northwind_App.Features.Category_CommQuery.Queries;
using Northwind_App.Features.Product_Feature.Commands;
//using Product_CoreDomain.Products_Infrastructure.Data;
//using Product_CoreDomain.Products_Infrastructure.DataModels;

namespace Product_CoreDomain.Controllers
{
    //[Route("[controller]/[action]")]
    //[Route("")]
    //[Route("Products")]
    //[Route("Products/Index")]
    public class ProductCatalogController : Controller
    {
        //private readonly NorthwindContext _context;
        private readonly IMediator _mediator;
        private readonly IPaging<ProductViewModel> _paging;

        public ProductCatalogController(IMediator mediator, IPaging<ProductViewModel> paging)
        {
           
            _mediator = mediator;
            _paging = paging;
        }

        // GET: ProductCatalog
       [HttpGet("[action]/list/page/{page:int?}")]
        [ActionName("Products")]
        public async Task<ActionResult<ProductViewModel>> ProductsIndex(ProductViewModel productVM, int page)
        {

            var productList = await _mediator.Send(new ProductListQuery(productVM));
          

            ViewBag.SelectedPage = page;
            var pagedItems = _paging.PaginatedItems(page, productList.ProductList.ToList());
            productList.PagedItems = pagedItems;      
              
            ViewData["ButtonPages"] = _paging.TotalPage();
          

            return View("Products",productList);
         
        }

        [HttpGet("[action]/list/{productViewModel?}")]
        [ActionName("Products")]

        public async Task<ActionResult<ProductViewModel>> SearchProduct(ProductViewModel productViewModel, int page)
        {

          //if (productViewModel.ProductFilter.SearchPhrase.IsNullOrEmpty())
          //  {
          //      return await ProductsIndex(productViewModel,page);
          //  }

          //  var searched = await _mediator.Send(new ProductSearch(productViewModel.ProductFilter));
            
          //  productViewModel.PagedItems = searched.ToList();
          //  ViewData["ButtonPages"] = _paging.TotalPage();

            return View("Products",productViewModel);

        }

        // GET: ProductCatalog/Details/5
        [HttpGet("detail/{id:int}")]
        [ActionName("Details")]
        public async Task<ActionResult<ProductViewModel>> Details(int? id)
        {

           var product = await _mediator.Send(new ProductQueryByID(id.GetValueOrDefault()));

            return View(product);
        }

        // GET: ProductCatalog/Create
        [HttpGet("[action]/new-product")]
        [ActionName("ProductsInfo")]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            ProductViewModel productViewModel = new ProductViewModel();
            ViewData["FormAction"] = "Create New";
         

            productViewModel.Categories = new SelectList(await _mediator.Send(new AllCategory()), "CategoryId", "CategoryName", "CategoryId");
            productViewModel.Suppliers = new SelectList(await _mediator.Send(new AllSupplier()), "SupplierId", "CompanyName", "SupplierId");

         
            return View(productViewModel);
        }

        // POST: ProductCatalog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("[action]/new-product")]
        [ActionName("ProductsInfo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,SupplierId,CategoryId,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued,ImageFile, FileData")] 
        ProductViewModel?product)
        {
            ViewData["FormAction"] = "Create New";
           


            await _mediator.Send(new CreateProduct(product));
         
            return RedirectToAction("Products");
        }

        // GET: ProductCatalog/Edit/5

        [HttpGet("[action]/edit/{id:int}")]
        [ActionName("ProductsInfo")]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["FormAction"] = "Edit Product";
           
            var singleProduct = await _mediator.Send(new ProductQueryByID(id.GetValueOrDefault()));
            
            

            singleProduct.Categories = new SelectList(await _mediator.Send(new AllCategory()), "CategoryId", "CategoryName", "CategoryId");
            singleProduct.Suppliers = new SelectList(await _mediator.Send(new AllSupplier()), "SupplierId", "CompanyName", "SupplierId");



            return View(singleProduct);
        }

        // POST: ProductCatalog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost("[action]/edit/{id:int}/{product?}")]
        [ActionName("ProductsInfo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,SupplierId,CategoryId,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued,ImageFile, FileData")] ProductViewModel? product)
        {


            await _mediator.Send(new UpdateProduct(product?.ProductId.GetValueOrDefault(), product));
            return RedirectToAction("Products");
       
        }

        // GET: ProductCatalog/Delete/5
        //[HttpGet("remove-item/{id:int}")]
        //[ActionName("RemoveProduct")]
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    var remove = await _mediator.Send(new ProductQueryByID(id.GetValueOrDefault()));

        //    return View(remove);
        //}

        // POST: ProductCatalog/Delete/5
        [HttpPost("remove-item/{id:int}/{productViewModel?}")]
        [ActionName("RemoveProduct")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, ProductViewModel productViewModel)
        {
         
            await _mediator.Send(new RemoveProduct(productViewModel.ProductId.GetValueOrDefault(), productViewModel));
         
            return RedirectToAction("ProductList");
        }

      
        private bool ProductExists(int id)
        {
            //return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
            return true;
        }
    }
}
