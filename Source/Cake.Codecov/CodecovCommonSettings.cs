// <copyright file="CodecovCommonSettings.cs" company="Cake Contrib">
// Copyright (c) 2017-2021 Larz White, Kim J. Nordmo and Cake Contrib.
// Licensed under the MIT license. See LICENSE in the project.
// </copyright>

using System;
using System.Collections.Generic;

using Cake.Core.Tooling;

namespace Cake.Codecov
{
    /// <summary>
    /// Common codebase for settings that can be used to support different Codecov Uploaders.
    /// </summary>
    public abstract class CodecovCommonSettings : ToolSettings
    {
        private readonly IDictionary<string, object> _arguments = new Dictionary<string, object>();

        /// <summary>
        /// Gets or sets a property specifing the branch name.
        /// </summary>
        /// <value>A property specifing the branch name.</value>
        public string Branch
        {
            get => GetValue<string>("--branch");
            set => SetValue("--branch", value);
        }

        /// <summary>
        /// Gets or sets a property specifing the build number.
        /// </summary>
        /// <value>A property specifing the build number.</value>
        public string Build
        {
            get => GetValue<string>("--build");
            set => SetValue("--build", value);
        }

        /// <summary>
        /// Gets or sets a property specifing the commit sha.
        /// </summary>
        /// <value>A property specifing the commit sha.</value>
        public string Commit
        {
            get => GetValue<string>("--sha");
            set => SetValue("--sha", value);
        }

        /// <summary>
        /// Gets or sets a value specifing the enviornment variables to be included with this build.
        /// (1) CODECOV_ENV=VAR1,VAR2. (2) -e VAR1 VAR2.
        /// </summary>
        /// <value>
        /// A value specifing the enviornment variables to be included with this build. (1)
        /// CODECOV_ENV=VAR1,VAR2. (2) -e VAR1 VAR2.
        /// </value>
        public IEnumerable<string> Envs
        {
            get => GetValue<IEnumerable<string>>("--env");
            set => SetValue("--env", value);
        }

        /// <summary>
        /// Gets or sets a value specifying which flags should be toggled on/off.
        /// </summary>
        /// <value>A value specifying which features should be toggled on or off.</value>
        public IEnumerable<string> Features
        {
            get => GetValue<IEnumerable<string>>("--feature");
            set => SetValue("--feature", value);
        }

        /// <summary>
        /// Gets or sets a value specifing the target file(s) to upload. (1) -f 'path/to/file'. Only
        /// upload this file. (2) -f 'path/to/file1 path/to/file2'. Only upload these files.
        /// </summary>
        /// <value>
        /// A value specifing the target file(s) to upload. (1) -f 'path/to/file'. Only upload this
        /// file. (2) -f 'path/to/file1 path/to/file2'. Only upload these files.
        /// </value>
        /// <remarks>Globbing in file paths are supported, but not when path starts with './'.</remarks>
        public IEnumerable<string> Files
        {
            get => GetValue<IEnumerable<string>>("--file");
            set => SetValue("--file", value);
        }

        /// <summary>
        /// Gets or sets a value specifing the flag the upload to group coverage metrics. (1) --flag
        /// unittests. This upload is only unittests. (2) --flag integration. This upload is only
        /// integration tests. (3) --flag ut,chrome. This upload is chrome - UI tests.
        /// </summary>
        /// <value>
        /// A value specifing the flag the upload to group coverage metrics. (1) --flag unittests.
        /// This upload is only unittests. (2) --flag integration. This upload is only integration
        /// tests. (3) --flag ut,chrome. This upload is chrome - UI tests.
        /// </value>
        public string Flags
        {
            get => GetValue<string>("--flag");
            set => SetValue("--flag", value);
        }

