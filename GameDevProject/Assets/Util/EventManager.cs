using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    private readonly Dictionary<string, MultiType<Func<bool>, UnityEvent>> events = new Dictionary<string, MultiType<Func<bool>, UnityEvent>>();

    void Update()
    {
        foreach (MultiType<Func<bool>, UnityEvent> t in events.Values){
            if (t.a()){
                t.b.Invoke();
            }
        }
    }
    public void PutTopic(string id, Func<bool> booleanSupplier){
        if (events.ContainsKey(id)){
            events[id].a = booleanSupplier;
        }
        events.Add(id, new MultiType<Func<bool>, UnityEvent>(booleanSupplier, new UnityEvent()));
    }
    public bool AddTopic(string id, Func<bool> booleanSupplier){
        if (events.ContainsKey(id)){
            return false;
        }
        events.Add(id, new MultiType<Func<bool>, UnityEvent>(booleanSupplier, new UnityEvent()));
        return true;
    }

    public bool RemoveTopic(string id, Func<bool> booleanSupplier){
        return events.Remove(id);
    }

    public bool AddListener(string id, UnityAction action){
        if (!events.ContainsKey(id)){
            return false;
        }
        events[id].b.AddListener(action);
        return true;
    }

    public bool RemoveListener(string id, UnityAction action){
        if (!events.ContainsKey(id)){
            return false;
        }
        events[id].b.RemoveListener(action);
        return true;
    }

    // public bool RemoveListener(UnityAction action){
    //     foreach (MultiType<Func<bool>, UnityEvent> t in events.Values){
    //         for (int i = 0; i < t.b.GetPersistentEventCount(); i++)
    //         {
    //             //if (unityEvent.GetPersistentTarget(i) == MyMethod) //something like this
    //             if (t.b.GetPersistentMethodName(i) == nameof(action)) //this might cause issues with similar names
    //             {
    //                 Debug.Log("Already exists");
    //                 alreadyContains = true;
    //                 break;
    //             }
    //         }
    //         if (t.b.){
    //             t.b.Invoke();
    //         }
    //     }
    // }
    
}
