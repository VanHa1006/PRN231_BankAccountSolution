using DataAccess.Models;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Implementations
{
    public class MemberAccountRepository : RepositoryBase<User>, IMemberAccountRepository
    {
        public MemberAccountRepository() { }
    }
}
