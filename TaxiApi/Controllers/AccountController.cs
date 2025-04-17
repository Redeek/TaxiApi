using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
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

        [HttpPost("{userId}")]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult AssignUserToDriver([FromRoute] int userId, [FromBody] CreateDriverDto dto)
        {
            var newDriver = _accountService.AssignUserToDriver(userId, dto);

            return Ok($"new assign id {newDriver}");
        }

        [HttpPost("register")]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            _accountService.RegisterUser(dto);
            return Ok();
        }
        [Authorize(Policy = "ContractContinues")]
        [Authorize(Roles = "Admin,Manager")]
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            string token = _accountService.GenerateJwt(dto);
            return Ok(token);
        }

        [HttpDelete("{userId}")]
        [Authorize(Roles = "Admin,Manager")]
        public ActionResult DeleteUser([FromRoute] int userId)
        {
            _accountService.DeleteUser(userId);
            return NoContent();
        }

    }
}
