// <copyright file="IPlatformDetector.cs" company="Cake Contrib">
// Copyright (c) 2017-2021 Larz White, Kim J. Nordmo and Cake Contrib.
// Licensed under the MIT license. See LICENSE in the project.
// </copyright>

namespace Cake.Codecov.Internals
{
    /// <summary>
    /// Helper interface for detecting the current platform we are running on.
    /// </summary>
    public interface IPlatformDetector
    {
        /// <summary>
        /// Determines whether we are running on the linux platform.
        /// </summary>
        /// <returns><c>true</c> if we are running on linux; otherwise, <c>false</c>.</returns>
        bool IsLinuxPlatform();

        /// <summary>
        /// Determines whether we are running on the osx platform.
        /// </summary>
        /// <returns><c>true</c> if we are running on osx; otherwise, <c>false</c>.</returns>
        bool IsOsxPlatform();
    }
}
