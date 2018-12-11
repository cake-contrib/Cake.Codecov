using Cake.Testing.Fixtures;

namespace Cake.Codecov.Tests
{
    internal class CodecovRunnerFixture : ToolFixture<CodecovSettings>
    {
        public CodecovRunnerFixture()
            : base("codecov.exe")
        {
        }

        protected override void RunTool()
        {
            var tool = new CodecovRunner(FileSystem, Environment, ProcessRunner, Tools);
            tool.Run(Settings);
        }
    }
}
