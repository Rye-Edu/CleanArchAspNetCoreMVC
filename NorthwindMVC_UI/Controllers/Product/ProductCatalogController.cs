using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Northwind_App.ViewModels.ProductVM;
using Northwind_App.Product_CommQuery.Commands;
using Northwind_App.Product_Feature.Commands;
using Northwind_App.Product_Feature.Queries;
using Northwind_App.Supplier_CommQuery.Queries;
using Northwind_App.Interfaces.Common;
using Northwind_App.ServicesHandler.CommonServices.Filters;
using Northwind_App.ServicesHandler.Upload_Services.Command;
using Northwind_App.Features.Category_CommQuery.Queries;
using Northwind_App.Features.Product_Feature.Commands;
using Microsoft.IdentityModel.Tokens;


namespace Product_CoreDomain.Controllers
{
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


        [HttpGet("[controller]/{list?}/current-page/{itemPage:int?}", Name = "ProductList")]
        [ActionName("Products")]
        public async Task<ActionResult<ProductViewModel>> ProductsIndex(string? list,string? filter, string? search, int? itemPage = 1)
        {

            ViewData["Filter"] = filter;
            ViewData["Search"] = search;
            ViewData["PageNumber"] = itemPage;
            ViewData["SelectedNav"] = list;
            var productList = await _mediator.Send(new ProductListQuery(itemPage.GetValueOrDefault(),new ProductViewModel { 
                
               ProductFilter = new ProductFilterVM { 
                    
                    SelectedFilter = filter,
                    SearchPhrase = search
                }
            }));
              
                ViewData["ButtonPages"] = productList.TotalPage;         

            return View("Products",productList);
         
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
       // [Authorize]
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
         
            return RedirectToAction("Products", "ProductCatalog", new { list = "list", productPage = 1 });
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
            return RedirectToAction("Products", "ProductCatalog", new { list = "list", productPage = 1 }); ;
       
        }

        [HttpPost("remove-item/{id:int}/{productViewModel?}")]
        [ActionName("RemoveProduct")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, ProductViewModel productViewModel)
        {
            try
            {
                await _mediator.Send(new RemoveProduct(productViewModel.ProductId.GetValueOrDefault(), productViewModel));

            }
            catch (Exception e){
                
                if (e.Message != null) {
                    return View("Error", id);
                }
                
            }
           
         
         
            return RedirectToAction("Products", "ProductCatalog", new { list = "list", itemPage = 1 });
        }

      
        private bool ProductExists(int id)
        {
            //return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
            return true;
        }
    }
}
