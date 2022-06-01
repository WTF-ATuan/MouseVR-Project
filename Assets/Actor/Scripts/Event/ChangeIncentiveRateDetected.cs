namespace Actor.Scripts.Event
{
    //機率修改
    public class ChangeIncentiveRateDetected
    {
        public float incentiveRate;
        
        public ChangeIncentiveRateDetected(float _incentiveRate)
        {
            incentiveRate = _incentiveRate;
        }
    }
}