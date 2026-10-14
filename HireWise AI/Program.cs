
using HireWise_AI.Data;
using HireWise_AI.Interfaces;
using HireWise_AI.Service;

namespace HireWise_AI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddDbContext<AppDbContext>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

			builder.Services.AddScoped<IJobService,JobService>();
            builder.Services.AddScoped<ICandidateService,CandidateService>();
            builder.Services.AddScoped<IPdfService,PdfService>();
            builder.Services.AddScoped<IResumeService ,ResumeService>();
			builder.Services.AddScoped<IAiService, AiService>();


			builder.Services.Configure<GeminiSettings>(
				builder.Configuration.GetSection("Gemini"));

			builder.Services.AddHttpClient();


			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
			app.UseMiddleware<HireWise_AI.Middleware.ExceptionHandlingMiddleware>(); 

			app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
