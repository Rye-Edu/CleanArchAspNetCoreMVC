//using Microsoft.CodeAnalysis.CSharp.Syntax;
using Northwind_App.Interfaces.IRepositories;

namespace Northwind_App.Interfaces.Common
{
    public interface IPaging<TEntity,T> where T: IEnumerable<T>
    {
        IList<int>? TotalPage();
        IList<T>? PaginatedItems(int page, List<T> list);


    }
}
