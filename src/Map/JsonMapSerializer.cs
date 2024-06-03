using Newtonsoft.Json;

namespace Crawly;

public class JsonMapSerializer : IMapSerializer
{
    public string Serialize(Map map)
    {
        return JsonConvert.SerializeObject(map);
    }

    public Map Deserialize(string serializedMap)
    {
        return JsonConvert.DeserializeObject<Map>(serializedMap) ?? null!;
    }
}