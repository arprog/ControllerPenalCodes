using System;
using System.Threading.Tasks;
using ControllerPenalCodes.Interfaces.ServiceInterfaces;
using ControllerPenalCodes.Models.ViewModels.UserViewModels;
using ControllerPenalCodes.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControllerPenalCodes.Controllers
{
	/// <response code="200">Ok</response>
	/// <response code="400">Bad Request</response>
	/// <response code="500">Internal Server Error</response>
	[ApiController]
	[Authorize]
	[Route("api/v1")]
	public class LoginController : ControllerBase
	{
		private readonly ILoginService _loginService;

		public LoginController(ILoginService loginService)
		{
			_loginService = loginService;
		}

		[HttpPost]
		[AllowAnonymous]
		[Route("login")]
		public async Task<IActionResult> Authenticate([FromServices] Authentication authentication, [FromBody] CreateUserViewModel userViewModel)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			try
			{
				var response = await _loginService.LoginAuthentication(authentication, userViewModel);

				return response.Ok ? Ok(response.Return) : BadRequest(response.Message);
			}
			catch(Exception)
			{
				return StatusCode(500);
			}
		}
	}
}
