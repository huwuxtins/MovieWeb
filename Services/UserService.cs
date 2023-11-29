using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieWeb.Models;
using MovieWeb.Services.Interfaces;

namespace MovieWeb.Services
{
    public class UserService : IUserService  
    {
        private readonly MovieDbContext _dbContext;
        public UserService(MovieDbContext dbContext) {  _dbContext = dbContext; }

        public async Task<ICollection<UserModel>> GetUsers(int page)
        {
            return await _dbContext.UserModels.OrderBy(user => user.RoleName).Skip((page -1) * 10).Take(10).ToListAsync();
        }

        public async Task<UserModel> GetUser(Guid id)
        {
            return await _dbContext.UserModels.FindAsync(id);
        }
        public async Task<UserModel> GetUser(string email)
        {
            return await _dbContext.UserModels.FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<bool> AddUser(UserModel user)
        {
            _dbContext.UserModels.Add(user);

            var isSaved = await _dbContext.SaveChangesAsync();
            return isSaved > 0;
        }

        public async Task<bool> PutUser(Guid id, UserModel userModel)
        {
            var isUpdated = await _dbContext.SaveChangesAsync();
            return isUpdated > 0;
        }

        public async Task<bool> DeleteUser(UserModel userModel)
        {
            _dbContext.UserModels.Remove(userModel);
            var isDeleted = await _dbContext.SaveChangesAsync();
            return isDeleted > 0;
        }
    }
}
