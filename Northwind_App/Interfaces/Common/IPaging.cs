//using Microsoft.CodeAnalysis.CSharp.Syntax;
using Northwind_App.Interfaces.IRepositories;

namespace Northwind_App.Interfaces.Common
{
    public interface IPaging<TEntity>
    {
        IList<int>? TotalPage();
        IList<TEntity>? PaginatedItems(int page, List<TEntity> list);


    }
}
