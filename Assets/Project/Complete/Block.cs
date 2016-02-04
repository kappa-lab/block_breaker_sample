using UnityEngine;
using System.Collections;
using System;
namespace Complete
{
	public class Block : MonoBehaviour 
	{

		public Action OnHit;

		[SerializeField] private BoxCollider boxCollider;
		private bool isDead=false;

		void OnCollisionEnter(Collision other)
		{
			if (isDead) {
				return;
			}
				
			isDead = true;
			transform.SetParent (transform.root);
			boxCollider.enabled = false;

			if (OnHit != null) {
				OnHit ();
			}

			StartCoroutine (Die ());

		}

		IEnumerator Die()
		{
			gameObject.AddComponent<Rigidbody> ();
			yield return new WaitForFixedUpdate ();

			var rigidbody = gameObject.GetComponent<Rigidbody> ();
			rigidbody.AddTorque(new Vector3(
				UnityEngine.Random.value * 10 + 10,
				UnityEngine.Random.value * 10 + 10, 
				UnityEngine.Random.value * 10 + 10)
			);
			yield return new WaitForSeconds (1f);
			boxCollider.enabled = true;

			Destroy (this);
		}

	}
}