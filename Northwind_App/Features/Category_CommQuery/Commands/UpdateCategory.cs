using MediatR;
using Northwind_App.ViewModels.CategoriesVM;

namespace Northwind_App.Features.Category_CommQuery.Commands
{
    public class UpdateCategory:IRequest<CategoryViewModel>
    {
    }
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategory, CategoryViewModel>
    {
        public Task<CategoryViewModel> Handle(UpdateCategory request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
