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
    public class MemberController : ControllerBase
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapper mapper;

        public MemberController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult Add(MemberCreate dto)
        {
            var c = unitOfWork.members.Getall()
                .Where(w => w.Email == dto.Email);

            if (c != null) return BadRequest("Email Must Is unique");

            var r = mapper.Map<Member>(dto);
            unitOfWork.members.Create(r);
            unitOfWork.Save();

            return Created();
        }


        [HttpGet("BestMembers")]
        public IActionResult GetTopReaders()
        {
            var data = unitOfWork.members.Getall()
                .Select(e => new
                {
                    Id = e.Id,
                    Name = e.FullName,
                    BorrowingCount = e.Borrowings.Count()
                }).OrderByDescending(c => c.BorrowingCount)
                .Take(5)
                .ToList();

            return Ok(data);
        }



        [HttpGet("{Id:int}")]
        public IActionResult GetById(int id) 
        {
            var member = unitOfWork.members.Getall()
                
                .Where(e => e.Id == id)
                .Select(e => new {
                Id = e.Id,
                Name = e.FullName,
                ReturnBooks = e.Borrowings.Count(d => d.ReturnDate != null),
                    CurrentlyBorrowedBooks = e.Borrowings
                .Count(b => b.ReturnDate == null)
                }).FirstOrDefault();

            if (member == null)
                return NotFound("Member not found.");

            return Ok(member);
        }
    }
}
