namespace Crawly;

public interface IMapSerializer
{
    string Serialize(Map map);
    Map Deserialize(string serializedMap);
}