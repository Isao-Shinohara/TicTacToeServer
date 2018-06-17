using System;
using System.Linq;
using TicTacToeServer.Cores;
using TicTacToeServer.Entitys;
using TicTacToeServer.Infrastructures;

namespace TicTacToeServer.Repositorys
{
	public class RoomRepository : Repository<RoomEntity>
	{
		public RoomRepository(SignalRContext signalRContext) : base(signalRContext)
		{
		}

		public RoomEntity GetByRoomId(long roomId)
		{
			return signalRContext.RoomSet.FirstOrDefault(x => x.Id == roomId);
		}

		public RoomEntity Create(RoomType roomType, PlayerEntity player)
		{
			var room = new RoomEntity(roomType, player);
			signalRContext.Update(room);
			return room;
		}

		public RoomEntity Create(int roomNumber, RoomType roomType, PlayerEntity player)
		{
			var room = new RoomEntity(roomNumber, roomType, player);
			signalRContext.Update(room);
			return room;
		}

		public RoomEntity GetByRoomNumber(int roomNumber)
		{
			return signalRContext.RoomSet.FirstOrDefault(x => x.RoomNumber == roomNumber);
		}
	}
}
