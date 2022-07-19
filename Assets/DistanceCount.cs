using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCount : MonoBehaviour
{
    [SerializeField] private Actor.Scripts.Actor actor;

    private float dis;

    private void Start()
    {
        actor = FindObjectOfType<Actor.Scripts.Actor>();
        
        dis = Vector3.Distance(transform.position, actor.transform.position);
    }

    public float GetDistance()
    {
        return dis;
    }
}
