using HireWise_AI.DTOs.Candidates;
using HireWise_AI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HireWise_AI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CandidateController : ControllerBase
	{
		ICandidateService candidateService;
		public CandidateController(ICandidateService _candidateService) 
		{
			candidateService = _candidateService;
		}

		[HttpPost]
		public async Task<IActionResult> Add(CreateCandidateDto createCandidateDto)
		{
			await candidateService.AddAsync(createCandidateDto);
			return Ok(new { message = "Candidate created successfully" });
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
		    var candidate =	await candidateService.GetAllCandidateAsync();
			return Ok(candidate);
		}
	}
}
