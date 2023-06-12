using Northwind_Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind_App.Interfaces.IRepositories
{
    public interface IPurchaseRequestRepository: IAsyncBaseRepository<PurchaseRequest>
    {
        public Task<IEnumerable<PurchaseRequest>> GetPurchaseRequestList();
    }
}
