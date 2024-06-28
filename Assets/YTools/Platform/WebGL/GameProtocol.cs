using Newtonsoft.Json;
using System;

[Serializable]
public class GameProtocol<T>
{
    public int id { get; set; }
    public string type { get; set; }
    public T data { get; set; }
}

[Serializable]
public class GameMessage<T>
{
    [JsonIgnore] private static int nextId = 1; // 下一个消息的 ID

    public int id { get; }
    public string type { get; set; }
    public T data { get; set; }

    public GameMessage()
    {
        id = nextId++; // 分配 ID，并递增下一个 ID
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
