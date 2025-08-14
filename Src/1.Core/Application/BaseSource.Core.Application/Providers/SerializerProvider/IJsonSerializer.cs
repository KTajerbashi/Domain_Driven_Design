namespace BaseSource.Core.Application.Providers.SerializerProvider;

public interface IJsonSerializer
{
    string Serialize<TInput>(TInput input);

    TOutput Deserialize<TOutput>(string input);

    object Deserialize(string input, Type type);
}
