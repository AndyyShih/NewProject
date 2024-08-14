using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
        {

        }
    }
}
