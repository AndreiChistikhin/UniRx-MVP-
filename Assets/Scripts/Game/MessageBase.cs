public class MessageBase
{
    public MessageType MessageType { get; }

    public MessageBase(MessageType messageType)
    {
        MessageType = messageType;
    }
}

public enum MessageType
{
    BulletHitMeteor,
    MeteorHitFinish
}