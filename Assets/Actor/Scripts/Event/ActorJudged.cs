namespace Environment.Scripts.Events
{
    public class ActorJudged
    {
        public bool isPunish;

        public bool onlyReward;

        public ActorJudged(bool _isPunish)
        { 
            isPunish = _isPunish;
        }
    }
}