﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WebApi
{
	using Microsoft.EntityFrameworkCore;

	using Newtonsoft.Json.Serialization;

	using WebApi.Models;

	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			this.Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion( CompatibilityVersion.Version_2_1 ).AddJsonOptions(
			                                                                                             options =>
				                                                                                             {
					                                                                                             var resolver = options.SerializerSettings.ContractResolver;
					                                                                                             if ( resolver != null )
					                                                                                             {
						                                                                                             ( resolver as DefaultContractResolver ).NamingStrategy = null;
					                                                                                             }
				                                                                                             } );
			services.AddDbContext<PaymentDetailContext>( options => options.UseSqlServer( this.Configuration.GetConnectionString( "DevConnection" ) ) );

			services.AddCors();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseCors( options => options.WithOrigins( "http://localhost:4200" ).AllowAnyMethod().AllowAnyHeader() );

			app.UseMvc();
		}
	}
}
