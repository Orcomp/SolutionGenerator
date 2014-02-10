// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleService.cs" company="Orcomp development team">
//   Copyright (c) 2012 - 2013 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SolutionGenerator.Services
{
    using System;
    using Catel;
    using Catel.Logging;

    public class ConsoleService : IConsoleService
    {
        public ConsoleService()
        {
            LogManager.LogMessage += OnLogMessage;
        }

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

        public event EventHandler<LogMessageEventArgs> LogMessage;
    }
}