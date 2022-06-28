using System;
using System.Collections;
using System.Collections.Generic;
using Actor.Scripts.Event;
using Project;
using UnityEngine;

public class AreaCount : MonoBehaviour
{
    public bool isLeft;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Actor.Scripts.Actor>())
        {
            EventBus.Post(new ChooseLeftRight(isLeft));
        }
    }
}
