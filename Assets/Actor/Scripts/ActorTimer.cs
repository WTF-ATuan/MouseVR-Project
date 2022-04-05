using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actor.Scripts.Event;
using Project;
using Sirenix.OdinInspector;
using UnityEngine;

public class ActorTimer : MonoBehaviour
{
    [SerializeField] private float Timer = 0 , LimitTime = 2.5f;

    [SerializeField] private Actor.Scripts.Actor actor;
    void Start()
    {
        EventBus.Subscribe<ActorMoveDetected>(OnActorMoveDetected);
        actor = GetComponent<Actor.Scripts.Actor>();
    }
    
    [Button]
    private async void OnActorMoveDetected(ActorMoveDetected obj)
    {
        if (Timer > LimitTime)
        {
            await Task.Delay(1000);
            actor.ResetActor();
            Timer = 0;
        }
        else
        {
            if (Mathf.Abs(obj.InputSpeed) >= 0.3f)
            {
                Timer = 0;
            }
            else
            {
                Timer += Time.deltaTime;
            }
        }
    }
}
