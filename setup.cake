#tool "nuget:?package=GitVersion.CommandLine"
#tool nuget:?package=OpenCover
#tool nuget:?package=Codecov
#addin nuget:?package=Cake.Codecov
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

Task("Coverage").IsDependentOn("Tests").Does(() =>
{
	if(AppVeyor.IsRunningOnAppVeyor)
	{
		Codecov("./Source/Cake.Codecov/bin/coverage.xml");
	}
});

Task("Pack").IsDependentOn("Coverage").Does(() =>
{
	var version = GitVersion();
	DotNetCorePack("./Source/Cake.Codecov",new DotNetCorePackSettings{VersionSuffix = version.MajorMinorPatch, Configuration = configuration});
});

Task("Default").IsDependentOn("Pack");

RunTarget(target);