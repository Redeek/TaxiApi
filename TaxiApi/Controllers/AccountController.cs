using Microsoft.AspNetCore.Mvc;
using TaxiApi.Models;
using TaxiApi.Services;

namespace TaxiApi.Controllers
{

    [Route("api/account")]
    [ApiController]
    public class AccountController: ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            _accountService.RegisterUser(dto);
            return Ok();
        }
    }
}
