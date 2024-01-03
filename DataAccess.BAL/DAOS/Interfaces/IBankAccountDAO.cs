using DataAccess.BAL.DTOS.BankAccounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BAL.DAOS.Interfaces
{
    public interface IBankAccountDAO
    {
        public List<GetBankAccount> GetAll(string? OpenDate, string? AccountName);
    }
}
