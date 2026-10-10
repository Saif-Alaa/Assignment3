using Library.Dtos;
using Library.GenaricRepos;
using Library.Models;
using Library.Repos.InterFaces;
using Microsoft.EntityFrameworkCore;

namespace Library.Repos
{
    public class CategoryRepo : GenaricRepo<Category>, Icategory
    {
        public CategoryRepo(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public List<CategoryDtoscoust> Categorywithcount()
        {
            return appDbContext.categories
                  .Select(e => new CategoryDtoscoust
                  {
                      Id = e.Id,
                      Name = e.Name,
                      Count = e.Books.Count()
                  })
                  .ToList();
        }
    }
}
