using UnityEngine;

namespace Actor.Scripts.Event
{
    public class ActorLickDetected
    {
        public Vector3 LickPosition;

        public ActorLickDetected(Vector3 _LickPosition)
        {
            LickPosition = _LickPosition;
        }
    }
}