using System;
using System.Collections;
using System.Collections.Generic;
using Actor.Scripts.Event;
using Project;
using Sirenix.OdinInspector;
using UnityEngine;

public class SettingPanel : MonoBehaviour
{
    [SerializeField] private int rewardCount , rewardLimit;
    
    [SerializeField] [Range(0, 100)] private float incentiveRate;

    private void Start()
    {
        EventBus.Subscribe<ActorInfiniteRewardDetected>(OnActorInfiniteRewardDetected);
    }

    private void OnActorInfiniteRewardDetected(ActorInfiniteRewardDetected obj)
    {
        rewardCount++;

        if (rewardCount >= rewardLimit)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        
        Debug.Log("Get Reward Count : " + rewardCount);
    }


    // Start is called before the first frame update

    [Button]
    private void CloseOpenRewardGizmos()
    {
        EventBus.Post(new CloseAllRewardGizmosDetected());
    }

    [Button]
    private void SettingReward()
    {
        EventBus.Post(new ChangeIncentiveRateDetected(incentiveRate));
    }
}
