using System;
using System.Net;
using System.Threading.Tasks;
using ControllerPenalCodes.Interfaces.ServiceInterfaces;
using ControllerPenalCodes.Models.ViewModels.UserViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControllerPenalCodes.Controllers
{
	/// <response code="200">Ok</response>
	/// <response code="201">Created</response>
	/// <response code="204">No Content</response>
	/// <response code="400">Bad Request</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="404">Not Found</response>
	/// <response code="500">Internal Server Error</response>
	[ApiController]
	[Authorize]
	[Route("api/v1/users")]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateUserViewModel userViewModel)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			try
			{
				var response = await _userService.Create(userViewModel);

				return response.Ok ? Created(response.Message, response.Return) : BadRequest(response.Message);
			}
			catch(Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var response = await _userService.GetAll();

				return response.Ok ? Ok(response.Return) : NoContent();
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			try
			{
				var response = await _userService.GetById(id);

				if (response.Ok)
					return Ok(response.Return);

				return string.IsNullOrEmpty(response.Message) ? NoContent() : BadRequest(response.Message);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> Put([FromBody] UpdateUserViewModel userViewModel)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			try
			{
				var response = await _userService.Update(userViewModel);

				return response.Ok ? Ok() : BadRequest(response.Message);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpDelete]
		[Route("{id}")]
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			try
			{
				var response = await _userService.Delete(id);

				return response.Ok ? Ok() : BadRequest(response.Message);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}
	}
}
