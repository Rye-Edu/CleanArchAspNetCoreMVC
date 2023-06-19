using AutoMapper;
using MediatR;
using Northwind_App.Interfaces.Common;
using Northwind_App.ViewModels.PurchaseVM;
using Northwind_Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind_App.Features.PurchaseRequest_CommQuery.Queries
{
    public class ApprovedRequestQuery: IRequest<StorePurchaseVM> {


        public int ItemPage { get; set; }
        public int TotalPage { get; set; }

        public ApprovedRequestQuery(int itemPage)
        {
            ItemPage = itemPage;
        }
    }
    public class ApprovedRequestQueryHandler:IRequestHandler<ApprovedRequestQuery, StorePurchaseVM >
    {
        private readonly IMapper _mapper;
        private readonly IStorePurchaseRepository _storePurchaseRepository;
        private readonly IPaging<StorePurchaseVM> _approvedRequests;

        public ApprovedRequestQueryHandler(IMapper mapper, IStorePurchaseRepository storePurchaseRepository, IPaging<StorePurchaseVM> approvedRequests)
        {
            _mapper = mapper;
            _storePurchaseRepository = storePurchaseRepository;
            _approvedRequests = approvedRequests;
        }

        public async Task<StorePurchaseVM> Handle(ApprovedRequestQuery request, CancellationToken cancellationToken)
        {
            var approve = new StorePurchaseVM();
            var purchases = await _storePurchaseRepository.GetStorePurchases();

            var purchaseList = _mapper.Map<IEnumerable<StorePurchaseVM>>(purchases);
            var pageItems = _approvedRequests.PaginatedItems(request.ItemPage, purchaseList.ToList());
            approve.ApprovedList = pageItems!.ToList();
            approve.TotalPage = _approvedRequests.TotalPage();

            return Task.FromResult(approve).Result;
        }
    }
}
