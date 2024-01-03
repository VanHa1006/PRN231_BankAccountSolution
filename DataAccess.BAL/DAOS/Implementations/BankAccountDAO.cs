using AutoMapper;
using DataAccess.BAL.DAOS.Interfaces;
using DataAccess.BAL.DTOS.BankAccounts;
using DataAccess.Repositories.Implementations;
using DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BAL.DAOS.Implementations
{
    public class BankAccountDAO : IBankAccountDAO
    {
        private BankAccountRepository _bankAccountRepository;
        private IMapper _mapper;
        public BankAccountDAO(IBankAccountRepository bankAccountRepository, IMapper mapper)  
        {
            this._bankAccountRepository = (BankAccountRepository)bankAccountRepository;
            this._mapper = mapper;
        }

        public List<GetBankAccount> GetAll(string? OpenDate, string? AccountName)
        {
            try
            {
                DateTime Opendate = new DateTime();
                if(OpenDate != null && DateTime.TryParse(OpenDate, out Opendate) == false)
                {
                    throw new Exception("OpenDate invalid!!!");
                }

                List<GetBankAccount> Accounts = this._mapper.Map<List<GetBankAccount>>(this._bankAccountRepository.Get().ToList());
                if(OpenDate != null)
                {
                    Accounts = Accounts.Where(p => p.OpenDate == Opendate).ToList();
                }
                if(AccountName != null)
                {
                    Accounts = Accounts.Where(p => p.AccountName.ToLower().Contains(AccountName.ToLower())).ToList();
                }
                return Accounts;

            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
