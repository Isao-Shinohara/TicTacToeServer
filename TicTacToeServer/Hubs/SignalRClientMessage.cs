using System.Collections.Generic;

namespace TicTacToeServer.Hubs
{
	public class SignalRClientMessage
	{
		public List<string> Clients { get; private set; }
		public string Method { get; private set; }
		public object[] Argument { get; private set; }

		public SignalRClientMessage(string client, string method)
		{
			Clients = new List<string> { client };
			Method = method;
		}

		public SignalRClientMessage(string client, string method, object[] argument)
		{
			Clients = new List<string> { client };
			Method = method;
			Argument = argument;
		}

		public SignalRClientMessage(string client, string method, object arg1)
		{
			Clients = new List<string> { client };
			Method = method;
			var argument = new object[] { arg1 };
			Argument = argument;
		}

		public SignalRClientMessage(string client, string method, object arg1, object arg2)
		{
			Clients = new List<string> { client };
			Method = method;
			var argument = new object[] { arg1, arg2 };
			Argument = argument;
		}

		public SignalRClientMessage(string client, string method, object arg1, object arg2, object arg3)
		{
			Clients = new List<string> { client };
			Method = method;
			var argument = new object[] { arg1, arg2, arg3 };
			Argument = argument;
		}

		public SignalRClientMessage(string client, string method, object arg1, object arg2, object arg3, object arg4)
		{
			Clients = new List<string> { client };
			Method = method;
			var argument = new object[] { arg1, arg2, arg3, arg4 };
			Argument = argument;
		}

		public SignalRClientMessage(List<string> clients, string method)
		{
			Clients = clients;
			Method = method;
		}

		public SignalRClientMessage(List<string> clients, string method, object[] argument)
		{
			Clients = clients;
			Method = method;
			Argument = argument;
		}

		public SignalRClientMessage(List<string> clients, string method, object arg1)
		{
			Clients = clients;
			Method = method;
			var argument = new object[] { arg1 };
			Argument = argument;
		}

		public SignalRClientMessage(List<string> clients, string method, object arg1, object arg2)
		{
			Clients = clients;
			Method = method;
			var argument = new object[] { arg1, arg2 };
			Argument = argument;
		}

		public SignalRClientMessage(List<string> clients, string method, object arg1, object arg2, object arg3)
		{
			Clients = clients;
			Method = method;
			var argument = new object[] { arg1, arg2, arg3 };
			Argument = argument;
		}

		public SignalRClientMessage(List<string> clients, string method, object arg1, object arg2, object arg3, object arg4)
		{
			Clients = clients;
			Method = method;
			var argument = new object[] { arg1, arg2, arg3, arg4 };
			Argument = argument;
		}

		public bool IsSingleClient
		{
			get {
				return Clients.Count == 1;
			}
		}

		public bool IsMultiClient
		{
			get {
				return Clients.Count > 1;
			}
		}

		public bool HaveArgument
		{
			get {
				return Argument != null;
			}
		}
	}
}
