using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public static class EventsManager
{
    public static UnityEvent OnCodexUp = new UnityEvent();
    public static UnityEvent OnCodexDown = new UnityEvent();
    public static UnityEvent OnCodexLeft = new UnityEvent();
    public static UnityEvent OnCodexRight = new UnityEvent();
    public static UnityEvent onCodexIn = new UnityEvent();
    public static UnityEvent onCodexOut = new UnityEvent();
    public static UnityEvent onMinigamePress = new UnityEvent();
    public static UnityEvent onMinigameExit = new UnityEvent();
    private static Dictionary<string, UnityEvent> conversationEvents = new Dictionary<string, UnityEvent>();

    public static void InvokeConversationEvent(string eventName)
    {
        if (HasConversationEvent(eventName))
        {
            conversationEvents[eventName]?.Invoke();
        }
    }
    public static void AddConversationEvent(string eventName)
    {
        if (!conversationEvents.ContainsKey(eventName))
        {
            conversationEvents[eventName] = new UnityEvent();
        }
    }
    public static UnityEvent GetConversationEvent(string eventName)
    {
        if (HasConversationEvent(eventName))
        {
            return conversationEvents[eventName];
        }

        return null;
    }
    private static bool HasConversationEvent(string eventName)
    {
        return conversationEvents.ContainsKey(eventName);
    }
}