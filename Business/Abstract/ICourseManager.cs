using Entites;
using Entites.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICourseManager
    {
        List<Course> GetAll();
        Task<List<Course>>? GetCourseWithFilter(FilterCourseItem item);
        Task<Course> GetById(int id);
        List<Course> GetCoursesByCategory(int categoryId);
        void Add(Course course);
        void Remove(int id);
        Task Update(int id, Course course);
    }
}
