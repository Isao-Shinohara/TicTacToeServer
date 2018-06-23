using MessagePack;

namespace TicTacToeServer.Domain.Entitys
{
	[MessagePackObject]
	public abstract class Entity
	{
		public abstract void SetId(int id);
	}
}
