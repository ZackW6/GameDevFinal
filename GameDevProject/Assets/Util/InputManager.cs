using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : EventManager
{
    public void AddKeyListener(KeyCode key, UnityAction action){
        AddTopic(key.ToString(), ()=>Input.GetKey(key));
        AddListener(key.ToString(), action);
    }
    public void AddKeyDownListener(KeyCode key, UnityAction action){
        AddTopic(key.ToString()+".Down", ()=>Input.GetKeyDown(key));
        AddListener(key.ToString()+".Down", action);
    }
    public void AddKeyUpListener(KeyCode key, UnityAction action){
        AddTopic(key.ToString()+".Up", ()=>Input.GetKeyUp(key));
        AddListener(key.ToString()+".Up", action);
    }


}
