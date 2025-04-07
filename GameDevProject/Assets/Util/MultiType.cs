using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MultiType<A, B>
{
    public A a;
    public B b;
    public MultiType(A a, B b){
        this.a = a;
        this.b = b;
    }
}

public class MultiType<A, B, C>
{
    public A a;
    public B b;
    public C c;
    public MultiType(A a, B b, C c){
        this.a = a;
        this.b = b;
        this.c = c;
    }
}


public class MultiType<A, B, C, D>
{
    public A a;
    public B b;
    public C c;
    public D d;
    public MultiType(A a, B b, C c, D d){
        this.a = a;
        this.b = b;
        this.c = c;
        this.d = d;
    }
}

