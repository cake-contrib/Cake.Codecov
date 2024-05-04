# Cake.Codecov

[![All Contributors][all-contributorsimage]](#contributors)
[![AppVeyor branch](https://img.shields.io/appveyor/build/cakecontrib/cake-codecov/develop?logo=appveyor)](https://ci.appveyor.com/project/cakecontrib/cake-codecov/branch/develop)
[![GitHub Build](https://img.shields.io/github/actions/workflow/status/cake-contrib/Cake.Codecov/build.yml?logo=github)](https://github.com/cake-contrib/Cake.Codecov/actions/workflows/build.yml?query=branch%3Adevelop)
[![NuGet](https://img.shields.io/nuget/v/Cake.Codecov?logo=nuget)](https://www.nuget.org/packages/Cake.Codecov/)
[![Codecov](https://img.shields.io/codecov/c/github/cake-contrib/Cake.Codecov?logo=codecov)](https://codecov.io/gh/cake-contrib/Cake.Codecov)

A [Cake](https://cakebuild.net) addin that extends Cake with the ability to use
the official
[Codecov](https://docs.codecov.com/docs/codecov-uploader#using-the-cli) CLI that
have been repackaged as a NuGet package named [CodecovUploader](https://www.nuget.org/packages/CodecovUploader).

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->
## Table of Contents

- [Usage](#usage)
- [Documentation](#documentation)
- [Codecov Tips](#codecov-tips)
- [Questions](#questions)
- [Known Issues](#known-issues)
- [Contributors](#contributors)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

## Usage

In order to use this addin, add to your Cake script

```csharp
#tool nuget:?package=CodecovUploader
#addin nuget:?package=Cake.Codecov // Any versions before 1.1.0 is not guaranteed to work anymore.
```

Then use one of the following snippets to upload your coverage report to Codecov.

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

Documentation for the addin can be found on the [Cake Website](https://cakebuild.net/extensions/cake-codecov/).

## Codecov Tips

1. While CI Service like GitHub Action, these workflows are likely to hit a rate
   limit on Codecov. As such it is recommended to always pass in a repository or
   global token when one is available.
2. Do not set NonZero to `true` when running without a Codecov token.

## Questions

Feel free to open an [issue](https://github.com/cake-contrib/Cake.Codecov/issues) or ask a question in [GitHub Discussions](https://github.com/cake-build/cake/discussions) under the [Extension Q&A](https://github.com/cake-build/cake/discussions/categories/extension-q-a) category, and by tagging us: **@larzw** and/or **@AdmiringWorm**.

## Known Issues

- Coverage report upload fails when using gitversion (or other tools that change the appveyor build version)
  Workaround: Add the following in your Upload Coverage task (*only needed if gitversion is run on the same call as the uploading of coverage reports in appveyor.yml*)

  ```csharp
  Task("Upload-Coverage")
      .Does(() =>
  {
      // The logic may differ from what you actually need.
      // This way is for the use with GitVersion.
      // Basically, the buildVersion format needs to be exactly the
      // same as the build version shown on appveyor when the build is done.
      var buildVersion = string.Format("{0}.build.{1}",
          variableThatStores_GitVersion_FullSemVer,
          BuildSystem.AppVeyor.Environment.Build.Number
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
<!-- prettier-ignore-start -->
<!-- markdownlint-disable -->
<table>
  <tbody>
    <tr>
      <td align="center" valign="top" width="14.28%"><a href="https://www.linkedin.com/in/larz-white-5a8264108"><img src="https://avatars0.githubusercontent.com/u/6298611?v=4?s=100" width="100px;" alt="Larz White"/><br /><sub><b>Larz White</b></sub></a><br /><a href="#maintenance-larzw" title="Maintenance">🚧</a></td>
      <td align="center" valign="top" width="14.28%"><a href="https://github.com/AdmiringWorm"><img src="https://avatars3.githubusercontent.com/u/1474648?v=4?s=100" width="100px;" alt="Kim J. Nordmo"/><br /><sub><b>Kim J. Nordmo</b></sub></a><br /><a href="#maintenance-AdmiringWorm" title="Maintenance">🚧</a></td>
      <td align="center" valign="top" width="14.28%"><a href="http://www.gep13.co.uk/blog"><img src="https://avatars3.githubusercontent.com/u/1271146?v=4?s=100" width="100px;" alt="Gary Ewan Park"/><br /><sub><b>Gary Ewan Park</b></sub></a><br /><a href="https://github.com/cake-contrib/Cake.Codecov/pulls?q=is%3Apr+reviewed-by%3Agep13" title="Reviewed Pull Requests">👀</a> <a href="https://github.com/cake-contrib/Cake.Codecov/issues?q=author%3Agep13" title="Ideas, Planning, & Feedback">🤔</a></td>
      <td align="center" valign="top" width="14.28%"><a href="https://github.com/vkbishnoi"><img src="https://avatars0.githubusercontent.com/u/8297727?v=4?s=100" width="100px;" alt="Vishal Bishnoi"/><br /><sub><b>Vishal Bishnoi</b></sub></a><br /><a href="https://github.com/cake-contrib/Cake.Codecov/commits?author=vkbishnoi" title="Code">💻</a></td>
      <td align="center" valign="top" width="14.28%"><a href="https://twitter.com/hereispascal"><img src="https://avatars1.githubusercontent.com/u/2190718?v=4?s=100" width="100px;" alt="Pascal Berger"/><br /><sub><b>Pascal Berger</b></sub></a><br /><a href="https://github.com/cake-contrib/Cake.Codecov/issues?q=author%3Apascalberger" title="Ideas, Planning, & Feedback">🤔</a></td>
      <td align="center" valign="top" width="14.28%"><a href="https://github.com/twenzel"><img src="https://avatars2.githubusercontent.com/u/500376?v=4?s=100" width="100px;" alt="Toni Wenzel"/><br /><sub><b>Toni Wenzel</b></sub></a><br /><a href="https://github.com/cake-contrib/Cake.Codecov/issues?q=author%3Atwenzel" title="Ideas, Planning, & Feedback">🤔</a></td>
      <td align="center" valign="top" width="14.28%"><a href="https://github.com/Jericho"><img src="https://avatars0.githubusercontent.com/u/112710?v=4?s=100" width="100px;" alt="jericho"/><br /><sub><b>jericho</b></sub></a><br /><a href="https://github.com/cake-contrib/Cake.Codecov/issues?q=author%3AJericho" title="Ideas, Planning, & Feedback">🤔</a></td>
    </tr>
    <tr>
      <td align="center" valign="top" width="14.28%"><a href="https://github.com/gitfool"><img src="https://avatars2.githubusercontent.com/u/750121?v=4?s=100" width="100px;" alt="Sean Fausett"/><br /><sub><b>Sean Fausett</b></sub></a><br /><a href="https://github.com/cake-contrib/Cake.Codecov/commits?author=gitfool" title="Code">💻</a></td>
      <td align="center" valign="top" width="14.28%"><a href="https://augustoproiete.net"><img src="https://avatars.githubusercontent.com/u/177608?v=4?s=100" width="100px;" alt="C. Augusto Proiete"/><br /><sub><b>C. Augusto Proiete</b></sub></a><br /><a href="https://github.com/cake-contrib/Cake.Codecov/commits?author=augustoproiete" title="Documentation">📖</a> <a href="https://github.com/cake-contrib/Cake.Codecov/issues?q=author%3Aaugustoproiete" title="Ideas, Planning, & Feedback">🤔</a></td>
      <td align="center" valign="top" width="14.28%"><a href="https://blog.nils-andresen.de"><img src="https://avatars.githubusercontent.com/u/349188?v=4?s=100" width="100px;" alt="Nils Andresen"/><br /><sub><b>Nils Andresen</b></sub></a><br /><a href="https://github.com/cake-contrib/Cake.Codecov/commits?author=nils-a" title="Code">💻</a></td>
    </tr>
  </tbody>
</table>

<!-- markdownlint-restore -->
<!-- prettier-ignore-end -->

<!-- ALL-CONTRIBUTORS-LIST:END -->

This project follows the [all-contributors](https://github.com/all-contributors/all-contributors) specification. Contributions of any kind welcome!

[all-contributors]: https://github.com/all-contributors/all-contributors
[all-contributorsimage]: https://img.shields.io/github/all-contributors/cake-contrib/Cake.Codecov.svg?color=orange&style=flat-square
