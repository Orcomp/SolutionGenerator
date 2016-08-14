// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace [[SOLUTION.NAME]]
{
	using System;
	using System.Reflection;

	static class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine($"[[SOLUTION.NAME]].Console v{Assembly.GetExecutingAssembly().GetName().Version}\n");
			Console.WriteLine("See unit tests for a sample how to read data files.\n");
		}
	}
}