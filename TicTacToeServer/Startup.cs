using System;
using MessagePack.AspNetCoreMvcFormatter;
using MessagePack.ImmutableCollection;
using MessagePack.ReactivePropertyExtension;
using MessagePack.Resolvers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using TicTacToeServer.Application.Hubs;
using TicTacToeServer.Application.Services;
using TicTacToeServer.Domain.Infrastructures;
using TicTacToeServer.Domain.Repositorys.IRepositorys;
using TicTacToeServer.Domain.Repositorys.Redis;

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

			services.AddSignalR().AddRedis(string.Format("{0}:{1}", Configuration["Redis:Host"], Configuration["Redis:Port"]));

			services.AddCors(options => {
				options.AddPolicy("MyPolicy", builder => {
					builder.AllowAnyOrigin()
						   .AllowAnyMethod()
						   .AllowAnyHeader();
				});
			});

			services.AddDistributedRedisCache(option =>
			{
				option.Configuration = string.Format("{0}:{1}", Configuration["Redis:Host"], Configuration["Redis:Port"]);
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

			// Dependency Injection.
			services.AddTransient<IPlayerRepository, RedisPlayerRepository>();
			services.AddTransient<IRoomRepository, RedisRoomRepository>();
			services.AddTransient<AppService, AppService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			Console.WriteLine(string.Format("Redis: {0}:{1}", Configuration["Redis:Host"], Configuration["Redis:Port"]));

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
			app.UseRouting();
			app.UseDefaultFiles();

			app.UseEndpoints(routes => {
				routes.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
			});

			app.UseEndpoints(route => {
				route.MapHub<SignalRHub>("/signalr");
			});
		}
	}
}
