using DataAccess.BAL.DTOS.Authentication;
using DataAccess.BAL.DTOS.MemberAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BAL.DAOS.Interfaces
{
    public interface IMemberAccountDAO
    {
        public GetMemberAccount Login(Account account,JwtAuth jwtAuth);
    }
}
