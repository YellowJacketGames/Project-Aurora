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
}