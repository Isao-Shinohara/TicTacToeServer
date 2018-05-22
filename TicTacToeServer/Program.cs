using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TicTacToeServer
{
	public class Program
	{
		public static void Main(string[] args)
		{
			BuildWebHost(args).Run();
		}

		public static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseKestrel(a => a.Listen(IPAddress.Any, 8000, o => o.UseHttps("server.pfx", "password")))
				.UseContentRoot(Directory.GetCurrentDirectory())
				.UseStartup<Startup>()
				.Build();
	}
}
