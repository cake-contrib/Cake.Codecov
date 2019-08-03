using System;
using System.Collections.Generic;
using System.Linq;
using Cake.Codecov.Internals;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Codecov
{
    internal sealed class CodecovRunner : Tool<CodecovSettings>
    {
        private readonly IPlatformDetector platformDetector;

        internal CodecovRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools)
            : this(new Internals.PlatformDetector(), fileSystem, environment, processRunner, tools)
        {
        }

        internal CodecovRunner(Internals.IPlatformDetector platformDetector, IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools)
            : base(fileSystem, environment, processRunner, tools)
        {
            this.platformDetector = platformDetector ?? throw new ArgumentNullException(nameof(platformDetector));
        }

        internal void Run(CodecovSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            Run(settings, GetArguments(settings));
        }

        protected override string GetToolName() => "Codecov";

        protected override IEnumerable<string> GetToolExecutableNames()
        {
            if (platformDetector.IsLinuxPlatform())
            {
                yield return "linux-x64/codecov";
                yield return "codecov";
            }
            else if (platformDetector.IsOsxPlatform())
            {
                yield return "osx-x64/codecov";
                yield return "codecov";
            }

            yield return "codecov.exe";
        }

        private static ProcessArgumentBuilder GetArguments(CodecovSettings settings)
        {
            var builder = new ProcessArgumentBuilder();

            // Branch
            if (!string.IsNullOrWhiteSpace(settings.Branch))
            {
                builder.Append("--branch");
                builder.AppendQuoted(settings.Branch);
            }

            // Build
            if (!string.IsNullOrWhiteSpace(settings.Build))
            {
                builder.Append("--build");
                builder.AppendQuoted(settings.Build);
            }

            // Commit
            if (!string.IsNullOrWhiteSpace(settings.Commit))
            {
                builder.Append("--sha");
                builder.AppendQuoted(settings.Commit);
            }

            // Disable Network
            if (settings.DisableNetwork)
            {
                builder.Append("--disable-network");
            }

            // Dump
            if (settings.Dump)
            {
                builder.Append("--dump");
            }

            // Envs
            if (settings.Envs != null)
            {
                builder.Append("--env");
                builder.AppendQuoted(string.Join(" ", settings.Envs.Where(x => !string.IsNullOrWhiteSpace(x))));
            }

            // Files
            if (settings.Files != null)
            {
                builder.Append("--file");
                builder.AppendQuoted(string.Join(" ", settings.Files.Where(x => !string.IsNullOrWhiteSpace(x))));
            }

            // Flags
            if (!string.IsNullOrWhiteSpace(settings.Flags))
            {
                builder.Append("--flag");
                builder.AppendQuoted(settings.Flags);
            }

            // Name
            if (!string.IsNullOrWhiteSpace(settings.Name))
            {
                builder.Append("--name");
                builder.AppendQuoted(settings.Name);
            }

            // No Color
            if (settings.NoColor)
            {
                builder.Append("--no-color");
            }

            // Pr
            if (!string.IsNullOrWhiteSpace(settings.Pr))
            {
                builder.Append("--pr");
                builder.AppendQuoted(settings.Pr);
            }

            // Required
            if (settings.Required)
            {
                builder.Append("--required");
            }

            // Root
            if (!string.IsNullOrWhiteSpace(settings.Root))
            {
                builder.Append("--root");
                builder.AppendQuoted(settings.Root);
            }

            // Slug
            if (!string.IsNullOrWhiteSpace(settings.Slug))
            {
                builder.Append("--slug");
                builder.AppendQuoted(settings.Slug);
            }

            // Tag
            if (!string.IsNullOrWhiteSpace(settings.Tag))
            {
                builder.Append("--tag");
                builder.AppendQuoted(settings.Tag);
            }

            // Token
            if (!string.IsNullOrWhiteSpace(settings.Token))
            {
                builder.Append("--token");
                builder.AppendQuoted(settings.Token);
            }

            // Url
            if (!string.IsNullOrWhiteSpace(settings.Url))
            {
                builder.Append("--url");
                builder.AppendQuoted(settings.Url);
            }

            // Verbose
            if (settings.Verbose)
            {
                builder.Append("--verbose");
            }

            return builder;
        }
    }
}
