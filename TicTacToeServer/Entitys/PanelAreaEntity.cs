using TicTacToeServer.Cores;

namespace TicTacToeServer.Entitys
{
	public class PanelAreaEntity : Entity
	{
		public PanelAreaType PanelAreaType { get; private set; }
		public TurnType TurnType { get; private set; }

		public PanelAreaEntity(PanelAreaType panelAreaType, TurnType turnType)
		{
			PanelAreaType = panelAreaType;
			TurnType = turnType;
		}
	}
}
