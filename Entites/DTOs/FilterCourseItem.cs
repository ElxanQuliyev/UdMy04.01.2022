using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.DTOs
{
    public class FilterCourseItem
    {
        public FilterCourseItem()
        {
            InstructorIds = new List<int>();
        }
        public string? Q { get; set; }
        public decimal? Rating { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? SortBy { get; set; }
        public List<int>? InstructorIds { get; set; }
    }
}
