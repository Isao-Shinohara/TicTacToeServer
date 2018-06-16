using TicTacToeServer.Cores;

namespace TicTacToeServer.Entitys
{
	public class PanelAreaEntity : Entity
	{
		public PanelAreaType PanelAreaType { get; set; }
		public TurnType TurnType { get; set; }

		public PanelAreaEntity(PanelAreaType panelAreaType, TurnType turnType)
		{
			PanelAreaType = panelAreaType;
			TurnType = turnType;
		}
	}
}
