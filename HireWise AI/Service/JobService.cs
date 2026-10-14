using AutoMapper;
using HireWise_AI.Data;
using HireWise_AI.DTOs.Jobs;
using HireWise_AI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HireWise_AI.Service
{
	public class JobService : IJobService
	{
		AppDbContext context;
		IMapper mapper;
		public JobService(AppDbContext _context,IMapper _mapper) 
		{
			context = _context;
			mapper = _mapper;
		}


		public async Task AddAsync(CreateJobDto createJobDto)
		{
			var entity = mapper.Map<Job>(createJobDto);
			entity.CreatedAt = DateTime.Now;

			await context.Jobs.AddAsync(entity);
			await context.SaveChangesAsync();


		}

		public async Task<List<JobResponseDto>> GetAllJobsAsync()
			{
				var jobs = await context.Jobs
					.AsNoTracking()
					.OrderByDescending(j => j.CreatedAt)
					.ToListAsync();

				var jobResponseDtos = jobs.Select(c => new JobResponseDto
				{
					Id = c.Id,
					Title = c.Title,
					Department = c.Department,
					Description = c.Description,
					Requirements = c.Requirements,
					MinYearsExperience = c.MinYearsExperience,
					Location = c.Location,
					EmploymentType = c.EmploymentType,
					CreatedAt = c.CreatedAt,
				}).ToList();

				return jobResponseDtos;
			}

		public async Task<JobResponseDto?> GetByIdAsync(int id)
		{
			var jod = await context.Jobs.AsNoTracking().FirstOrDefaultAsync(j => j.Id == id);

			if (jod == null)
				return null;

			return new JobResponseDto
			{
				Id = jod.Id,
				Title = jod.Title,
				Department = jod.Department,
				Description = jod.Description,
				Requirements = jod.Requirements,
				MinYearsExperience = jod.MinYearsExperience,
				Location = jod.Location,
				EmploymentType = jod.EmploymentType,
				CreatedAt = jod.CreatedAt
			};

		}
	}

}	

