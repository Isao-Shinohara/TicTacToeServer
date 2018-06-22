using TicTacToeServer.Entitys;
using TicTacToeServer.Infrastructures;
using TicTacToeServer.Repositorys.IRepositorys;

namespace TicTacToeServer.Repositorys
{
	public class EFRepository<T> where T : Entity
	{
		protected readonly EFContext efContext;

		public EFRepository(EFContext efContext)
		{
			this.efContext = efContext;
		}

		public void Save()
		{
			efContext.SaveChanges();
		}

		public void Remove(T entiry)
		{
			efContext.Remove(entiry);
		}
	}
}
