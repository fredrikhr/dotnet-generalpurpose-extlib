
using Microsoft.Extensions.Configuration;

namespace FredrikHr.DotnetGeneralPurpose.Extlib.Prototype;

public static class ConfigurationBuilderInsertExtensions
{
    private class TemporaryConfigurationBuilder(IConfigurationBuilder origin) : IConfigurationBuilder
    {
        public IDictionary<string, object> Properties { get; } = origin.Properties;
        public IList<IConfigurationSource> Sources { get; } = [];

        public IConfigurationBuilder Add(IConfigurationSource source)
        {
            Sources.Add(source);
            return this;
        }

        public IConfigurationRoot Build()
        {
            throw new InvalidOperationException();
        }
    }

    public static IConfigurationBuilder Insert(this IConfigurationBuilder config, int index, Action<IConfigurationBuilder> addAction)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(config);
#else
        _ = config ?? throw new ArgumentNullException(nameof(config));
#endif
        if (addAction is null) return config;

        var tmpBuilder = new TemporaryConfigurationBuilder(config);
        addAction(tmpBuilder);
        for (int i = 0, j = index; i < tmpBuilder.Sources.Count; i++, j++)
        {
            config.Sources.Insert(j, tmpBuilder.Sources[i]);
        }

        return config;
    }
}
