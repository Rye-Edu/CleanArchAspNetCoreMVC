using MediatR;
using Northwind_App.ViewModels.SupplierVM;

namespace Northwind_App.Supplier_CommQuery.Queries
{
    public class SupplierByID:IRequest<SupplierViewModel>
    {
    }
    public class SupplierByIDCommandHandler : IRequestHandler<SupplierByID, SupplierViewModel>
    {
        public Task<SupplierViewModel> Handle(SupplierByID request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
