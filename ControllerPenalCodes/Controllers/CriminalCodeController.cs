using System;
using System.Linq;
using System.Threading.Tasks;
using ControllerPenalCodes.Interfaces.ServiceInterfaces;
using ControllerPenalCodes.Models.ViewModels.CriminalCodeViewModels;
using ControllerPenalCodes.Shared;
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
	[Route("api/v1/criminal-codes")]
	public class CriminalCodeController : ControllerBase
	{
		private readonly ICriminalCodeService _criminalCodeService;

		public CriminalCodeController(ICriminalCodeService criminalCodeService)
		{
			_criminalCodeService = criminalCodeService;
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] CreateCriminalCodeViewModel criminalCodeViewModel)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			try
			{
				var userId = Authentication.GetUserId(User?.Claims);

				var response = await _criminalCodeService.Create(userId, criminalCodeViewModel);

				return response.Ok ? Created(response.Message, response.Return) : BadRequest(response.Message);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var response = await _criminalCodeService.GetAll();

				return response.Ok ? Ok(response.Return) : NoContent();
			}
			catch(Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> Get(Guid id)
		{
			try
			{
				var response = await _criminalCodeService.Get(id);

				return response.Ok ? Ok(response.Return) : NoContent();
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpPut]
		[Route("{id}")]
		public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateCriminalCodeViewModel criminalCodeViewModel)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			try
			{
				var userId = Authentication.GetUserId(User?.Claims);

				var response = await _criminalCodeService.Update(id, userId, criminalCodeViewModel);

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
				var response = await _criminalCodeService.Delete(id);

				return response.Ok ? Ok() : BadRequest(response.Message);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}
	}
}
