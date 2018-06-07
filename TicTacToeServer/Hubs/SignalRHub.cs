using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TicTacToeServer.Core;
using TicTacToeServer.Infrastructures;
using TicTacToeServer.Services;

namespace TicTacToeServer.Hubs
{
	public class SignalRHub : Hub
	{
		AppService appService;

		public SignalRHub(SignalRContext context)
		{
			AppSignalRLogger.Level = AppSignalRLogger.Loglevels.Debug;
			appService = new AppService(context);
		}

		public override Task OnConnectedAsync()
		{
			AppSignalRLogger.Log("OnConnected {0}", Context.ConnectionId);
			appService.AddPlayer(Context.ConnectionId);
			return base.OnConnectedAsync();
		}

		public override Task OnDisconnectedAsync(Exception e)
		{
			AppSignalRLogger.Log("OnDisconnectedAsync {0}", Context.ConnectionId);
			appService.RemovePlayer(Context.ConnectionId);
			return base.OnDisconnectedAsync(e);
		}

		public void StartSingleGame()
		{
			AppSignalRLogger.LogVerbose("[Called '{0}']", MethodBase.GetCurrentMethod().Name);
			var message = appService.StartSingleGame(Context.ConnectionId);
			CallClientMethod(message);
		}

		public void CreateRoom(int roomId)
		{
			AppSignalRLogger.LogVerbose("[Called '{0}'] {1}", MethodBase.GetCurrentMethod().Name, roomId);
			var message = appService.CreateRoom(Context.ConnectionId, roomId);
			CallClientMethod(message);
		}

		void CallClientMethod(SignalRClientMessage message)
		{
			if(!message.HaveArgument)
			{
				Clients.Clients(message.Clients).SendAsync(message.Method);
			} else {
				Clients.Clients(message.Clients).SendCoreAsync(message.Method, message.Argument);
			}
		}
	}
}
