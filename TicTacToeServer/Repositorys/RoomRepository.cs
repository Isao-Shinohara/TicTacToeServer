using System.Linq;
using Microsoft.EntityFrameworkCore;
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

		public RoomEntity GetByRoomId(int roomId)
		{
			var room = signalRContext.RoomSet.Include(x => x._1stPlayer).Include(x => x._2ndPlayer).Include(x => x.PanelAreaList)
								 .FirstOrDefault(x => x.Id == roomId);
			if (room != null) return room;

			return signalRContext.RoomSet.Include(x => x.PanelAreaList).FirstOrDefault(x => x.Id == roomId);
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
			return signalRContext.RoomSet.Include(x => x._1stPlayer).Include(x => x._2ndPlayer).Include(x => x.PanelAreaList)
				                 .FirstOrDefault(x => x.RoomNumber == roomNumber);
		}
	}
}
