using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TicTacToeServer.Infrastructures;

namespace TicTacToeServer.Hubs
{
	public class SignalRHub : Hub
	{
		private readonly SignalRContext signalRContext;

		public SignalRHub(SignalRContext context)
		{
			this.signalRContext = context;
		}

		public override Task OnConnectedAsync()
		{
			Console.WriteLine("OnConnected " + Context.ConnectionId);

			if (!this.signalRContext.SignalRItemList.Any(item => item.ConnectionId == Context.ConnectionId)) {
				this.signalRContext.Update(new SignalRItem { ConnectionId = Context.ConnectionId });
				this.signalRContext.SaveChanges();
			}

			return base.OnConnectedAsync();
		}

		public override Task OnDisconnectedAsync(Exception e)
		{
			Console.WriteLine("OnDisconnected " + Context.ConnectionId);

			SignalRItem signalRItem = this.signalRContext.SignalRItemList.FirstOrDefault(item => item.ConnectionId == Context.ConnectionId);
			if (signalRItem != null) {
				this.signalRContext.Remove(signalRItem);
				this.signalRContext.SaveChanges();
			}

			return base.OnDisconnectedAsync(e);
		}

		public void Send(string message)
		{
			Console.WriteLine(String.Format("[Called '{0}'] {1}", MethodBase.GetCurrentMethod().Name, message));
			Clients.All.SendAsync(message);
		}
	}
}
