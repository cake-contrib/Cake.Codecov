#tool "nuget:?package=GitVersion.CommandLine"
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
	DotNetCoreTest("./Source/Cake.Codecov.Tests/Cake.Codecov.Tests.csproj");
});

Task("Pack").IsDependentOn("Tests").Does(() =>
{
	var version = GitVersion();
	DotNetCorePack("./Source/Cake.Codecov",new DotNetCorePackSettings{VersionSuffix = version.MajorMinorPatch, Configuration = configuration});
});

Task("Default").IsDependentOn("Pack");

RunTarget(target);