// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleService.cs" company="Orcomp development team">
//   Copyright (c) 2012 - 2014 Orcomp development team. All rights reserved.
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
		#region Constructors
		public ConsoleService()
		{
			LogManager.LogMessage += OnLogMessage;
		}
		#endregion

		#region IConsoleService Members
		public event EventHandler<LogMessageEventArgs> LogMessage;
		#endregion

		#region Methods
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
		#endregion
	}
}