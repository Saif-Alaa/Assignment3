using AutoMapper;
using Library.Dtos;
using Library.Models;
using Library.Work;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class BookController : ControllerBase
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapper mapper;

        public BookController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public IActionResult Add(BookCreateDtos bookCreateDtos)
        {
            var checkcategory = unitOfWork.categorys.GetById(bookCreateDtos.CategoryId);

            if (checkcategory == null) return NotFound("Category Not Found");

            var chechisbn = unitOfWork.books.Getall()
                .FirstOrDefault(f => f.ISBN == bookCreateDtos.ISBN);

            if (chechisbn != null) return BadRequest("ISBN Must br unique");

            if (bookCreateDtos.Price <= 0) return BadRequest("Price Must More than 0");
            var r = mapper.Map<Book>(bookCreateDtos);

            unitOfWork.books.Create(r);
            unitOfWork.Save();

            return Created();

        }

        [HttpGet("search")]
        public IActionResult Search(string s) 
        {
            if (string.IsNullOrEmpty(s)) return BadRequest("Write Any Thing To search");

            s = s.ToLower();

            var data = unitOfWork.books.Getall()
                .Where(u => u.Title.ToLower().Contains(s)
                || u.Author.ToLower().Contains(s))
                .OrderBy(t => t.Title)
                .Select(e => new
                {
                    Id = e.Id,
                    Title = e.Title,
                    Author = e.Author,
                    Price = e.Price
                }).ToList();


            return Ok(data);
        }


        [HttpGet("{price:decimal}")]
        public IActionResult GetHighestPrice(decimal price)
        {


            var chechisbn = unitOfWork.books.Getall()
               .OrderBy(o => o.Price)
               .FirstOrDefault() ;

       

            var r = mapper.Map<BookDtos>(chechisbn);

            return Ok(r);


        }
    }
}
