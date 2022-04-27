using System;
using System.Threading.Tasks;
using ControllerPenalCodes.Interfaces.ServiceInterfaces;
using ControllerPenalCodes.Models.ViewModels.StatusViewModels;
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
	public class StatusController : ControllerBase
	{
		private readonly IStatusService _statusService;

		public StatusController(IStatusService statusService)
		{
			_statusService = statusService;
		}

		[HttpPost]
		[Route("status")]
		public async Task<IActionResult> Post([FromBody] CreateStatusViewModel statusViewModel)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			try
			{
				var response = await _statusService.Create(statusViewModel);

				return response.Ok ? Created(response.Message, response.Return) : BadRequest(response.Message);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpGet]
		[Route("status")]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var response = await _statusService.GetAll();

				return response.Ok ? Ok(response.Return) : NoContent();
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpGet]
		[Route("status/id/{id}")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			try
			{
				var response = await _statusService.GetById(id);

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
		[Route("status/{id}")]
		public async Task<IActionResult> Put([FromBody] UpdateStatusViewModel statusViewModel)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			try
			{
				var response = await _statusService.Update(statusViewModel);

				return response.Ok ? Ok() : BadRequest(response.Message);
			}
			catch(Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpDelete]
		[Route("status/{id}")]
		public async Task<IActionResult> Delete([FromRoute] Guid id)
		{
			try
			{
				var response = await _statusService.Delete(id);

				return response.Ok ? Ok() : BadRequest(response.Message);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}
	}
}
