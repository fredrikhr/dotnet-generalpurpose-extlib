using Microsoft.Extensions.Configuration;

using FredrikHr.DotnetGeneralPurpose.Extlib.Prototype;

namespace FredrikHr.DotnetGeneralPurpose.Extlib.Test.Prototype;

[System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Style",
    "IDE0053: Use expression body for lambda expression",
    Justification = nameof(Assert)
    )]
public class ConfigurationBuilderInsertExtensionTests
{
    [Fact]
    public static void ConfigurationBuilder_Insert_throws_if_builder_null()
    {
        IConfigurationBuilder config = null!;
        Assert.Throws<ArgumentNullException>(
            () => config.Insert(default, (_) => { })
        );
    }

    [Fact]
    public static void ConfigurationBuilder_Insert_noops_if_action_null()
    {
        var config = new ConfigurationBuilder();
        config.Insert(default, null!);

        Assert.Empty(config.Sources);
    }

    [Fact]
    public static void ConfigurationBuilder_Insert_returns_same_instance()
    {
        var config = new ConfigurationBuilder();

        var retConfig = config.Insert(default, (_) => { });

        Assert.Same(retConfig, config);
    }

    [Fact]
    public static void ConfigurationBuilder_Insert_action_argument_is_not_same_instance()
    {
        var config = new ConfigurationBuilder();

        config.Insert(default, (insConfig) => {
            Assert.NotSame(insConfig, config);
        });
    }

    [Fact]
    public static void ConfigurationBuilder_Insert_action_argument_has_no_sources()
    {
        var config = new ConfigurationBuilder();
        config.AddInMemoryCollection();
        config.Insert(default, (insConfig) => {
            Assert.Empty(insConfig.Sources);
        });
    }

    [Fact]
    public static void ConfigurationBuilder_Insert_adds_sources()
    {
        var config = new ConfigurationBuilder();
        config.AddInMemoryCollection();
        config.Insert(default, (insConfig) => {
            insConfig.AddInMemoryCollection();
        });

        Assert.Equal(2, config.Sources.Count);
    }
}