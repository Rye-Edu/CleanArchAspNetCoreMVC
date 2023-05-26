using AutoMapper;
using MediatR;
using Northwind_App.ViewModels.CategoriesVM;
using Northwind_App.Interfaces.IRepositories;


namespace Northwind_App.Features.Category_CommQuery.Queries
{
    public class CategoryByID:IRequest<CategoryViewModel>
    {
        public int? CategoryId { get; set; }
        public CategoryByID(int categoryId)
        {
            CategoryId = categoryId;
        }
    }
    public class CategoryByIDCommandHandler : IRequestHandler<CategoryByID, CategoryViewModel>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryByIDCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public  Task<CategoryViewModel> Handle(CategoryByID request, CancellationToken cancellationToken)
        {
            CategoryViewModel? categoryValue= null;

            if (request.CategoryId.HasValue) { 
                var category = _categoryRepository.GetByIDAsync(request.CategoryId.Value);
                if (category != null)
                {
                    var categoryItem = _mapper.Map<CategoryViewModel>(category);
                    categoryValue = categoryItem;
                }
            }
            return Task.FromResult(categoryValue);
        }
    }
}
