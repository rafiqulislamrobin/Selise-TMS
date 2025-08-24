using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Application.features.User.Login
{
    public interface IRepository
    {
        Task<LoginUserResponse> LoginUser(LoginUserCommand command);
    }
}
