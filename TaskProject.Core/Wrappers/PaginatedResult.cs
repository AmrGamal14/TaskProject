using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskProject.Core.Wrappers
{
    public class PaginatedResult<T>
    {
        public  PaginatedResult(List<T> data)
            {
            Data=data;

            }
        public List<T> Data { get; set; }
        internal PaginatedResult(bool succeeded , List<T> data = default, List<string>message=null , int count=0 , int Page =1 , int PageSize=10)
        {
            Data=data;
            CurrentPage=Page;
            Succeeded=succeeded;
            Pagesize=PageSize;
            TotalPages=(int)Math.Ceiling(count/(double)PageSize);
            Totalcount=count;
        }        
        public static PaginatedResult<T> success(List<T>data, int count ,int Page, int PageSize)
        {
            return new(true, data, null, count, Page, PageSize);
        }
        public int CurrentPage { get; set; }    
        public int TotalPages { get; set; }
        public int Totalcount { get; set; }
        public object Meta { get; set; }
        public int Pagesize { get; set; }
        public bool HasPreviousPage => CurrentPage>1;
        public bool HasNextPage => CurrentPage <TotalPages;
        public List<string> Messages { get; set; } = new();
        public bool Succeeded { get; set; }


    }
}
