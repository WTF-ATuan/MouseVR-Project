using System.Threading.Tasks;
using System.Timers;
using Actor.Editor;
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
		[SerializeField] public bool canMoveForward = true;

		[BoxGroup("DelayTime")] [SerializeField]
		private int punishDelayTime;

		[BoxGroup("DelayTime")] [SerializeField]
		private int rewardDelayTime;

		public Vector3 StartPosition{ get; private set; }
		private Rigidbody _rigidbody;
		private IRotate _rotate;
		private float _delayTime;

		public float triggerDistance;
		public bool isTriggerLock;

		public int distanceCount = 1;
		
		private void Start(){
			StartPosition = transform.position;
			_rotate = GetComponent<IRotate>();
			_rigidbody = GetComponent<Rigidbody>();
		}

		public void Move(float inputValue){
			if(!canMoveBack && (inputValue < 0)) return;
			if(!canMoveForward && inputValue > 0) return;
			var forwardDirection = transform.forward;
			var moveDirection = inputValue * forwardDirection * speed;
			_rigidbody.velocity = moveDirection;
		}

		public void WriteMessage(float inputValue){
			var actorTransform = transform;
			var positionInfo = new BehaviorDataInfo{
				Animal_Speed = inputValue,
				Actor_Speed = inputValue * speed,
			};
			var position = actorTransform.position;
			positionInfo.Actor_PositionX = position.x;
			positionInfo.Actor_PositionZ = position.z;
			positionInfo.Animal_Distance = Vector3.Distance(position, StartPosition);
			EventBus.Post(new SavedDataMessage(positionInfo, positionInfo.GetType(), BehaviorEventType.Actor));
		}

		public void Teleport(Vector3 targetPosition){
			transform.position = targetPosition;
		}

		[Button]
		public void ResetActor(){
			Teleport(StartPosition);
			transform.eulerAngles = Vector3.zero;
		}

		public void ReceiveJudged(bool isPunish , bool onlyReward){
			if(isPunish){
				EventBus.Post(new ScreenEffectDetected(1, 0 , Color.green));
				_delayTime = punishDelayTime;
			}
			else{
				if (onlyReward)
				{
					GetReward();
					return;
				}
				EventBus.Post(new ScreenEffectDetected(1, 0 , Color.green));
				_delayTime = rewardDelayTime;
				GetReward();
			}
		}

		public void GetReward(){
			EventBus.Post(new ArduinoTriggerRequested("V"));
			Debug.Log("Get Reward");
		}

		public void GetTrigger()
		{
			EventBus.Post(new ArduinoTriggerRequested("A"));

			isTriggerLock = true;
			Debug.Log("Get Trigger");
		}
		

		private void Update() => TickTime();
		private bool invokeFlag = true;

		private void TickTime(){
			if(_delayTime < 0 && !invokeFlag){
				EventBus.Post(new ScreenEffectDetected(0, 0 , Color.green));
				ResetActor();
				invokeFlag = true;
				return;
			}

			if(_delayTime < 0) return;
			invokeFlag = false;
			_delayTime -= Time.deltaTime;
		}

		public void SelectDirection(bool isRight){
			if(!canRotate) return;
			_rotate.Rotate(isRight);
		}

		public void Lick(){
			EventBus.Post(new ActorLickDetected(transform.position));
		}

		public float GetDistance(){
			return Vector3.Distance(transform.position, StartPosition);
		}

		public float GetSpeed(){
			return speed;
		}

		public void SetPunishTime(float time)
		{
			punishDelayTime = (int) time;
		}

		public void SetTriggerDistance(float distance)
		{
			triggerDistance = distance;
		}

		public void SetGetRewardTime(float time)
		{
			rewardDelayTime = (int) time;
		}

		public int GetRewardTime()
		{
			return rewardDelayTime;
		}

		public int GetPunishTime()
		{
			return punishDelayTime;
		}
	}
}