﻿using System;
using Microsoft.Extensions.Caching.Distributed;
using TicTacToeServer.Core;
using TicTacToeServer.Domain.Entitys;
using TicTacToeServer.Domain.Repositorys.IRepositorys;

namespace TicTacToeServer.Domain.Repositorys.Redis
{
	public class RedisRoomRepository : RedisRepository<RoomEntity>, IRoomRepository
	{
		public RedisRoomRepository(IDistributedCache cache) : base(cache)
		{
		}

		public RoomEntity GetByRoomId(int roomId)
		{
			return Get(nameof(roomId), roomId);
		}

		public RoomEntity GetByRoomNumber(int roomNumber)
		{
			var roomId = GetString(nameof(roomNumber), roomNumber);
			if(String.IsNullOrEmpty(roomId)) return null;
			return GetByRoomId(int.Parse(roomId));
		}

		public RoomEntity Create(RoomType roomType, PlayerEntity player)
		{
			var room = new RoomEntity(roomType, player);
			SetPrimaryId(room);
			Set(nameof(room.RoomId), room.RoomId, room);
			SetString(nameof(room.RoomNumber), room.RoomNumber, room.RoomId);
			return room;
		}

		public RoomEntity Create(int roomNumber, RoomType roomType, PlayerEntity player)
		{
			var room = new RoomEntity(roomNumber, roomType, player);
			SetPrimaryId(room);
			Set(nameof(room.RoomId), room.RoomId, room);
			SetString(nameof(room.RoomNumber), room.RoomNumber, room.RoomId);
			return room;
		}

		public void Save(RoomEntity room)
		{
			Set(nameof(room.RoomId), room.RoomId, room);
		}

		public void Remove(RoomEntity room)
		{
			Remove(nameof(room.RoomNumber), room.RoomNumber);
			Remove(nameof(room.RoomId), room.RoomId);
		}
	}
}
