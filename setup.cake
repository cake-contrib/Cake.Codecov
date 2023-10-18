#load nuget:?package=Cake.Recipe&version=3.1.1
#tool nuget:?package=NuGet.CommandLine&version=5.7.0 // Workaround necessary due to incompatibility with GHA nuget

Environment.SetVariableNames();

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

// Tasks we want to override
// ((CakeTask)BuildParameters.Tasks.UploadCodecovReportTask.Task).Actions.Clear();
// BuildParameters.Tasks.UploadCodecovReportTask
//     .IsDependentOn("DotNetCore-Pack")
//     .Does<BuildVersion>((version) => RequireTool(BuildParameters.IsDotNetCoreBuild ? ToolSettings.CodecovGlobalTool : ToolSettings.CodecovTool, () => {
//         var nugetPkg = $"nuget:file://{MakeAbsolute(BuildParameters.Paths.Directories.NuGetPackages)}?package=Cake.Codecov&version={version.SemVersion}&prerelease";
//         Information("PATH: " + nugetPkg);

//         var coverageFilter = BuildParameters.Paths.Directories.TestCoverage + "/coverlet/*.xml";
//         Information($"Passing coverage filter to codecov: \"{coverageFilter}\"");

//         var environmentVariables = new Dictionary<string, string>();

//         if (version != null && !string.IsNullOrEmpty(version.FullSemVersion) && BuildParameters.BuildProvider.SupportsTokenlessCodecov)
//         {
//             var buildVersion = string.Format("{0}.build.{1}",
//                 version.FullSemVersion,
//                 BuildSystem.AppVeyor.Environment.Build.Number);
//             environmentVariables.Add("APPVEYOR_BUILD_VERSION", buildVersion);
//         }

//         var script = string.Format(@"#addin ""{0}""
// Codecov(new CodecovSettings {{
//     Files = new[] {{ ""{1}"" }},
//     Root = ""{2}"",
//     Required = true
// }});",
//             nugetPkg, coverageFilter, BuildParameters.RootDirectoryPath);

//         RequireAddin(script, environmentVariables);
//     })
// );

Build.RunDotNetCore();
