# Cake.Codecov

[![AppVeyor branch](https://img.shields.io/appveyor/ci/larzw/cake-codecov/master.svg)](https://ci.appveyor.com/project/larzw/cake-codecov/branch/master)
[![NuGet](https://img.shields.io/nuget/v/Cake.Codecov.svg)](https://www.nuget.org/packages/Cake.Codecov/)

A Cake addin for Codecov. In order to use this addin, add to the top of your Cake script

```csharp
#tool nuget:?package=Codecov
#addin nuget:?package=Cake.Codecov
```

## Examples

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

## Codecov Tips

1. The [Codecov](https://github.com/codecov/codecov-exe) uploader defined in `#tool nuget:?package=Codecov` currently only supports windows builds. However, OS X and Linux builds should come soon. In the mean time, there is a [bash](https://github.com/codecov/codecov-bash) uploader that can be used.
2. Many CI services (like AppVeyor) do not require you to provide a Codecov upload token. However, TeamCity is a rare exception.
3. Using Codecov with TeamCity MAY require configuration. Please refer to the [documentation](https://github.com/codecov/codecov-exe#teamcity).

# Questions

Feel free to open an issue or **@larzw** me via [Gitter](https://gitter.im/cake-contrib/Lobby)
