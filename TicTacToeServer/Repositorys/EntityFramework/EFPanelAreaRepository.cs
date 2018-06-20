using System.Collections.Generic;
using System.Linq;
using TicTacToeServer.Entitys;
using TicTacToeServer.Infrastructures;
using TicTacToeServer.Repositorys.IRepositorys;

namespace TicTacToeServer.Repositorys
{
	public class EFPanelAreaRepository : EFRepository<PanelAreaEntity>, IPanelAreaRepository
	{
		public EFPanelAreaRepository(EFContext efContext) : base(efContext)
		{
		}

		public List<PanelAreaEntity> GetByRoomId(int roomId)
		{
			return efContext.PanelAreaSet.Where(x => x.RoomEntity.Id == roomId).ToList();
		}
	}
}
