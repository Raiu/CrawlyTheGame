using Newtonsoft.Json;

namespace Con2D;

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