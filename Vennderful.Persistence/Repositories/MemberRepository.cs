using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vennderful.Application.Contracts.Persitence;
using Vennderful.Domain.Entities;
using Vennderful.Persistence.Contexts;

namespace Vennderful.Persistence.Repositories
{
    public class MemberRepository : BaseRepository<Member>, IMemberRepository
    {
        private readonly VennderfulDbContext _dbContext;
        public MemberRepository(VennderfulDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Member>> GetMembersByCompanyId(Guid comapnyId)
        {
            var members = await _dbContext.Members
                .Include(m => m.Profile)
                .Where(m => m.Profile.CompanyId == comapnyId).ToListAsync();

            return members;
        }
    }
}
