using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TicTacToeServer.Core;
using TicTacToeServer.Infrastructures;
using TicTacToeServer.Models;
using TicTacToeServer.Services;

namespace TicTacToeServer.Hubs
{
	public class SignalRHub : Hub
	{
		AppService appService;

		public SignalRHub(SignalRContext context)
		{
			AppSignalRLogger.Level = AppSignalRLogger.Loglevels.Verbose;
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

		public void CreateRoom(int roomId)
		{
			AppSignalRLogger.LogVerbose("[Called '{0}'] {1}", MethodBase.GetCurrentMethod().Name, roomId);

			var errorMessage = appService.CreateRoom(roomId);
			var message = SignalRClientMessage.Create(Context.ConnectionId, MethodBase.GetCurrentMethod().Name, roomId, errorMessage);

			CallClientMethod(message);
		}

		public void JoinRoom(int roomId)
		{
			AppSignalRLogger.LogVerbose("[Called '{0}'] {1}", MethodBase.GetCurrentMethod().Name, roomId);

			var errorMessage = appService.JoinRoom(roomId);
			var message = SignalRClientMessage.Create(Context.ConnectionId, MethodBase.GetCurrentMethod().Name, roomId, errorMessage);

			CallClientMethod(message);
		}

		public void InitializeSingleGame()
		{
			AppSignalRLogger.LogVerbose("[Called '{0}']", MethodBase.GetCurrentMethod().Name);

			var turnType = appService.InitializeSingleGame();
			var message = SignalRClientMessage.Create(Context.ConnectionId, MethodBase.GetCurrentMethod().Name, turnType);

			CallClientMethod(message);
		}

		public void StartSingleGame()
		{
			AppSignalRLogger.LogVerbose("[Called '{0}']", MethodBase.GetCurrentMethod().Name);

			appService.StartSingleGame();
			var message = SignalRClientMessage.Create(Context.ConnectionId, MethodBase.GetCurrentMethod().Name);

			CallClientMethod(message);
		}

		public void SelectPanelArea(PanelAreaType panelAreaType, TurnType turnType)
		{
			AppSignalRLogger.LogVerbose("[Called '{0}'] {1} {2}", MethodBase.GetCurrentMethod().Name, panelAreaType, turnType);

			var resultType = appService.SelectPanelArea(panelAreaType, turnType);
			var message = SignalRClientMessage.Create(Context.ConnectionId, MethodBase.GetCurrentMethod().Name, panelAreaType, resultType);

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
