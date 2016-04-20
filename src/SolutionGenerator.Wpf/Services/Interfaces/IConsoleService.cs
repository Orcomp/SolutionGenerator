// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IConsoleService.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
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