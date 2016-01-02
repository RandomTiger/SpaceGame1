using UnityEngine;

public class DebugLog
{
    public enum Filter
    {
        Default,
        Game,
        System,
        AI,
    }

    public enum Level
    {
        Info,
        Warning,
        Error
    }

    public static void Add(string msg, Filter filter = Filter.Default, Level level = Level.Info)
    {
        switch(level)
        {
            case Level.Error: Debug.LogError(msg); break;
            case Level.Warning: Debug.LogWarning(msg); break;
            case Level.Info: Debug.Log(msg); break;
        }
    }
}
