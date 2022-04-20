using System.Collections;
using System.Collections.Generic;
using Actor.Scripts.Event;
using Project;
using UnityEngine;

public class BlockCreate : MonoBehaviour
{
    [SerializeField] private GameObject[] BlockPre;

    [SerializeField] private int BlockCount = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        EventBus.Subscribe<InfiniteLevelIns>(OnInfiniteLevelIns);
    }

    private void OnInfiniteLevelIns(InfiniteLevelIns obj)
    {
        if (BlockCount != 1)
        {
            Instantiate(BlockPre[BlockPre.Length - 1], new Vector3(0, 0, 50 + (BlockCount * 9.9f)), Quaternion.identity);
        }
        else
        {
            Instantiate(BlockPre[BlockPre.Length - 1], new Vector3(0, 0, 60), Quaternion.identity);
        }
        BlockCount++;
    }
}

