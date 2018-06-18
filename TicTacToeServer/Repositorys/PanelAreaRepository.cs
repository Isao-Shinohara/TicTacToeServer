using System.Collections.Generic;
using System.Linq;
using TicTacToeServer.Entitys;
using TicTacToeServer.Infrastructures;

namespace TicTacToeServer.Repositorys
{
	public class PanelAreaRepository : Repository<PanelAreaEntity>
	{
		public PanelAreaRepository(SignalRContext signalRContext) : base(signalRContext)
		{
		}

		public List<PanelAreaEntity> GetByRoomId(int roomId)
		{
			return signalRContext.PanelAreaSet.Where(x => x.RoomEntity.Id == roomId).ToList();
		}
	}
}
