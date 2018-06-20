using TicTacToeServer.Entitys;

namespace TicTacToeServer.Repositorys.IRepositorys
{
	public interface IRepository<T> where T : Entity
	{
		void Save();
		void Remove(T entiry);
	}
}
