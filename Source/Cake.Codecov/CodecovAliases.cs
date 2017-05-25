using System.Collections.Generic;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Codecov
{
    /// <summary>
    /// <para>
    /// Uploads coverage reports to <see href="https://codecov.io">Codecov</see>. Note that, many CI services (like AppVeyor) do not require you to provide a Codecov upload token. However, TeamCity is a rare exception.
    /// </para>
    /// <para>
    /// In order to use the commands for this addin, you will need to include the following in your cake script:
    /// <code>
    /// #tool nuget:?package=Codecov
    /// </code>
    /// <code>
    /// #addin nuget:?package=Cake.Codecov
    /// </code>
    /// </para>
    /// </summary>
    [CakeAliasCategory("Codecov")]
    public static class CodecovAliases
    {
        /// <summary>
        /// Uploads coverage reports to <see href="https://codecov.io">Codecov</see> using the given settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">The settings.</param>
        [CakeMethodAlias]
        public static void Codecov(this ICakeContext context, CodecovSettings settings)
        {
            var runner = new CodecovRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.Run(settings);
        }

        /// <summary>
        /// Uploads coverage reports to <see href="https://codecov.io">Codecov</see> by specifying the report files and Codecov token.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="files">The coverage reports.</param>
        /// <param name="token">The Codecov upload token.</param>
        [CakeMethodAlias]
        public static void Codecov(this ICakeContext context, IEnumerable<string> files, string token)
        {
            Codecov(context, new CodecovSettings { Files = files, Token = token });
        }

        /// <summary>
        /// Uploads coverage reports to <see href="https://codecov.io">Codecov</see> by specifying the report files. Note that, many CI services (like AppVeyor) do not require you to provide a Codecov upload token. However, TeamCity is a rare exception.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="files">The coverage reports.</param>
        [CakeMethodAlias]
        public static void Codecov(this ICakeContext context, IEnumerable<string> files)
        {
            Codecov(context, new CodecovSettings { Files = files });
        }

        /// <summary>
        /// Uploads coverage report to <see href="https://codecov.io">Codecov</see> by specifying the report file and Codecov token.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="file">The coverage report.</param>
        /// <param name="token">The Codecov upload token.</param>
        [CakeMethodAlias]
        public static void Codecov(this ICakeContext context, string file, string token)
        {
            Codecov(context, new[] { file }, token);
        }

        /// <summary>
        /// Uploads coverage report to <see href="https://codecov.io">Codecov</see> by specifying the report file. Note that, many CI services (like AppVeyor) do not require you to provide a Codecov upload token. However, TeamCity is a rare exception.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="file">The coverage report.</param>
        [CakeMethodAlias]
        public static void Codecov(this ICakeContext context, string file)
        {
            Codecov(context, new[] { file });
        }
    }
}