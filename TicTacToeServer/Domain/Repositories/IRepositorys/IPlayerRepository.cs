using TicTacToeServer.Domain.Entitys;

namespace TicTacToeServer.Domain.Repositorys.IRepositorys
{
	public interface IPlayerRepository : IRepository<PlayerEntity>
	{
		PlayerEntity GetByConnectionId(string connectionId);
		PlayerEntity Create();
		PlayerEntity Create(string connectionId);
	}
}