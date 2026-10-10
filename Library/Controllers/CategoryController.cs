using AutoMapper;
using Library.Dtos;
using Library.Models;
using Library.Work;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapper mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        [HttpPost]
        public IActionResult Add(CategoryCreateDto dto) 
        {
            var c = unitOfWork.categorys.Getall()
                .Where(w => w.Name == dto.Name);

            if (c != null) return BadRequest("Name Must Is unique");

            var r = mapper.Map<Category>(dto);
            unitOfWork.categorys.Create(r);
            unitOfWork.Save();
                
                return Created();
        }


        [HttpGet("CategoryWithCountBooks")]
        public IActionResult GetAllcategory() 
        {
            var data = unitOfWork.categorys
                .Categorywithcount();

            return Ok(data);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var data = unitOfWork.categorys.GetById(id);

            if (data == null) return NotFound();


            unitOfWork.categorys.Delete(id);
            unitOfWork.Save();
            return NoContent();
        }
    }
}
