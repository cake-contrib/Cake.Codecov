using System.Collections.Generic;
using System.Linq;
using Cake.Core;
using Cake.Core.IO;
using Cake.Testing;
using Cake.Testing.Fixtures;
using Moq;

namespace Cake.Codecov.Tests
{
    internal class CodecovAliasesFixture : CodecovRunnerFixture
    {
        internal ICakeContext _context;
        public IEnumerable<string> Files { get; set; }
        public string File { get; set; }
        public string Token { get; set; }

        public CodecovAliasesFixture()
        {
            var argumentsMoq = new Mock<ICakeArguments>();
            var registryMoq = new Mock<IRegistry>();
            var dataService = new Mock<ICakeDataService>();
            _context = new CakeContext(
                FileSystem,
                Environment,
                Globber,
                new FakeLog(),
                argumentsMoq.Object,
                ProcessRunner,
                registryMoq.Object,
                Tools,dataService.Object);
        }

        protected override void RunTool()
        {
            if (Settings != null)
            {
                CodecovAliases.Codecov(_context, Settings);
            }
            else if (!string.IsNullOrEmpty(File))
            {
                if (!string.IsNullOrEmpty(Token))
                {
                    CodecovAliases.Codecov(_context, File, Token);
                }
                else
                {
                    CodecovAliases.Codecov(_context, File);
                }
            }
            else if (Files != null && Files.Any())
            {
                if (!string.IsNullOrEmpty(Token))
                {
                    CodecovAliases.Codecov(_context, Files, Token);
                }
                else
                {
                    CodecovAliases.Codecov(_context, Files);
                }
            }
        }
    }
}
