using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Vennderful.Identity.Interfaces
{
    public interface ITokenService<T> where T : class
    {
        Task<string> CreateTokenAsync(T user);
    }
}
