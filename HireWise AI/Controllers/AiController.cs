using HireWise_AI.Data;
using HireWise_AI.DTOs.AI.AnalyzeResume;
using HireWise_AI.DTOs.AI.InterviewQuestions;
using HireWise_AI.DTOs.AI.MatchCandidate;
using HireWise_AI.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HireWise_AI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AiController : ControllerBase
	{
		private readonly AppDbContext _context;
		private readonly IAiService _aiService;

		public AiController(AppDbContext context, IAiService aiService)
		{
			_context = context;
			_aiService = aiService;
		}

		[HttpPost("analyze-resume")]
		public async Task<IActionResult> AnalyzeResume(AnalyzeResumeRequestDto dto)
		{
			var resume = await _context.Resumes
				.AsNoTracking()
				.FirstOrDefaultAsync(r => r.Id == dto.ResumeId);

			if (resume == null)
				return NotFound(new { message = "Resume not found" });

			if (string.IsNullOrWhiteSpace(resume.ExtractedText))
				return BadRequest(new { message = "Resume text is empty" });

			var result = await _aiService.AnalyzeResumeAsync(resume.ExtractedText);

			return Ok(result);
		}
		// Controllers/AiController.cs
		[HttpPost("match-candidate")]
		public async Task<IActionResult> MatchCandidate([FromBody] MatchCandidateRequestDto request)
		{
			var result = await _aiService.MatchCandidateWithJobAsync(request);
			return Ok(result);
		}


		[HttpPost("generate-interview-questions")]
		public async Task<IActionResult> GenerateInterviewQuestions([FromBody] GenerateInterviewQuestionsRequestDto request)
		{
			var result = await _aiService.GenerateInterviewQuestionsAsync(request);
			return Ok(result);
		}
	}
}
