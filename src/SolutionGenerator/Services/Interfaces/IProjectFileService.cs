// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProjectFileService.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Services
{
    public interface IProjectFileService
    {
        void Open(string fileName);
        void AddItem(string buildAction, string siblingNodeContains, string item);
        void RemoveItem(string buildAction, string item);
        void Save(string postfix = "");
    }
}