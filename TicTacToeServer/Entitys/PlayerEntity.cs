namespace TicTacToeServer.Entitys
{
	public class PlayerEntity : Entity
	{
		public string ConnectionId { get; private set; }
		public int RoomId { get; private set; }

		public PlayerEntity()
		{
			ConnectionId = "auto";
		}
		public PlayerEntity(string connectionId)
		{
			ConnectionId = connectionId;
		}

		public void SetRoomId(int roomId)
		{
			RoomId = roomId;
		}
	}
}
