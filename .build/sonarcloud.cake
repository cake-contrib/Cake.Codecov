const string SonarQubeTool = "#tool nuget:?package=MSBuild.SonarQube.Runner.Tool&version=4.6.0";
const string SonarQubeAddin = "#addin nuget:?package=Cake.Sonar&version=1.1.22";

Task("SonarCloud-Begin")
    .IsDependentOn("DotNetCore-Restore")
    .IsDependeeOf("DotNetCore-Build")
    .WithCriteria(() => HasEnvironmentVariable("SONARCLOUD_TOKEN"), "Missing sonar cloud token environment variable")
    .WithCriteria(() => HasEnvironmentVariable("SONARCLOUD_ORGANIZATION"), "Missing sonar cloud organization environment variable")
    .WithCriteria(() => HasEnvironmentVariable("SONARCLOUD_PROJECT_KEY"), "Missing sonar cloud project key environment variable")
    .Does(() => RequireTool(SonarQubeTool,
    () =>
{
    Information("Starting SonarCloud analysing");

    RequireAddin(SonarQubeAddin + @"
    SonarBegin(new SonarBeginSettings {
        Key = EnvironmentVariable(""TEMP_PROJECT_KEY""),
        Branch = EnvironmentVariable(""TEMP_BUILD_BRANCH""),
        Organization = EnvironmentVariable(""TEMP_ORGANIZATION""),
        Url = ""https://sonarcloud.io"",
        Exclusions = ""**/*.Tests,**/GlobalSuppressions.cs"", // Global suppresions is added to prevent sonar cloud to report suppressed warnings
        OpenCoverReportsPath = EnvironmentVariable(""TEMP_OPENCOVER_FILTER""),
        Login = EnvironmentVariable(""TEMP_TOKEN""),
        Version = EnvironmentVariable(""TEMP_VERSION""),
        XUnitReportsPath = EnvironmentVariable(""TEMP_TEST_RESULTS""),
    });",
    new Dictionary<string, string> {
        { "TEMP_PROJECT_KEY", EnvironmentVariable("SONARCLOUD_PROJECT_KEY") },
        { "TEMP_BUILD_BRANCH", BuildParameters.BuildProvider.Repository.Branch },
        { "TEMP_ORGANIZATION", EnvironmentVariable("SONARCLOUD_ORGANIZATION") },
        { "TEMP_OPENCOVER_FILTER", BuildParameters.Paths.Files.TestCoverageOutputFilePath.ToString().Replace(".xml", "*.xml") },
        { "TEMP_TOKEN", EnvironmentVariable("SONARCLOUD_TOKEN") },
        { "TEMP_VERSION", BuildParameters.Version.SemVersion },
        { "TEMP_TEST_RESULTS", BuildParameters.Paths.Directories.TestResults.CombineWithFilePath("TestResults.xml").ToString() },
    });
}));

Task("SonarCloud-End")
    .IsDependentOn("Test")
    .IsDependeeOf("Analyze")
    .WithCriteria(() => HasEnvironmentVariable("SONARCLOUD_TOKEN"), "Missing sonar cloud token environment variable")
    .WithCriteria(() => HasEnvironmentVariable("SONARCLOUD_ORGANIZATION"), "Missing sonar cloud organization environment variable")
    .WithCriteria(() => HasEnvironmentVariable("SONARCLOUD_PROJECT_KEY"), "Missing sonar cloud project key environment variable")
    .Does(() => RequireTool(SonarQubeTool, () => RequireAddin(SonarQubeAddin + @"
    SonarEnd(new SonarEndSettings {
        Login = EnvironmentVariable(""TEMP_TOKEN""),
    });",
    new Dictionary<string, string> {
        { "TEMP_TOKEN", EnvironmentVariable("SONARCLOUD_TOKEN") },
    })
));
