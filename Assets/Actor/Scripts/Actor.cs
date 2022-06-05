using System.Threading.Tasks;
using System.Timers;
using Actor.Scripts.Event;
using Actor.Scripts.EventMessage;
using Project;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Actor.Scripts{
	public class Actor : MonoBehaviour{
		[SerializeField] private float speed = 5;
		[SerializeField] public bool canRotate = true;
		[SerializeField] public bool canMoveBack = true;

		[BoxGroup("DelayTime")] [SerializeField]
		private int punishDelayTime;

		[BoxGroup("DelayTime")] [SerializeField]
		private int rewardDelayTime;

		public Vector3 StartPosition{ get; private set; }
		private Rigidbody _rigidbody;
		private IRotate _rotate;
		private float _delayTime;

		private ActorBehaviorInfo _behaviorInfo = new ActorBehaviorInfo();

		private void Start(){
			StartPosition = transform.position;
			_rotate = GetComponent<IRotate>();
			_rigidbody = GetComponent<Rigidbody>();
		}

		public void Move(float inputValue){
			if(!canMoveBack && (inputValue < 0)) return;
			var forwardDirection = transform.forward;
			var moveDirection = inputValue * forwardDirection * speed;
			_rigidbody.velocity = moveDirection;
		}

		public void WriteMessage(float inputValue){
			var actorTransform = transform;
			_behaviorInfo.Animal_Speed = inputValue;
			_behaviorInfo.Actor_Speed = inputValue * speed;
			var position = actorTransform.position;
			_behaviorInfo.Actor_PositionX = position.x;
			_behaviorInfo.Actor_PositionZ = position.z;
			_behaviorInfo.Animal_Distance = Vector3.Distance(position, StartPosition);
			_behaviorInfo.Time = Time.time;
			EventBus.Post(_behaviorInfo);
		}

		public void Teleport(Vector3 targetPosition){
			transform.position = targetPosition;
		}

		[Button]
		public void ResetActor(){
			Teleport(StartPosition);
			transform.eulerAngles = Vector3.zero;
		}

		public void ReceiveJudged(bool isPunish){
			if(isPunish){
				EventBus.Post(new ScreenEffectDetected(1, 0));
				_delayTime = punishDelayTime;
			}
			else{
				EventBus.Post(new ScreenEffectDetected(1, 0));
				_delayTime = rewardDelayTime;
				GetReward();
			}
		}

		public void GetReward(){
			Debug.Log("Get Reward");
		}

		private void Update() => TickTime();
		private bool invokeFlag = true;

		private void TickTime(){
			if(_delayTime < 0 && !invokeFlag){
				EventBus.Post(new ScreenEffectDetected(0, 0));
				ResetActor();
				invokeFlag = true;
				return;
			}

			if(_delayTime < 0) return;
			_delayTime -= Time.deltaTime;
			invokeFlag = false;
		}

		public void SelectDirection(bool isRight){
			if(!canRotate) return;
			_rotate.Rotate(isRight);
		}

		public void Lick(){
			EventBus.Post(new ActorLickDetected(transform.position));
		}
	}
}