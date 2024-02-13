using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Bll.Dtos
{
    public class Pagination<T>(int pageIndex, int pageSize, int count, IEnumerable<T> data) where T : class
    {
        public int PageIndex { get; set; } = pageIndex;
        public int PageSize { get; set; } = pageSize;
        public int Count { get; set; } = count;
        public IEnumerable<T>? Data { get; set; } = data;
    }
}
