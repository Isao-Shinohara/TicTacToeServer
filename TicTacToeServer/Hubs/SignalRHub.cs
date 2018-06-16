using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TicTacToeServer.Cores;
using TicTacToeServer.Infrastructures;
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

		public void InitializeSingleGame()
		{
			AppSignalRLogger.LogVerbose("[Called '{0}']", MethodBase.GetCurrentMethod().Name);
			var result = appService.InitializeSingleGame(Context.ConnectionId);
			var message = SignalRClientMessage.Create(Context.ConnectionId, MethodBase.GetCurrentMethod().Name, result.TurnType, result.ErrorMessage);
			CallClientMethod(message);
		}

		public void CreateRoom(int roomNumber)
		{
			AppSignalRLogger.LogVerbose("[Called '{0}'] {1}", MethodBase.GetCurrentMethod().Name, roomNumber);
			var result = appService.CreateRoom(Context.ConnectionId, roomNumber);
			var message = SignalRClientMessage.Create(Context.ConnectionId, MethodBase.GetCurrentMethod().Name, roomNumber, result.TurnType, result.ErrorMessage);
			CallClientMethod(message);
		}

		public void JoinRoom(int roomNumber)
		{
			AppSignalRLogger.LogVerbose("[Called '{0}'] {1}", MethodBase.GetCurrentMethod().Name, roomNumber);
			var result = appService.JoinRoom(Context.ConnectionId, roomNumber);
			var message = SignalRClientMessage.Create(Context.ConnectionId, MethodBase.GetCurrentMethod().Name, roomNumber, result.TurnType, result.ErrorMessage);
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
