﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Connections.Features;
using Northwind_App.Interfaces.IRepositories;
using Northwind_App.ViewModels.PurchaseVM;
using Northwind_Core.Domain.Entities;
using Northwind_Infrastructure.Repositories;
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
        private readonly IStorePurchaseRepository _storePurchaseRepository;
      //  private readonly IPurchaseRequestRepository _purchaseRequestRepository;

        public ApprovePurchaseRequestHandler(IMapper mapper, IStorePurchaseRepository storePurchaseRepository)
        {
            _mapper = mapper;
            _storePurchaseRepository = storePurchaseRepository;
          
        }
        public async Task<ApprovePurchaseRequestVM> Handle(ApprovePurchaseRequestCommand request, CancellationToken cancellationToken)
        {
           var requestDetail = new ApprovePurchaseRequestVM();
            if (request != null) {
               // var requestDetail = await _purchaseRequestRepository.GetByIDAsync(request.PurchaseRequestID);
                //var productPrice = await

                    request.ApproveRequestVM = new ApprovePurchaseRequestVM { 
                        
                        PurchaseRequestId = request!.RequestDetailVM!.RequestId.GetValueOrDefault(),
                        UserApproverId = 3,
                        ApprovedQuantity = request.RequestDetailVM.QuantityRequested,
                        DateApproved = DateTime.Now,
                        TotalAmount = request.RequestDetailVM.UnitPrice * request.RequestDetailVM.QuantityRequested,
                    };
                requestDetail = request.ApproveRequestVM;
                    await _storePurchaseRepository.AddEntityAsync(_mapper.Map<StorePurchase>(requestDetail));
                  //  request.ApproveRequestVM = _mapper.Map<StorePurchaseVM>();
                
            }
            return request!.ApproveRequestVM ?? new();
        }
    }

}
