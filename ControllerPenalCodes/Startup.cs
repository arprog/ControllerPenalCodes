using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControllerPenalCodes.Interfaces;
using ControllerPenalCodes.Models.Entities;
using ControllerPenalCodes.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace ControllerPenalCodes
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			AddScopeds(services);
			services.AddDbContext<DBContext>(options =>
			{
				options.UseMySql(Configuration.GetConnectionString("MySqlDB"), new MySqlServerVersion(new Version(5, 0, 0)));
			});
			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "ControllerPenalCodes", Version = "v1" });
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ControllerPenalCodes v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}

		private void AddScopeds(IServiceCollection services)
		{
			services.AddScoped<IRepository<CriminalCode>, CriminalCodeRepository>();
			services.AddScoped<IRepository<Status>, StatusRepository>();
			services.AddScoped<IRepository<User>, UserRepository>();
		}
	}
}
