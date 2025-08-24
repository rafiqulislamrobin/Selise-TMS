using System.Collections.Generic;
using TMS.Application.Entities;

namespace TMS.Application.features.Task.Get
{
    public interface IRepository
    {
        List<Entities.Task> GetAllTasks(ResourseParameter resourse);
    }
}