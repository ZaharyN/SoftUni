using ForumApp.Core.Contracts;
using ForumApp.Core.Models;
using ForumApp.Data;
using ForumApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumApp.Core.Services
{
	public class PostService : IPostService
	{
		private readonly ForumAppDbContext context;

        public PostService(ForumAppDbContext _context)
        {
            context = _context;
        }

        public async Task AddAsync(PostModel model)
        {
			var entity = new Post()
			{
				Title = model.Title,
				Content = model.Content
			};

			await context.AddAsync(entity);
			await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await context.FindAsync<Post>(id);

            if (entity == null)
            {
                throw new ApplicationException("Invalid Post");
            }

			context.Remove(entity);
			await context.SaveChangesAsync();
        }

        public async Task EditAsync(PostModel model)
        {
			var entity = await context.FindAsync<Post>(model.Id); 

			if(entity == null)
			{
				throw new ApplicationException("Invalid Post");
			}
			entity.Title = model.Title;
			entity.Content = model.Content;

			await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostModel>> GetAllPostsAsync()
		{
			return await context.Posts 
				.Select(p => new PostModel()
				{
					Id = p.Id,
					Title = p.Title,
					Content = p.Content
				})
				.AsNoTracking()
				.ToListAsync();
		}

        public async Task<PostModel?> GetByIdAsync(int id)
        {
			return await context.Posts
				.Where(p => p.Id == id)
				.Select(p => new PostModel()
				{
					Id = p.Id,
					Title = p.Title,
					Content = p.Content
				})
				.FirstOrDefaultAsync();
        }
    }
}
