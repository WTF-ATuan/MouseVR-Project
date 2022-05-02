using System.Threading.Tasks;
using System.Timers;
using Actor.Scripts.Event;
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
		private new Rigidbody rigidbody;
		private IRotate rotate;

		private float delayTime;

		private void Start(){
			StartPosition = transform.position;
			rotate = GetComponent<IRotate>();
			rigidbody = GetComponent<Rigidbody>();
		}

		public void Move(float inputValue){
			if(!canMoveBack && (inputValue < 0)) return;
			
			var forwardDirection = transform.forward;
			var moveDirection = inputValue * forwardDirection * speed;
			rigidbody.velocity = moveDirection;
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
				//blocker?.SetActive(true);
				EventBus.Post(new ScreenEffectDetected(0 , 0));
				
				delayTime = punishDelayTime;
			}
			else{
				//blocker?.SetActive(true);
				EventBus.Post(new ScreenEffectDetected(0 , 0));

				delayTime = rewardDelayTime;
				GetReward();
			}
		}

		public void GetReward()
		{
			Debug.Log("Get Reward");
		}

		private void Update() => TickTime();
		private bool invokeFlag = true;

		private void TickTime(){
			if(delayTime < 0 && !invokeFlag){
				//blocker?.SetActive(false);
				EventBus.Post(new ScreenEffectDetected(1 , 0));

				ResetActor();
				invokeFlag = true;
				return;
			}

			if(delayTime < 0) return;
			delayTime -= Time.deltaTime;
			invokeFlag = false;
		}

		public void SelectDirection(bool isRight){
			if(!canRotate) return;
			rotate.Rotate(isRight);
		}

		public void Lick(){
			EventBus.Post(new ActorLickDetected(transform.position));
			Debug.Log("lick");
		}
	}
}