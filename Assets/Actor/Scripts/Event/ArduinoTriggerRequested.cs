namespace Actor.Editor
{
    public class ArduinoTriggerRequested
    {
        public string sendText;
        public float time;

        public ArduinoTriggerRequested(string _sendText, float _time = 0)
        {
            sendText = _sendText;
            time = _time;
        }
    }
}