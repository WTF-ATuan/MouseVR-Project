using System.Threading.Tasks;
using Actor.Scripts.Event;
using Environment.Scripts.Events;
using Project;
using UnityEngine;

namespace Actor.Scripts
{
    public class ActorController : MonoBehaviour
    {
        private Actor actor;

        private void Start()
        {
            actor = GetComponent<Actor>();
            EventBus.Subscribe<ActorMoveDetected>(OnActorMoveDetected);
            EventBus.Subscribe<ActorTeleportDetected>(OnActorTeleportDetected);
            EventBus.Subscribe<ActorJudged>(OnActorJudged);
            EventBus.Subscribe<ActorInfiniteRewardDetected>(OnActorInfiniteRewardDetected);
            EventBus.Subscribe<ActorLickRequested>(x => DetectActorLick());
            EventBus.Subscribe<ActorRotateRequested>(x => actor.SelectDirection(x.IsRight));
        }

        private void OnActorInfiniteRewardDetected(ActorInfiniteRewardDetected obj)
        {
            actor.GetReward();
        }

        private void OnActorJudged(ActorJudged obj)
        {
            var isPunish = obj.isPunish;
            actor.ReceiveJudged(isPunish);

            actor.isTriggerLock = false;
        }

        private void OnActorTeleportDetected(ActorTeleportDetected obj)
        {
            var targetPosition = obj.TargetPosition;
            actor.Teleport(targetPosition);
        }

        private void OnActorMoveDetected(ActorMoveDetected obj)
        {
            var inputSpeed = obj.InputSpeed;
            actor.WriteMessage(inputSpeed);
            actor.Move(inputSpeed);
        }

        private void Update()
        {
            DetectMoveValue();
            DetectDirectionAngle();

            if (actor.GetDistance() >= actor.triggerDistance && !actor.isTriggerLock)
            {
                actor.GetTrigger();
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                DetectTeleportValue();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                DetectActorLick();
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }

        private void DetectActorLick()
        {
            actor.Lick();
        }

        private void DetectDirectionAngle()
        {
            var left = Input.GetKeyDown(KeyCode.A);
            var right = Input.GetKeyDown(KeyCode.D);
            if (!right && !left) return;
            var isRight = right && !left;
            actor.SelectDirection(isRight);
        }

        private void DetectMoveValue()
        {
            var scrollDeltaOffsetY = Input.GetAxisRaw("Vertical");
            EventBus.Post(new ActorMoveDetected(scrollDeltaOffsetY));
        }

        private void DetectTeleportValue()
        {
            var actorStartPosition = actor.StartPosition;
            EventBus.Post(new ActorTeleportDetected(actorStartPosition));
        }
    }
}