public enum PMEvents
{
    // ****** Property Change Events *********
    AnyPropertyChange = 0,


    // ***** Custom Events *******

}

public static class PMEventsExtensions
{
    public static string EventName(this PMEvents events)
    {
        return "PMEvent-" + events.ToString();
    }
}
