using AutoMapper;
using CardCostApplication.Application.Interfaces;
using CardCostApplication.Contracts.Responses;
using CardCostApplication.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CardCostApplication.Controllers
{
	[ApiController]
	[Route("api/[controller]")] // /api/clearing-costs
	public class ClearingCostsController : ControllerBase
	{
		private readonly IClearingCostService _service;
		private readonly IMapper _mapper;

		public ClearingCostsController(IClearingCostService service, IMapper mapper)
		{
			_service = service;
			_mapper = mapper;
		}

		// GET /api/clearing-costs
		[HttpGet]
		public async Task<ActionResult<List<ClearingCostRepresentation>>> GetAll()
		{
			var entities = await _service.GetAllAsync();
			var dtos = _mapper.Map<List<ClearingCostRepresentation>>(entities);
			return Ok(dtos);
		}

		// GET /api/clearing-costs/{id}
		[HttpGet("{id:long}")]
		public async Task<ActionResult<ClearingCostRepresentation>> GetById(long id)
		{
			var entity = await _service.GetByIdAsync(id);
			if (entity is null) return NotFound();

			return Ok(_mapper.Map<ClearingCostRepresentation>(entity));
		}

		// POST /api/clearing-costs
		[HttpPost]
		public async Task<ActionResult<ClearingCostRepresentation>> Create([FromBody] ClearingCostRepresentation rep)
		{
			// DTO -> Entity (Id αγνοείται από AutoMapper profile)
			var toSave = _mapper.Map<ClearingCost>(rep);
			var saved = await _service.SaveAsync(toSave);

			var dto = _mapper.Map<ClearingCostRepresentation>(saved);
			// /api/clearing-costs/{id}
			return CreatedAtAction(nameof(GetById), new { id = saved.Id }, dto);
		}

		// PUT /api/clearing-costs/{id}
		// Στο Spring κάνεις upsert-by-countryCode και αγνοείς το id path.
		// Κρατάμε ίδια συμπεριφορά: χρησιμοποιούμε το rep.CountryCode για upsert.
		[HttpPut("{id:long}")]
		public async Task<ActionResult<ClearingCostRepresentation>> Update(long id, [FromBody] ClearingCostRepresentation rep)
		{
			var incoming = _mapper.Map<ClearingCost>(rep);
			var updated = await _service.UpsertByCountryCodeAsync(incoming);

			return Ok(_mapper.Map<ClearingCostRepresentation>(updated));
		}

		// DELETE /api/clearing-costs/{id}
		[HttpDelete("{id:long}")]
		public async Task<IActionResult> Delete(long id)
		{
			var exists = await _service.GetByIdAsync(id);
			if (exists is null) return NotFound();

			await _service.DeleteByIdAsync(id);
			return NoContent();
		}
	}
}
