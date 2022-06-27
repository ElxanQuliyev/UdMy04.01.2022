using Business.Abstract;
using DataAccess.Abstract;
using Entites;
using Entites.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CourseSpecifactioManager : ICourseManager
    {
        private readonly ICourseDal _coursedal;

        public CourseSpecifactioManager(ICourseDal coursedal)
        {
            _coursedal = coursedal;
        }

        public void Add(Course dtoCourse)
        {
            _coursedal.AddCourseWithLesson(dtoCourse);
        }

        public List<Course> GetAll()
        {
            return _coursedal.ListCourses();
        }

        public async Task<Course> GetById(int id)
        {
            return await _coursedal.GetCourse(id);
        }

        public List<Course> GetCoursesByCategory(int categoryId)
        {
           return _coursedal.GetCourseByCategory(categoryId);
        }

        public Task<List<Course>>? GetCourseWithFilter(FilterCourseItem item)
        {
            return _coursedal.FilterCourse(item);
        }

        public void Remove(int id)
        {
            var getCourse= _coursedal.Get(c => c.Id == id && !c.IsDeleted);
            if (getCourse != null)
            {
                getCourse.IsDeleted = true;
                _coursedal.Update(getCourse);
            }
        }

        public async Task Update(int id, Course course)
        {
            await _coursedal.UpdateCourse(id, course);   
        }
    }
}
