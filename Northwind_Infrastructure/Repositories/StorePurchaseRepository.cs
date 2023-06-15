using Northwind_Core.Domain.Entities;
using Northwind_Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind_Infrastructure.Repositories
{
    public class StorePurchaseRepository : AsyncBaseRepository<StorePurchase>, IStorePurchaseRepository
    {
        private readonly NorthwindContext _northwndContext;

        public StorePurchaseRepository(NorthwindContext northwndContext) : base(northwndContext)
        {
            _northwndContext = northwndContext;
        }

    }
}
