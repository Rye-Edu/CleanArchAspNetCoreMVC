using AutoMapper;
using MediatR;
using Northwind_App.Interfaces.IRepositories;
using Northwind_App.ViewModels.PurchaseVM;
using Northwind_Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind_App.Features.PurchaseRequest_CommQuery.Commands
{
    public class CreatePurchaseRequest : IRequest<PurchaseRequestDetailVM>
    {
        public PurchaseRequestDetailVM PurchaseRequestDetail { get; set; } = new();

        public CreatePurchaseRequest(PurchaseRequestDetailVM purchaseRequestDetailVM)
        {
            PurchaseRequestDetail = purchaseRequestDetailVM;
        }
    }

    public class CreatePurchaseRequestHandler : IRequestHandler<CreatePurchaseRequest, PurchaseRequestDetailVM>
    {
        private readonly IMapper _mapper;
        private readonly IPurchaseRequestRepository _purchaseRequestRepository;

        public CreatePurchaseRequestHandler(IMapper mapper, IPurchaseRequestRepository purchaseRequestRepository)
        {
            _mapper = mapper;
            _purchaseRequestRepository = purchaseRequestRepository;
        }
        public async Task<PurchaseRequestDetailVM> Handle(CreatePurchaseRequest request, CancellationToken cancellationToken)
        {
            var purchaseDetail = new PurchaseRequestDetailVM();
                var requestDetail = new PurchaseRequestDetailVM
                {

                    DateRequested = DateTime.Now,
                    UserId = 1,
                    ProductID = request!.PurchaseRequestDetail!.Product!.ProductId.GetValueOrDefault(),
                    QuantityRequested = request.PurchaseRequestDetail.QuantityRequested,
                    Status = RequestStatus.Requested.ToString(),
                };


                var purchaseRequest = _mapper.Map<PurchaseRequest>(requestDetail);

                var created = await _purchaseRequestRepository.AddEntityAsync(purchaseRequest);

                purchaseDetail = _mapper.Map<PurchaseRequestDetailVM>(created);

           return Task.FromResult(purchaseDetail).Result;

        
        }
    }
}
