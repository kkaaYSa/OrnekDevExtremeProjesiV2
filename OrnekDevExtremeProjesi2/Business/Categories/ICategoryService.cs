using System.Collections.Generic;
using OrnekDevExtremeProjesi2.Models;

namespace OrnekDevExtremeProjesi2.Business.Categories
{
    public interface ICategoryService
    {
        List<Category> GetAllCategories();
    }
}