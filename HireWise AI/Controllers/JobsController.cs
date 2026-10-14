using HireWise_AI.Data;
using HireWise_AI.DTOs.Candidates;
using HireWise_AI.DTOs.Jobs;
using HireWise_AI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HireWise_AI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class JobsController : ControllerBase
	{
		IJobService jobService;
		public JobsController(IJobService _jobService) 
		{
			jobService = _jobService;
		}

		[HttpPost]
		public async Task <IActionResult> AddAsync(CreateJobDto createJobDto)
		{
			await jobService.AddAsync(createJobDto);
			return Ok(new { message = "Job created successfully" });
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var jobs = await jobService.GetAllJobsAsync();
			return Ok(jobs);

		}
	}
}
