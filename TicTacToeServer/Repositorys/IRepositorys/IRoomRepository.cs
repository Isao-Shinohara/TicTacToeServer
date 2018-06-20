using TicTacToeServer.Cores;
using TicTacToeServer.Entitys;

namespace TicTacToeServer.Repositorys.IRepositorys
{
	public interface IRoomRepository
	{
		RoomEntity GetByRoomId(int roomId);
		RoomEntity GetByRoomNumber(int roomNumber);
		RoomEntity Create(RoomType roomType, PlayerEntity player);
		RoomEntity Create(int roomNumber, RoomType roomType, PlayerEntity player);
	}
}
