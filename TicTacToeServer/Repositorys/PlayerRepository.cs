using System;
using System.Linq;
using TicTacToeServer.Entitys;
using TicTacToeServer.Infrastructures;

namespace TicTacToeServer.Repositorys
{
	public class PlayerRepository : Repository<PlayerEntity>
	{
		public PlayerRepository(SignalRContext signalRContext) : base(signalRContext)
		{
		}

		public PlayerEntity GetById(int id)
		{
			return signalRContext.PlayerSet.FirstOrDefault(x => x.Id == id);
		}

		public PlayerEntity GetByConnectionId(string connectionId)
		{
			return signalRContext.PlayerSet.FirstOrDefault(x => x.ConnectionId == connectionId);
		}

		public PlayerEntity Create()
		{
			var player = new PlayerEntity();
			signalRContext.Update(player);
			return player;
		}

		public PlayerEntity Create(string connectionId)
		{
			var player = new PlayerEntity(connectionId);
			signalRContext.Update(player);
			return player;
		}
	}
}
