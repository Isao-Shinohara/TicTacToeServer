using Microsoft.EntityFrameworkCore;
using TicTacToeServer.Domain.Entitys;

namespace TicTacToeServer.Domain.Infrastructures
{
	public class EFContext : DbContext
	{
		public EFContext(DbContextOptions<EFContext> options) : base(options)
		{
		}

		public DbSet<PlayerEntity> PlayerSet { get; set; }
		public DbSet<RoomEntity> RoomSet { get; set; }
		public DbSet<PanelAreaEntity> PanelAreaSet { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		}
	}
}