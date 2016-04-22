// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProjectFileService.cs" company="WildGums">
//   Copyright (c) 2012 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace SolutionGenerator.Services
{
	using System;
	using System.Linq;
	using System.Xml.Linq;

	public class ProjectFileService : IProjectFileService
	{
		private XDocument _document;
		private string _fileName;
		private XNamespace _nameSpace;

		public void Open(string fileName)
		{
			_fileName = fileName;
			_document = XDocument.Load(fileName);
			_nameSpace = _document.Root?.Name.Namespace;
		}

		public void AddItem(string buildAction, string siblingNodeContains, string item)
		{
			var siblingElement = _document.Root?
				.Descendants(_nameSpace + buildAction)
				//.FirstOrDefault(e => e.Attribute(_nameSpace + "Include") != null && e.Attribute(_nameSpace + "Include").Value.ToLower().Contains(siblingNodeContains));
				.FirstOrDefault(e => e.Attribute("Include") != null && e.Attribute("Include").Value.ToLower().Contains(siblingNodeContains));

			var parentElement = siblingElement?.Parent;
			if (parentElement == null)
			{
				throw new ApplicationException($"Can not add {buildAction}Item {item} to project file {_fileName}");
			}
			var siblingAttributeValue = siblingElement.Attribute("Include").Value;
			var split = siblingAttributeValue.Split('\\');
			if (split.Length <= 1)
			{
				split = siblingAttributeValue.Split(')');
			}
			var newAttributeValue = siblingAttributeValue.Replace(split.Last(), item);
			var newElement = new XElement(siblingElement);
			newElement.Attribute("Include").SetValue(newAttributeValue);

			parentElement.Add(newElement);
		}

		public void RemoveItem(string buildAction, string item)
		{
			var element = _document.Root?
				.Descendants(_nameSpace + buildAction)
				.FirstOrDefault(e => e.Attribute("Include").Value.ToLower().Contains(item));

			if (element == null)
			{
				throw new ApplicationException($"Can not remove {buildAction} Item {item} to project file {_fileName}");
			}
			element.Remove();
		}

		public void Save(string postfix = "")
		{
			_document.Save(_fileName+postfix, SaveOptions.None);
		}
	}
}