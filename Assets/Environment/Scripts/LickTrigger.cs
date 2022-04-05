using System;
using System.Collections;
using System.Collections.Generic;
using Actor.Scripts.Event;
using Environment.Scripts.Events;
using Project;
using UnityEngine;

public class LickTrigger : MonoBehaviour
{
    [SerializeField] private int correctLickCount, wrongLickCount;

    [SerializeField] private int correctLickCountLimit, wrongLickCountLimit;

    [SerializeField] private Collider collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
        
        EventBus.Subscribe<ActorLickDetected>(OnActorLickDetected);
    }

    private void OnActorLickDetected(ActorLickDetected obj)
    {
        var isContains = collider.bounds.Contains(obj.LickPosition);

        if (isContains)
        {
            correctLickCount++;
        }
        else
        {
            wrongLickCount++;
        }
        
        CalculateLickCount();
    }

    private void CalculateLickCount()
    {
        if (correctLickCount >= correctLickCountLimit)
        {
            EventBus.Post(new ActorJudged(false));
            correctLickCount = 0;
            wrongLickCount = 0;
        }

        if (wrongLickCount >= wrongLickCountLimit)
        {
            EventBus.Post(new ActorJudged(true));
            correctLickCount = 0;
            wrongLickCount = 0;
        }
    }
}