# Cake.Codecov
[![All Contributors](https://img.shields.io/badge/all_contributors-8-orange.svg?style=flat-square)](#contributors)

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

1. The [codecov-exe](https://github.com/codecov/codecov-exe) uploader defined in `#tool nuget:?package=Codecov` currently only supports windows builds. However, OS X and Linux builds should come soon. In the mean time, there is a [bash global uploader](https://github.com/codecov/codecov-bash) that can be used. (*Note: There also the [Codecov.Tool](https://www.nuget.org/packages/Codecov.Tool) utility, however, Linux and OS X is only in partial support. IE, it will run, but no CI will be automatically found*)
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

## Contributors

Thanks goes to these wonderful people ([emoji key](https://allcontributors.org/docs/en/emoji-key)):

<!-- ALL-CONTRIBUTORS-LIST:START - Do not remove or modify this section -->
<!-- prettier-ignore -->
<table><tr><td align="center"><a href="https://www.linkedin.com/in/larz-white-5a8264108"><img src="https://avatars0.githubusercontent.com/u/6298611?v=4" width="100px;" alt="Larz White"/><br /><sub><b>Larz White</b></sub></a><br /><a href="#maintenance-larzw" title="Maintenance">🚧</a></td><td align="center"><a href="https://github.com/AdmiringWorm"><img src="https://avatars3.githubusercontent.com/u/1474648?v=4" width="100px;" alt="Kim J. Nordmo"/><br /><sub><b>Kim J. Nordmo</b></sub></a><br /><a href="#maintenance-AdmiringWorm" title="Maintenance">🚧</a></td><td align="center"><a href="http://www.gep13.co.uk/blog"><img src="https://avatars3.githubusercontent.com/u/1271146?v=4" width="100px;" alt="Gary Ewan Park"/><br /><sub><b>Gary Ewan Park</b></sub></a><br /><a href="#review-gep13" title="Reviewed Pull Requests">👀</a> <a href="https://github.com/cake-contrib/Cake.Codecov/issues?q=author%3Agep13" title="Ideas, Planning, & Feedback">🤔</a></td><td align="center"><a href="https://github.com/vkbishnoi"><img src="https://avatars0.githubusercontent.com/u/8297727?v=4" width="100px;" alt="Vishal Bishnoi"/><br /><sub><b>Vishal Bishnoi</b></sub></a><br /><a href="https://github.com/cake-contrib/Cake.Codecov/commits?author=vkbishnoi" title="Code">💻</a></td><td align="center"><a href="https://twitter.com/hereispascal"><img src="https://avatars1.githubusercontent.com/u/2190718?v=4" width="100px;" alt="Pascal Berger"/><br /><sub><b>Pascal Berger</b></sub></a><br /><a href="https://github.com/cake-contrib/Cake.Codecov/issues?q=author%3Apascalberger" title="Ideas, Planning, & Feedback">🤔</a></td><td align="center"><a href="https://github.com/twenzel"><img src="https://avatars2.githubusercontent.com/u/500376?v=4" width="100px;" alt="Toni Wenzel"/><br /><sub><b>Toni Wenzel</b></sub></a><br /><a href="https://github.com/cake-contrib/Cake.Codecov/issues?q=author%3Atwenzel" title="Ideas, Planning, & Feedback">🤔</a></td><td align="center"><a href="https://github.com/Jericho"><img src="https://avatars0.githubusercontent.com/u/112710?v=4" width="100px;" alt="jericho"/><br /><sub><b>jericho</b></sub></a><br /><a href="https://github.com/cake-contrib/Cake.Codecov/issues?q=author%3AJericho" title="Ideas, Planning, & Feedback">🤔</a></td></tr><tr><td align="center"><a href="https://github.com/gitfool"><img src="https://avatars2.githubusercontent.com/u/750121?v=4" width="100px;" alt="Sean Fausett"/><br /><sub><b>Sean Fausett</b></sub></a><br /><a href="https://github.com/cake-contrib/Cake.Codecov/commits?author=gitfool" title="Code">💻</a></td></tr></table>
<!-- ALL-CONTRIBUTORS-LIST:END -->

This project follows the [all-contributors](https://github.com/all-contributors/all-contributors) specification. Contributions of any kind welcome!
