using Anyar.Core.Entities;
using Anyor.DataAccess.Contexts;
using Anyor.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anyor.DataAccess.Repositories.Implementations
{
    public class TeamItemRepository : Repository<TeamItem>, ITeamItemRepository
    {
        public TeamItemRepository(AppDbContext context) : base(context)
        {
        }
    }
}
