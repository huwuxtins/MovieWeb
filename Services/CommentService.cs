using Microsoft.EntityFrameworkCore;
using MovieWeb.Models;
using MovieWeb.Services.Interfaces;
using System.IO;
using System.Linq;

namespace MovieWeb.Services
{
    public class CommentService : ICommentService
    {
        private readonly MovieDbContext _dbContext;
        public CommentService(MovieDbContext context)
        {
            _dbContext = context;
        }
        public async Task<ICollection<CommentModel>> GetComments(int? size, Guid filmId)
        {
            ICollection<CommentModel> comments = await _dbContext.CommentModels
                .Include(cmt => cmt.Film)
                .Where(cmt => cmt.Film.Id.Equals(filmId)).
                ToListAsync();
            if(size.HasValue)
            {
                comments = comments.Take(size.Value).ToList();
            }
            else
            {
                comments = comments.Take(5).ToList();
            }
            return comments;
        }

        public async Task<CommentModel>  GetComment(Guid id)
        {
            return await _dbContext.CommentModels.FindAsync(id);
        }

        public async Task<bool> AddComment(CommentModel commentModel)
        {
            _dbContext.CommentModels.Add(commentModel);
            var isSaved = await _dbContext.SaveChangesAsync();
            return isSaved > 0;
        }

        public async Task<bool> UpdateComment(Guid id, CommentModel commentModel)
        {
            _dbContext.CommentModels.Update(commentModel);
            var isUpdated = await _dbContext.SaveChangesAsync();
            return isUpdated > 0;
        }

        public async Task<bool> DeleteComment(CommentModel commentModel)
        {
            _dbContext.CommentModels.Remove(commentModel);
            var isDeleted = await _dbContext.SaveChangesAsync();
            return isDeleted > 0;
        }
    }
}
