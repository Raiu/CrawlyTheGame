namespace Con2D;

public interface IMapSerializer
{
    string Serialize(Map map);
    Map Deserialize(string serializedMap);
}