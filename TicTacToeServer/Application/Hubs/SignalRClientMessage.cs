using System.Collections.Generic;

namespace TicTacToeServer.Application.Hubs
{
	public class SignalRClientMessage
	{
		public List<string> Clients { get; set; }
		public string Method { get; set; }
		public object[] Argument { get; set; }

		SignalRClientMessage(string client, string method)
		{
			Clients = new List<string> { client };
			Method = method;
		}

		SignalRClientMessage(string client, string method, object[] argument)
		{
			Clients = new List<string> { client };
			Method = method;
			Argument = argument;
		}

		SignalRClientMessage(string client, string method, object arg1)
		{
			Clients = new List<string> { client };
			Method = method;
			var argument = new object[] { arg1 };
			Argument = argument;
		}

		SignalRClientMessage(string client, string method, object arg1, object arg2)
		{
			Clients = new List<string> { client };
			Method = method;
			var argument = new object[] { arg1, arg2 };
			Argument = argument;
		}

		SignalRClientMessage(string client, string method, object arg1, object arg2, object arg3)
		{
			Clients = new List<string> { client };
			Method = method;
			var argument = new object[] { arg1, arg2, arg3 };
			Argument = argument;
		}

		SignalRClientMessage(string client, string method, object arg1, object arg2, object arg3, object arg4)
		{
			Clients = new List<string> { client };
			Method = method;
			var argument = new object[] { arg1, arg2, arg3, arg4 };
			Argument = argument;
		}

		SignalRClientMessage(string client, string method, object arg1, object arg2, object arg3, object arg4, object arg5)
		{
			Clients = new List<string> { client };
			Method = method;
			var argument = new object[] { arg1, arg2, arg3, arg4, arg5};
			Argument = argument;
		}

		SignalRClientMessage(string client, string method, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6)
		{
			Clients = new List<string> { client };
			Method = method;
			var argument = new object[] { arg1, arg2, arg3, arg4, arg5, arg6 };
			Argument = argument;
		}

		SignalRClientMessage(List<string> clients, string method)
		{
			Clients = clients;
			Method = method;
		}

		SignalRClientMessage(List<string> clients, string method, object[] argument)
		{
			Clients = clients;
			Method = method;
			Argument = argument;
		}

		SignalRClientMessage(List<string> clients, string method, object arg1)
		{
			Clients = clients;
			Method = method;
			var argument = new object[] { arg1 };
			Argument = argument;
		}

		SignalRClientMessage(List<string> clients, string method, object arg1, object arg2)
		{
			Clients = clients;
			Method = method;
			var argument = new object[] { arg1, arg2 };
			Argument = argument;
		}

		SignalRClientMessage(List<string> clients, string method, object arg1, object arg2, object arg3)
		{
			Clients = clients;
			Method = method;
			var argument = new object[] { arg1, arg2, arg3 };
			Argument = argument;
		}

		SignalRClientMessage(List<string> clients, string method, object arg1, object arg2, object arg3, object arg4)
		{
			Clients = clients;
			Method = method;
			var argument = new object[] { arg1, arg2, arg3, arg4 };
			Argument = argument;
		}

		SignalRClientMessage(List<string> clients, string method, object arg1, object arg2, object arg3, object arg4, object arg5)
		{
			Clients = clients;
			Method = method;
			var argument = new object[] { arg1, arg2, arg3, arg4, arg5 };
			Argument = argument;
		}

		SignalRClientMessage(List<string> clients, string method, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6)
		{
			Clients = clients;
			Method = method;
			var argument = new object[] { arg1, arg2, arg3, arg4, arg5, arg6 };
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

		///  Factory method
		public static SignalRClientMessage Create(string client, string method)
		{
			return new SignalRClientMessage(client, ConvertCallMethod(method));
		}

		public static SignalRClientMessage Create(string client, string method, object[] argument)
		{
			return new SignalRClientMessage(client, ConvertCallMethod(method), argument);
		}

		public static SignalRClientMessage Create(string client, string method, object arg1)
		{
			return new SignalRClientMessage(client, ConvertCallMethod(method), arg1);
		}

		public static SignalRClientMessage Create(string client, string method, object arg1, object arg2)
		{
			return new SignalRClientMessage(client, ConvertCallMethod(method), arg1, arg2);
		}

		public static SignalRClientMessage Create(string client, string method, object arg1, object arg2, object arg3)
		{
			return new SignalRClientMessage(client, ConvertCallMethod(method), arg1, arg2, arg3);
		}

		public static SignalRClientMessage Create(string client, string method, object arg1, object arg2, object arg3, object arg4)
		{
			return new SignalRClientMessage(client, ConvertCallMethod(method), arg1, arg2, arg3, arg4);
		}

		public static SignalRClientMessage Create(string client, string method, object arg1, object arg2, object arg3, object arg4, object arg5)
		{
			return new SignalRClientMessage(client, ConvertCallMethod(method), arg1, arg2, arg3, arg4, arg5);
		}

		public static SignalRClientMessage Create(string client, string method, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6)
		{
			return new SignalRClientMessage(client, ConvertCallMethod(method), arg1, arg2, arg3, arg4, arg5, arg6);
		}

		public static SignalRClientMessage Create(List<string> clients, string method)
		{
			return new SignalRClientMessage(clients, ConvertCallMethod(method));
		}

		public static SignalRClientMessage Create(List<string> clients, string method, object[] argument)
		{
			return new SignalRClientMessage(clients, ConvertCallMethod(method), argument);
		}

		public static SignalRClientMessage Create(List<string> clients, string method, object arg1)
		{
			return new SignalRClientMessage(clients, ConvertCallMethod(method), arg1);
		}

		public static SignalRClientMessage Create(List<string> clients, string method, object arg1, object arg2)
		{
			return new SignalRClientMessage(clients, ConvertCallMethod(method), arg1, arg2);
		}

		public static SignalRClientMessage Create(List<string> clients, string method, object arg1, object arg2, object arg3)
		{
			return new SignalRClientMessage(clients, ConvertCallMethod(method), arg1, arg2, arg3);
		}

		public static SignalRClientMessage Create(List<string> clients, string method, object arg1, object arg2, object arg3, object arg4)
		{
			return new SignalRClientMessage(clients, ConvertCallMethod(method), arg1, arg2, arg3, arg4);
		}

		public static SignalRClientMessage Create(List<string> clients, string method, object arg1, object arg2, object arg3, object arg4, object arg5)
		{
			return new SignalRClientMessage(clients, ConvertCallMethod(method), arg1, arg2, arg3, arg4, arg5);
		}

		public static SignalRClientMessage Create(List<string> clients, string method, object arg1, object arg2, object arg3, object arg4, object arg5, object arg6)
		{
			return new SignalRClientMessage(clients, ConvertCallMethod(method), arg1, arg2, arg3, arg4, arg5, arg6);
		}

		public static string ConvertCallMethod(string method)
		{
			return string.Format("On{0}", method);
		}
	}
}
