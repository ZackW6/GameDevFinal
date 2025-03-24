using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat<T>
{

    private T value;
    public Stat(T defaultVal){
        value = defaultVal;
    }

    public T getValue(){
        return value;
    }
}
