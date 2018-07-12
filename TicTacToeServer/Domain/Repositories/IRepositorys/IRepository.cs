using TicTacToeServer.Domain.Entitys;

namespace TicTacToeServer.Domain.Repositorys.IRepositorys
{
	public interface IRepository<T> where T : Entity
	{
		void Remove(T entity);
		void Save(T entity);
	}
}
