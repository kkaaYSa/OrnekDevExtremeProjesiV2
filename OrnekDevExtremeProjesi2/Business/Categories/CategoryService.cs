using System.Collections.Generic;
using OrnekDevExtremeProjesi2.Models;
using OrnekDevExtremeProjesi2.DataAccess.Categories;

namespace OrnekDevExtremeProjesi2.Business.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService()
        {
            _categoryRepository = new CategoryRepository();
        }

        public List<Category> GetAllCategories()
        {
            return _categoryRepository.GetAllCategories();
        }
    }
}