using TicTacToeServer.Cores;

namespace TicTacToeServer.Entitys
{
	public class PanelAreaEntity : Entity
	{
		public PanelAreaType PanelAreaType { get; private set; }
		public TurnType TurnType { get; private set; }
		public bool Selected { get; private set; } = false;

		public RoomEntity RoomEntity { get; private set; }

		public PanelAreaEntity(PanelAreaType panelAreaType)
		{
			PanelAreaType = panelAreaType;
		}

		public void SetTurnType(TurnType turnType)
		{
			TurnType = turnType;
			Selected = true;
		}
	}
}
