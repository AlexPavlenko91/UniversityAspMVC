using Context;
using Entities;
using Repositories.Generic;


namespace Repositories
{
    public class FacultyRepository:DbRepository<Faculty>, IFacultyRepository 
    {
        public FacultyRepository(AppDbContext context): base(context)
        {
        }
    }
}
