using AutoMapper;
using MediatR;
using Northwind_App.ViewModels.CategoriesVM;
using Northwind_App.Interfaces.IRepositories;

namespace Northwind_App.Features.Category_CommQuery.Queries
{
    public class CategorySelect:IRequest<IEnumerable<CategorySelectVM>>
    {
       
    }

    //public class CategorySelectCommandHandler : IRequestHandler<CategorySelect, IEnumerable<CategorySelectVM>>
    //{
    //    private readonly ICategoryRepository _categoryRepository;
    //    private readonly IMapper _mapper;

    //    public CategorySelectCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
    //    {
    //        _categoryRepository = categoryRepository;
    //        _mapper = mapper;
    //    }
    //    public Task<IEnumerable<CategorySelectVM>> Handle(CategorySelect request, CancellationToken cancellationToken)
    //    {
    //        var categories = _categoryRepository.GetListEntitiesAsync();
    //        var categorySelect = _mapper.Map<IEnumerable<CategorySelectVM>>(categories);

    //        var selection = new CategorySelectVM
    //        {
    //            CategoryItem = new List {
    //                new SelectList { 
    //                    DataTextField = 
    //                }
    //            }
    //        };
    //    }
    //}
}
