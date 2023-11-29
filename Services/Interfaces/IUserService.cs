using Microsoft.AspNetCore.Mvc;
using MovieWeb.Models;

namespace MovieWeb.Services.Interfaces
{
    public interface IUserService
    {
        public Task<ICollection<UserModel>> GetUsers(int page);
        public Task<UserModel> GetUser(Guid id);
        public Task<UserModel> GetUser(string email);
        public Task<bool> AddUser(UserModel userModel);
        public Task<bool> PutUser(Guid id, UserModel userModel);
        public Task<bool> DeleteUser(UserModel userModel);

    }
}
