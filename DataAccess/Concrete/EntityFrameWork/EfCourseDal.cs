using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entites;
using Entites.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFrameWork
{
    public class EfCourseDal : EFEntityRepositoryBase<UdMyDbContext, Course>, ICourseDal
    {
        public void AddCourseWithLesson(Course course)
        {
            using var context = new UdMyDbContext();
            context.Courses.Add(course);
            context.SaveChanges();
        }

        public async Task<Course> GetCourse(int id)
        {
            using UdMyDbContext context = new();
            var singleCourse =await  context.Courses
                .Include(c => c.Category)
                .Include(c => c.CourseSpecifactions)
                .ThenInclude(c => c.Specifaction)
                .Include(cs => cs.Instructor)
                .Include(c => c.Lessons)
                .ThenInclude(c=>c.LessonVideos)
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
            return singleCourse;
        }

        public List<Course> GetCourseByCategory(int categoryId)
        {
            using UdMyDbContext context = new();
            return context.Courses
                .Where(c => !c.IsDeleted && (c.CategoryId == categoryId || c.Category.ParentCategoryId==categoryId))
                .Include(c => c.Category.ParentCategory)
                //.ThenInclude(c=>c.ParentCategory)
                .Include(c => c.Instructor)
                .ToList();
        }

        public List<Course> ListCourses()
        {
            using UdMyDbContext context = new();
              var myCourses= context.Courses
                .Include(c => c.Instructor)
                .Include(c => c.Category)
                .Include(c => c.Lessons)
                .Include(c => c.CourseSpecifactions)
                .ThenInclude(c=>c.Specifaction)
                .Where(c=>!c.IsDeleted)
                .ToList();
            return myCourses;

        }

        public async Task<List<Course>> FilterCourse(FilterCourseItem item)
        {
            using UdMyDbContext context = new();
            var myCourses =  context.Courses
                .Include(c => c.Instructor)
                .Include(c => c.Category)
                .Where(c=>!c.IsDeleted)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(item.Q) && item.Q != null)
            {
                myCourses = myCourses.Where(c => c.Name.Contains(item.Q)
             || c.Category.Name.Contains(item.Q)
             || c.Instructor.FullName.Contains(item.Q));
            }
          
            if(item.MinPrice.HasValue && item.MaxPrice.HasValue)
            {
                myCourses = myCourses.Where(c => c.Price >= item.MinPrice && c.Price <= item.MaxPrice);
            }
            if (item.Rating.HasValue)
            {
                myCourses = item.Rating.Value switch
                {
                    1 => myCourses.Where(c => c.Reyting >= 1 && c.Reyting < 2),
                    2 => myCourses.Where(c => c.Reyting >= 2 && c.Reyting < 3),
                    3 => myCourses.Where(c => c.Reyting >= 3 && c.Reyting < 4),
                    4 => myCourses.Where(c => c.Reyting >= 4 && c.Reyting < 5),
                    5 => myCourses.Where(c => c.Reyting==5),
                    _ => myCourses.Where(c => c.Reyting >= 1 && c.Reyting <= 5),
                };
            }

            if ( item.InstructorIds.Count> 0)
            {
                myCourses = myCourses.Where(c => item.InstructorIds.Contains(c.InstructorId));
            }

            if (item.SortBy.HasValue)
            {
                myCourses = item.SortBy.Value switch
                {
                    0 => myCourses.OrderByDescending(c => c.Price),
                    1 => myCourses.OrderBy(c => c.Price),
                    _ => myCourses.OrderBy(c => c.PublishDate),
                };
            }

            return await myCourses.ToListAsync();
        }
        public async void UpdateCourse(int id, Course course)
        {
            using UdMyDbContext context = new();
            course.Id = id;
            var singleCourse = await GetCourse(id);
            
            context.RemoveRange(singleCourse.CourseSpecifactions);
            //context.Specifactions.RemoveRange(singleCourse.CourseSpecifactions.Where(c=>c.CourseId==id).ToArray());
            context.Courses.Update(course);
            context.SaveChanges();
        }
    }
}
