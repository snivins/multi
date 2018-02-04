﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {
	public const int maxHealth = 100;
	public bool respawnable;
	public bool nave;
	[SyncVar]
	public int health = maxHealth;
	
	// Update is called once per frame
	public void TakeDamage(int dmg) {
		if (!isServer)
			return;
		health -= dmg;
		if (health <= 0) {
			if (respawnable) {
				health = maxHealth;
				RpcRespawn ();
			} else if (nave){
				//RpcDed ();
				gameObject.GetComponent<follower> ().flagship.GetComponent<PlayerControllerbeta>().flota.Remove (gameObject);
				gameObject.GetComponent<follower> ().alive = false;
			} else {
				Destroy (gameObject);
			}
		}
	}

	[ClientRpc]
	void RpcRespawn() {
		//if (isLocalPlayer)
			transform.position = Vector3.zero;
	}
}
