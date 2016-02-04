using UnityEngine;
using System.Collections;
namespace Complete
{
	public class GameManager : MonoBehaviour 
	{

		[SerializeField] private Rigidbody ball;
		[SerializeField] private Rigidbody paddle;
		[SerializeField] private DeadLine deadLine;
		[SerializeField] private Transform blockContainer;
		[SerializeField] private Transform gameContaner;
		[SerializeField] private Camera topCamera; 
		[SerializeField] private Camera mainCamera; 
		[SerializeField] private Block blockPrefab; 


		private Vector3 phase = Vector3.zero;
		private bool isLeft = false;
		private GameStatus status;
		[SerializeField] private int numBlocks = 4;

		enum GameStatus
		{
			Play,
			GameOver,
			GameClear
		}

		void Awake()
		{
			for (int i = 0; i < numBlocks; i++) {
				Block block = Instantiate (blockPrefab) as Block;
				block.transform.SetParent(blockContainer);
				block.transform.localPosition = new Vector3 (
					i % 3 * 4 - 4,
				 	0,
					i / 3 * 3 - 2
				);
				block.OnHit = OnHit;
			}
		}

		void Start () 
		{
			ball.AddForce (Vector3.forward * 300 * 2);
			ball.AddTorque (new Vector3 (
				Random.value * 10 + 10,
				Random.value * 10 + 10, 
				Random.value * 10 + 10));

			deadLine.OnDead = GameOver;
		}
		

		void Update () 
		{

			MoveContainer ();

			if (Input.GetKey (KeyCode.LeftArrow)) {
				if (!isLeft) {
					paddle.velocity = paddle.velocity * 0.1f;
				} else {
					paddle.AddForce (new Vector3(-5000,0,0));	
				}
				isLeft = true;
			} else if (Input.GetKey (KeyCode.RightArrow)) {
				if (isLeft) {
					paddle.velocity = paddle.velocity * 0.1f;
				} else {
					paddle.AddForce (new Vector3(5000,0,0));
				}
				isLeft = false;
			}

			paddle.velocity = new Vector3 (paddle.velocity.x, 0, 0);
			paddle.transform.localRotation = new Quaternion();


			if (numBlocks <= 0) {
				GameClear ();
			}

			///Unti pattern
	//		if (blockContainer.childCount <= 0) {
	//			GameClear ();
	//		}

		}

		void OnGUI()
		{
			int offset = 10;
			int width = Screen.width - offset * 2;
			int height = Screen.height - offset * 2;

			if (status == GameStatus.Play) {
				if(GUI.Button (new Rect (offset, offset, width, 40), "Switch View")) {
					SwitchCamera ();
				}
				return;
			} 


			if (status == GameStatus.GameOver){
				if(GUI.Button (new Rect (offset, offset, width, height), "Game Over")) {
					Application.LoadLevel ("Block");
				}
			}else if (status == GameStatus.GameClear){
				if(GUI.Button (new Rect (offset, offset, width, height), "!! GAME CLEAR !!")) {
					Application.LoadLevel ("Block");
				}
			}

		}


		void SwitchCamera()
		{
			if (topCamera.enabled) {
				topCamera.enabled = false;
				mainCamera.enabled = true;
			} else {
				topCamera.enabled = true;
				mainCamera.enabled = false;
			}
	//		topCamera.enabled = !(mainCamera.enabled = topCamera.enabled);
		}
		void MoveContainer()
		{
			var p = gameContaner.localPosition;
			p.x = Mathf.Cos (phase.x) * 130;
			p.z = Mathf.Sin (phase.z) * 120 - 50;
			gameContaner.localPosition = p;

			phase.x += 0.001f;
			phase.z += 0.003f;
		}

		void OnHit()
		{
			numBlocks--;
		}

		void GameOver()
		{
			status = GameStatus.GameOver;
		}

		void GameClear()
		{
			status = GameStatus.GameClear;
		}

	}
}