        /// <summary>
        /// Gets or sets a value specifing the custom defined name of the upload. Visible in Codecov UI.
        /// </summary>
        /// <value>
        /// A value specifing the custom defined name of the upload. Visible in Codecov UI.
        /// </value>
        public string Name
        {
            get => GetValue<string>("--name");
            set => SetValue("--name", value);
        }

        /// <summary>
        /// Gets or sets a value specifing the pull request number.
        /// </summary>
        /// <value>A value specifing the pull request number.</value>
        public string Pr
        {
            get => GetValue<string>("--pr");
            set => SetValue("--pr", value);
        }

        /// <summary>
        /// Gets or sets a value specifing the owner/repo slug used instead of the private repo
        /// token in Enterprise. (option) Set environment variable CODECOV_SLUG=:owner/:repo.
        /// </summary>
        /// <value>
        /// A value specifing the owner/repo slug used instead of the private repo token in
        /// Enterprise. (option) Set environment variable CODECOV_SLUG=:owner/:repo.
        /// </value>
        public string Slug
        {
            get => GetValue<string>("--slug");
            set => SetValue("--slug", value);
        }

        /// <summary>
        /// Gets or sets a value specifing the git tag.
        /// </summary>
        /// <value>A value specifing the git tag.</value>
        public string Tag
        {
            get => GetValue<string>("--tag");
            set => SetValue("--tag", value);
        }

        /// <summary>
        /// Gets or sets a value specifing the private repository token. (option) set the
        /// enviornment variable CODECOV_TOKEN-uuid. (1) -t @/path/to/token_file. (2) -t uuid.
        /// </summary>
        /// <value>
        /// A value specifing the private repository token. (option) set the enviornment variable
        /// CODECOV_TOKEN-uuid. (1) -t @/path/to/token_file. (2) -t uuid.
        /// </value>
        public string Token
        {
            get => GetValue<string>("!--token");
            set => SetValue("!--token", value);
        }

        /// <summary>
        /// Gets or sets a value specifing the target url for Enterprise customers. (option) Set
        /// environment variable CODECOV_URL=https://my-hosted-codecov.com.
        /// </summary>
        /// <value>
        /// A value specifing the target url for Enterprise customers. (option) Set environment
        /// variable CODECOV_URL=https://my-hosted-codecov.com.
        /// </value>
        public Uri Url
        {
            get => GetValue<Uri>("--url");
            set => SetValue("--url", value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether to run in verbose mode.
        /// </summary>
        /// <value>A value indicating whether to run in verbose mode.</value>
        public bool Verbose
        {
            get => GetValue<bool>("--verbose");
            set => SetValue("--verbose", value);
        }

        internal IDictionary<string, object> GetAllArguments()
            => _arguments;

        /// <summary>
        /// Gets a value that is already set in the underlying dictionary. If no value is set, then
        /// <paramref name="defaultValue"/> is returned instead.
        /// </summary>
        /// <typeparam name="TValue">The type of the value expected in the dictionary.</typeparam>
        /// <param name="key">The key or name of the value.</param>
        /// <param name="defaultValue">The default value if no value is found.</param>
        /// <returns>
        /// The found value with the specified <paramref name="key"/>, or the <paramref
        /// name="defaultValue"/> is no value is found.
        /// </returns>
        protected TValue GetValue<TValue>(string key, TValue defaultValue = default)
        {
            if (_arguments.TryGetValue(key, out var objValue) && objValue is TValue value)
            {
                return value;
            }

            return defaultValue;
        }

        /// <summary>
        /// Sets the specified <paramref name="value"/> in the underlying dictionary using the
        /// specified <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The key to use when adding/updating the dictionary.</param>
        /// <param name="value">The value to insert.</param>
        protected void SetValue(string key, object value)
        {
            if (value is string stringValue && string.IsNullOrWhiteSpace(stringValue))
            {
                if (_arguments.ContainsKey(key))
                {
                    _arguments.Remove(key);
                }

                return;
            }

            _arguments[key] = value;
        }
    }
}
