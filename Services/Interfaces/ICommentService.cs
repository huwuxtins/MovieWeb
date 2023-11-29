using MovieWeb.Models;

namespace MovieWeb.Services.Interfaces
{
    public interface ICommentService
    {
        public Task<ICollection<CommentModel>> GetComments(int? size, Guid filmId);
        public Task<CommentModel> GetComment(Guid id);
        public Task<bool> AddComment(CommentModel commentModel);
        public Task<bool> UpdateComment(Guid id, CommentModel commentModel);
        public Task<bool> DeleteComment(CommentModel commentModel);
    }
}
