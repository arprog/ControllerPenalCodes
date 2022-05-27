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
	/// <response code="404">Not Found</response>
	/// <response code="500">Internal Server Error</response>
	[ApiController]
	[Authorize]
	[Route("api/v1/status")]
	public class StatusController : ControllerBase
	{
		private readonly IStatusService _statusService;

		public StatusController(IStatusService statusService)
		{
			_statusService = statusService;
		}

		[HttpPost]
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
		public async Task<IActionResult> GetAll(
			[FromBody] FilterStatusViewModel statusViewModel,
			[FromQuery] int page = 1,
			[FromQuery] int itemsByPage = 25)
		{
			try
			{
				page = (page <= 0) ? 1 : page;
				itemsByPage = (itemsByPage <= 0 || itemsByPage > 100) ? 25 : itemsByPage;

				var response = await _statusService.GetAll(statusViewModel, page, itemsByPage);

				return response.Ok ? Ok(response.Return) : NoContent();
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> Get([FromRoute] Guid id)
		{
			try
			{
				var response = await _statusService.Get(id);

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
		public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateStatusViewModel statusViewModel)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			try
			{
				var response = await _statusService.Update(id, statusViewModel);

				return response.Ok ? Ok() : BadRequest(response.Message);
			}
			catch(Exception)
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
