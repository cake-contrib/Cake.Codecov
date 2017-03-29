using System;
using System.Collections.Generic;
using System.Linq;
using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Codecov
{
    public sealed class CodecovRunner : Tool<CodecovSettings>
    {
        private ICakeEnvironment Environment { get; }

        public CodecovRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner, IToolLocator tools) : base(fileSystem, environment, processRunner, tools)
        {
            Environment = environment;
        }

        protected override string GetToolName()
        {
            return "Codecov";
        }

        protected override IEnumerable<string> GetToolExecutableNames()
        {
            if (Environment.Platform.IsUnix())
            {
                yield return "codecov";
            }
            else
            {
                yield return "codecov.exe";
            }
        }

        public void Run(CodecovSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            Run(settings, GetArguments(settings));
        }

        private ProcessArgumentBuilder GetArguments(CodecovSettings settings)
        {
            var builder = new ProcessArgumentBuilder();

            // Branch
            if (!string.IsNullOrWhiteSpace(settings.Branch))
            {
                builder.Append("--Branch");
                builder.AppendQuoted(settings.Branch);
            }

            // Build
            if (!string.IsNullOrWhiteSpace(settings.Build))
            {
                builder.Append("--Build");
                builder.AppendQuoted(settings.Build);
            }

            // Dump
            if (settings.Dump)
            {
                builder.Append("--Dump");
            }

            // Env
            if (settings.Env != null)
            {
                builder.Append("--Env");
                builder.AppendQuoted(string.Join(" ", settings.Env.Where(x => !string.IsNullOrWhiteSpace(x))));
            }

            // File
            if (settings.File != null)
            {
                builder.Append("--File");
                builder.AppendQuoted(string.Join(" ", settings.File.Where(x => !string.IsNullOrWhiteSpace(x))));
            }

            // Flag
            if (settings.Flag != null)
            {
                builder.Append("--Flag");
                builder.AppendQuoted(string.Join(" ", settings.Flag.Where(x => !string.IsNullOrWhiteSpace(x))));
            }

            // Name
            if (!string.IsNullOrWhiteSpace(settings.Name))
            {
                builder.Append("--Name");
                builder.AppendQuoted(settings.Name);
            }

            // Pr
            if (!string.IsNullOrWhiteSpace(settings.Pr))
            {
                builder.Append("--Pr");
                builder.AppendQuoted(settings.Pr);
            }

            // Required
            if (settings.Required)
            {
                builder.Append("--Required");
            }

            // Root
            if (!string.IsNullOrWhiteSpace(settings.Root))
            {
                builder.Append("--Root");
                builder.AppendQuoted(settings.Root);
            }

            // Sha
            if (!string.IsNullOrWhiteSpace(settings.Sha))
            {
                builder.Append("--Sha");
                builder.AppendQuoted(settings.Sha);
            }

            // Slug
            if (!string.IsNullOrWhiteSpace(settings.Slug))
            {
                builder.Append("--Slug");
                builder.AppendQuoted(settings.Slug);
            }

            // Tag
            if (!string.IsNullOrWhiteSpace(settings.Tag))
            {
                builder.Append("--Tag");
                builder.AppendQuoted(settings.Tag);
            }

            // Token
            if (!string.IsNullOrWhiteSpace(settings.Token))
            {
                builder.Append("--Token");
                builder.AppendQuoted(settings.Token);
            }

            // Url
            if (!string.IsNullOrWhiteSpace(settings.Url))
            {
                builder.Append("--Url");
                builder.AppendQuoted(settings.Url);
            }

            // Verbose
            if (settings.Verbose)
            {
                builder.Append("--Verbose");
            }

            return builder;
        }
    }
}
