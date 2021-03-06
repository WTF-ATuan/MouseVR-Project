using System.Threading.Tasks;
using Actor.Scripts.Event;
using Actor.Scripts.EventMessage;
using Environment.Scripts.Events;
using Project;
using UnityEngine;

namespace Actor.Scripts
{
    public class ActorController : MonoBehaviour
    {
        private Actor actor;
        private ArduinoBasic arduinoBasic;


        private int _trailNumber;
        private int _trailSuccess;

        private BehaviorDataInfo behaviorDataInfo;

        private void Start()
        {
            actor = GetComponent<Actor>();
            arduinoBasic = FindObjectOfType<ArduinoBasic>();
            EventBus.Subscribe<ActorMoveDetected>(OnActorMoveDetected);
            EventBus.Subscribe<ActorTeleportDetected>(OnActorTeleportDetected);
            EventBus.Subscribe<ActorJudged>(OnActorJudged);
            EventBus.Subscribe<ActorInfiniteRewardDetected>(OnActorInfiniteRewardDetected);
            EventBus.Subscribe<ActorLickRequested>(x => DetectActorLick());
            EventBus.Subscribe<ActorRotateRequested>(x => SetDirection(x.IsRight));
        }

        private void OnActorInfiniteRewardDetected(ActorInfiniteRewardDetected obj)
        {
            actor.GetReward();
        }

        private void OnActorJudged(ActorJudged obj)
        {
            var isPunish = obj.isPunish;
            var onlyReward = obj.onlyReward;
            if (!isPunish)
            {
                _trailSuccess++;
            }

            _trailNumber++;
            behaviorDataInfo.Trail_Number = _trailNumber;
            behaviorDataInfo.Trail_Success = _trailSuccess;
            behaviorDataInfo.Licking = 0;
            behaviorDataInfo.LeverPress = 0;
            EventBus.Post(new SavedDataMessage(behaviorDataInfo, behaviorDataInfo.GetType(), BehaviorEventType.Trial));

            actor.ReceiveJudged(isPunish, onlyReward);
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
            // DetectMoveValue();
            // DetectDirectionAngle();

            if (arduinoBasic.GetDistance() >= actor.triggerDistance * actor.distanceCount)
            {
                actor.GetTrigger();
                actor.distanceCount++;
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
            behaviorDataInfo.Licking = 0;
            EventBus.Post(new SavedDataMessage(behaviorDataInfo, behaviorDataInfo.GetType(), BehaviorEventType.Trial));
            actor.Lick();
        }

        private void SetDirection(bool isRight)
        {
            behaviorDataInfo.LeverPress = isRight ? 2 : 1;
            EventBus.Post(new SavedDataMessage(behaviorDataInfo, behaviorDataInfo.GetType(), BehaviorEventType.Trial));
            actor.SelectDirection(isRight);
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