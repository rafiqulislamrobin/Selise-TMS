using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TMS.Application.Entities;
using TMS.Application.features.User.Get;
using TMS.Application.features.User.Login;
using TMS.Infrastructure.Data;
using Delete = TMS.Application.features.User.Delete;
using Get = TMS.Application.features.User.Get;
using Login = TMS.Application.features.User.Login;
using Post = TMS.Application.features.User.Post;
using Put = TMS.Application.features.User.Put;
using Task = System.Threading.Tasks.Task;

namespace TMS.Infrastructure.Repositories
{
    public class UserRepository : Get.IRepository, Post.IRepository, Delete.IRepository, Put.IRepository, Login.IRepository
    {

        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        // And update the constructor to accept the correct types:
        public UserRepository(AppDbContext dbContext, IConfiguration configuration, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async System.Threading.Tasks.Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<bool> CreateUser(User user)
        {
           

            // 1. Create user
            var createResult = await _userManager.CreateAsync(user, user.Password);

            if (!createResult.Succeeded)
             return false;

            if (!await _roleManager.RoleExistsAsync(user.Role))
            {
                await _roleManager.CreateAsync(new IdentityRole(user.Role));
            }

            // 3. Add user to the role
            await _userManager.AddToRoleAsync(user, user.Role);

            return true;
        }

        public void Delete(User user)
        {
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _dbContext.Users.FindAsync(id);
        }

        public List<User> GetUsers(ResourseParameter resourse)
        {
            var query = _dbContext.Users.AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(resourse.SearchQuery))
            {
                var search = resourse.SearchQuery.Trim().ToLower();
                query = query.Where(c => c.UserName.ToLower().Contains(search));
            }

            // OrderBy
            if (!string.IsNullOrWhiteSpace(resourse.OrderBy))
            {
                switch (resourse.OrderBy.ToLower())
                {
                    case "name":
                        query = query.OrderBy(c => c.UserName);
                        break;
                    case "email":
                        query = query.OrderBy(c => c.Email);
                        break;
                    case "role":
                        query = query.OrderBy(c => c.Role);
                        break;
                    case "id":
                        query = query.OrderBy(c => c.Id);
                        break;
                    default:
                        query = query.OrderBy(c => c.UserName);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(c => c.UserName);
            }

            // Pagination
            int pageNumber = resourse.PageNumber > 0 ? resourse.PageNumber : 1;
            int pageSize = resourse.PageSize > 0 ? resourse.PageSize : 10;
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return query.ToList();
        }

        public async Task<LoginUserResponse?> LoginUser(LoginUserCommand command)
        {
            var user = await _userManager.FindByNameAsync(command.Email);
            if (user == null)
                return null; // user not found

            var isValid = await _userManager.CheckPasswordAsync(user, command.Password);
            if (!isValid)
                return null; 
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Email),
        new Claim(ClaimTypes.NameIdentifier, user.Id)
    };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new LoginUserResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Email = user.Email,
                Role = roles.FirstOrDefault() ?? ""
            };
        }


        public bool Update(User user)
        {
            _dbContext.Users.Update(user);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
