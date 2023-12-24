using AutoMapper;
using DataAccess.BAL.DTOS.MemberAccounts;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BAL.Profiles
{
    public class MemberAccountProfile : Profile
    {
        public MemberAccountProfile() 
        {
            CreateMap<User, GetMemberAccount>().ReverseMap();
        }
    }
}
