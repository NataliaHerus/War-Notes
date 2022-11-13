using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.EntityFramework.Entities;
using DataAccessLayer.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class CategoryService : ICategoryService
    {
        protected readonly IMapper _mapper;
        private readonly IRepository<Category> _categoryrepository;

        public CategoryService(IMapper mapper, IRepository<Category> categoryRepository)
        {
            _mapper = mapper;
            _categoryrepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _categoryrepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<string> GetCategoryNameById(int id)
        {
            var category = await _categoryrepository.GetByIdAsync(id);
            return category.CategoryName;
        }
    }
}
