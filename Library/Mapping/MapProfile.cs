using AutoMapper;
using AutoMapper.Execution;
using Library.Dtos;
using Library.Models;

namespace Library.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Models.Member, MemberDtos>();
            CreateMap<MemberCreate, Models.Member>();


            CreateMap<Category, CategoryDtos>();
            CreateMap<CategoryCreateDto , Category>();


            CreateMap<Book, BookDtos>();
            CreateMap<BookCreateDtos, Book>();

            CreateMap<Borrowing, BorrowingDtos>();
            CreateMap<BorrowingCreateDtos, Borrowing>();
        }
    }
}
