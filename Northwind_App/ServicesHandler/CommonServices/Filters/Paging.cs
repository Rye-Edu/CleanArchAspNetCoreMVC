//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.CodeAnalysis.CSharp.Syntax;
using Northwind_App.Interfaces.Common;

namespace Northwind_App.ServicesHandler.CommonServices.Filters
{
    public class Paging<TEntity> : IPaging<TEntity> where TEntity : class
    {
        public  IEnumerable<TEntity>? PaginatedList { get; set; } =null!;
        public  IList<int>? Pages { get;} = new List<int>();
        public static int PageIndex { get; set; }
        public static int CurrentPage { get; set; }
        public int PageItems { get; set; } = 10;
        public int TotalItems { get; private set; }

        public  IList<int>? TotalPage (){

            int pageNumber = (int)Math.Ceiling((decimal) TotalItems / PageItems);
         
            if (TotalItems < PageItems)
            {
                Pages?.Add(1);
            }
            else {
                for (int i = 1; i <= pageNumber; i++)
                {
                    Pages?.Add(i);
                }
            }
          
            return Pages?.ToList();
        }

        public IList<TEntity>? PaginatedItems(int page, List<TEntity> list) {

            TotalItems = list.Count();
            //  TEntity? result = null;
            Paging<TEntity> pageList = new();
            //pageList.PaginatedList = new List<TEntity>();
            pageList.PaginatedList = list;
            var itemsPerPage = pageList.PaginatedList?.Skip((page - 1) * pageList.PageItems).Take(pageList.PageItems);
            
            return itemsPerPage?.ToList();
        
        }
      
    }
}
