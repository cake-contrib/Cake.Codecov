using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Codecov
{
    [CakeAliasCategory("Codecov")]
    public static class CodecovAliases
    {
        [CakeMethodAlias]
        public static void Codecov(this ICakeContext context, CodecovSettings settings)
        {
            var runner = new CodecovRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
            runner.Run(settings);
        }
    }
}
