# Cake.Codecov

[![AppVeyor branch](https://img.shields.io/appveyor/ci/cakecontrib/cake-codecov/master.svg)](https://ci.appveyor.com/project/cakecontrib/cake-codecov/branch/master)
[![NuGet](https://img.shields.io/nuget/v/Cake.Codecov.svg)](https://www.nuget.org/packages/Cake.Codecov/)
[![Gitter](https://img.shields.io/gitter/room/nwjs/nw.js.svg?maxAge=2592000)](https://gitter.im/cake-contrib/Lobby)
[![Codecov](https://img.shields.io/codecov/c/github/cake-contrib/Cake.Codecov.svg)](https://codecov.io/gh/cake-contrib/Cake.Codecov)

A [Cake](http://cakebuild.net/) addin for [Codecov](https://codecov.io/).

## Usage

 In order to use this addin, add to your Cake script

```csharp
#tool nuget:?package=Codecov
#addin nuget:?package=Cake.Codecov
```

Then use one of the following snippets to upload your coverage report to Codecov

```csharp
Task("Upload-Coverage")
    .Does(() =>
{
    // Upload a coverage report.
    Codecov("coverage.xml");
});
```

```csharp
Task("Upload-Coverage")
    .Does(() =>
{
    // Upload coverage reports.
    Codecov(new[] { "coverage1.xml", "coverage2.xml" });
});
```

```csharp
Task("Upload-Coverage")
    .Does(() =>
{
    // Upload a coverage report by providing the Codecov upload token.
    Codecov("coverage.xml", "00000000-0000-0000-0000-000000000000");
});
```

```csharp
Task("Upload-Coverage")
    .Does(() =>
{
    // Upload coverage reports by providing the Codecov upload token.
    Codecov(new[] { "coverage1.xml", "coverage2.xml" }, "00000000-0000-0000-0000-000000000000");
});
```

```csharp
Task("Upload-Coverage")
    .Does(() =>
{
    // Upload a coverage report using the CodecovSettings.
    Codecov(new CodecovSettings {
        Files = new[] { "coverage.xml" },
        Token = "00000000-0000-0000-0000-000000000000",
        Flags = "ut"
    });
});
```

## Documentation

Documentation for the addin can be found on the [Cake Website](http://cakebuild.net/dsl/codecov/).

## Codecov Tips

1. The [codecov-exe](https://github.com/codecov/codecov-exe) uploader defined in `#tool nuget:?package=Codecov` currently only supports windows builds. However, OS X and Linux builds should come soon. In the mean time, there is a [bash global uploader](https://github.com/codecov/codecov-bash) that can be used.
2. Many CI services (like AppVeyor) do not require you to provide a Codecov upload token. However, TeamCity is a rare exception.
3. Using Codecov with TeamCity MAY require configuration. Please refer to the [codecov-exe documentation](https://github.com/codecov/codecov-exe#teamcity).

## Questions

Feel free to open an [issue](https://github.com/cake-contrib/Cake.Codecov/issues) or ask a question in [Gitter](https://gitter.im/cake-contrib/Lobby) by tagging us: **@larzw** and/or **@AdmiringWorm**.

## Known Issues
- Coverage report upload fails when using gitversion (or other tools that change the appveyor build version)
  Workaround: Add the following in your Upload Coverage task
  ```csharp
  Task("Upload-Coverage")
      .Does(() =>
  {
      var buildVersion = string.Format("{0}.build.{1}",
          variableThatStores_GitVersion_FullSemVer,
          BuildSystem.AppVeyor.Environment.Build.Version
      );
      var settings = new CodecovSettings {
          Files = new[] { "coverage.xml" },
          EnvironmentVariables = new Dictionary<string,string> { { "APPVEYOR_BUILD_VERSION", buildVersion } }
      };
      Codecov(settings);
  });
  ```
