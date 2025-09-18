using System.Net.Http;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CardCostApplication.Application.Interfaces;

namespace CardCostApplication.Controllers
{
	public class CardLookupRequest
	{
		public string CardNumber { get; set; } = string.Empty;
	}

	[ApiController]
	[Route("api/given-card-related-cost")]
	[Authorize]
	public class CardCostController : ControllerBase
	{
		private readonly IHttpClientFactory _httpClientFactory;
		private readonly IClearingCostService _clearingCostService;
		private readonly ILogger<CardCostController> _logger;

		public CardCostController(
			IHttpClientFactory httpClientFactory,
			IClearingCostService clearingCostService,
			ILogger<CardCostController> logger)
		{
			_httpClientFactory = httpClientFactory;
			_clearingCostService = clearingCostService;
			_logger = logger;
		}

		[HttpPost]
		public async Task<IActionResult> GetCardRelatedCost([FromBody] CardLookupRequest payload, CancellationToken ct)
		{
			if (string.IsNullOrWhiteSpace(payload.CardNumber))
				return BadRequest(new { error = "cardNumber is required" });

			var client = _httpClientFactory.CreateClient();
			var req = new HttpRequestMessage(HttpMethod.Get, $"https://lookup.binlist.net/{payload.CardNumber}");
			req.Headers.Add("Accept-Version", "3");

			try
			{
				_logger.LogInformation("Sending request to Binlist for cardNumber: {CardNumber}", payload.CardNumber);

				using var resp = await client.SendAsync(req, ct);
				if (!resp.IsSuccessStatusCode)
				{
					_logger.LogWarning("Binlist returned {Status} for card {CardNumber}", resp.StatusCode, payload.CardNumber);
					return BadRequest(new { error = "Invalid card number" });
				}

				await using var stream = await resp.Content.ReadAsStreamAsync(ct);
				using var doc = await JsonDocument.ParseAsync(stream, cancellationToken: ct);

				if (!doc.RootElement.TryGetProperty("country", out var countryEl) ||
					!countryEl.TryGetProperty("alpha2", out var alpha2El) ||
					alpha2El.ValueKind != JsonValueKind.String)
				{
					return BadRequest(new { error = "Country not found" });
				}

				var countryCode = alpha2El.GetString();
				if (string.IsNullOrWhiteSpace(countryCode))
					return BadRequest(new { error = "Country not found" });

				var costEntry =
					await _clearingCostService.GetByCountryCodeAsync(countryCode) ??
					await _clearingCostService.GetByCountryCodeAsync("OTHERS");

				if (costEntry is null)
					return StatusCode(500, new { error = "No cost configuration found" });

				return Ok(new { country = countryCode, cost = costEntry.Cost });
			}
			catch (HttpRequestException ex)
			{
				_logger.LogError(ex, "HTTP error when calling Binlist");
				return BadRequest(new { error = "Invalid card number" });
			}
		}
	}
}
