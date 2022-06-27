using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.DTOs
{
    public class CourseListDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = null!;
        public string Summary { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public bool IsFeatured { get; set; }
        public decimal? Reyting { get; set; }
        public string? TrailerUrl { get; set; }
        public int? CategoryId { get; set; }
        public string  CategoryName { get; set; }
        public int InstructorId { get; set; }
        public string InstructorName { get; set; }
        public string? InstructorPhoto { get; set; }
        public DateTime PublishDate { get; set; }
        public List<LessonDTOs>? Lessons { get; set; }
        public List<SpecificationDTOs>? SpecificationList { get; set; }
    }
}
