using System;
using System.Collections;
using System.Collections.Generic;
using Actor.Scripts.Event;
using Project;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlockCreate : MonoBehaviour
{
    [SerializeField] private GameObject[] BlockPre;

    [SerializeField] private int BlockCount = 1;

    [SerializeField] private bool isRules;

    [SerializeField] [Range(0, 100)] private float incentiveRate;
    
    // Start is called before the first frame update
    void Start()
    {
        EventBus.Subscribe<InfiniteLevelIns>(OnInfiniteLevelIns);
    }

    private void OnInfiniteLevelIns(InfiniteLevelIns obj)
    {
        if (isRules)
        {
            if (BlockCount != 1)
            {
                for (int i = 0 ; i < BlockPre.Length ; i++)
                {
                    Instantiate(BlockPre[i], new Vector3(0, 0, 50 + (BlockCount * 9.86f)), Quaternion.identity);
                    BlockCount++;
                }
            }
            else
            {
                for (int i = 0 ; i < BlockPre.Length ; i++)
                {
                    Instantiate(BlockPre[i], new Vector3(0, 0, 60 + (i * 9.86f)), Quaternion.identity);
                    BlockCount++;
                }
            }
        }
        else
        {
            if (BlockCount != 1)
            {
                for (int i = 0 ; i < BlockPre.Length ; i++)
                {
                    Instantiate(BlockPre[Random.Range(0 , BlockPre.Length)], new Vector3(0, 0, 50 + (BlockCount * 9.86f)), Quaternion.identity);
                    BlockCount++;
                }
            }
            else
            {
                for (int i = 0 ; i < BlockPre.Length ; i++)
                {
                    Instantiate(BlockPre[Random.Range(0 , BlockPre.Length)], new Vector3(0, 0, 60 + (i * 9.86f)), Quaternion.identity);
                    BlockCount++;
                }
            }
        }
        
        
    }
    
    [Button]
    private void SetIncentiveRate()
    {
        EventBus.Post(new ChangeIncentiveRateDetected(incentiveRate));
    }
}

