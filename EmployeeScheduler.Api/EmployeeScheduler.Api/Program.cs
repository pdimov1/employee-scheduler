using Microsoft.EntityFrameworkCore;
using EmployeeScheduler.Application.Context;
using EmployeeScheduler.Application.Services;
using EmployeeScheduler.Infrastructure;

namespace EmployeeScheduler.Api
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
            builder.Services.AddHealthChecks();

            builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IShiftService, ShiftService>();
            builder.Services.AddScoped<IRoleService, RoleService>();

            var app = builder.Build();

            app.MapHealthChecks("/healthz");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
