using System;
using MessagePack;

namespace TicTacToeServer.Domain.Entitys
{
	[MessagePackObject]
	public class PlayerEntity : Entity
	{
		public const string 	AIConnectionId = "ai";

		[Key(0)]
		public int PlayerId { get; private set; }
		[Key(1)]
		public string ConnectionId { get; private set; }
		[Key(2)]
		public int RoomId { get; private set; }

		public PlayerEntity()
		{
			ConnectionId = AIConnectionId;
		}

		public PlayerEntity(string connectionId)
		{
			ConnectionId = connectionId;
		}

		public override void SetId(int id)
		{
			PlayerId = id;
		}

		[IgnoreMember]
		public bool IsAI
		{
			get {
				return ConnectionId == "";
			}
		}

		public void SetRoomId(int roomId)
		{
			RoomId = roomId;
		}

		public override bool Equals(object obj)
		{
			var entity = obj as PlayerEntity;
			return entity != null &&
				   PlayerId == entity.PlayerId;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(PlayerId);
		}
	}
}
