namespace Actor.Scripts.Event
{
    public class ScreenEffectDetected
    {
        public float value;
        
        public float time;

        public ScreenEffectDetected(float _value , float _time)
        {
            value = _value;
            time = _time;
        }
    }
}