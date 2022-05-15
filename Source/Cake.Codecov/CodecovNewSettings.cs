// <copyright file="CodecovNewSettings.cs" company="Cake Contrib">
// Copyright (c) 2017-2021 Larz White, Kim J. Nordmo and Cake Contrib.
// Licensed under the MIT license. See LICENSE in the project.
// </copyright>

using System.IO;

namespace Cake.Codecov
{
    /// <summary>
    /// Contains settings used by <see cref="CodecovRunner"/> for the new Codecov Uploader provided
    /// by their team.
    /// </summary>
    /// <seealso cref="CodecovCommonSettings"/>
    /// <remarks>This will be the default in v2.0.0 and legacy support will be dropped.</remarks>
    public sealed class CodecovNewSettings : CodecovCommonSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether Discovered coverage reports should be moved to
        /// the trash.
        /// </summary>
        /// <value>
        /// A value indicating whether Discovered coverage reports should be moved to the trash.
        /// </value>
        public bool CleanReports
        {
            get => GetValue<bool>("--clean");
            set => SetValue("--clean", value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether files should be uploaded to Codecov.
        /// </summary>
        /// <value>A value indicating whether files should be uploaded to Codecov.</value>
        public bool DryRun
        {
            get => GetValue<bool>("--dryRun");
            set => SetValue("--dryRun", value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether Codecov should run with gcov support or not.
        /// </summary>
        /// <value>A value indicating whether Codecov should run with gcov support or not.</value>
        public bool EnableGcovSupport
        {
            get => GetValue<bool>("--gcov");
            set => SetValue("--gcov", value);
        }

        /// <summary>
        /// Gets or sets the commit SHA of the parent for which you are uploading coverage. If not
        /// set, the parent will be determined using the API of your repository provider. When using
        /// the repository provider's API, the parent is determined via finding the closest ancestor
        /// of the commit.
        /// </summary>
        /// <value>The commit SHA of the parent for which you are uploading coverage.</value>
        public string ParentSha
        {
            get => GetValue<string>("--parent");
            set => SetValue("--parent", value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether Codecov should exit with a non-zero exit code on errors.
        /// </summary>
        /// <value>Whether Codecov should exit with a non-zero exit code on errors.</value>
        public bool Required
        {
            get => GetValue<bool>("--nonZero");
            set => SetValue("--nonZero", value);
        }

        /// <summary>
        /// Gets or sets the root directory when it is not a git repository.
        /// </summary>
        /// <value>The root directory when it is not a git repository.</value>
        public DirectoryInfo RootDirectory
        {
            get => GetValue<DirectoryInfo>("--rootDir");
            set => SetValue("--rootDir", value);
        }

        /// <summary>
        /// Gets or sets the directory to use when searching for coverage reports.
        /// </summary>
        /// <value>The directory to use when searching for coverage reports.</value>
        public DirectoryInfo SearchDirectory
        {
            get => GetValue<DirectoryInfo>("--dir");
            set => SetValue("--dir", value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the a link should be displayed to the current changelog.
        /// </summary>
        /// <value>A value indicating whether a link should be showed to the changelog.</value>
        public bool ShowChangelog
        {
            get => GetValue<bool>("--changelog");
            set => SetValue("--changelog", value);
        }
    }
}
