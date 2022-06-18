using System;
using System.Collections;
using Actor.Editor;
using Project;
using UnityEngine;

namespace Actor
{
    public class CameraTrigger : MonoBehaviour
    {
        [SerializeField] private string messageHigh;
        [SerializeField] private string messageLow;
        [SerializeField] private float duringHigh = 0.01f;
        [SerializeField] private float duringLow = 0.0334f;


        private void Start()
        {
            StartCoroutine(SandingMessage());
        }

        private IEnumerator SandingMessage()
        {
            while (gameObject.activeSelf)
            {
                EventBus.Post(new ArduinoTriggerRequested(messageHigh));
                yield return new WaitForSeconds(duringHigh);
                EventBus.Post(new ArduinoTriggerRequested(messageLow));
                yield return new WaitForSeconds(duringLow);
            }
        }
    }
}