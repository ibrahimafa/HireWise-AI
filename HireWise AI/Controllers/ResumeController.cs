using HireWise_AI.DTOs.Resumes;
using HireWise_AI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HireWise_AI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ResumeController : ControllerBase
	{
		private readonly IResumeService _resumeService;

		public ResumeController(IResumeService resumeService)
		{
			_resumeService = resumeService;
		}

		[HttpPost]
		public async Task<IActionResult> Upload([FromForm] UploadResumeDto dto)
		{
			await _resumeService.UploadAsync(dto);
			return Ok(new { message = "Resume uploaded successfully" });
		}
	}
}
