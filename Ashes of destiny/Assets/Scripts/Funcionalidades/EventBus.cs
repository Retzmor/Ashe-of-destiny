using System;

public static class EventBus 
{
    public static Action GameStart;

    public static void Clear()
    {
        GameStart = null;
    }
}
