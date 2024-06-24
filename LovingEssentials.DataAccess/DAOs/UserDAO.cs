using LovingEssentials.BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace LovingEssentials.DataAccess.DAOs;

public class UserDAO
{
    private readonly DataContext _context;

    public UserDAO(DataContext context)
    {
        _context = context;
    }

    public async Task<User> Login(string email)
    {
        User user = null;
        try
        {
            if (email != null)
            {
                user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return user;
    }

    public async Task Register(User user)
    {
        var u = await GetUserById(user.Id);
        try
        {
            if (u == null)
            {
                var newUser = new User
                {
                    Name = user.Name,
                    Role = Role.Client,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Password = user.Password,
                    Status = 1
                };

                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("User is existed");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<User>> ListAllUser()
    {
        List<User> list = null;
        try
        {
            list = await _context.Users.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return list;
    }

    public async Task<User> GetUserById(int id)
    {
        User user = null;
        try
        {
            user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return user;
    }

    public async Task<bool> UserExistsByEmail(string email)
    {
        try
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task CreateUser(User user)
    {
        var u = await GetUserById(user.Id);
        try
        {
            if (u == null)
            {
                var newUser = new User
                {
                    Name = user.Name,
                    Role = user.Role,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Password = user.Password,
                    Status = 1
                };

                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("User is existed");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task UpdateUser(User user)
    {
        var u = await GetUserById(user.Id);
        try
        {
            if (u != null)
            {
                u.Email = user.Email;
                u.PhoneNumber = user.PhoneNumber;
                u.Name = user.Name;
                u.Status = user.Status;

                _context.Users.Update(u);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("User not found");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task DeleteUser(User user)
    {
        var u = await GetUserById(user.Id);
        try
        {
            if (u != null)
            {
                u.Status = 0;
                _context.Users.Update(u);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("User not found");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}