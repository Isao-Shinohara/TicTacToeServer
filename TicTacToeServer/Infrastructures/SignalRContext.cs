using Microsoft.EntityFrameworkCore;
using TicTacToeServer.Entitys;

namespace TicTacToeServer.Infrastructures
{
	public class SignalRContext : DbContext
	{
		public SignalRContext(DbContextOptions<SignalRContext> options) : base(options)
		{
		}

		public DbSet<PlayerEntity> PlayerSet { get; set; }
		public DbSet<RoomEntity> RoomSet { get; set; }
		public DbSet<PanelAreaEntity> PanelAreaSet { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<PanelAreaEntity>()
			            .HasOne(p => p.RoomEntity)
			            .WithMany(b => b.PanelAreaList);
		}
	}
}