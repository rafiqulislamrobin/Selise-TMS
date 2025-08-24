using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Application.features.Task.Get
{
    public class ResourseParameter
    {
        private int _pageNumber;
        private int _pageSize;

        public int PageNumber
        {
            get => _pageNumber > 0 ? _pageNumber : 1;
            set => _pageNumber = value;
        }

        public int PageSize
        {
            get => _pageSize > 0 ? _pageSize : 10;
            set => _pageSize = value;
        }

        public string OrderBy { get; set; } = "Title";
        public string? SearchQuery { get; set; }
    }
}
