using Microsoft.EntityFrameworkCore;
using Northwind_App.Interfaces.IRepositories;
using Northwind_Core.Domain.Entities;
using Northwind_Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind_Infrastructure.Repositories
{
    public class PurchaseRequestRepository : AsyncBaseRepository<PurchaseRequest>, IPurchaseRequestRepository
    {
        private readonly NorthwindContext _northwndContext;

        public PurchaseRequestRepository(NorthwindContext northwndContext) : base(northwndContext)
        {
            _northwndContext = northwndContext;
        }

        public async Task<IEnumerable<PurchaseRequest>> GetPurchaseRequestList()
        {
            var requestList = await _northwndContext.PurchaseRequests.Include(product => product.Product)
                .Include(user => user.User).ThenInclude(emp => emp.Employee).ToListAsync();

            return requestList;
        }
    }
}
