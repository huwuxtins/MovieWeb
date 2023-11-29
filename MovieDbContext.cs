using Microsoft.EntityFrameworkCore;
using MovieWeb.Models;
using System.IO;

namespace MovieWeb
{
    public class MovieDbContext: DbContext
    {
        public DbSet<UserModel> UserModels { get; set; }
        public DbSet<FilmModel> FilmModels { get; set; }
        public DbSet<CommentModel> CommentModels { get; set; }

        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options) { }
    }
}
