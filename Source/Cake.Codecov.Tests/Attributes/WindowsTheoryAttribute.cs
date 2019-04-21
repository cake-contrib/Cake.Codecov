using System.Runtime.InteropServices;
using Xunit;

namespace Cake.Codecov.Tests.Attributes
{
    public class WindowsTheoryAttribute : TheoryAttribute
    {

        public WindowsTheoryAttribute(string reason = null)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Skip = reason ?? "windows test.";
            }
        }
    }
}