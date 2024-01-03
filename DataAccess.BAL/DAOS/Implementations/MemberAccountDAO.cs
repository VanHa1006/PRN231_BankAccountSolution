using AutoMapper;
using DataAccess.BAL.DAOS.Interfaces;
using DataAccess.BAL.DTOS.Authentication;
using DataAccess.BAL.DTOS.MemberAccounts;
using DataAccess.Models;
using DataAccess.Repositories.Implementations;
using DataAccess.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BAL.DAOS.Implementations
{
    public class MemberAccountDAO : IMemberAccountDAO
    {
        private MemberAccountRepository _memberAccountRepository;
        private IMapper _mapper;
        public MemberAccountDAO(IMemberAccountRepository memberAccountRepository, IMapper mapper) 
        {
            this._memberAccountRepository = (MemberAccountRepository)memberAccountRepository;
            this._mapper = mapper;
        }

        public GetMemberAccount Login(Account account,JwtAuth jwtAuth)
        {
            try
            {
                User existedAccount = this._memberAccountRepository.Get(x => x.UserId.Equals(account.UserId) 
                && x.Password.Equals(account.Password)).SingleOrDefault();
                
                if(existedAccount == null)
                {
                    throw new Exception("User or Password is in vaild");
                }
                GetMemberAccount getMemberAccount = this._mapper.Map<GetMemberAccount>(existedAccount);
                //Generation Token
                switch (getMemberAccount.UserRole)
                {
                    case "1":
                        {
                            getMemberAccount.UserRole = "Administrator";
                            break;
                        }
                    case "2":
                        {
                            getMemberAccount.UserRole = "Manager";
                            break;
                        }
                    case "3":
                        {
                            getMemberAccount.UserRole = "Staff";
                            break;
                        }
                }
                return GenerateToken(getMemberAccount, jwtAuth);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // To generate token
        private GetMemberAccount GenerateToken(GetMemberAccount getMemberAccount, JwtAuth jwtAuth)
        {
            try
            {
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAuth.Key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new ClaimsIdentity(new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub,getMemberAccount.UserId),
                new Claim(JwtRegisteredClaimNames.Name,getMemberAccount.UserName),
                new Claim("Role",getMemberAccount.UserRole),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            });

                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(2),
                    SigningCredentials = credentials,
                };

                var token = jwtTokenHandler.CreateToken(tokenDescription);
                string accessToken = jwtTokenHandler.WriteToken(token);

                getMemberAccount.AccessToken = accessToken;

                return getMemberAccount;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
