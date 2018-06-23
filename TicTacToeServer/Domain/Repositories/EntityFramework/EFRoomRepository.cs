using System.Linq;
using Microsoft.EntityFrameworkCore;
using TicTacToeServer.Core;
using TicTacToeServer.Domain.Entitys;
using TicTacToeServer.Domain.Infrastructures;
using TicTacToeServer.Domain.Repositorys.IRepositorys;

namespace TicTacToeServer.Domain.Repositorys.EntityFramework
{
	public class EFRoomRepository : EFRepository<RoomEntity>, IRoomRepository
	{
		public EFRoomRepository(EFContext efContext) : base(efContext)
		{
		}

		public RoomEntity GetByRoomId(int roomId)
		{
			var room = efContext.RoomSet.Include(x => x._1stPlayer).Include(x => x._2ndPlayer).Include(x => x.PanelAreaList)
			                    .FirstOrDefault(x => x.RoomId == roomId);
			if (room != null) return room;

			return efContext.RoomSet.Include(x => x.PanelAreaList).FirstOrDefault(x => x.RoomId == roomId);
		}

		public RoomEntity GetByRoomNumber(int roomNumber)
		{
			var room = efContext.RoomSet.Include(x => x._1stPlayer).Include(x => x._2ndPlayer).Include(x => x.PanelAreaList)
								 .FirstOrDefault(x => x.RoomNumber == roomNumber);
			if (room != null) return room;

			return efContext.RoomSet.Include(x => x.PanelAreaList).FirstOrDefault(x => x.RoomNumber == roomNumber);
		}

		public RoomEntity Create(RoomType roomType, PlayerEntity player)
		{
			var room = new RoomEntity(roomType, player);
			efContext.Update(room);
			return room;
		}

		public RoomEntity Create(int roomNumber, RoomType roomType, PlayerEntity player)
		{
			var room = new RoomEntity(roomNumber, roomType, player);
			efContext.Update(room);
			return room;
		}
	}
}
