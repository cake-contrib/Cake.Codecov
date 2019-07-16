#load "nuget:https://ci.appveyor.com/nuget/cake-recipe-pylg5x5ru9c2?package=Cake.Recipe&prerelease&version=0.3.0-alpha0500"
#tool "nuget:?package=Codecov&version=1.6.1"
#addin "nuget:?package=Cake.Coverlet&version=2.3.4"

Environment.SetVariableNames();

BuildParameters.SetParameters(context: Context,
                              buildSystem: BuildSystem,
                              sourceDirectoryPath: "./Source",
                              title: "Cake.Codecov",
                              repositoryOwner: "cake-contrib",
                              repositoryName: "Cake.Codecov",
                              appVeyorAccountName: "cakecontrib",
                              shouldRunDotNetCorePack: true,
                              shouldBuildNugetSourcePackage: false,
                              shouldExecuteGitLink: false,
                              shouldGenerateDocumentation: false,
                              shouldRunCodecov: true,
                              shouldRunGitVersion: true);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(context: Context,
                             dupFinderExcludePattern: new string[] {
                                 BuildParameters.RootDirectoryPath + "/Source/Cake.Codecov.Tests/*.cs"
                             },
                             dupFinderExcludeFilesByStartingCommentSubstring: new string[] {
                                 "<auto-generated>"
                             },
                             testCoverageFilter: "+[Cake.Codecov]*",
                             frameworkPathApiVersion: "4.5");

// Tasks we want to override
((CakeTask)BuildParameters.Tasks.UploadCodecovReportTask.Task).Actions.Clear();
((CakeTask)BuildParameters.Tasks.UploadCodecovReportTask.Task).Criterias.Clear();
BuildParameters.Tasks.UploadCodecovReportTask
    .IsDependentOn("DotNetCore-Pack")
    /*.WithCriteria(() => FileExists(BuildParameters.Paths.Files.TestCoverageOutputFilePath))
    .WithCriteria(() => BuildParameters.IsMainRepository)*/
    .Does(() => {
        var nugetPkg = $"nuget:file://{MakeAbsolute(BuildParameters.Paths.Directories.NuGetPackages)}?package=Cake.Codecov&version={BuildParameters.Version.SemVersion}&prepelease";
        Information("PATH: " + nugetPkg);

        var reportFile = BuildParameters.Paths.Files.TestCoverageOutputFilePath;
        Information("Using Coverage report from: {0}", reportFile);
        var script = string.Format(@"#addin ""{0}""
Codecov(new CodecovSettings {{
    Files = new[] {{ ""{1}"" }},
    Root = ""{2}"",
    Required = true,
    EnvironmentVariables = new Dictionary<string,string> {{ {{ ""APPVEYOR_BUILD_VERSION"", EnvironmentVariable(""TEMP_BUILD_VERSION"") }} }}
}});",
            nugetPkg, reportFile, BuildParameters.RootDirectoryPath);
        RequireAddin(script, new Dictionary<string,string> {
            { "TEMP_BUILD_VERSION", BuildParameters.Version.FullSemVersion + ".build." + BuildSystem.AppVeyor.Environment.Build.Number }
            });

});

((CakeTask)BuildParameters.Tasks.DotNetCoreTestTask.Task).Actions.Clear();
((CakeTask)BuildParameters.Tasks.DotNetCoreTestTask.Task).Criterias.Clear();
((CakeTask)BuildParameters.Tasks.DotNetCoreTestTask.Task).Dependencies.Clear();

BuildParameters.Tasks.DotNetCoreTestTask
    .IsDependentOn("Install-ReportGenerator")
    .Does(() => {
    var projects = GetFiles(BuildParameters.TestDirectoryPath + (BuildParameters.TestFilePattern ?? "/**/*Tests.csproj"));
    var testFileName = BuildParameters.Paths.Files.TestCoverageOutputFilePath.GetFilename();
    var testDirectory = BuildParameters.Paths.Files.TestCoverageOutputFilePath.GetDirectory();

    var settings = new CoverletSettings {
        CollectCoverage = true,
        CoverletOutputFormat = CoverletOutputFormat.opencover,
        CoverletOutputDirectory = testDirectory,
        CoverletOutputName = testFileName.ToString(),
        MergeWithFile = BuildParameters.Paths.Files.TestCoverageOutputFilePath
    };
    foreach (var line in ToolSettings.TestCoverageExcludeByFile.Split(';')) {
        foreach (var file in GetFiles("**/" + line)) {
            settings = settings.WithFileExclusion(file.FullPath);
        }
    }

    foreach (var item in ToolSettings.TestCoverageFilter.Split(' ')) {
        if (item[0] == '+') {
            settings.WithInclusion(item.TrimStart('+'));
        }
        else if (item[0] == '-') {
            settings.WithFilter(item.TrimStart('-'));
        }
    }

    var testSettings = new DotNetCoreTestSettings {
        Configuration = BuildParameters.Configuration,
        NoBuild = true
    };

    foreach (var project in projects) {
        DotNetCoreTest(project.FullPath, testSettings, settings);
    }

    if (FileExists(BuildParameters.Paths.Files.TestCoverageOutputFilePath)) {
        ReportGenerator(BuildParameters.Paths.Files.TestCoverageOutputFilePath, BuildParameters.Paths.Directories.TestCoverage);
    }
});

// Enable drafting a release when running on the master branch
if (BuildParameters.IsRunningOnAppVeyor &&
    BuildParameters.IsMainRepository && BuildParameters.IsMasterBranch && !BuildParameters.IsTagged)
{
    BuildParameters.Tasks.AppVeyorTask.IsDependentOn("Create-Release-Notes");
}

Task("Linux")
    .IsDependentOn("Package")
    .IsDependentOn("Upload-Coverage-Report");

Task("Appveyor-Linux")
    .IsDependentOn("Linux")
    .IsDependentOn("Upload-AppVeyor-Artifacts");


Build.RunDotNetCore();
