// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleService.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Wpf.Services
{
	using System;
	using Catel;
	using Catel.Logging;
	using Interfaces;

	public class ConsoleService : IConsoleService
	{
		public ConsoleService()
		{
			LogManager.LogMessage += OnLogMessage;
		}

		public event EventHandler<LogMessageEventArgs> LogMessage;

		private void OnLogMessage(object sender, LogMessageEventArgs e)
		{
			if (e.LogEvent != LogEvent.Debug)
			{
				if (!e.Message.Contains("Catel"))
				{
					LogMessage.SafeInvoke(this, e);
				}
			}
		}
	}
}