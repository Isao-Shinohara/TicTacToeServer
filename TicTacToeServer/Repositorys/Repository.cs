using TicTacToeServer.Entitys;
using TicTacToeServer.Infrastructures;

namespace TicTacToeServer.Repositorys
{
	public class Repository<T> where T : Entity
	{
		public SignalRContext signalRContext;

		public Repository(SignalRContext signalRContext)
		{
			this.signalRContext = signalRContext;
		}

		public void Save()
		{
			signalRContext.SaveChanges();
		}

		public void Remove(T entiry)
		{
			signalRContext.Remove(entiry);
		}
	}
}
