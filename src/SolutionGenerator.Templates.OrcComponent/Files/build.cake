var projectName = "[[SOLUTION.NAME]]";
var projectsToPackage = new [] { "[[SOLUTION.NAME]]" };
var company = "[[COMPANY.NAME]]";
var startYear = 2018;
var defaultRepositoryUrl = string.Format("https://github.com/{0}/{1}", company, projectName);

#l "./deployment/cake/variables.cake"
#l "./deployment/cake/tasks.cake"
