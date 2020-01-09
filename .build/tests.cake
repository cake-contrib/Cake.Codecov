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
        NoBuild = true,
        Logger = "xunit",
        ResultsDirectory = BuildParameters.Paths.Directories.TestResults,
        ArgumentCustomization = (args) => args.AppendSwitch("--logger", ":", "appveyor")
    };

    foreach (var project in projects) {
        DotNetCoreTest(project.FullPath, testSettings, settings);
    }

    var coverageFilter = BuildParameters.Paths.Files.TestCoverageOutputFilePath.ToString().Replace(".xml", "*.xml");
    Information($"Finding coverage results with filer: {coverageFilter}");

    var reportFiles = GetFiles(coverageFilter);
    Information($"Found {reportFiles.Count} files");

    if (reportFiles.Any()) {
        ReportGenerator(reportFiles, BuildParameters.Paths.Directories.TestCoverage);
    }
});
