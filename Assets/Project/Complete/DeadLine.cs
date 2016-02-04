using UnityEngine;
using System.Collections;
using System;
namespace Complete
{
	public class DeadLine : MonoBehaviour 
	{
		public Action OnDead;
		void OnTriggerEnter (Collider other) 
		{
			Debug.Log ("Dead");


			if (OnDead != null) {
				OnDead ();
			}


			///Unti pattern
	//		GameObject.Find("Stage").SendMessage ("GameOver");
			///
		}
	}
}