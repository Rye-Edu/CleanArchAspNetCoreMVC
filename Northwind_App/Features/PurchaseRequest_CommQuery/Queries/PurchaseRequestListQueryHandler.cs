using AutoMapper;
using MediatR;
using Northwind_App.Interfaces.IRepositories;
using Northwind_App.ViewModels.PurchaseVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind_App.Features.PurchaseRequest_CommQuery.Queries
{
    public class PurchaseRequestListQuery: IRequest<IEnumerable<PurchaseRequestDetailVM>> {

        public List<PurchaseRequestDetailVM> PurchaseRequests { get; set; } = new();
    }
    public class PurchaseRequestListQueryHandler : IRequestHandler<PurchaseRequestListQuery, IEnumerable<PurchaseRequestDetailVM>>
    {
        private readonly IMapper _mapper;
        private readonly IPurchaseRequestRepository _purchaseRequestRepository;

        public PurchaseRequestListQueryHandler(IMapper mapper, IPurchaseRequestRepository purchaseRequestRepository)
        {
            _mapper = mapper;
            _purchaseRequestRepository = purchaseRequestRepository;
        }
        public async Task<IEnumerable<PurchaseRequestDetailVM>> Handle(PurchaseRequestListQuery request, CancellationToken cancellationToken)
        {

            //already getting the needed data for purchase request
            var requestList = await _purchaseRequestRepository.GetPurchaseRequestList();
            var list = _mapper.Map<IEnumerable<PurchaseRequestDetailVM>>(requestList);

            return Task.FromResult(list).Result;
        }
    }
}
