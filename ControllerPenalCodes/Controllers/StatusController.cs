using System;
using System.Threading.Tasks;
using ControllerPenalCodes.Models.Interfaces.ServiceInterfaces;
using ControllerPenalCodes.Models.ViewModels.StatusViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControllerPenalCodes.Controllers
{
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

				return response.Ok ? Ok(response.Return) : NoContent();
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

				if (response.Ok)
					return Ok();

				return String.IsNullOrEmpty(response.Message) ? NoContent() : BadRequest(response.Message);
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

				return response.Ok ? Ok() : NoContent();
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}
	}
}
