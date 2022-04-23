using ControllerPenalCodes.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ControllerPenalCodes.Services;
using ControllerPenalCodes.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using ControllerPenalCodes.Interfaces.RepositoryInterfaces;
using ControllerPenalCodes.Interfaces.ServiceInterfaces;
using System.Reflection;
using System;
using System.IO;

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
			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.EnableAnnotations();

				c.SwaggerDoc("v1", new OpenApiInfo { Title = "ControllerPenalCodes", Version = "v1" });

				AddSwaggerConfigurationJwtBearer(c);

				AddSwaggerResponsesDocumentations(c);
			});
			AddDatabaseConfiguration(services);
			AddScopeds(services);
			AddJwtBearerAuthentication(services);
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DBContext context)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ControllerPenalCodes v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthentication();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			context.Database.Migrate();
		}

		private void AddDatabaseConfiguration(IServiceCollection services)
		{
			services.AddDbContext<DBContext>(options =>
			{
				var mySqlConnection = Configuration.GetConnectionString("MySqlDB");
				options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection));
			});
		}

		private void AddScopeds(IServiceCollection services)
		{
			services.AddScoped<ICriminalCodeRepository, CriminalCodeRepository>();
			services.AddScoped<IStatusRepository, StatusRepository>();
			services.AddScoped<IUserRepository, UserRepository>();

			services.AddScoped<ICriminalCodeService, CriminalCodeService>();
			services.AddScoped<IStatusService, StatusService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<ILoginService, LoginService>();
		}

		private void AddJwtBearerAuthentication(IServiceCollection services)
		{
			Authentication authentication = new Authentication();

			services.AddSingleton(authentication);

			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = false;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = authentication.Key,
					ValidateIssuer = false,
					ValidateAudience = false
				};
			});
		}

		private void AddSwaggerConfigurationJwtBearer(SwaggerGenOptions swaggerGenOptions)
		{
			var openApiSecurityScheme = new OpenApiSecurityScheme
			{
				Name = "Authorization",
				BearerFormat = "JWT",
				Scheme = "bearer",
				Description = "",
				In = ParameterLocation.Header,
				Type = SecuritySchemeType.Http
			};

			swaggerGenOptions.AddSecurityDefinition("Bearer", openApiSecurityScheme);

			var security = new OpenApiSecurityRequirement
			{
				{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type = ReferenceType.SecurityScheme,
							Id = "Bearer"
						}
					},
					new string[]
					{

					}
				}
			};

			swaggerGenOptions.AddSecurityRequirement(security);
		}

		private void AddSwaggerResponsesDocumentations(SwaggerGenOptions swaggerGenOptions)
		{
			var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
			var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
			swaggerGenOptions.IncludeXmlComments(xmlPath);
		} 
	}
}
