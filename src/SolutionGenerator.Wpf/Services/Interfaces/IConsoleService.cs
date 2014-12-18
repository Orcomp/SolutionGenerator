// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IConsoleService.cs" company="Orcomp development team">
//   Copyright (c) 2012 - 2014 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SolutionGenerator.Wpf.Services.Interfaces
{
	using System;
	using Catel.Logging;

	public interface IConsoleService
	{
		event EventHandler<LogMessageEventArgs> LogMessage;
	}
}