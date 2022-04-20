using System.Threading.Tasks;
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


		private IRotate rotate;


		public Vector3 StartPosition{ get; private set; }

		private new Rigidbody rigidbody;

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

		public async void ReceiveJudged(bool isPunish){
			if(isPunish){
				blocker?.SetActive(true);
				
				await Task.Delay(punishDelayTime * 1000);
				blocker?.SetActive(false);
				ResetActor();
			}
			else{
				blocker?.SetActive(true);
				
				await Task.Delay(rewardDelayTime * 1000);
				blocker?.SetActive(false);
				// Give Reward TODO;
				ResetActor();
			}
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