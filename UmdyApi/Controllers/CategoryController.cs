using AutoMapper;
using Business.Abstract;
using Entites;
using Entites.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UdmyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryManager categoryManager, IMapper mapper)
        {
            _categoryManager = categoryManager;
            _mapper = mapper;
        }

        [HttpGet("getall")]
        public List<CategoryWithChildernDTO> GetCategories()
        {
            return _categoryManager.GetAll();
        }

        [HttpGet("with-parent")]
        public async Task<List<CategoryWithParentDTO>> GetWithParent()
        {
            var categoryList = await _categoryManager.GetCategoryWithParent();
            var categoryMapper = _mapper.Map<List<CategoryWithParentDTO>>(categoryList);
            return categoryMapper;
        }
        [HttpGet("getchildrens/{parentId}")]
        public List<CategoryListDTO>? GetChildrens(int? parentId)
        {
            if (parentId == null) return null;
            return _categoryManager.GetChildrenByParentId(parentId.Value);
        }
        [HttpPost("Add")]
        public JsonResult Add([FromBody]CategoryDTO category)
        {
            JsonResult res = new(new { });
            try
            {
                category.ParentCategoryId = category.ParentCategoryId == 0 ? null : category.ParentCategoryId;
                _categoryManager.Add(category);
                res.Value = new { status = 201, message = "category created successfully" };
                return res;
            }
            catch (Exception)
            {
                res.Value = new { status = 403, message = "Some problems when category created" };
                return res;
            }
        } 
    }
}
