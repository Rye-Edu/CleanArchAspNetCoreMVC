using Microsoft.EntityFrameworkCore;
using Northwind_App.Interfaces.IRepositories;
using Northwind_Core.Domain.Entities;
using Northwind_Infrastructure.Data;

namespace Northwind_Infrastructure.Repositories
{
    public class SupplierRepository : AsyncBaseRepository<Supplier>, ISupplierRepository
    {
        private readonly NorthwindContext northwndContext;

        public SupplierRepository(NorthwindContext northwndContext) : base(northwndContext)
        {
            this.northwndContext = northwndContext;
        }

        public async Task<IEnumerable<Supplier>> SupplierNamesAndID()
        {
            var suppliers = await  northwndContext.Suppliers.Select(supplier => new Supplier
            {
               SupplierId = supplier.SupplierId,
               CompanyName = supplier.CompanyName
            }).ToListAsync();
         //   List<Supplier> result = new List<Supplier>();
        
            //foreach (var supplier in suppliers)
            //{
            //    result.Add(new Supplier { SupplierId = supplier.supplierID, CompanyName = supplier.companyName});
            //}
            return Task.FromResult(suppliers).Result;
        }
    }
}
