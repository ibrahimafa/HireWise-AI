using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace HireWise_AI.Data
{
	public class AppDbContext:DbContext
	{
		IConfiguration config;
		public AppDbContext(IConfiguration _config)
		{
			config = _config;
		}
		public DbSet<Candidate> Candidates { get; set; }
		public DbSet<Resume> Resumes { get; set; }
		public DbSet<Job> Jobs { get; set; }
		public DbSet<Skill> Skills { get; set; }
		public DbSet<CandidateSkill> CandidateSkills { get; set; }
		public DbSet<JobSkill> JobSkills { get; set; }
		public DbSet<MatchResult> MatchResults { get; set; }
		public DbSet<InterviewQuestion> InterviewQuestions { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(config.GetConnectionString("ApplicationDbContext"));
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<CandidateSkill>()
				.HasOne(cs => cs.Candidate)
				.WithMany(c => c.CandidateSkills)
				.HasForeignKey(cs => cs.CandidateId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<CandidateSkill>()
				.HasOne(cs => cs.Skill)
				.WithMany(s => s.CandidateSkills)
				.HasForeignKey(cs => cs.SkillId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<JobSkill>()
				.HasOne(js => js.Job)
				.WithMany(j => j.JobSkills)
				.HasForeignKey(js => js.JobId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<JobSkill>()
				.HasOne(js => js.Skill)
				.WithMany(s => s.JobSkills)
				.HasForeignKey(js => js.SkillId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Resume>()
				.HasOne(r => r.Candidate)
				.WithMany(c => c.Resumes)
				.HasForeignKey(r => r.CandidateId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<MatchResult>()
				.HasOne(m => m.Candidate)
				.WithMany(c => c.MatchResults)
				.HasForeignKey(m => m.CandidateId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<MatchResult>()
				.HasOne(m => m.Job)
				.WithMany(j => j.MatchResults)
				.HasForeignKey(m => m.JobId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<InterviewQuestion>()
				.HasOne(i => i.Candidate)
				.WithMany(c => c.InterviewQuestions)
				.HasForeignKey(i => i.CandidateId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<InterviewQuestion>()
				.HasOne(i => i.Job)
				.WithMany(j => j.InterviewQuestions)
				.HasForeignKey(i => i.JobId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}


}
