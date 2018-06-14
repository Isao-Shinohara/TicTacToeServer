using System;
namespace TicTacToeServer.Models
{
	public class PanelAreaModel
	{
		public long Id { get; set; }
		public PanelAreaType PanelAreaType { get; set; }
		public TurnType TurnType { get; set; }

		public PanelAreaModel(PanelAreaType panelAreaType, TurnType turnType)
		{
			PanelAreaType = panelAreaType;
			TurnType = turnType;
		}
	}
}
