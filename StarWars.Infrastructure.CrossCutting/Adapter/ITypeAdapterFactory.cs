namespace StarWars.Infrastructure.CrossCutting.Adapter
{
    public interface ITypeAdapterFactory
    {
        ITypeAdapter Create();

    }
}
