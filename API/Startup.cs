using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
using API.Middleware;
using API.SignalR;
using Application;
using Application.Activities;
using Application.Core;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Persistence;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
            // Configuration = configuration; // old way
        }

        // public IConfiguration Configuration { get; } // old way

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers(opt => 
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
                // means every single endpoint from the app requires authorization
                // unless we set it otherwise
            })
            .AddFluentValidation(config => {

                config.RegisterValidatorsFromAssemblyContaining<Create>();
            });
            services.AddApplicationServices(_config);
            services.AddIdentityServices(_config);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseXContentTypeOptions();
            app.UseReferrerPolicy(opt => opt.NoReferrer());
            app.UseXXssProtection(opt => opt.EnabledWithBlockMode());
            app.UseXfo(opt => opt.Deny());

            //usecsp iti poate crapa fb pe local daca nu sunt policy urile setate, foloseste usecspreporting si adauga policy urile
            app.UseCsp(opt => opt
            .BlockAllMixedContent()
            .StyleSources(s => s.Self().CustomSources(
                "https://fonts.googleapis.com/",
                "sha256-yChqzBduCCi4o4xdbXRXh4U/t1rP4UUUMJt+rB+ylUI=",
                "sha256-r3x6D0yBZdyG8FpooR5ZxcsLuwuJ+pSQ/80YzwXS5IU="))
            .FontSources(s => s.Self().CustomSources(
                "https://fonts.gstatic.com",
                "data:"))
            .FormActions(s => s.Self())
            .FrameAncestors(s => s.Self())
            .ImageSources(s => s.Self().CustomSources(
                "https://res.cloudinary.com",
                "https://www.facebook.com", 
                "data:", 
                "https://platform-lookaside.fbsbx.com"))
            .ScriptSources(s => s.Self().CustomSources(
                "sha256-Vc2vYH2FgDRnl6+I1cQZWRBn/goi99XlJKDlfY8EqtM=",
                "https://connect.facebook.net", 
                "sha256-2taMchLhAg9J4LO0BiT1qneM7fWDDZe7AnAwIpJnGkA="))
            

            
            );

            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }
            else
            {
                app.Use(async (context, next) => {
                    context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000");
                    await next.Invoke();
                });

            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseCors("CorsPolicy");
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chat");
                endpoints.MapFallbackToController("Index", "Fallback");
            });
        }
    }
}
