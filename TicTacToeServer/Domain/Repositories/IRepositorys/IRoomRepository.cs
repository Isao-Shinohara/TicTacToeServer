using TicTacToeServer.Core;
using TicTacToeServer.Domain.Entitys;

namespace TicTacToeServer.Domain.Repositorys.IRepositorys
{
	public interface IRoomRepository : IRepository<RoomEntity>
	{
		RoomEntity GetByRoomId(int roomId);
		RoomEntity GetByRoomNumber(int roomNumber);
		RoomEntity Create(RoomType roomType, PlayerEntity player);
		RoomEntity Create(int roomNumber, RoomType roomType, PlayerEntity player);
	}
}
