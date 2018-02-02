using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void OnCollisionEnter(Collision collision) {
		GameObject hit = collision.gameObject;
		if (hit.GetComponent<Health>() != null) {
			hit.GetComponent<Health>().TakeDamage (10);
			Destroy (gameObject);
		}
	}
}
