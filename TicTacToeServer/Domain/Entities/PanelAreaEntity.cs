using System;
using MessagePack;
using TicTacToeServer.Cores;

namespace TicTacToeServer.Entitys
{
	[MessagePackObject]
	public class PanelAreaEntity : Entity
	{
		[Key(0)]
		public int PanelAreaId { get; private set; }
		[Key(1)]
		public PanelAreaType PanelAreaType { get; private set; }
		[Key(2)]
		public TurnType TurnType { get; private set; }
		[Key(3)]
		public bool Selected { get; private set; }

		public PanelAreaEntity()
		{
		}

		public PanelAreaEntity(PanelAreaType panelAreaType)
		{
			PanelAreaType = panelAreaType;
		}

		public override void SetId(int id)
		{
			PanelAreaId = id;
		}

		public void SetTurnType(TurnType turnType)
		{
			TurnType = turnType;
			Selected = true;
		}

		public override bool Equals(object obj)
		{
			var entity = obj as PanelAreaEntity;
			return entity != null &&
				   PanelAreaId == entity.PanelAreaId;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(PanelAreaId);
		}
	}
}
