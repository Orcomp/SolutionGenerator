// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITemplateRenderer.cs" company="Orcomp development team">
//   Copyright (c) 2012 - 2013 Orcomp development team. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace SolutionGenerator.Services
{
    public interface ITemplateRenderer
    {
        #region Methods
        string RenderFile<T>(string templateContent, T model);
        string RenderContent<T>(string templateContent, T model);
        #endregion
    }
}