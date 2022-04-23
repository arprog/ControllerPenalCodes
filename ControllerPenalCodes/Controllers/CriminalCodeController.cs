using System;
using System.Linq;
using System.Threading.Tasks;
using ControllerPenalCodes.Models.Interfaces.ServiceInterfaces;
using ControllerPenalCodes.Models.ViewModels.CriminalCodeViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ControllerPenalCodes.Controllers
{
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
				var userId = User.Claims.FirstOrDefault(i => i.Type.Equals("NameIdentifier")).Value;

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

		[HttpGet]
		[Route("criminal-codes/id/{id}")]
		public async Task<IActionResult> GetById([FromRoute] Guid id)
		{
			try
			{
				var response = await _criminalCodeService.GetById(id);

				return response.Ok ? Ok(response.Return) : NoContent();
			}
			catch (Exception)
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
				var userId = User.Claims.FirstOrDefault(i => i.Type.Equals("NameIdentifier")).Value;

				var response = await _criminalCodeService.Update(criminalCodeViewModel, userId);

				if (response.Ok)
					return Ok();

				return String.IsNullOrEmpty(response.Message) ? NoContent() : BadRequest(response.Message);
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

				return response.Ok ? Ok() : NoContent();
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}
	}
}
