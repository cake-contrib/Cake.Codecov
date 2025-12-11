#load nuget:?package=Cake.Recipe&version=4.0.0

Environment.SetVariableNames(
    codecovRepoTokenVariable: "CODECOV_TOKEN" // This is the name that codecov themself expect
);

BuildParameters.SetParameters(
                            context: Context,
                            buildSystem: BuildSystem,
                            sourceDirectoryPath: "./Source",
                            title: "Cake.Codecov",
                            repositoryOwner: "cake-contrib",
                            repositoryName: "Cake.Codecov",
                            appVeyorAccountName: "cakecontrib",
                            shouldRunDotNetCorePack: true,
                            shouldGenerateDocumentation: false,
                            shouldRunCodecov: true,
                            shouldRunCoveralls: false,
                            shouldUseDeterministicBuilds: true,
                            shouldUseTargetFrameworkPath: false,
                            preferredBuildAgentOperatingSystem: PlatformFamily.Linux,
                            preferredBuildProviderType: BuildProviderType.GitHubActions);

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(
                            context: Context,
                            testCoverageFilter: "+[Cake.Codecov]*");
ToolSettings.SetToolPreprocessorDirectives();

// Since Cake.Recipe does not properly detect .NET only test projects, we need
// to override how the tests are running.
((CakeTask)BuildParameters.Tasks.DotNetCoreTestTask.Task).Actions.Clear();
BuildParameters.Tasks.DotNetCoreTestTask.Does<DotNetCoreMSBuildSettings>((context, msBuildSettings) => {

    var projects = GetFiles(BuildParameters.TestDirectoryPath + (BuildParameters.TestFilePattern ?? "/**/*Tests.csproj"));
    // We create the coverlet settings here so we don't have to create the filters several times
    var coverletSettings = new CoverletSettings
    {
        CollectCoverage         = true,
        // It is problematic to merge the reports into one, as such we use a custom directory for coverage results
        CoverletOutputDirectory = BuildParameters.Paths.Directories.TestCoverage.Combine("coverlet"),
        CoverletOutputFormat    = CoverletOutputFormat.opencover,
        ExcludeByFile           = ToolSettings.TestCoverageExcludeByFile.Split(new [] {';' }, StringSplitOptions.None).ToList(),
        ExcludeByAttribute      = ToolSettings.TestCoverageExcludeByAttribute.Split(new [] {';' }, StringSplitOptions.None).ToList()
    };

    foreach (var filter in ToolSettings.TestCoverageFilter.Split(new [] {' ' }, StringSplitOptions.None))
    {
        if (filter[0] == '+')
        {
            coverletSettings.WithInclusion(filter.TrimStart('+'));
        }
        else if (filter[0] == '-')
        {
            coverletSettings.WithFilter(filter.TrimStart('-'));
        }
    }
    var settings = new DotNetCoreTestSettings
    {
        Configuration = BuildParameters.Configuration,
        NoBuild = true
    };

    foreach (var project in projects)
    {
        Action<ICakeContext> testAction = tool =>
        {
            tool.DotNetCoreTest(project.FullPath, settings);
        };

        var parsedProject = ParseProject(project, BuildParameters.Configuration);

        var coverletPackage = parsedProject.GetPackage("coverlet.msbuild");
        bool shouldAddSourceLinkArgument = false; // Set it to false by default due to OpenCover
        if (coverletPackage != null)
        {
            // If the version is a pre-release, we will assume that it is a later
            // version than what we need, and thus TryParse will return false.
            // If TryParse is successful we need to compare the coverlet version
            // to ensure it is higher or equal to the version that includes the fix
            // for using the SourceLink argument.
            // https://github.com/coverlet-coverage/coverlet/issues/882
            Version coverletVersion;
            shouldAddSourceLinkArgument = !Version.TryParse(coverletPackage.Version, out coverletVersion)
                || coverletVersion >= Version.Parse("2.9.1");
        }

        settings.ArgumentCustomization = args => {
            args.AppendMSBuildSettings(msBuildSettings, context.Environment);
            if (shouldAddSourceLinkArgument && parsedProject.HasPackage("Microsoft.SourceLink.GitHub"))
            {
                args.Append("/p:UseSourceLink=true");
            }
            return args;
        };

        coverletSettings.CoverletOutputName = parsedProject.RootNameSpace.Replace('.', '-');
            DotNetCoreTest(project.FullPath, settings, coverletSettings);
    }
});

// Tasks we want to override
((CakeTask)BuildParameters.Tasks.UploadCodecovReportTask.Task).Actions.Clear();
BuildParameters.Tasks.UploadCodecovReportTask
    .IsDependentOn("DotNetCore-Pack")
    .Does<BuildVersion>((version) => RequireTool(ToolSettings.CodecovTool, () => {
        // var nugetPkg =  $"nuget:file://{MakeAbsolute(BuildParameters.Paths.Directories.NuGetPackages)}?package=Cake.Codecov&version={version.SemVersion}&prerelease";
        var nugetPkg = "nuget:?package=Cake.Codecov&version=1.1.0"; // We are unable to dogfood the library until Cake.Recipe supports Cake 2.0.0
        Information("PATH: " + nugetPkg);

        var coverageFilter = BuildParameters.Paths.Directories.TestCoverage + "/coverlet/*.xml";
        Information($"Passing coverage filter to codecov: \"{coverageFilter}\"");

        var environmentVariables = new Dictionary<string, string>();

        if (version != null && !string.IsNullOrEmpty(version.FullSemVersion))
        {
            var buildVersion = string.Format("{0}.build.{1}",
                version.FullSemVersion,
                BuildSystem.AppVeyor.Environment.Build.Number);
            environmentVariables.Add("APPVEYOR_BUILD_VERSION", buildVersion);
        }

        if (!string.IsNullOrEmpty(BuildParameters.Codecov.RepoToken))
        {
            environmentVariables.Add("CODECOV_TOKEN", BuildParameters.Codecov.RepoToken);
        }

        var script = string.Format(@"#addin ""{0}""
Codecov(new CodecovSettings {{
    Files = new[] {{ ""{1}"" }},
    RootDirectory = ""{2}"",
    NonZero = !string.IsNullOrEmpty(EnvironmentVariable(""CODECOV_TOKEN""))
}});",
            nugetPkg, coverageFilter, BuildParameters.RootDirectoryPath);

        RequireAddin(script, environmentVariables);
    })
);

Build.RunDotNetCore();
