using System;

namespace TicTacToeServer.Core
{
	public static class AppSignalRLogger
	{
		public static AppSignalRLogger.Loglevels Level { get; set; } = AppSignalRLogger.Loglevels.Debug;

		public static void LogVerbose(string format, params object[] args)
		{
			if (AppSignalRLogger.Level > AppSignalRLogger.Loglevels.Verbose) return;
			Console.WriteLine(string.Format(format, args));
		}

		public static void Log(string format, params object[] args)
		{
			if (AppSignalRLogger.Level > AppSignalRLogger.Loglevels.Debug) return;
			Console.WriteLine(string.Format(format, args));
		}

		public static void LogWarning(string format, params object[] args)
		{
			if (AppSignalRLogger.Level > AppSignalRLogger.Loglevels.Warning) return;
			Console.WriteLine(string.Format(format, args));
		}

		public static void LogError(string format, params object[] args)
		{
			if (AppSignalRLogger.Level > AppSignalRLogger.Loglevels.Error) return;
			Console.WriteLine(string.Format(format, args));
		}

		public enum Loglevels : byte
		{
			Verbose,
			Debug,
			Warning,
			Error,
			None,
		}
	}

}
