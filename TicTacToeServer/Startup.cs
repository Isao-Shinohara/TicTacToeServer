using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagePack.AspNetCoreMvcFormatter;
using MessagePack.ImmutableCollection;
using MessagePack.ReactivePropertyExtension;
using MessagePack.Resolvers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using TicTacToeServer.Hubs;
using TicTacToeServer.Infrastructures;

namespace TicTacToeServer
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
			services.AddMemoryCache();
			services.AddSignalR();
			services.AddCors(options => {
				options.AddPolicy("MyPolicy", builder => {
					builder.AllowAnyOrigin()
						   .AllowAnyMethod()
						   .AllowAnyHeader();
				});
			});

			services.AddDistributedRedisCache(option =>
			{
				option.Configuration = Configuration["Redis:Host"];
				option.InstanceName = "master";
			});

			// set extensions to default resolver.
			CompositeResolver.RegisterAndSetAsDefault(
				// enable extension packages first
				ImmutableCollectionResolver.Instance,
				ReactivePropertyResolver.Instance,
				MessagePack.Unity.Extension.UnityBlitResolver.Instance,
				MessagePack.Unity.UnityResolver.Instance,

				// finaly use standard(default) resolver
				StandardResolverAllowPrivate.Instance
			);

			//services.AddMvc();
			services.AddMvc().AddMvcOptions(options => {
				// MessagePack.
				options.FormatterMappings.SetMediaTypeMappingForFormat("msgpack", new MediaTypeHeaderValue("application/x-msgpack"));
				options.OutputFormatters.Add(new MessagePackOutputFormatter(ContractlessStandardResolver.Instance));
				options.InputFormatters.Add(new MessagePackInputFormatter(ContractlessStandardResolver.Instance));
			});

			services.AddDbContext<EFContext>(opt => opt.UseInMemoryDatabase("TicTacToeServer"));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			} else {
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles(new StaticFileOptions() {
				ServeUnknownFileTypes = true,
				DefaultContentType = "application/octet-stream"
			});

			app.UseCors("MyPolicy");

			app.UseMvc(routes => {
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});

			app.UseSignalR(route => {
				route.MapHub<SignalRHub>("/signalr");
			});
			app.UseDefaultFiles();
		}
	}
}
