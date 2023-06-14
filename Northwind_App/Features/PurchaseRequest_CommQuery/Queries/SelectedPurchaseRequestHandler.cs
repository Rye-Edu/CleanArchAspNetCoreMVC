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
    public class SelectedPurchasRequestCommand : IRequest<PurchaseRequestDetailVM> {

        public int RequestID { get; }
        public SelectedPurchasRequestCommand(int requestID)
        {
            RequestID = requestID;
        }

       
    }
    public class SelectedPurchaseRequestHandler : IRequestHandler<SelectedPurchasRequestCommand, PurchaseRequestDetailVM>
    {
        private readonly IMapper _mapper;
        private readonly IPurchaseRequestRepository _purchaseRequestRepository;

        public SelectedPurchaseRequestHandler(IMapper mapper, IPurchaseRequestRepository purchaseRequestRepository)
        {
            _mapper = mapper;
            _purchaseRequestRepository = purchaseRequestRepository;
        }
        public async Task<PurchaseRequestDetailVM> Handle(SelectedPurchasRequestCommand request, CancellationToken cancellationToken)
        {
            var reuestDetail = new PurchaseRequestDetailVM();
            if (request.RequestID != 0) {
                //var purchaseRequest = await _purchaseRequestRepository.GetByIDAsync(request.RequestID);
                var purchaseRequest = await _purchaseRequestRepository.GetSelectedPurchaseRequest(request.RequestID);
                if (purchaseRequest != null)
                {
                    reuestDetail = _mapper.Map<PurchaseRequestDetailVM>(purchaseRequest);
                }
            }
            return reuestDetail;
        }
    }
}
