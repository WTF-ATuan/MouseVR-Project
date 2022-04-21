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

		[SerializeField] private GameObject blocker;

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
				blocker?.SetActive(true);
				delayTime = punishDelayTime;
			}
			else{
				blocker?.SetActive(true);
				delayTime = rewardDelayTime;
				// Give Reward TODO;
			}
		}

		private void Update() => TickTime();
		private bool invokeFlag = true;

		private void TickTime(){
			if(delayTime < 0 && !invokeFlag){
				blocker?.SetActive(false);
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