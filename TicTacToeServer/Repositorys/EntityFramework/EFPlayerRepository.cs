using System;
using System.Linq;
using TicTacToeServer.Entitys;
using TicTacToeServer.Infrastructures;
using TicTacToeServer.Repositorys.IRepositorys;

namespace TicTacToeServer.Repositorys
{
	public class EFPlayerRepository : EFRepository<PlayerEntity>, IPlayerRepository
	{
		public EFPlayerRepository(EFContext efContext) : base(efContext)
		{
		}

		public PlayerEntity GetById(int id)
		{
			return efContext.PlayerSet.FirstOrDefault(x => x.Id == id);
		}

		public PlayerEntity GetByConnectionId(string connectionId)
		{
			return efContext.PlayerSet.FirstOrDefault(x => x.ConnectionId == connectionId);
		}

		public PlayerEntity Create()
		{
			var player = new PlayerEntity();
			efContext.Update(player);
			return player;
		}

		public PlayerEntity Create(string connectionId)
		{
			var player = new PlayerEntity(connectionId);
			efContext.Update(player);
			return player;
		}
	}
}
