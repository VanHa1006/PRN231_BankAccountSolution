using DataAccess.BAL.DAOS.Implementations;
using DataAccess.BAL.DAOS.Interfaces;
using DataAccess.BAL.DTOS.Authentication;
using DataAccess.BAL.DTOS.MemberAccounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.Extensions.Options;

namespace PRN231_BankAccountSolution.Controllers
{
    public class MemberAccountsController : ODataController
    {
        private readonly IMemberAccountDAO _memAccountDAO;
        private IOptions<JwtAuth> _jwtAuthOptions;
        public MemberAccountsController(IMemberAccountDAO memberAccountDAO, IOptions<JwtAuth> jwtAuthOptions) 
        {
            this._memAccountDAO = memberAccountDAO;
            this._jwtAuthOptions = jwtAuthOptions;
        }

        public IActionResult Post([FromBody]Account account)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                GetMemberAccount getMemberAccount = this._memAccountDAO.Login(account, this._jwtAuthOptions.Value);
                return Ok(new
                {
                    Data = getMemberAccount
                });

            }catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message,
                });
            }
        }
    }
}
