using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Vennderful.Identity.Interfaces;
using Vennderful.Identity;

namespace Vennderful.Identity.Repositories
{
    public class IdentityUnitOfWork : IidentityUnitOfWork
    {
        private readonly VennderfulIdentityDBContext _dbContext;
     
        private IUserRepository _userRepository;
  
        public IdentityUnitOfWork(VennderfulIdentityDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUserRepository UserRepository =>
            _userRepository ??= new UserRepository(_dbContext);
        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
