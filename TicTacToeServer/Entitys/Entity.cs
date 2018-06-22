using MessagePack;

namespace TicTacToeServer.Entitys
{
	[MessagePackObject]
	public abstract class Entity
	{
		public abstract void SetId(int id);
	}
}
