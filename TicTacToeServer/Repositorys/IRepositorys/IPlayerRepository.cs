using TicTacToeServer.Entitys;

namespace TicTacToeServer.Repositorys.IRepositorys
{
	public interface IPlayerRepository
	{
		PlayerEntity GetById(int id);
		PlayerEntity GetByConnectionId(string connectionId);
		PlayerEntity Create();
		PlayerEntity Create(string connectionId);
	}
}