using System.Collections.Generic;
using Cake.Core.Tooling;

namespace Cake.Codecov
{
    /// <summary>
    /// Contains settings used by <see cref="CodecovRunner"/>. See <see href="https://github.com/codecov/codecov-exe/blob/master/Source/Codecov/Program/CommandLineOptions.cs">CommandLineOptions</see> or run <code>.\codecov.exe --help</code> for more details.
    /// </summary>
    public sealed class CodecovSettings : ToolSettings
    {
        /// <summary>
        /// Gets or sets a property specifing the branch name.
        /// </summary>
        public string Branch { get; set; }

        /// <summary>
        /// Gets or sets a property specifing the build number.
        /// </summary>
        public string Build { get; set; }

        /// <summary>
        /// Gets or sets a property specifing the commit sha.
        /// </summary>
        public string Commit { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to toggle functionalities. (1) --disable-network.
        /// Disable uploading the file network.
        /// </summary>
        public bool DisableNetwork { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to don't upload and dump to stdin.
        /// </summary>
        public bool Dump { get; set; }

        /// <summary>
        /// Gets or sets a value specifing the enviornment variables to be included with this build.
        /// (1) CODECOV_ENV=VAR1,VAR2. (2) -e VAR1 VAR2.
        /// </summary>
        public IEnumerable<string> Envs { get; set; }

        /// <summary>
        /// Gets or sets a value specifing the target file(s) to upload. (1) -f 'path/to/file'. Only
        /// upload this file. (2) -f 'path/to/file1 path/to/file2'. Only upload these files.
        /// </summary>
        public IEnumerable<string> Files { get; set; }

        /// <summary>
        /// Gets or sets a value specifing the flag the upload to group coverage metrics. (1) --flag
        /// unittests. This upload is only unittests. (2) --flag integration. This upload is only
        /// integration tests. (3) --flag ut,chrome. This upload is chrome - UI tests.
        /// </summary>
        public string Flags { get; set; }

        /// <summary>
        /// Gets or sets a value specifing the custom defined name of the upload. Visible in Codecov UI.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to remove color from the output.
        /// </summary>
        public bool NoColor { get; set; }

        /// <summary>
        /// Gets or sets a value specifing the pull request number.
        /// </summary>
        public string Pr { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to exit with 1 if not successful. Default will
        /// Exit with 0.
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// Gets or sets a value used when not in git project to identify project root directory.
        /// </summary>
        public string Root { get; set; }

        /// <summary>
        /// Gets or sets a value specifing the owner/repo slug used instead of the private repo token
        /// in Enterprise. (option) Set environment variable CODECOV_SLUG=:owner/:repo.
        /// </summary>
        public string Slug { get; set; }

        /// <summary>
        /// Gets or sets a value specifing the git tag.
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Gets or sets a value specifing the private repository token. (option) set the enviornment
        /// variable CODECOV_TOKEN-uuid. (1) -t @/path/to/token_file. (2) -t uuid.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets a value specifing the target url for Enterprise customers. (option) Set
        /// environment variable CODECOV_URL=https://my-hosted-codecov.com.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to run in verbose mode.
        /// </summary>
        public bool Verbose { get; set; }
    }
}