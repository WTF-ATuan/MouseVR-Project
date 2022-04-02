using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Actor.Scripts.Event;
using Project;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class ScreenEffect : MonoBehaviour
{
    [SerializeField] private Image ScreenPanel;

    public void Start()
    {
        EventBus.Subscribe<ScreenEffectDetected>(OnScreenEffectDetected);
        //EventBus.Post(new ScreenEffectDetected(0));
    }

    public async void OnScreenEffectDetected(ScreenEffectDetected value)
    {
        if (Mathf.Abs(ScreenPanel.color.a - value.value) > 0.1f)
        {
            ScreenPanel.color = new Color(ScreenPanel.color.r, ScreenPanel.color.g, ScreenPanel.color.b, Mathf.Lerp(ScreenPanel.color.a , value.value , 0.05f));
            await Task.Delay(30);
            OnScreenEffectDetected(value);
        }
    }
    
    
    
    

}
