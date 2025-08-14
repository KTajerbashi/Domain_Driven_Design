namespace BaseSource.Core.Application.Providers;

public class ProviderFactory
{
    public IJsonSerializer JsonSerializer;
    public ProviderFactory(IJsonSerializer jsonSerializer)
    {
        JsonSerializer = jsonSerializer;
    }
}
