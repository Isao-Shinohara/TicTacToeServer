using Microsoft.EntityFrameworkCore;
using TicTacToeServer.Entitys;
using TicTacToeServer.Models;

namespace TicTacToeServer.Infrastructures
{
	public class SignalRContext : DbContext
	{
		public SignalRContext(DbContextOptions<SignalRContext> options) : base(options)
		{
		}

		public DbSet<PlayerEntity> PlayerSet { get; set; }
		public DbSet<SignalRItem> SignalRItemSet { get; set; }
		public DbSet<PanelAreaModel> PanelAreaModelSet { get; set; }
	}
}