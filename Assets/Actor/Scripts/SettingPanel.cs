using System;
using System.Collections;
using System.Collections.Generic;
using Actor.Editor;
using Actor.Scripts.Event;
using Environment.Scripts.Events;
using Project;
using Sirenix.OdinInspector;
using UnityEngine;

public class SettingPanel : MonoBehaviour
{
    [SerializeField] private int rewardCount , rewardLimit;
    
    [SerializeField] [Range(0, 100)] private float incentiveRate;

    [SerializeField] protected int lickCount , fallCount , successCount , manualReward , chooseLeft , chooseRight;

    private void Start()
    {
        EventBus.Subscribe<ActorInfiniteRewardDetected>(OnActorInfiniteRewardDetected);
        EventBus.Subscribe<ActorLickDetected>(OnActorLickDetected);
        EventBus.Subscribe<ActorJudged>(OnActorJudged);
        
        EventBus.Subscribe<ChooseLeftRight>(OnChooseLeftRight);
    }

    private void OnChooseLeftRight(ChooseLeftRight obj)
    {
        if (obj.isLeft)
        {
            chooseLeft++;
        }
        else
        {
            chooseRight++;
        }
    }

    [Button]
    public void GetReward()
    {
        EventBus.Post(new ActorJudged(false));
        manualReward++;
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
            EventBus.Post(new ArduinoTriggerRequested("V"));
            UnityEditor.EditorApplication.isPlaying = false; //哈哈我不是唯讀
        }
        
        Debug.Log("Get Reward Count : " + rewardCount);
    }


    // Start is called before the first frame update

    [Button]
    public void CloseOpenRewardGizmos(bool isGizmos)
    {
        EventBus.Post(new CloseAllRewardGizmosDetected(isGizmos));
    }

    [Button]
    public void SettingReward(float _incentiveRate)
    {
        EventBus.Post(new ChangeIncentiveRateDetected(_incentiveRate));
    }

    public void SetReward(int value)
    {
        rewardLimit = value;
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

    public int GetManualReward()
    {
        return manualReward;
    }

    public int GetChooseLeft()
    {
        return chooseLeft;
    }

    public int GetChooseRight()
    {
        return chooseRight;
    }

    public string GetRewardSize()
    {
        var allRewardArea = GameObject.FindGameObjectsWithTag("RewardArea");
        float sizeX = 0 , sizeY = 0 , sizeZ = 0;

        if (allRewardArea.Length != 0)
        {
            foreach (var rewardArea in allRewardArea)
            {
                sizeX = sizeX + rewardArea.GetComponent<Collider>().bounds.size.x;
                sizeY = sizeY + rewardArea.GetComponent<Collider>().bounds.size.y;
                sizeZ = sizeZ + rewardArea.GetComponent<Collider>().bounds.size.z;
            }

            return "x : " + sizeX + "  " + "y : " + sizeY + "  " + "z : " + sizeZ;
        }
        else
        {
            return "x : 0  y : 0  z : 0";
        }
        
        
    }

    public float GetRewardDistance()
    {
        var allRewardArea = GameObject.FindGameObjectsWithTag("RewardArea");
        var dis = Vector3.Distance(allRewardArea[0].transform.position , allRewardArea[allRewardArea.Length - 1].transform.position);

        return (dis / allRewardArea.Length);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            UnityEditor.EditorApplication.isPlaying = false; //哈哈我不是唯讀
        }
    }
}
