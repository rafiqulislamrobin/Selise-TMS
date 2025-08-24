using CSharpFunctionalExtensions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using TMS.Application.Common.Domain;

namespace TMS.Application.Entities
{
    public class User : IdentityUser, IUser<string>
    {
        public string Email { get; private set; }

        [NotMapped]
        public string Role { get; private set; }

        [NotMapped]
        public string Password { get; private set; }

        public void Create(string fullName, string email, string role, string password)
        {
            UserName = fullName;
            Email = email;
            Role = role;
            Password = password;
        }

        public void Update(string fullName, string email, string role )
        {
            UserName = fullName;
            Email = email;
            Role = role;
        }
    }
}
