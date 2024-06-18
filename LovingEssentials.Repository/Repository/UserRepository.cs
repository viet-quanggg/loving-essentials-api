using LovingEssentials.BusinessObject;
using LovingEssentials.DataAccess.DAOs;
using LovingEssentials.Repository.IRepository;

namespace LovingEssentials.Repository.Repository;

public class UserRepository : IUserRepository
{
    private readonly UserDAO _userDAO;
    public UserRepository(UserDAO userDAO)
    {
        _userDAO = userDAO;
    }

    public async Task<User> Login(string email, string password)
    {
        return await _userDAO.Login(email, password);
    }
    public async Task Register(User user)
    {
        await _userDAO.Register(user);
    }

    public async Task<List<User>> ListAllUser()
    {
        return await _userDAO.ListAllUser();
    }

    public async Task<User> GetUserById(int id)
    {
        return await _userDAO.GetUserById(id);
    }

    public async Task CreateUser(User user)
    {
        await _userDAO.CreateUser(user);
    }

    public async Task UpdateUser(User user)
    {
        await _userDAO.UpdateUser(user);
    }

    public async Task DeleteUser(User user)
    {
        await _userDAO.DeleteUser(user);
    }
}