using System.Collections.Generic;
using OrnekDevExtremeProjesi2.Models;

namespace OrnekDevExtremeProjesi2.DataAccess.Categories
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
    }
}