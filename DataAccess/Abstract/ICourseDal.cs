using Core;
using Entites;
using Entites.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICourseDal :IEntityRepository<Course>
    {
        public void AddCourseWithLesson(Course courseDTOs);
        public List<Course> ListCourses();
        public Task<Course> GetCourse(int id);
        public Task<List<Course>>? FilterCourse(FilterCourseItem item);
        public Task UpdateCourse(int id,Course courseDTOs);
        public List<Course> GetCourseByCategory(int categoryId);
    }
}
