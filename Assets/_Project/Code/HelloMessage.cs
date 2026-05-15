using Mirror;

public struct HelloMessage : NetworkMessage
{
    public string Content;

    public HelloMessage(string content)
    {
        Content = content;
    }
}
