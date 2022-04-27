using System;
using System.Linq;
using System.Threading.Tasks;
using ControllerPenalCodes.Interfaces.ServiceInterfaces;
using ControllerPenalCodes.Models.ViewModels.CriminalCodeViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControllerPenalCodes.Controllers
{
	/// <response code="200">Ok</response>
	/// <response code="201">Created</response>
	/// <response code="204">No Content</response>
	/// <response code="400">Bad Request</response>
	/// <response code="401">Unauthorized</response>
	/// <response code="500">Internal Server Error</response>
	[ApiController]
	[Authorize]
	[Route("api/v1")]
	public class CriminalCodeController : ControllerBase
	{
		private readonly ICriminalCodeService _criminalCodeService;

		public CriminalCodeController(ICriminalCodeService criminalCodeService)
		{
			_criminalCodeService = criminalCodeService;
		}

		[HttpPost]
		[Route("criminal-codes")]
		public async Task<IActionResult> Post([FromBody] CreateCriminalCodeViewModel criminalCodeViewModel)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			try
			{
				var userId = User.Claims.FirstOrDefault(i => i.Type.Contains("nameidentifier")).Value;

				var response = await _criminalCodeService.Create(criminalCodeViewModel, userId);

				return response.Ok ? Created(response.Message, response.Return) : BadRequest(response.Message);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpGet]
		[Route("criminal-codes")]
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

		[HttpPut]
		[Route("criminal-codes/{id}")]
		public async Task<IActionResult> Put([FromBody] UpdateCriminalCodeViewModel criminalCodeViewModel)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			try
			{
				var userId = User.Claims.FirstOrDefault(i => i.Type.Contains("nameidentifier")).Value;

				var response = await _criminalCodeService.Update(criminalCodeViewModel, userId);

				return response.Ok ? Ok() : BadRequest(response.Message);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpDelete]
		[Route("criminal-codes/{id}")]
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
