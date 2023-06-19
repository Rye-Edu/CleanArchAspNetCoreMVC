using Microsoft.EntityFrameworkCore;
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

        public async Task<IQueryable<StorePurchase>> GetStorePurchases()
        {

            var approvedPurchases = await (from storePurchases in _northwndContext.StorePurchases
                                    join purchaseRequest in _northwndContext.PurchaseRequests on storePurchases.PurchaseRequestId equals purchaseRequest.RequestId
                                    join requester in _northwndContext.Users on purchaseRequest.UserId equals requester.UserId
                                    join empDetail in _northwndContext.Employees on requester.EmployeeId equals empDetail.EmployeeId
                                    join product in _northwndContext.Products on purchaseRequest.ProductID equals product.ProductId
                                    join approver in _northwndContext.Users on storePurchases.UserApproverId equals approver.UserId
                                    join empApprover in _northwndContext.Employees on approver.EmployeeId equals empApprover.EmployeeId

                                    select new StorePurchase
                                    {
                                        PurchaseId = storePurchases.PurchaseId,
                                        PurchaseRequest = new PurchaseRequest
                                        {
                                            DateRequested = purchaseRequest.DateRequested,
                                           
                                            Product = new Product
                                            {
                                                ProductId = product.ProductId,
                                                ProductName = product.ProductName,
                                            },
                                            QuantityRequested = purchaseRequest.QuantityRequested,
                                            UserId = requester.UserId,
                                            User = new User
                                            {
                                                Employee = new Employee
                                                {
                                                    FirstName = empDetail.FirstName,
                                                    LastName = empDetail.LastName
                                                }
                                            }

                                        },
                                        DateApproved = storePurchases.DateApproved,
                                        ApprovedQuantity = storePurchases.ApprovedQuantity,
                                        UserApproverId = storePurchases.UserApproverId,
                                        UserApprover = new User
                                        {

                                            Employee = new Employee
                                            {
                                                FirstName = empApprover.FirstName,
                                                LastName = empApprover.LastName,
                                            }
                                        },
                                        TotalAmount = storePurchases.TotalAmount
                                    }).ToListAsync();
                
            return Task.FromResult(approvedPurchases.AsQueryable()).Result;

        }
    }
}
