SolutionGenerator
=================

Simple application that will create the project structure, solution and project files as well as other artifacts to start working on a new project at the click of a button.

There are a lot of projects out there that make building and releasing code easier, but there is not a lot out there that makes creating new projects easier. Our aim is to fill this gap with SolutionGenerator.

This project was born out of the need to have a "standard" for creating projects quickly.

We follow the standards documented here: https://github.com/Orcomp/Standards, but the tool should be flexible enough to be customised to your own needs.

![ScreenShot](/img/screenshot.png)

## Features

Things SolutionGenerator will do for you **at the click of a button**:

- Runtime solution template discovery. 
- Create your own solution template with unlimited number of projects
- This repo has 2 simple templates out of the box
- Create the following files (and populates them):
    - Editable Readme.md
    - Pick Licence.txt from more than a dozen well known licence types
    - .gitignore (currently comes from the template)
    - .gitattributes (currently comes from the template)
    - stlylecop settings (currently comes from the template)
    - Resharper settings (currently comes from the template)
    - Initializes a git repository (files not added yet)


Out of the box templates include the following features: 

- All sources are under the /src folder
- All projects are configured to build artifacts into an "/output/configname/projectname" folder.
- NuGet are configured to use the /lib folder
- NuGet.exe included in the /tools/nuget folder
- Utility scripts included in the repository root:
	- Update NuGet.exe
    - Restore packages
    - Clean all


Once you click the button to create the solution, it will do all of the above, and  immediately start Visual Studio, so you can start working on your project in no time ;)

## Customising

Adding a new template to the template set is as easy as:

- create a VS solution interactively using your favorite tool
- remove some folders to spare with space like 'obj'. (optional, recommended) 
- place a text file named '.description' into the root (optional, recommended). For syntax please refer to the included template zips in SolutionGenerator project's /Templates folder.
- ZIP the whole folder, name according your preference
- place the zip file to the executable's folder under ./templates
- if you would like to make your template to an out of the box template, include the .zip file into the SolutionValidator project's Templates folder as content file, and set its property 'Copy to Output Directory'  to 'Copy if newer'

The tool will pick up the .zip templates at run time.
The solution name and 'base' project name will be inferred from the template, no magic macros are used.
To allow this simple infer to work there is a simple rule about project naming: The longest common project name starting part will be inferred as 'base' project name and will replaced with the new project name. For example in case the template contains 4 projects like:

- AnyName.Core.csproj
- AnyName.Core.Tests.csproj
- AnyName.Wpf..csproj
- AnyName.Wpf.Tests.csproj

then AnyName will be inferred as 'base' project name. In case your input for generation is MyNewApp then you will get a new solution with projects:

- MyNewApp.Core.csproj
- MyNewApp.Core.Tests.csproj
- MyNewApp.Wpf..csproj
- MyNewApp.Wpf.Tests.csproj

Solution name is infrerred and replaced independently from 'base' project name.
Solution root folder name (repository root) is also an independent input.


## Building The Solution

The first time you build the solution, NuGet will fetch the required packages to the /lib folder. In case of any error please do not enable NuGet restore on the solution, instead close VS, go to the repository root folder and execute 'scripts - Restore packages.bat' 

## Roadmap

- **SolutionChecker** : will be used to check whether the solution structure and code conforms to a set of standards.

## License

This project is open source and released under the MIT license.

Please contribute to make it better ;)