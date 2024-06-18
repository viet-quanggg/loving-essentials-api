using LovingEssentials.BusinessObject;

namespace LovingEssentials.Repository.IRepository
{
    public interface IUserRepository
    {
        Task CreateUser(User user);
        Task DeleteUser(User user);
        Task<User> GetUserById(int id);
        Task<List<User>> ListAllUser();
        Task UpdateUser(User user);
    }
}