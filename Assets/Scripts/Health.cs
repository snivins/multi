using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {
	public const int maxHealth = 100;
	public bool respawnable;
	[SyncVar]
	public int health = maxHealth;
	
	// Update is called once per frame
	public void TakeDamage(int dmg) {
		health -= dmg;
		if (health <= 0) {
			if (respawnable) {
				health = maxHealth;
				RpcRespawn ();
			} else {
				Destroy (gameObject);
			}
		}
	}

	[ClientRpc]
	void RpcRespawn() {
		if (isLocalPlayer)
			transform.position = Vector3.zero;
	}
}
