using System;

namespace TicTacToeServer.Entitys
{
	public class PlayerEntity : Entity
	{
		public string ConnectionId { get; private set; }

		public PlayerEntity(string connectionId)
		{
			ConnectionId = connectionId;
		}
	}
}
