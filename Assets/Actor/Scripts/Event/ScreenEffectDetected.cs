using UnityEngine;

namespace Actor.Scripts.Event
{
    public class ScreenEffectDetected
    {
        public float value;
        
        public float time;

        public Color color;

        public ScreenEffectDetected(float _value , float _time , Color _color)
        {
            value = _value;
            time = _time;
            color = _color;
        }
    }
}