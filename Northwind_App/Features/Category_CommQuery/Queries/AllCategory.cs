using AutoMapper;
using MediatR;
using Northwind_App.ViewModels.CategoriesVM;
using Northwind_App.Interfaces.IRepositories;

namespace Northwind_App.Features.Category_CommQuery.Queries
{
    public class AllCategory:IRequest<IEnumerable<CategoryViewModel>>
    {
    }

    public class AllCategoryCommandHandler : IRequestHandler<AllCategory, IEnumerable<CategoryViewModel>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public AllCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CategoryViewModel>> Handle(AllCategory request, CancellationToken cancellationToken)
        {
            var categories =  await _categoryRepository.GetListEntitiesAsync();
            
            return Task.FromResult(_mapper.Map<IEnumerable<CategoryViewModel>>(categories)).Result;
        }
    }
}
