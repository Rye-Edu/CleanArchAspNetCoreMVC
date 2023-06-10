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
        public PurchaseRequestRepository(NorthwindContext northwndContext) : base(northwndContext)
        {
        }
    }
}
