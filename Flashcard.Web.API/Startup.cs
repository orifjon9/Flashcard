using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Flashcard.Models.Context;
using Flashcard.Repositories;
using Flashcard.Repositories.Persistence;
using Flashcard.Services.Interfaces;
using Flashcard.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Flashcard.Web.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddDbContext<FlashcardContext>(opt => opt.UseInMemoryDatabase("Flashcard"));
			var configurationService = new ConfigurationService(Configuration["Jwt:Key"], Configuration["Jwt:Issuer"]);

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(confgOptions =>
				{
					confgOptions.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = configurationService.Issuer,
						ValidAudience = configurationService.Issuer,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurationService.SecurityKey))
					};
					confgOptions.Events = new JwtBearerEvents
					{
						OnAuthenticationFailed = context =>
						{
							Console.WriteLine("OnAuthenticationFailed: " +
								context.Exception.Message);
							return Task.CompletedTask;
						},
						OnTokenValidated = context =>
						{
							Console.WriteLine("OnTokenValidated: " +
								context.SecurityToken);
							return Task.CompletedTask;
						}
					};
				});

			services.AddSingleton<IConfigurationService>(configurationService);
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<ICardRepository, CardRepository>();
			services.AddScoped<ICardService, CardService>();

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

			services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

			app.UseAuthentication();
            // app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
