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

    [SerializeField] [Range(0 , 0.05f)] private float closeTime;
    
    Color green = Color.green;
    Color black = Color.black;

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
                EventBus.Post(new ScreenEffectDetected(1 , closeTime , Color.black));
            }
            else
            {
                EventBus.Post(new ScreenEffectDetected(0 , closeTime , Color.black));
            }

            isClosePanel = !isClosePanel;
        }
    }

    public void ChangeScreenBlank()
    {
        if (isClosePanel)
        {
            EventBus.Post(new ScreenEffectDetected(1 , closeTime , Color.black));
        }
        else
        {
            EventBus.Post(new ScreenEffectDetected(0 , closeTime , Color.black));
        }

        isClosePanel = !isClosePanel;
    }

    public string GetState()
    {
        if (isClosePanel)
        {
            return "Disable";
        }
        else
        {
            return "Enable";
        }
    }

    private void OnScreenEffectDetected(ScreenEffectDetected obj)
    {
        StopAllCoroutines();
        ScreenPanel.color = obj.color;
        StartCoroutine(StartLerpEffect(obj.value , obj.time));
    }
    
    private IEnumerator StartLerpEffect(float value , float time)
    {

        if (time != 0)
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
                yield return new WaitForSeconds(time);
            }
        }
        else
        {
            if (value == 0)
            {
                ScreenPanel.color = new Color(ScreenPanel.color.r, ScreenPanel.color.g, ScreenPanel.color.b,0);
            }
            else
            {
                ScreenPanel.color = new Color(ScreenPanel.color.r, ScreenPanel.color.g, ScreenPanel.color.b,1);
            }
        }
   
    }

    public void SetColor(Color color)
    {
        ScreenPanel.color = color;
    }

    public void SetBlankActive(bool isActive)
    {
        ScreenPanel.gameObject.SetActive(isActive);
    }
    
    
    
    

}
