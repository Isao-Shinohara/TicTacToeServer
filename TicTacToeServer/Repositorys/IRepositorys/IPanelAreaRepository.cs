using System.Collections.Generic;
using TicTacToeServer.Entitys;

namespace TicTacToeServer.Repositorys.IRepositorys
{
	public interface IPanelAreaRepository
	{
		List<PanelAreaEntity> GetByRoomId(int roomId);
	}
}
