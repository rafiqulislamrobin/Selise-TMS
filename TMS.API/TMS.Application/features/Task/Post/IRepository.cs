using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Application.features.Task.Post
{
    public interface IRepository
    {
        Entities.Team GetTeamById(int id);
        Entities.User GetUserId(string Id);
    }
}
