using System;
using System.Collections;
using System.Collections.Generic;
using Actor.Scripts.Event;
using Environment.Scripts.Events;
using Project;
using Sirenix.OdinInspector;
using UnityEngine;

public class SettingPanel : MonoBehaviour
{
    [SerializeField] private int rewardCount , rewardLimit;
    
    [SerializeField] [Range(0, 100)] private float incentiveRate;

    [SerializeField] private int lickCount , fallCount , successCount;

    private void Start()
    {
        EventBus.Subscribe<ActorInfiniteRewardDetected>(OnActorInfiniteRewardDetected);
        EventBus.Subscribe<ActorLickDetected>(OnActorLickDetected);
        EventBus.Subscribe<ActorJudged>(OnActorJudged);
    }

    private void OnActorJudged(ActorJudged obj)
    {
        var judge = obj.isPunish;

        if (judge)
        {
            fallCount++;
        }
        else
        {
            successCount++;
        }
    }

    private void OnActorLickDetected(ActorLickDetected obj)
    {
        lickCount++;
    }

    private void OnActorInfiniteRewardDetected(ActorInfiniteRewardDetected obj)
    {
        rewardCount++;

        if (rewardCount >= rewardLimit)
        {
            UnityEditor.EditorApplication.isPlaying = false; //哈哈我不是唯讀
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
    public void SettingReward(float _incentiveRate)
    {
        EventBus.Post(new ChangeIncentiveRateDetected(_incentiveRate));
    }

    public int GetRewardCount()
    {
        return rewardCount;
    }

    public int GetLickCount()
    {
        return lickCount;
    }

    public int GetFallCount()
    {
        return fallCount;
    }

    public int GetSuccessCount()
    {
        return successCount;
    }
}
