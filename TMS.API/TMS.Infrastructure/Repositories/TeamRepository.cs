using TMS.Application.Entities;
using TMS.Application.features.Team.Get;
using TMS.Infrastructure.Data;
using Get = TMS.Application.features.Team.Get;

namespace TMS.Infrastructure.Repositories
{
    public class TeamRepository : Get.IRepository
    {
        private readonly AppDbContext _dbContext;
        public TeamRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Team> GetTeams(ResourseParameter resourse)
        {
            var query = _dbContext.Teams.AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(resourse.SearchQuery))
            {
                var search = resourse.SearchQuery.Trim().ToLower();
                query = query.Where(c => c.Name.ToLower().Contains(search));
            }

            // OrderBy
            if (!string.IsNullOrWhiteSpace(resourse.OrderBy))
            {
                switch (resourse.OrderBy.ToLower())
                {
                    case "name":
                        query = query.OrderBy(c => c.Name);
                        break;
                    case "description":
                        query = query.OrderBy(c => c.Description);
                        break;
                    case "id":
                        query = query.OrderBy(c => c.Id);
                        break;
                    default:
                        query = query.OrderBy(c => c.Name);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(c => c.Name);
            }

            // Pagination
            int pageNumber = resourse.PageNumber > 0 ? resourse.PageNumber : 1;
            int pageSize = resourse.PageSize > 0 ? resourse.PageSize : 10;
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return query.ToList();
        }
    }
}
