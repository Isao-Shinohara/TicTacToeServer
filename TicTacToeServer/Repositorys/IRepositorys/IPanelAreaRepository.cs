using System.Collections.Generic;
using TicTacToeServer.Entitys;

namespace TicTacToeServer.Repositorys.IRepositorys
{
	public interface IPanelAreaRepository : IRepository<PanelAreaEntity>
	{
		List<PanelAreaEntity> GetByRoomId(int roomId);
	}
}
