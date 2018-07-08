using System.Collections.Generic;
using Microsoft.Extensions.Caching.Distributed;
using TicTacToeServer.Core;
using TicTacToeServer.Domain.Entitys;
using TicTacToeServer.Domain.Infrastructures;
using TicTacToeServer.Domain.Repositorys.Redis;

namespace TicTacToeServer.Application.Services
{
	public class AppService
	{
		EFContext efContext;
		RedisPlayerRepository playerRepository;
		RedisRoomRepository roomRepository;

		public AppService(EFContext context, IDistributedCache cache)
		{
			efContext = context;
			playerRepository = new RedisPlayerRepository(cache);
			roomRepository = new RedisRoomRepository(cache);
		}

		public void AddPlayer(string connectionId)
		{
			var player = playerRepository.GetByConnectionId(connectionId);
			if (player == null) {
				playerRepository.GetByConnectionId(connectionId);
				playerRepository.Create(connectionId);
			}
		}

		public void RemovePlayer(string connectionId)
		{
			var player = playerRepository.GetByConnectionId(connectionId);
			if (player != null)
			{
				var room = roomRepository.GetByRoomId(player.RoomId);
				if(room != null){
					room.Remove(player);
					if(room.IsRemovedAllPlayer){
						AppSignalRLogger.Log("Remove Room {0}", room.RoomId);
						roomRepository.Remove(room);
					}
				}

				playerRepository.Remove(player);
			}
		}

		public (TurnType TurnType, string ErrorMessage) CreateRoom(string connectionId, int roomNumber)
		{
			var player = playerRepository.GetByConnectionId(connectionId);

			var room = roomRepository.GetByRoomNumber(roomNumber);
			if(room != null){
				return (TurnType._1stPlayer, ErrorMessage.ExistsSameRoomNumber);
			}

			var newRoom = roomRepository.Create(roomNumber, RoomType.Multi, player);

			player.SetRoomId(newRoom.RoomId);
			playerRepository.Save(player);

			return (TurnType._1stPlayer, "");
		}

		public (TurnType TurnType, string ErrorMessage) JoinRoom(string connectionId, int roomNumber)
		{
			var player = playerRepository.GetByConnectionId(connectionId);

			var room = roomRepository.GetByRoomNumber(roomNumber);
			if (room == null) {
				return (TurnType._2ndPlayer, ErrorMessage.NotExistsRoomNumber);
			}

			room.Set2ndPlayer(player);
			roomRepository.Save(room);

			player.SetRoomId(room.RoomId);
			playerRepository.Save(player);

			return (TurnType._2ndPlayer, "");
		}

		public (TurnType TurnType, string ErrorMessage) InitializeGame(string connectionId)
		{
			var _1stPlayer = playerRepository.GetByConnectionId(connectionId);
			var newRoom = roomRepository.Create(RoomType.Single, _1stPlayer);

			_1stPlayer.SetRoomId(newRoom.RoomId);
			playerRepository.Save(_1stPlayer);

			var _2ndPlayer = playerRepository.Create();
			_2ndPlayer.SetRoomId(newRoom.RoomId);
			playerRepository.Save(_2ndPlayer);

			newRoom.Set2ndPlayer(_2ndPlayer);
			roomRepository.Save(newRoom);

			return (TurnType._1stPlayer, "");
		}

		public void StartGame()
		{
			return;
		}

		public RoomEntity GetRoomByConnectionId(string connectionId)
		{
			var player = playerRepository.GetByConnectionId(connectionId);
			return roomRepository.GetByRoomId(player.RoomId);
		}

		public (List<string> ConnectionIds, RoomEntity Room) Ready(string connectionId)
		{
			var player = playerRepository.GetByConnectionId(connectionId);
			var room = roomRepository.GetByRoomId(player.RoomId);
			room.Ready(player);

			List<string> connectionIds = new List<string>() { room._1stPlayer.ConnectionId, room._2ndPlayer.ConnectionId};
			return (connectionIds, room);
		}

		public (List<string> ConnectionIds, RoomEntity Room) SelectPanelArea(string connectionId, PanelAreaType panelAreaType)
		{
			var player = playerRepository.GetByConnectionId(connectionId);
			var room = roomRepository.GetByRoomId(player.RoomId);

			room.SelectPanelArea(player, panelAreaType);
			room.NextTurn();
			roomRepository.Save(room);

			List<string> connectionIds = new List<string>() { room._1stPlayer.ConnectionId };
			if (room.IsMulti && room.Exsists2ndPlayer) connectionIds.Add(room._2ndPlayer.ConnectionId);

			return (connectionIds, room);
		}

		public (RoomEntity Room, PanelAreaType SelectedPanelAreaType) SelectPanelAreaByAI(RoomEntity room)
		{
			var panelAreaType = room.SelectPanelAreaByAI();
			room.NextTurn();
			roomRepository.Save(room);

			return (room, panelAreaType);
		}
	}
}
