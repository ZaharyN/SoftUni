using Microsoft.EntityFrameworkCore;
using ShoppingListApp.Contracts;
using ShoppingListApp.Data;
using ShoppingListApp.Data.Models;
using ShoppingListApp.Models;

namespace ShoppingListApp.Services
{
	public class ProductService : IProductService
	{
		private readonly ShoppingListDbContext context;

        public ProductService(ShoppingListDbContext _context)
        {
			context = _context;
        }
        public async Task AddProductAsync(ProductViewModel model)
		{
			var entity = new Product()
			{
				Name = model.Name,
			};
			await context.Products.AddAsync(entity);
			await context.SaveChangesAsync();
		}

		public async Task DeleteProductAsync(int id)
		{
			var entity = await context.Products.FindAsync(id);

			if(entity == null)
			{
				throw new ArgumentException("Invalid product");
			}

			context.Products.Remove(entity);
			await context.SaveChangesAsync();
		}

		public async Task<IEnumerable<ProductViewModel>> GetAllAsync()
		{
			var result = await context.Products
				.Select(x => new ProductViewModel()
				{
					Id = x.Id,
					Name = x.Name
				})
				.AsNoTracking()
				.ToListAsync();

			return result;
		}

		public async Task<ProductViewModel> GetByIdAsync(int id)
		{
			var entity = await context.Products.FindAsync(id);

			if(entity == null)
			{
				throw new ArgumentNullException("Invalid product Id");
			}

			return new ProductViewModel()
			{
				Id = id,
				Name = entity.Name
			};
		}

		public async Task UpdateProductAsync(ProductViewModel model)
		{
			var entity = await context.Products.FindAsync(model.Id);

			entity.Name = model.Name;

			await context.SaveChangesAsync();
		}
	}
}
