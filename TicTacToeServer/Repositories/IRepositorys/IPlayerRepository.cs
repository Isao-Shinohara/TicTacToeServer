using TicTacToeServer.Entitys;

namespace TicTacToeServer.Repositorys.IRepositorys
{
	public interface IPlayerRepository : IRepository<PlayerEntity>
	{
		PlayerEntity GetById(int id);
		PlayerEntity GetByConnectionId(string connectionId);
		PlayerEntity Create();
		PlayerEntity Create(string connectionId);
	}
}