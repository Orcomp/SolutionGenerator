// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITemplateRenderer.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Services
{
	public interface ITemplateRenderer
	{
		string RenderFile<T>(string templateContent, T model);
		string RenderContent<T>(string templateContent, T model);
	}
}