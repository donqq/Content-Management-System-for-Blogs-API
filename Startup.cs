/* Copyright (C) 2021/January Badde Liyanage Don Dilanga ( github@dilanga.com ) - All Rights Reserved
 * You may use, distribute and modify this code under the
 * terms of the Simple non code license (SNCL)
 * https://tldrlegal.com/license/simple-non-code-license-(sncl)
 * You should have received a copy of the Simple non code license (SNCL) with
 * this file. If not, please write to: github@dilanga.com , or visit : https://github.com/donqq/Content-Management-System-for-Blogs
 */

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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using contentMngApp.api.Data;
using contentMngApp.api.DataTransferObject;

namespace contentMngApp.api
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
            services.AddScoped<IDeserializeToken, DeserializeToken>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IPlagiarismChecking, PlagiarismChecking>();
            services.AddScoped<IXMLtoJSON, XMLtoJSON>();
            services.AddCors();
            services.AddHttpClient();
            services.AddControllers();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
                options.Authority = "https://securetoken.google.com/firebase_project_id";
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidIssuer = "https://securetoken.google.com/firebase_project_id",
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = "firebase_project_id",
                    ValidateLifetime = true
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(corspolicy => corspolicy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            //app.UseHsts();
            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
