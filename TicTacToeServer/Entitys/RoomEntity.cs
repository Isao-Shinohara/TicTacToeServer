using TicTacToeServer.Cores;

namespace TicTacToeServer.Entitys
{
	public class RoomEntity : Entity
	{
		public int RoomNumber { get; private set; }
		public RoomType RoomType { get; private set; }
		public PlayerEntity _1stPlayer { get; private set; }
		public PlayerEntity _2ndPlayer { get; private set; }

		public RoomEntity()
		{
		}

		public RoomEntity(RoomType roomType, PlayerEntity _1stPlayer) : this(99999, roomType, _1stPlayer)
		{
		}

		public RoomEntity(int roomNumber, RoomType roomType, PlayerEntity _1stPlayer)
		{
			RoomNumber = roomNumber;
			RoomType = roomType;
			this._1stPlayer = _1stPlayer;
		}

		public void Set2ndPlayer(PlayerEntity player)
		{
			_2ndPlayer = player;
		}
	}
}
