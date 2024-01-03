using AutoMapper;
using DataAccess.BAL.DTOS.BankAccounts;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BAL.Profiles
{
    public class BankAccountProfile :Profile
    {
        public BankAccountProfile() 
        {
            CreateMap<BankAccount, GetBankAccount>().ReverseMap();        
        }
    }
}
