using System;
using System.Linq;
using System.Reflection;
using TicTacToeServer.Core;
using TicTacToeServer.Hubs;
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
			if (!signalRContext.SignalRItemList.Any(item => item.ConnectionId == connectionId)) {
				signalRContext.Update(new SignalRItem { ConnectionId = connectionId });
				signalRContext.SaveChanges();
			}
		}

		public void RemovePlayer(string connectionId)
		{
			SignalRItem signalRItem = signalRContext.SignalRItemList.FirstOrDefault(item => item.ConnectionId == connectionId);
			if (signalRItem != null) {
				signalRContext.Remove(signalRItem);
				signalRContext.SaveChanges();
			}
		}

		public SignalRClientMessage StartSingleGame(string connectionId)
		{
			string method = string.Format("On{0}", MethodBase.GetCurrentMethod().Name);
			return new SignalRClientMessage(connectionId, method);
		}

		public SignalRClientMessage CreateRoom(string connectionId, int roomId)
		{
			string method = string.Format("On{0}", MethodBase.GetCurrentMethod().Name);
			string message = roomId > 0 ? "" : ErrorMessage.ExistsSameRoomNumber;
			return new SignalRClientMessage(connectionId, method, roomId, message);
		}

		internal SignalRClientMessage JoinRoom(string connectionId, int roomId)
		{
			string method = string.Format("On{0}", MethodBase.GetCurrentMethod().Name);
			string message = roomId > 0 ? "" : ErrorMessage.NotExistsRoomNumber;
			return new SignalRClientMessage(connectionId, method, roomId, message);
		}
	}
}
