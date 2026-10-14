using HireWise_AI.Data;
using HireWise_AI.DTOs.AI.AnalyzeResume;
using HireWise_AI.DTOs.AI.InterviewQuestions;
using HireWise_AI.DTOs.AI.MatchCandidate;
using HireWise_AI.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace HireWise_AI.Service
{
	public class AiService : IAiService
	{
		private readonly HttpClient _httpClient;
		private readonly GeminiSettings _settings;
		private readonly AppDbContext _context;
		private readonly IConfiguration _configuration;

		public AiService(HttpClient httpClient, IOptions<GeminiSettings> options , AppDbContext context, IConfiguration configuration)
		{
			_httpClient = httpClient;
			_settings = options.Value;
			_context = context;
			_configuration = configuration;

		}

		public async Task<ResumeAnalysisResponseDto> AnalyzeResumeAsync(string resumeText)
		{
			var prompt =
				"You are an expert technical recruiter.\n" +
				"Analyze the following resume text and return ONLY valid JSON.\n" +
				"Do not add markdown.\n" +
				"Do not add explanation.\n" +
				"Do not wrap in ```.\n\n" +
				"Required JSON shape:\n" +
				"{\n" +
				"  \"fullName\": \"string\",\n" +
				"  \"currentTitle\": \"string\",\n" +
				"  \"yearsOfExperience\": 0,\n" +
				"  \"skills\": [\"string\"],\n" +
				"  \"strengths\": [\"string\"],\n" +
				"  \"weaknesses\": [\"string\"],\n" +
				"  \"summary\": \"string\"\n" +
				"}\n\n" +
				"Resume text:\n" + resumeText;

			var requestBody = new
			{
				contents = new[]
				{
					new
					{
						parts = new[]
						{
							new { text = prompt }
						}
					}
				}
			};

			var requestJson = JsonSerializer.Serialize(requestBody);

			var url =
				$"https://generativelanguage.googleapis.com/v1beta/models/{_settings.Model}:generateContent?key={_settings.ApiKey}";

			var response = await _httpClient.PostAsync(
				url,
				new StringContent(requestJson, Encoding.UTF8, "application/json"));

			var responseContent = await response.Content.ReadAsStringAsync();

			if (!response.IsSuccessStatusCode)
				throw new Exception($"Gemini API Error: {responseContent}");

			using var jsonDoc = JsonDocument.Parse(responseContent);

			var outputText = jsonDoc.RootElement
				.GetProperty("candidates")[0]
				.GetProperty("content")
				.GetProperty("parts")[0]
				.GetProperty("text")
				.GetString();

			if (string.IsNullOrWhiteSpace(outputText))
				throw new Exception("Gemini returned empty response.");

			var result = JsonSerializer.Deserialize<ResumeAnalysisResponseDto>(
				outputText,
				new JsonSerializerOptions
				{
					PropertyNameCaseInsensitive = true
				});

			if (result == null)
				throw new Exception("Failed to parse Gemini response.");

			return result;
		}


		// Service/AiService.cs
		public async Task<MatchResultResponseDto> MatchCandidateWithJobAsync(MatchCandidateRequestDto request)
		{
			// 1. جيب الـ Candidate + Resume من الـ DB
			var candidate = await _context.Candidates
				.Include(c => c.Resumes)
				.FirstOrDefaultAsync(c => c.Id == request.CandidateId);

			if (candidate == null)
				throw new KeyNotFoundException("Candidate not found.");

			var resume = candidate.Resumes.FirstOrDefault();
			if (resume == null || string.IsNullOrEmpty(resume.ExtractedText))
				throw new KeyNotFoundException("No resume or extracted text found for this candidate.");

			// 2. جيب الـ Job من الـ DB
			var job = await _context.Jobs
				.FirstOrDefaultAsync(j => j.Id == request.JobId);

			if (job == null)
				throw new KeyNotFoundException("Job not found.");

			// 3. ابني الـ Prompt
			var prompt = $$"""
				You are an expert HR recruiter AI. Analyze the match between the candidate and the job.

				## Candidate Resume:
				{{resume.ExtractedText}}

				## Job Title:
				{{job.Title}}

				## Job Description:
				{{job.Description}}

				Respond ONLY with valid JSON in this exact format, no extra text:
				{
				  "matchScore": <integer from 0 to 100>,
				  "strengths": ["strength1", "strength2"],
				  "weaknesses": ["weakness1", "weakness2"],
				  "missingSkills": ["skill1", "skill2"],
				  "recommendation": "<brief recommendation string>"
				}
				""";

			// 4. ابعت على Gemini
			var requestBody = new
			{
				contents = new[]
				{
			new { parts = new[] { new { text = prompt } } }
		}
			};

			var jsonContent = new StringContent(
				JsonSerializer.Serialize(requestBody),
				Encoding.UTF8,
				"application/json"
			);

			var apiKey = _configuration["Gemini:ApiKey"];
			var model = _configuration["Gemini:Model"];
			var url = $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent?key={apiKey}";

			var response = await _httpClient.PostAsync(url, jsonContent);
			response.EnsureSuccessStatusCode();

			var responseString = await response.Content.ReadAsStringAsync();

			// 5. استخرج الـ JSON من رد Gemini
			using var doc = JsonDocument.Parse(responseString);
			var text = doc.RootElement
				.GetProperty("candidates")[0]
				.GetProperty("content")
				.GetProperty("parts")[0]
				.GetProperty("text")
				.GetString();

			// 6. نظّف الـ JSON لو فيه backticks
			var cleanJson = text!
				.Replace("```json", "")
				.Replace("```", "")
				.Trim();

			var result = JsonSerializer.Deserialize<MatchResultResponseDto>(cleanJson, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});

			// 7. احفظ الـ MatchResult في الـ DB
			var matchResult = new MatchResult
			{
				CandidateId = request.CandidateId,
				JobId = request.JobId,
				MatchScore = result!.MatchScore,
				Recommendation = result.Recommendation,
				CreatedAt = DateTime.UtcNow
			};

			_context.MatchResults.Add(matchResult);
			await _context.SaveChangesAsync();

			return result;
		}


		public async Task<InterviewQuestionsResponseDto> GenerateInterviewQuestionsAsync(GenerateInterviewQuestionsRequestDto request)
		{
			var candidate = await _context.Candidates
				.Include(c => c.Resumes)
				.FirstOrDefaultAsync(c => c.Id == request.CandidateId);

			if (candidate == null)
				throw new Exception("Candidate not found.");

			var resume = candidate.Resumes.FirstOrDefault();
			if (resume == null || string.IsNullOrEmpty(resume.ExtractedText))
				throw new Exception("No resume or extracted text found for this candidate.");

			var job = await _context.Jobs
				.FirstOrDefaultAsync(j => j.Id == request.JobId);

			if (job == null)
				throw new Exception("Job not found.");

			var prompt = $$"""
        You are an expert technical interviewer. Generate interview questions for this candidate based on the job.

        ## Candidate Resume:
        {{resume.ExtractedText}}

        ## Job Title:
        {{job.Title}}

        ## Job Description:
        {{job.Description}}

        Respond ONLY with valid JSON in this exact format, no extra text:
        {
          "questions": [
            "Question 1?",
            "Question 2?",
            "Question 3?"
          ]
        }
        """;

			var cleanJson = await SendToGeminiAsync(prompt);

			var result = JsonSerializer.Deserialize<InterviewQuestionsResponseDto>(cleanJson, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true
			});

			return result!;
		}

		// Add this private helper method to AiService to fix CS0103
		private async Task<string> SendToGeminiAsync(string prompt)
		{
			var apiKey = _configuration["Gemini:ApiKey"];
			var model = _configuration["Gemini:Model"];
			var url = $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent?key={apiKey}";

			var requestBody = new
			{
				contents = new[]
				{
					new { parts = new[] { new { text = prompt } } }
				}
			};

			var jsonContent = new StringContent(
				JsonSerializer.Serialize(requestBody),
				Encoding.UTF8,
				"application/json"
			);

			var response = await _httpClient.PostAsync(url, jsonContent);
			response.EnsureSuccessStatusCode();

			var responseString = await response.Content.ReadAsStringAsync();

			using var doc = JsonDocument.Parse(responseString);
			var text = doc.RootElement
				.GetProperty("candidates")[0]
				.GetProperty("content")
				.GetProperty("parts")[0]
				.GetProperty("text")
				.GetString();

			// Clean up any backticks or markdown formatting
			var cleanJson = text!
				.Replace("```json", "")
				.Replace("```", "")
				.Trim();

			return cleanJson;
		}
	}
}