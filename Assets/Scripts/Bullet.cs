using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	public GameObject bom;
	// Use this for initialization
	void OnCollisionEnter(Collision collision) {
		GameObject hit = collision.gameObject;
		if (hit.GetComponent<Health>() != null) {
			hit.GetComponent<Health>().TakeDamage (10);
			GameObject boom = Instantiate (bom,transform.position, transform.rotation);
			Destroy (boom, 4f);
			Destroy (gameObject);
		}
	}
}
