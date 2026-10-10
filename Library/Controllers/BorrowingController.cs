using AutoMapper;
using Library.Dtos;
using Library.Models;
using Library.Work;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowingController : ControllerBase
    {
           protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapper mapper;

        public BorrowingController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult Add(BorrowingCreateDtos dtos) 
        {
            var cb = unitOfWork.books.GetById(dtos.BookId);

            if (cb.IsAvailable == false) return BadRequest("Book Isn't Available");
            var cm = unitOfWork.members.GetById(dtos.MemberId);

            if (cm  == null) return NotFound("Member Not Found");

            var r = mapper.Map<Borrowing>(dtos);

            unitOfWork.borrowings.Create(r);
            cb.IsAvailable = false;
            unitOfWork.Save();
            return Created();
        }

        [HttpGet("AllBorrowing")]
        public IActionResult GetAll() 
        {
            return Ok(unitOfWork.borrowings.BorrowingswithBookandmember());
              
        }

        [HttpGet("GetById")]
        public IActionResult Get(int id , DateTime dateTime) 
        {
            var data = unitOfWork.borrowings.GetById(id);
            if (data == null) return NotFound();
            data.ReturnDate = dateTime;
            var b = unitOfWork.books.GetById(data.BookId);
            b.IsAvailable = true;
            unitOfWork.Save();

            return NoContent();


        }

    }
}
