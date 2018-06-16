using System;

namespace TicTacToeServer.Entitys
{
	public class PlayerEntity
	{
		public long Id { get; private set; }
		public string ConnectionId { get; private set; }

		public PlayerEntity(string connectionId)
		{
			ConnectionId = connectionId;
		}
	}
}
