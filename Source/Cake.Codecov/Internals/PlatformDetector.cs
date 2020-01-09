using System.Runtime.InteropServices;

namespace Cake.Codecov.Internals
{
    /// <summary>
    /// Responsible for detecting the current platfrom we are running on. Also used for unit test.
    /// </summary>
    /// <remarks>This class is not to be consumed publically.</remarks>
    internal class PlatformDetector : IPlatformDetector
    {
        public bool IsLinuxPlatform()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        }

        public bool IsOsxPlatform()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
        }
    }
}
