using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actor.Scripts.Event;
using Project;
using UnityEngine;

public class ActorTimer : MonoBehaviour
{
    [SerializeField] private float Timer = 0;
    void Start()
    {
        EventBus.Subscribe<ActorMoveDetected>(OnActorMoveDetected);
    }
    

    private async void OnActorMoveDetected(ActorMoveDetected obj)
    {
        if (Timer > 2.5f)
        {
            //Teleport!!!!
            await Task.Delay(1000);
            Debug.Log("Teleport");
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
