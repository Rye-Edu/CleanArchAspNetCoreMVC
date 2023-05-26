using MediatR;
using Northwind_App.ViewModels.CategoriesVM;

namespace Northwind_App.Features.Category_CommQuery.Commands
{
    public class CreateCategory:IRequest<CategoryViewModel>
    {
    }

    public class CreateCategoryCommandHadler : IRequestHandler<CreateCategory, CategoryViewModel>
    {
        public Task<CategoryViewModel> Handle(CreateCategory request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
