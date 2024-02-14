using ForumApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForumApp.Data.Configuration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        private Post[] initialPosts = new Post[]
        {
            new Post(){Id = 1, Title = "My first post", Content = "First Post Content"},
            new Post(){Id = 2, Title = "My second post", Content = "Second Post Content"},
            new Post(){Id = 3, Title = "My third post", Content = "Third Post Content"}
        };
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasData(initialPosts);
        }
    }
}
