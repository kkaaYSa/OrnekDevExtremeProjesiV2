using System.Collections.Generic;
using System.Linq;
using OrnekDevExtremeProjesi2.Models;

namespace OrnekDevExtremeProjesi2.DataAccess.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _db;

        public CategoryRepository()
        {
            _db = new AppDbContext();
        }

        public List<Category> GetAllCategories()
        {
            return _db.Categories.ToList();
        }
    }
}