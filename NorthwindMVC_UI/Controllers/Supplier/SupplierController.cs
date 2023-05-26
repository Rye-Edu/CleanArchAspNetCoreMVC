using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northwind_App.Supplier_CommQuery.Queries;
using System.Text.Json.Serialization;

namespace Product_CoreDomain.Controllers
{
    public class SupplierController : Controller
    {
        private readonly IMediator _mediatr;

        public SupplierController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }
        // GET: SupplierController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SupplierController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SupplierController/Create
        public ActionResult Create()
        {
            return View();
        }

     
        // POST: SupplierController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SupplierController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SupplierController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SupplierController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SupplierController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetSupplierNames()
        {
            //ViewData["Suppliers"] = new SelectList(await _mediator.Send(new AllSupplier()), "SupplierId", "CompanyName", "SupplierId");
            //var p = await _mediator.Send(new AllSupplier());
            //ProductViewModel productViewModel = new ProductViewModel();
            var suppliers = await _mediatr.Send(new SupplierNames());
            var x = suppliers;
            // return View("_SearchOptionDropdown", productViewModel);
            return Json(suppliers);
        }
    }
}
