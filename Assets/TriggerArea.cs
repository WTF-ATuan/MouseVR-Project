using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Actor.Scripts.Actor>())
        {
            var actor = GetComponent<Actor.Scripts.Actor>();
            
            actor.GetTrigger();
        }
    }
}
