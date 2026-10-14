using AutoMapper;
using HireWise_AI.Data;
using HireWise_AI.DTOs.Candidates;
using HireWise_AI.Interfaces;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.EntityFrameworkCore;

namespace HireWise_AI.Service
{
	public class CandidateService : ICandidateService
	{
		AppDbContext context;
		IMapper mapper;
		public CandidateService(AppDbContext _context, IMapper _mapper)
		{
			context = _context;
			mapper = _mapper;
		}

		public async Task AddAsync(CreateCandidateDto createCandidateDto)
		{
			var create = mapper.Map<Candidate>(createCandidateDto);
			await context.Candidates.AddAsync(create);
			await context.SaveChangesAsync();
		}


		public async Task<List<CandidateResponseDto>> GetAllCandidateAsync()
		{
			var candidates = await context.Candidates
				.AsNoTracking()
				.ToListAsync();

			var candidateResponseDtos = candidates.Select(c => new CandidateResponseDto
			{
				Id = c.Id,
				FullName = c.FullName,
				Email = c.Email,
				Education = c.Education,
				PhoneNumber = c.PhoneNumber,
				CreatedAt = c.CreatedAt,
				CurrentTitle = c.CurrentTitle,
				Summary = c.Summary,
				YearsOfExperience = c.YearsOfExperience,
			}).ToList();


			return candidateResponseDtos;
		} 

	}
}
