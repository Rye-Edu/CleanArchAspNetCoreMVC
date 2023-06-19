using AutoMapper;
using MediatR;
using Northwind_App.Interfaces.Common;
using Northwind_App.Interfaces.IRepositories;
using Northwind_App.ViewModels.PurchaseVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind_App.Features.PurchaseRequest_CommQuery.Queries
{
    public class PurchaseRequestListQuery: IRequest<PurchaseRequestDetailVM> {

        public int ItemPage { get; set; }
        public int TotalPage { get; set; }
        //public List<PurchaseRequestDetailVM> PurchaseRequests { get; set; } = new();

     //   public PurchaseRequestDetailVM RequestDetailVM { get; set; } = new();
        public PurchaseRequestListQuery(int itemPage)
        {
            ItemPage = itemPage;
       

        }
    }
    public class PurchaseRequestListQueryHandler : IRequestHandler<PurchaseRequestListQuery, PurchaseRequestDetailVM>
    {
        private readonly IMapper _mapper;
        private readonly IPurchaseRequestRepository _purchaseRequestRepository;
        private readonly IPaging<PurchaseRequestDetailVM> _purchaseRequestDetails;

        public PurchaseRequestListQueryHandler(IMapper mapper, IPurchaseRequestRepository purchaseRequestRepository, IPaging<PurchaseRequestDetailVM> purchaseRequestDetails)
        {
            _mapper = mapper;
            _purchaseRequestRepository = purchaseRequestRepository;
            _purchaseRequestDetails = purchaseRequestDetails;
        }
        public async Task<PurchaseRequestDetailVM> Handle(PurchaseRequestListQuery request, CancellationToken cancellationToken)
        {

            //already getting the needed data for purchase request
            PurchaseRequestDetailVM requestDetailVM = new();
            var requestList = await _purchaseRequestRepository.GetPurchaseRequestList();
            var list = _mapper.Map<IEnumerable<PurchaseRequestDetailVM>>(requestList);

            var pagedItems = _purchaseRequestDetails.PaginatedItems(request.ItemPage,list.ToList());
             request.TotalPage = _purchaseRequestDetails.TotalPage();

            requestDetailVM.RequestList = pagedItems!.ToList();
            requestDetailVM.Pages = _purchaseRequestDetails.TotalPage();
            return Task.FromResult(requestDetailVM!).Result;
        }
    }
}
