Solution Generator
==================

Simple application that will create the project structure, solution and project files as well as other artifacts to start working on a new project at the click of a button.

There are a lot of projects out there that make building and releasing code easier, but there is not a lot out there that makes creating new projects easier. Our aim is to fill this gap with Solution Generator.

This project was born out of the need to have a "standard" for creating projects quickly.

We follow the standards documented here: https://github.com/Orcomp/Standards, but the tool should be flexible enough to be customised to your own needs.

![ScreenShot](/doc/img/screenshot.png)

# How to use

## Creating a solution

To use this app, please templates (in the form of assemblies) into one of the following directories:

* [exeDirectory]\Templates
* [exeDirectory]\Plugins
* %appdata%\WildGums\Solution Generator

Run the app, select your template generator plugin and enter the required values.

## Creating a template

To create a new template, you need to create a .NET 4.6 library assembly and reference the `SolutionGenerator.Api` assembly.

### Creating a template part

The template parts are separated parts in the template system that take care of replacing the template keys by actual values. The Solution Generator provides some template parts out of
the box, but below is an extended one showing how to define additional data for a GitHub repository.


    public class GitHubTemplate : TemplateBase
    {
        public GitHubTemplate()
        {
            // This could be used for additional data
        }

        public string Company { get; set; }

        public string RepositoryName { get; set; }
    }


Template parts are very important because they automatically map to the keys that are available in the template system:

`[TemplateContainerName].[PropertyName]'

In the example above, the following constants are available to the template files:

* GitHub.Company
* GitHub.RepositoryName

Template keys are always surrounded by double block quotes, so the keys become:

* [[GitHub.Company]]
* [[GitHub.RepositoryName]]

### Creating a template context

The template context describes the context of the template every time it is being run. It's a container store for the settings of the template. The default `TemplateContextBase` already
implements the basics. If more values are needed, additional `ITemplate` implementations can be added (as we created in the previous part).

The template context below defines an additional template part on the context, but also sets some default values for the existing ones.


    public class OrcComponentTemplateContext : TemplateContextBase
    {
        public OrcComponentTemplateContext()
        {
            var assembly = GetType().GetAssemblyEx();

            Company.Name = assembly.Company();

            Solution.Name = "Orc.";

            NuGet.PackageName = "Orc.";

            GitHub = new GitHubTemplate();
            GitHub.Company = assembly.Company();
            GitHub.RepositoryName = "Orc.";
        }

        public GitHubTemplate GitHub { get; protected set; }
    }


### Creating a template definition

The template definition describes the plugin model of the Solution Generator. Below is an example of a template definition that uses the template context created in the previous 
steps. It also returns a settings view so the options can be customized.


	public class OrcComponentTemplateDefinition : TemplateDefinitionBase<OrcComponentTemplateContext>
	{
		public override FrameworkElement GetView()
		{
			return new SettingsView();
		}
	}


### Creating a custom settings view

If a custom settings view is required, just create any WPF user control. The `DataContext` of the control will automatically be set to the template context. 

If no settings are required, return `null` in the `GetView` method of the template definition.

### Creating the template content

The actual content are just files. These can be based on an already existing project that has been set up and works very well. The only thing left is to create a subfolder in the 
class library called `Files` (can be customized if required, but this is the default). Copy all files that should be used for template creation into this directory and set the
build action to `Resource` (the default type).

The supported file template file types are:

* EmbeddedResource
* FileResource
* ZippedFileResource
* Resource

The last step is to replace any important values by template keys. Below is an example for a license:


	The MIT License (MIT)
	
	Copyright (c) 2014-[[DATETIME.YEAR]] [[COMPANY.NAME]]
	
	Permission is hereby granted, free of charge, ... etc

### Using modifiers

The template keys support modifiers. Modifiers are placed after the template key (but inside the block) and the values will be modified in the order in which they appear. The
template key in the following example will use the template value `Solution.Name` and will then convert it to upper case:

[[Solution.Name|uppercase]]

Multiple modifiers are supported, they must be separated by a `|`.

For now the following modifiers are supported:

* Uppercase => Make the value uppercase (e.g. 'SomeValue' becomes 'SOMEVALUE')
* Lowercase => Make the value lowercase (e.g. 'SomeValue' becomes 'somevalue')
* Camelcase => Use camelcase (e.g. 'SomeValue' becomes 'someValue')
* UpperCamelCase => Use camelcase but make the first letter uppercase (e.g. 'someValue' becomes 'SomeValue')
* Alphanumeric => Replace all characters that are not alphanumber (e.g. 'Some! Value' becomes 'SomeValue')

## Using loops

### For template files

To create a template for a loop, use variable names that return a collection and prefix them with `Foreach`:

	[[ForEach D.RECORDS]]Map

This will generate a file for each item returned by `D.RECORDS` (which should return a collection). The containing item should implement a `ToString()` method because that will be used to generate the file name.

### Inside template files

It's possible to use loops inside the templates.

	[[BeginForeach D.RECORDS]]
	[[RECORDNAME]]s = new List<[[RECORDNAME]]>();
	[[EndForeach]]

`D.RECORDS` is the template property to bind to (this should be a collection of any type). Inside the `Foreach` template, the scope is the item itself (so the type of the collection items).

**Nested foreach templates are not yet supported, feel free to add support using a PR**
