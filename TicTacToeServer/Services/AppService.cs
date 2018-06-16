using System.Collections.Generic;
using System.Linq;
using TicTacToeServer.Cores;
using TicTacToeServer.Entitys;
using TicTacToeServer.Infrastructures;

namespace TicTacToeServer.Services
{
	public class AppService
	{
		SignalRContext signalRContext;

		public AppService(SignalRContext context)
		{
			signalRContext = context;
		}

		public void AddPlayer(string connectionId)
		{
			if (!signalRContext.PlayerSet.Any(v => v.ConnectionId == connectionId)) {
				signalRContext.Update(new PlayerEntity(connectionId));
				signalRContext.SaveChanges();
			}
		}

		public void RemovePlayer(string connectionId)
		{
			var player = signalRContext.PlayerSet.FirstOrDefault(v => v.ConnectionId == connectionId);
			if (player != null) {
				signalRContext.Remove(player);
				signalRContext.SaveChanges();
			}
		}

		public string CreateRoom(int roomId)
		{
			var errorMessage = roomId > 0 ? "" : ErrorMessage.ExistsSameRoomNumber;
			return errorMessage;
		}

		internal string JoinRoom(int roomId)
		{
			string errorMessage = roomId > 0 ? "" : ErrorMessage.NotExistsRoomNumber;
			return errorMessage;
		}

		public TurnType InitializeSingleGame()
		{
			return TurnType._1stPlayer;
		}

		public void StartSingleGame()
		{
			return;
		}

		public ResultType SelectPanelArea(PanelAreaType panelAreaType, TurnType turnType)
		{
			signalRContext.Update(new PanelAreaEntity(panelAreaType, turnType));
			signalRContext.SaveChanges();

			var panelAreaModelList = signalRContext.PanelAreaSet.Where(x => x.TurnType == turnType).ToList();
			var isClear = IsClear(panelAreaModelList);
			AppSignalRLogger.LogVerbose("[isClear '{0}']", isClear);

			var isEnd = IsEnd(signalRContext.PanelAreaSet.ToList());
			AppSignalRLogger.LogVerbose("[isEnd '{0}']", isClear);

			var resultType = ResultType.None;
			if (isClear) resultType = ResultType.Win;
			if (isEnd) resultType = ResultType.Draw;
			AppSignalRLogger.LogVerbose("[resultType '{0}']", resultType);

			return resultType;
		}

		private bool IsClear(List<PanelAreaEntity> list)
		{
			var existsArea1 = list.Exists(x => x.PanelAreaType == PanelAreaType.Area1);
			var existsArea2 = list.Exists(x => x.PanelAreaType == PanelAreaType.Area2);
			var existsArea3 = list.Exists(x => x.PanelAreaType == PanelAreaType.Area3);
			var existsArea4 = list.Exists(x => x.PanelAreaType == PanelAreaType.Area4);
			var existsArea5 = list.Exists(x => x.PanelAreaType == PanelAreaType.Area5);
			var existsArea6 = list.Exists(x => x.PanelAreaType == PanelAreaType.Area6);
			var existsArea7 = list.Exists(x => x.PanelAreaType == PanelAreaType.Area7);
			var existsArea8 = list.Exists(x => x.PanelAreaType == PanelAreaType.Area8);
			var existsArea9 = list.Exists(x => x.PanelAreaType == PanelAreaType.Area9);

			// Vertical
			var isClear = existsArea1 && existsArea2 && existsArea3;
			if (isClear) return true;
			isClear = existsArea4 && existsArea5 && existsArea6;
			if (isClear) return true;
			isClear = existsArea7 && existsArea8 && existsArea9;
			if (isClear) return true;

			// Horizontal
			isClear = existsArea1 && existsArea4 && existsArea7;
			if (isClear) return true;
			isClear = existsArea2 && existsArea5 && existsArea8;
			if (isClear) return true;
			isClear = existsArea7 && existsArea8 && existsArea9;
			if (isClear) return true;

			// Diagonal
			isClear = existsArea1 && existsArea5 && existsArea9;
			if (isClear) return true;
			isClear = existsArea3 && existsArea5 && existsArea7;
			if (isClear) return true;

			return false;
		}

		private bool IsEnd(List<PanelAreaEntity> list)
		{
			return list.Count >= 9;
		}
	}
}
