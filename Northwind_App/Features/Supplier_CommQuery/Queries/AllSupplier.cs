using AutoMapper;
using MediatR;
using Northwind_App.ViewModels.SupplierVM;
using Northwind_App.Interfaces.IRepositories;

namespace Northwind_App.Supplier_CommQuery.Queries
{
    public class AllSupplier:IRequest<IEnumerable<SupplierViewModel>>
    {
    }

    public class AllSupplierCommandHandler : IRequestHandler<AllSupplier, IEnumerable<SupplierViewModel>>
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public AllSupplierCommandHandler(ISupplierRepository supplierRepository, IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SupplierViewModel>> Handle(AllSupplier request, CancellationToken cancellationToken)
        {
            var suppliers = await _supplierRepository.GetListEntitiesAsync();

            return Task.FromResult(_mapper.Map<IEnumerable<SupplierViewModel>>(suppliers)).Result;
        }
    }
}
