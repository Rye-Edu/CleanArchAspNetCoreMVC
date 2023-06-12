using AutoMapper;
using MediatR;
using Northwind_App.Interfaces.IRepositories;
using Northwind_App.ViewModels.PurchaseVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind_App.Features.PurchaseRequest_CommQuery.Commands
{
    public class ApprovePurchaseRequestCommand : IRequest<ApprovePurchaseRequestVM> {

        public int PurchaseRequestID { get; set; }
        public ApprovePurchaseRequestVM? ApproveRequestVM { get; set; }
        public PurchaseRequestDetailVM? RequestDetailVM { get; set; }
        public ApprovePurchaseRequestCommand(int requestID, PurchaseRequestDetailVM purchaseRequestDetailVM)
        {
            PurchaseRequestID = requestID;
            RequestDetailVM = purchaseRequestDetailVM;

        }
    }
    public class ApprovePurchaseRequestHandler : IRequestHandler<ApprovePurchaseRequestCommand, ApprovePurchaseRequestVM>
    {
        private readonly IMapper _mapper;
        private readonly IPurchaseRequestRepository _purchaseRequestRepository;

        public ApprovePurchaseRequestHandler(IMapper mapper, IPurchaseRequestRepository purchaseRequestRepository)
        {
            _mapper = mapper;
            _purchaseRequestRepository = purchaseRequestRepository;
        }
        public async Task<ApprovePurchaseRequestVM> Handle(ApprovePurchaseRequestCommand request, CancellationToken cancellationToken)
        {
            if (request != null) {
                var x = await _purchaseRequestRepository.GetByIDAsync(request.PurchaseRequestID);
            }
            return Task.FromResult(new ApprovePurchaseRequestVM()).Result;
        }
    }

}
