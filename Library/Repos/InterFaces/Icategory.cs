using Library.Dtos;
using Library.GenaricRepos;
using Library.Models;

namespace Library.Repos.InterFaces
{
    public interface Icategory  : IGenaricRepo<Category>
    {
        List<CategoryDtoscoust> Categorywithcount();
    }
}
