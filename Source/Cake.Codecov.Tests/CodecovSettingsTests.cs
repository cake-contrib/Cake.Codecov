using System;
using FluentAssertions;
using Xunit;

namespace Cake.Codecov.Tests
{
    public sealed class CodecovSettingsTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Should_Remove_Empty_String_Value(string value)
        {
            // Given
            var settings = new CodecovSettings
            {
                Tag = "v1.1.0"
            };

            // When
            settings.Tag = value;

            // Then
            settings.Tag.Should().BeNull();
        }

        [Fact]
        public void Should_Set_Branch_Value()
        {
            // Given
            var expected = "next";
            var settings = new CodecovSettings
            {

                // When
                Branch = expected
            };

            // Then
            settings.Branch.Should().Be(expected);
        }

        [Fact]
        public void Should_Set_Build_Value()
        {
            // Given
            var expected = "543";
            var settings = new CodecovSettings
            {

                // When
                Build = expected
            };

            // Then
            settings.Build.Should().Be(expected);
        }

        [Fact]
        public void Should_Set_Commit_Value()
        {
            // Given
            var expected = "02eecc9564a8f89ddf13cf63b615cf08adee23cc";
            var settings = new CodecovSettings
            {

                // When
                Commit = expected
            };

            // Then
            settings.Commit.Should().Be(expected);
        }

        [Fact]
        public void Should_Set_DisableNetwork_Value()
        {
            // Given
            var settings = new CodecovSettings
            {

                // When
                DisableNetwork = true
            };

            // Then
            settings.DisableNetwork.Should().BeTrue();
        }

        [Fact]
        public void Should_Set_Dump_Value()
        {
            // Given
            var settings = new CodecovSettings
            {

                // When
                Dump = true
            };

            // Then
            settings.Dump.Should().BeTrue();
        }

        [Fact]
        public void Should_Set_Envs_Value()
        {
            // Given
            var expected = new[] { "APPVEYOR_IMAGE", "TESTING" };
            var settings = new CodecovSettings
            {

                // When
                Envs = expected
            };

            // Then
            settings.Envs.Should().HaveCount(expected.Length).And.BeSameAs(expected);
        }

        [Fact]
        public void Should_Set_Files_Value()
        {
            // Given
            var expected = new[] { "file1.txt", "file2.xml" };
            var settings = new CodecovSettings
            {

                // When
                Files = expected
            };

            // Then
            settings.Files.Should().HaveCount(expected.Length).And.BeSameAs(expected);
        }

        [Fact]
        public void Should_Set_Flags_Value()
        {
            // Given
            var expected = "Integration";
            var settings = new CodecovSettings
            {

                // When
                Flags = expected
            };

            // Then
            settings.Flags.Should().Be(expected);
        }

        [Fact]
        public void Should_Set_Name_Value()
        {
            // Given
            var expected = "Some Name";
            var settings = new CodecovSettings
            {

                // When
                Name = expected
            };

            // Then
            settings.Name.Should().Be(expected);
        }

        [Fact]
        public void Should_Set_NoColor_Value()
        {
            // Given
            var settings = new CodecovSettings
            {

                // When
                NoColor = true
            };

            // Then
            settings.NoColor.Should().BeTrue();
        }

        [Fact]
        public void Should_Set_Pr_Value()
        {
            // Given
            var expected = "512";
            var settings = new CodecovSettings
            {

                // When
                Pr = expected
            };

            // Then
            settings.Pr.Should().Be(expected);
        }

        [Fact]
        public void Should_Set_Required_Value()
        {
            // Given
            var settings = new CodecovSettings
            {

                // When
                Required = true
            };

            // Then
            settings.Required.Should().BeTrue();
        }

        [Fact]
        public void Should_Set_Root_Value()
        {
            // Given
            var expected = "C:/test/root";
            var settings = new CodecovSettings
            {

                // When
                Root = expected
            };

            // Then
            settings.Root.Should().Be(expected);
        }

        [Fact]
        public void Should_Set_Slug_Value()
        {
            // Given
            var expected = "my-test-slug";
            var settings = new CodecovSettings
            {

                // When
                Slug = expected
            };

            // Then
            settings.Slug.Should().Be(expected);
        }

        [Fact]
        public void Should_Set_Tag_Value()
        {
            // Given
            var expected = "v1.0.0";
            var settings = new CodecovSettings
            {

                // When
                Tag = expected
            };

            // Then
            settings.Tag.Should().Be(expected);
        }

        [Fact]
        public void Should_Set_Token_Value()
        {
            // Given
            var expected = Guid.NewGuid().ToString();
            var settings = new CodecovSettings
            {

                // When
                Token = expected
            };

            // Then
            settings.Token.Should().Be(expected);
        }

        [Fact]
        public void Should_Set_Url_Value()
        {
            // Given
            var expected = new Uri("https://localhost.com/test/server");
            var settings = new CodecovSettings
            {

                // When
                Url = expected
            };

            // Then
            settings.Url.Should().Be(expected);
        }

        [Fact]
        public void Should_Set_Verbose_Value()
        {
            // Given
            var settings = new CodecovSettings
            {

                // When
                Verbose = true,
            };

            // Then
            settings.Verbose.Should().BeTrue();
        }
    }
}
