using AutoMapper;
using Northwind_App.ViewModels.CategoriesVM;
using Northwind_App.ViewModels.ProductVM;
using Northwind_App.ViewModels.PurchaseVM;
using Northwind_App.ViewModels.SupplierVM;
using Northwind_Core.Domain.Entities;

namespace Northwind_App.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();  
            CreateMap<Supplier, SupplierViewModel>().ReverseMap();
            CreateMap<Category, CategorySelectVM>().ReverseMap();
            CreateMap<PurchaseRequest, PurchaseRequestDetailVM>().ReverseMap();
            CreateMap<StorePurchase, ApprovePurchaseRequestVM>().ReverseMap();
            CreateMap<StorePurchase, StorePurchaseVM>().ReverseMap();
        }
    }
}
