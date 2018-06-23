using Microsoft.Extensions.Caching.Distributed;
using TicTacToeServer.Domain.Entitys;

namespace TicTacToeServer.Domain.Repositorys.Redis
{
	public class RedisPlayerRepository : RedisRepository<PlayerEntity>
	{
		public RedisPlayerRepository(IDistributedCache cache) : base(cache)
		{
		}

		public PlayerEntity GetByConnectionId(string connectionId)
		{
			var player = Get(nameof(connectionId), connectionId);
			return player;
		}

		public PlayerEntity Create()
		{
			var ai = GetByConnectionId(PlayerEntity.AIConnectionId);
			if (ai != null) return ai;

			ai = new PlayerEntity();
			SetPrimaryId(ai);

			Set(nameof(ai.ConnectionId), ai.ConnectionId, ai);
			return ai;
		}

		public PlayerEntity Create(string connectionId)
		{
			var player = new PlayerEntity(connectionId);
			SetPrimaryId(player);
			Set(nameof(connectionId), connectionId, player);
			return player;
		}

		public void Save(PlayerEntity player)
		{
			Set(nameof(player.ConnectionId), player.ConnectionId, player);
		}

		public void Remove(PlayerEntity player)
		{
			Remove(nameof(player.ConnectionId), player.ConnectionId);
		}
	}
}
