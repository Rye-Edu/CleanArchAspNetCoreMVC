using AutoMapper;
using MediatR;
using Northwind_App.ViewModels.SupplierVM;
using Northwind_App.Interfaces.IRepositories;
using System.Collections.Generic;

namespace Northwind_App.Supplier_CommQuery.Queries
{
    public class SupplierNames:IRequest<IEnumerable<SupplierNames>>
    {
        public int SupplierID { get; set; }
        public string? CompanyName { get; set; }

 
        //public SupplierNames(SupplierViewModel supplierViewModel)
        //{
        //    SupplierID = supplierViewModel.SupplierId;
        //    CompanyName = supplierViewModel.CompanyName;
        //}
    }

    public class SupplierNameHandler : IRequestHandler<SupplierNames, IEnumerable<SupplierNames>>
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;

        public SupplierNameHandler(ISupplierRepository supplierRepository, IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SupplierNames>> Handle(SupplierNames request, CancellationToken cancellationToken)
        {
            // var suppliers = await Task.FromResult(_mapper.Map<IEnumerable<SupplierViewModel>>(_supplierRepository.GetListEntitiesAsync()));
           // var suppliers = await _supplierRepository.GetListEntitiesAsync();
           var suppliers = await _supplierRepository.SupplierNamesAndID();
            var x = _mapper.Map<IEnumerable<SupplierViewModel>>(suppliers);
            List<SupplierNames> supplierNames = new List<SupplierNames>();
            foreach (var item in x)
            {
                supplierNames.Add(new SupplierNames { SupplierID = item.SupplierId, CompanyName = item.CompanyName});
            }
            
            return supplierNames;
        }
    }
}
