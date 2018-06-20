using System;
using Microsoft.Extensions.Caching.Distributed;
using TicTacToeServer.Cores;
using TicTacToeServer.Entitys;
using TicTacToeServer.Repositorys.IRepositorys;

namespace TicTacToeServer.Repositorys.Redis
{
	public class RoomRepository : RedisRepository<RoomEntity>, IRoomRepository
	{
		public RoomRepository(IDistributedCache cache) : base(cache)
		{
		}

		public RoomEntity GetByRoomId(int roomId)
		{
			//var key = string.Format("roomId:{0}", roomId);
			//var existingTime = (RoomEntity)cache.Get(key);

			//var room = efContext.RoomSet.Include(x => x._1stPlayer).Include(x => x._2ndPlayer).Include(x => x.PanelAreaList)
			//					 .FirstOrDefault(x => x.Id == roomId);
			//if (room != null) return room;

			//return efContext.RoomSet.Include(x => x.PanelAreaList).FirstOrDefault(x => x.Id == roomId);

			return null;
		}

		public RoomEntity GetByRoomNumber(int roomNumber)
		{
			//var room = efContext.RoomSet.Include(x => x._1stPlayer).Include(x => x._2ndPlayer).Include(x => x.PanelAreaList)
			//					 .FirstOrDefault(x => x.RoomNumber == roomNumber);
			//if (room != null) return room;

			//return efContext.RoomSet.Include(x => x.PanelAreaList).FirstOrDefault(x => x.RoomNumber == roomNumber);

			return null;
		}

		public RoomEntity Create(RoomType roomType, PlayerEntity player)
		{
			//var room = new RoomEntity(roomType, player);
			//efContext.Update(room);
			//return room;

			return null;
		}

		public RoomEntity Create(int roomNumber, RoomType roomType, PlayerEntity player)
		{
			//var room = new RoomEntity(roomNumber, roomType, player);
			//efContext.Update(room);
			//return room;

			return null;
		}
	}
}
