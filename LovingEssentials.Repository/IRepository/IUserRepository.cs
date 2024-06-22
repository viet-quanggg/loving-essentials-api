using LovingEssentials.BusinessObject;

namespace LovingEssentials.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<User> Login(string email);
        Task Register(User user);
        Task CreateUser(User user);
        Task DeleteUser(User user);
        Task<User> GetUserById(int id);
        Task<bool> UserExistsByEmail(string email);
        Task<List<User>> ListAllUser();
        Task UpdateUser(User user);
    }
}