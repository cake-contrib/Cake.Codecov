// <copyright file="CodecovSettings.cs" company="Cake Contrib">
// Copyright (c) 2017-2021 Larz White, Kim J. Nordmo and Cake Contrib.
// Licensed under the MIT license. See LICENSE in the project.
// </copyright>

using System;

using Cake.Core.IO;

namespace Cake.Codecov
{
    /// <summary>
    /// Contains settings used by <see cref="CodecovRunner"/> when targeting the old C# Codecov
    /// executable. See <see
    /// href="https://github.com/codecov/codecov-exe/blob/master/Source/Codecov/Program/CommandLineOptions.cs">CommandLineOptions</see>
    /// or run.
    /// <code>
    /// .\codecov.exe --help
    /// </code>
    /// for more details.
    /// </summary>
    /// <seealso cref="CodecovCommonSettings"/>
    /// <remarks>This will be replaced by the setting <see cref="CodecovNewSettings"/> in v2.0.0.</remarks>
    public sealed class CodecovSettings : CodecovCommonSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether to toggle functionalities. (1)
        /// --disable-network. Disable uploading the file network.
        /// </summary>
        /// <value>
        /// A value indicating whether to toggle functionalities. (1) --disable-network. Disable
        /// uploading the file network.
        /// </value>
        /// <remarks>
        /// Will only be effective if used together with old C# edition of the Codecov uploader.
        /// </remarks>
        [Obsolete("This property have been deprecated, and will be removed in v2.0.0. Use property 'DryRun' instead")]
        public bool DisableNetwork
        {
            get => GetValue<bool>("--disable-network");
            set => SetValue("--disable-network", value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether to don't upload and dump to stdin.
        /// </summary>
        /// <value>A value indicating whether to don't upload and dump to stdin.</value>
        public bool Dump
        {
            get => GetValue<bool>("--dump");
            set => SetValue("--dump", value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether to remove color from the output.
        /// </summary>
        /// <value>A value indicating whether to remove color from the output.</value>
        public bool NoColor
        {
            get => GetValue<bool>("--no-color");
            set => SetValue("--no-color", value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether to exit with 1 if not successful. Default will
        /// Exit with 0.
        /// </summary>
        /// <value>
        /// A value indicating whether to exit with 1 if not successful. Default will Exit with 0.
        /// </value>
        public bool Required
        {
            get => GetValue<bool>("--required");
            set => SetValue("--required", value);
        }

        /// <summary>
        /// Gets or sets a value used when not in git project to identify project root directory.
        /// </summary>
        /// <value>A value used when not in git project to identify project root directory.</value>
        public DirectoryPath Root
        {
            get => GetValue<string>("--root");
            set => SetValue("--root", value?.ToString());
        }
    }
}
