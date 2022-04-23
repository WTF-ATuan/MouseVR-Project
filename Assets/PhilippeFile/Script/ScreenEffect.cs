using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actor.Scripts.Event;
using DG.Tweening;
using Project;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class ScreenEffect : MonoBehaviour
{
    [SerializeField] private Image ScreenPanel;

    [SerializeField] private bool isClosePanel;

    public void Start()
    {
        EventBus.Subscribe<ScreenEffectDetected>(OnScreenEffectDetected);
        //EventBus.Post(new ScreenEffectDetected(1));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (isClosePanel)
            {
                EventBus.Post(new ScreenEffectDetected(1));
            }
            else
            {
                EventBus.Post(new ScreenEffectDetected(0));
            }

            isClosePanel = !isClosePanel;
        }
    }

    private void OnScreenEffectDetected(ScreenEffectDetected obj)
    {
        StopAllCoroutines();
        StartCoroutine(StartLerpEffect(obj.value , 0.1f));
    }
    
    private IEnumerator StartLerpEffect(float value , float time)
    {
        
        while (true)
        {
            if (Mathf.Abs(ScreenPanel.color.a - value) > 0.01f)
            {
                ScreenPanel.color = new Color(ScreenPanel.color.r, ScreenPanel.color.g, ScreenPanel.color.b, Mathf.Lerp(ScreenPanel.color.a , value , 0.05f));
            }
            else
            {
                break;
            }
            yield return new WaitForFixedUpdate();
        }
        
        
    }
    
    
    
    

}
