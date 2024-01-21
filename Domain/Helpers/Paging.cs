using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers;
   public class Paging<T>
    {
        public T DataReturn { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public Paging(T dataReturn, int totalpages, int currentPage, int pageSize, int totalItems)
        {
            DataReturn = dataReturn;
            TotalPages = totalpages;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalItems = totalItems;
        }
    }
