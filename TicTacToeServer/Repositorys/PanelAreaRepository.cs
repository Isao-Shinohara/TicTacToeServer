using TicTacToeServer.Entitys;
using TicTacToeServer.Infrastructures;

namespace TicTacToeServer.Repositorys
{
	public class PanelAreaRepository : Repository<PanelAreaEntity>
	{
		public PanelAreaRepository(SignalRContext signalRContext) : base(signalRContext)
		{
		}
	}
}
