//using Microsoft.CodeAnalysis.CSharp.Syntax;
using Northwind_App.Interfaces.IRepositories;

namespace Northwind_App.Interfaces.Common
{
    public interface IPaging<TEntity>
    {
        IList<int>? TotalPage();
        IList<TEntity>? PaginatedItems(int page, List<TEntity> list);

        bool IsFilteredPage(ref int page, List<string> filters);
        
        

    }
}
