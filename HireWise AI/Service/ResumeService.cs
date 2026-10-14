using HireWise_AI.Data;
using HireWise_AI.DTOs.Resumes;
using HireWise_AI.Interfaces;
using UglyToad.PdfPig.Content;

namespace HireWise_AI.Service
{
	public class ResumeService :IResumeService
	{
		private readonly AppDbContext _context;
		private readonly IWebHostEnvironment _env;
		private readonly IPdfService _pdfService;

		public ResumeService(AppDbContext context, IWebHostEnvironment env, IPdfService pdfService)
		{
			_context = context;
			_env = env;
			_pdfService = pdfService;
		}

		public async Task UploadAsync(UploadResumeDto dto)
		{
			var folderPath = Path.Combine(_env.WebRootPath ?? "wwwroot", "resumes");

			if (!Directory.Exists(folderPath))
				Directory.CreateDirectory(folderPath);

			var fileName = Guid.NewGuid() + Path.GetExtension(dto.File.FileName);
			var filePath = Path.Combine(folderPath, fileName);

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await dto.File.CopyToAsync(stream);
			}

			var extractedText = _pdfService.ExtractText(filePath);

			var resume = new Resume
			{
				CandidateId = dto.CandidateId,
				FileName = dto.File.FileName,
				FilePath = filePath,
				ExtractedText = extractedText,
				UploadedAt = DateTime.UtcNow
			};

			await _context.Resumes.AddAsync(resume);
			await _context.SaveChangesAsync();
		}
	}
}
