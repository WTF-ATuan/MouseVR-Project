namespace Actor.Scripts.Event
{
    public class ChangeIncentiveRateDetected
    {
        public float incentiveRate;

        public ChangeIncentiveRateDetected(float _incentiveRate)
        {
            incentiveRate = _incentiveRate;
        }
    }
}