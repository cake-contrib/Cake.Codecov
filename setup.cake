#tool nuget:?package=GitVersion.CommandLine
#tool nuget:?package=OpenCover&version=4.6.519
#tool nuget:?package=Codecov&version=1.0.3
#addin nuget:?package=Cake.Figlet

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

Setup(context =>
{
	Information(Figlet("Cake.Codecov"));
});

Task("Clean").Does(() =>
{
	CleanDirectories(new[]{"./Source/Cake.Codecov/bin","./Source/Cake.Codecov.Tests/bin"});
});

Task("Restore").IsDependentOn("Clean").Does(() =>
{
	DotNetCoreRestore("./Source");
});

Task("Build").IsDependentOn("Restore").Does(() =>
{
	DotNetCoreBuild("./Source", new DotNetCoreBuildSettings{Configuration = configuration});
});

Task("Tests").IsDependentOn("Build").Does(() =>
{
	OpenCover(tool => tool.DotNetCoreTest("./Source/Cake.Codecov.Tests/Cake.Codecov.Tests.csproj"), new FilePath("./Source/Cake.Codecov/bin/coverage.xml"), new OpenCoverSettings { OldStyle = true }.WithFilter("+[Cake.Codecov]*"));
});

Task("Coverage").IsDependentOn("Tests").WithCriteria(AppVeyor.IsRunningOnAppVeyor).Does(() =>
{
	// Using GetFiles instead of File just so we don't need to make a call to MakeAbsolute.
	var file = GetFiles("./Source/**/net46/Cake.Codecov.dll").First();
	var tool = Context.Tools.Resolve("Codecov.exe");
	var reportFile = GetFiles("./Source/**/coverage.xml").First();
	Information("Loading built addin from: {0}", file);
	Information("Using Codecov tool from: {0}", tool);
	Information("Using Coverage report from: {0}", reportFile);
		CakeExecuteExpression(
			string.Format(
				@"#reference ""{0}""
Codecov(new CodecovSettings {{ ToolPath = ""{1}"", Files = new[] {{ ""{2}"" }}, Root = ""{3}"" }});
				",
				file, tool, reportFile, Directory("./").Path.MakeAbsolute(Context.Environment))
		);
});

Task("Pack").IsDependentOn("Coverage").Does(() =>
{
	var version = GitVersion();
	DotNetCorePack("./Source/Cake.Codecov",new DotNetCorePackSettings{VersionSuffix = version.MajorMinorPatch, Configuration = configuration});
});

Task("Default").IsDependentOn("Pack");

RunTarget(target);
