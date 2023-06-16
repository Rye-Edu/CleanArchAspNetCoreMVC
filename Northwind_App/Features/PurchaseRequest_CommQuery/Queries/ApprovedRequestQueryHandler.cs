using AutoMapper;
using MediatR;
using Northwind_App.ViewModels.PurchaseVM;
using Northwind_Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind_App.Features.PurchaseRequest_CommQuery.Queries
{
    public class ApprovedRequestQuery: IRequest<IEnumerable<StorePurchaseVM>> { 

    
    }
    public class ApprovedRequestQueryHandler:IRequestHandler<ApprovedRequestQuery, IEnumerable<StorePurchaseVM> >
    {
        private readonly IMapper _mapper;
        private readonly IStorePurchaseRepository _storePurchaseRepository;

        public ApprovedRequestQueryHandler(IMapper mapper, IStorePurchaseRepository storePurchaseRepository)
        {
            _mapper = mapper;
            _storePurchaseRepository = storePurchaseRepository;
        }

        public async Task<IEnumerable<StorePurchaseVM>> Handle(ApprovedRequestQuery request, CancellationToken cancellationToken)
        {
            var purchases = await _storePurchaseRepository.GetStorePurchases();

            var purchaseList = _mapper.Map<IEnumerable<StorePurchaseVM>>(purchases);
            var s = "asdf";

            return purchaseList;
        }
    }
}
