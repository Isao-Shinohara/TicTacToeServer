﻿using System.Collections.Generic;
using System.Linq;
using TicTacToeServer.Cores;
using TicTacToeServer.Entitys;
using TicTacToeServer.Infrastructures;
using TicTacToeServer.Repositorys;

namespace TicTacToeServer.Services
{
	public class AppService
	{
		SignalRContext signalRContext;
		PlayerRepository playerRepository;
		RoomRepository roomRepository;
		PanelAreaRepository panelAreaRepository;

		public AppService(SignalRContext context)
		{
			signalRContext = context;
			playerRepository = new PlayerRepository(context);
			roomRepository = new RoomRepository(context);
			panelAreaRepository = new PanelAreaRepository(context);
		}

		public void AddPlayer(string connectionId)
		{
			var player = playerRepository.GetByConnectionId(connectionId);
			if (player == null) {
				playerRepository.Create(connectionId);
				playerRepository.Save();
			}
		}

		public void RemovePlayer(string connectionId)
		{
			var player = playerRepository.GetByConnectionId(connectionId);
			if (player != null) {
				var room = roomRepository.GetByRoomId(player.RoomId);
				if(room != null){
					roomRepository.Remove(room);
					roomRepository.Save();
				}

				playerRepository.Remove(player);
				playerRepository.Save();
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

			roomRepository.Save();
			player.SetRoomId(newRoom.Id);
			playerRepository.Save();

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
			roomRepository.Save();

			player.SetRoomId(room.Id);
			playerRepository.Save();

			return (TurnType._2ndPlayer, "");
		}

		public (TurnType TurnType, string ErrorMessage) InitializeGame(string connectionId)
		{
			var player = playerRepository.GetByConnectionId(connectionId);
			var newRoom = roomRepository.Create(RoomType.Single, player);
			roomRepository.Save();

			player.SetRoomId(newRoom.Id);
			var _2ndPlayer = playerRepository.Create();
			_2ndPlayer.SetRoomId(newRoom.Id);
			playerRepository.Save();

			newRoom.Set2ndPlayer(_2ndPlayer);
			roomRepository.Save();

			return (TurnType._1stPlayer, "");
		}

		public void StartGame()
		{
			return;
		}

		public (List<string> ConnectionIds, RoomEntity Room) SelectPanelArea(string connectionId, PanelAreaType panelAreaType)
		{
			var player = playerRepository.GetByConnectionId(connectionId);
			var room = roomRepository.GetByRoomId(player.RoomId);

			room.SelectPanelArea(player, panelAreaType);
			room.NextTurn();
			roomRepository.Save();

			List<string> connectionIds = new List<string>() { room._1stPlayer.ConnectionId };
			if (room.RoomType == RoomType.Multi) connectionIds.Add(room._2ndPlayer.ConnectionId);

			return (connectionIds, room);
		}

		public (RoomEntity Room, PanelAreaType SelectedPanelAreaType) SelectPanelAreaByAI(RoomEntity room)
		{
			var panelAreaType = room.SelectPanelAreaByAI();
			room.NextTurn();
			roomRepository.Save();

			return (room, panelAreaType);
		}
	}
}
