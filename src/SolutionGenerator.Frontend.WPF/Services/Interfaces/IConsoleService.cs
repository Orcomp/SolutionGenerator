// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IConsoleService.cs" company="Orcomp development team">
//   Copyright (c) 2012 - 2013 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SolutionGenerator.Services
{
    using System;
    using Catel.Logging;

    public interface IConsoleService
    {
        event EventHandler<LogMessageEventArgs> LogMessage;
    }
}