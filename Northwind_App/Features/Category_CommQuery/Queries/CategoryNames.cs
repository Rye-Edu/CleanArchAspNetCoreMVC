using AutoMapper;
using MediatR;
using Northwind_App.ViewModels.CategoriesVM;
using Northwind_App.Interfaces.IRepositories;

namespace Northwind_App.Features.Category_CommQuery.Queries
{
    public class CategoryNames:IRequest<IEnumerable<CategoryNames>>
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }

    public class CategoryNameHandler : IRequestHandler<CategoryNames, IEnumerable<CategoryNames>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryNameHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryNames>> Handle(CategoryNames request, CancellationToken cancellationToken)
        {
            var categoryName = _mapper.Map<IEnumerable<CategoryViewModel>>(await _categoryRepository.CategoryNames());

            List<CategoryNames> categories = new List<CategoryNames>();
            foreach (var item in categoryName)
            {
                categories.Add(new CategoryNames { CategoryID = item.CategoryId, CategoryName = item.CategoryName});
            }
            return categories;
        }
    }
}
