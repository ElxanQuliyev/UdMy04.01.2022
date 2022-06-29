using AutoMapper;
using Entites.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.MyProfilers
{
    public class CategoryParentProfile : Profile
    {

        public CategoryParentProfile()
        {
            CreateMap<Category, CategoryWithParentDTO>()
                .ForMember(
                    dest => dest.CategoryId,
                    opt => opt.MapFrom(src => src.Id)
                    )
                .ForMember(
                    dest => dest.ParentId,
                    opt => opt.MapFrom(src => src.ParentCategoryId)
                    )
                .ForMember(
                    dest => dest.ParentName,
                    opt => opt.MapFrom(src => src.ParentCategory.Name)
                    );
        }
    }
}
