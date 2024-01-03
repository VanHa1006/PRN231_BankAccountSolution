using DataAccess.BAL.DAOS.Authentications;
using DataAccess.BAL.DAOS.Interfaces;
using DataAccess.BAL.DTOS.BankAccounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace PRN231_BankAccountSolution.Controllers
{
    public class BankAccountsController : ODataController
    {
        private IBankAccountDAO _bankAccountDAO;

        public BankAccountsController(IBankAccountDAO bankAccountDAO) 
        {
            this._bankAccountDAO = bankAccountDAO;
        }

        [EnableQuery]
        /*[PermissionAuthorize("Administrator")]*/
        public IActionResult Get([FromRoute] string? OpenDate, [FromRoute]string? AccountName)
        {
            try
            {
                List<GetBankAccount> Accounts = this._bankAccountDAO.GetAll(OpenDate, AccountName);
                return Ok(new
                {
                    Data = Accounts
                });
            }catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message,
                });
            }

        }
    }
}
