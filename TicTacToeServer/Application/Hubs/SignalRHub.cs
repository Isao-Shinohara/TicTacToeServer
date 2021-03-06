﻿using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TicTacToeServer.Core;
using TicTacToeServer.Application.Services;

namespace TicTacToeServer.Application.Hubs
{
	public class SignalRHub : Hub
	{
		AppService appService;

		public SignalRHub(AppService appService)
		{
			AppSignalRLogger.Level = AppSignalRLogger.Loglevels.Verbose;
			this.appService = appService;
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

			if(result.ErrorMessage != ""){
				AppSignalRLogger.LogVerbose("[JoinRoom ErrorMessage] {0}", result.ErrorMessage);
				return;
			}

			var room = appService.GetRoomByConnectionId(Context.ConnectionId);
			var initMessage = SignalRClientMessage.Create(room._1stPlayer.ConnectionId, "InitializeGame", TurnType._1stPlayer, "");
			CallClientMethod(initMessage);

			var initResult = appService.GetRoomByConnectionId(Context.ConnectionId);
			initMessage = SignalRClientMessage.Create(room._2ndPlayer.ConnectionId, "InitializeGame", TurnType._2ndPlayer, "");
			CallClientMethod(initMessage);
		}

		public void InitializeGame()
		{
			AppSignalRLogger.LogVerbose("[Called '{0}']", MethodBase.GetCurrentMethod().Name);
			var result = appService.InitializeGame(Context.ConnectionId);
			var message = SignalRClientMessage.Create(Context.ConnectionId, MethodBase.GetCurrentMethod().Name, result.TurnType, result.ErrorMessage);
			CallClientMethod(message);
		}

		public void Ready()
		{
			AppSignalRLogger.LogVerbose("[Called '{0}']", MethodBase.GetCurrentMethod().Name);
			var result = appService.Ready(Context.ConnectionId);
			var message = SignalRClientMessage.Create(Context.ConnectionId, MethodBase.GetCurrentMethod().Name);
			CallClientMethod(message);

			if(result.Room.IsReadyAllPlayer){
				AppSignalRLogger.LogVerbose("[Call 'OnStartGame']");
				var startGameMessage = SignalRClientMessage.Create(result.ConnectionIds, "StartGame");
				CallClientMethod(startGameMessage);
			}
		}

		public void StartGame()
		{
			AppSignalRLogger.LogVerbose("[Called '{0}']", MethodBase.GetCurrentMethod().Name);
			appService.StartGame();
			var message = SignalRClientMessage.Create(Context.ConnectionId, MethodBase.GetCurrentMethod().Name);
			CallClientMethod(message);
		}

		public void SelectPanelArea(PanelAreaType panelAreaType)
		{
			AppSignalRLogger.LogVerbose("[Called '{0}'] {1}", MethodBase.GetCurrentMethod().Name, panelAreaType);
			var result = appService.SelectPanelArea(Context.ConnectionId, panelAreaType);
			var message = SignalRClientMessage.Create(result.ConnectionIds, MethodBase.GetCurrentMethod().Name, panelAreaType, result.Room._1stPlayerResult, result.Room._2ndPlayerResult, result.Room.NowTurnType
			                                          , result.Room._1stPlayerReachPanelAreaTypeList, result.Room._2ndPlayerReachPanelAreaTypeList);
			CallClientMethod(message);

			if (result.Room.CanPlayAI) {
				Thread.Sleep(1000);
				var aiResult = appService.SelectPanelAreaByAI(result.Room);
				var aiMessage = SignalRClientMessage.Create(Context.ConnectionId, MethodBase.GetCurrentMethod().Name, aiResult.SelectedPanelAreaType, aiResult.Room._1stPlayerResult, aiResult.Room._2ndPlayerResult, aiResult.Room.NowTurnType
				                                            , aiResult.Room._1stPlayerReachPanelAreaTypeList, aiResult.Room._2ndPlayerReachPanelAreaTypeList);
				CallClientMethod(aiMessage);
			}
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
