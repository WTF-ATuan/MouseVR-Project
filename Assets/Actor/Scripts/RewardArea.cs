using System.Collections;
using System.Collections.Generic;
using Actor.Scripts.Event;
using Project;
using UnityEngine;

public class RewardArea : MonoBehaviour
{
    [SerializeField] private float incentiveRate;

    [SerializeField] private bool isOpen;
    
    // Start is called before the first frame update
    void Start()
    {
        EventBus.Subscribe<ChangeIncentiveRateDetected>(OnChangeIncentiveRateDetected);
        EventBus.Subscribe<CloseAllRewardGizmosDetected>(OnCloseAllRewardGizmosDetected);
    }

    private void OnCloseAllRewardGizmosDetected(CloseAllRewardGizmosDetected obj)
    {
        isOpen = !isOpen;
    }

    private void OnChangeIncentiveRateDetected(ChangeIncentiveRateDetected obj)
    {
        incentiveRate = obj.incentiveRate;
    }

    private void OnTriggerExit(Collider other){
        //TODO
        if(other.GetComponent<Actor.Scripts.Actor>()){
            
            var rate = Random.Range(0, 100);
            Debug.Log($"rate = {rate}");
            if (rate <= incentiveRate)
            {
                EventBus.Post(new ActorInfiniteRewardDetected());
            }
        }
    }
    
    private void OnDrawGizmos(){
        if(!isOpen) return;
        
        var collider = GetComponent<Collider>();
        var bounds = collider.bounds;
        var boundsCenter = bounds.center;
        var boundsSize = bounds.size;
        Gizmos.color = Color.red;
        Gizmos.DrawCube(boundsCenter, boundsSize);
    }
    
}
