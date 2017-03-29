using System.Collections.Generic;
using Cake.Core.Tooling;

namespace Cake.Codecov
{
    public sealed class CodecovSettings : ToolSettings
    {
        public string Branch { get; set; }

        public string Build { get; set; }

        public bool Dump { get; set; }

        public IEnumerable<string> Env { get; set; }

        public IEnumerable<string> File { get; set; }

        public IEnumerable<string> Flag { get; set; }

        public string Name { get; set; }

        public string Pr { get; set; }

        public bool Required { get; set; }

        public string Root { get; set; }

        public string Sha { get; set; }

        public string Slug { get; set; }

        public string Tag { get; set; }

        public string Token { get; set; }

        public string Url { get; set; }

        public bool Verbose { get; set; }
    }
}
