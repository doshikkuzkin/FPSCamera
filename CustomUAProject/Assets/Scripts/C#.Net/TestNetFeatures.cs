using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNetFeatures : MonoBehaviour
{
    void Start()
    {
        List<Action> actions = new List<Action>();
        for(var count=0; count<10; count++)
        {
            actions.Add(() => Debug.Log(count));
        }
        foreach(var action in actions)
        {
            action();
        }

    }
}
