namespace GraphLib.Domain.Serializers
{
    public interface ISerializer<T>
        where T : class
    {
        T Deserialize(string obj);
        string Serialize(T obj);
    }
}