using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ServerSpawner : NetworkBehaviour {
	public GameObject target;
	[SyncVar]
	public string texto;
	[SyncVar]
	public int naves;
	public Text estado;
	// Use this for initialization
	void Start () {
		if (isServer){
			texto = "cargando";
			InvokeRepeating ("setTexterino" , 1f, 5f);
		}
	}
	void Update () {
		estado.text = texto;
	}
	void setTexterino (){
		string numa = "Estado :";
		foreach (GameObject nave in GameObject.FindGameObjectsWithTag ("Player")) {
			numa += nave.GetComponent<PlayerControllerbeta> ().nick;
			numa += " tiene ";
			numa += nave.GetComponent<PlayerControllerbeta> ().flota.Count;
			naves = nave.GetComponent<PlayerControllerbeta> ().flota.Count;
			numa += " / ";
		}
		texto = numa;
	}
	public override void OnStartServer ()
	{
		//Debug.Log (GameObject.FindGameObjectsWithTag ("Player").Length);
		//GameObject.FindGameObjectsWithTag ("Player") [GameObject.FindGameObjectsWithTag ("Player").Length].name = "mimi";
		//base.OnStartLocalPlayer ();

		for (int i = 0; i < 20; i++) {
			GameObject tgt = Instantiate (target, new Vector3(Random.Range(-50f,50f),Random.Range(-50f,50f),Random.Range(-50f,50f)), transform.rotation);
			NetworkServer.Spawn (tgt);
		}
		/*
		foreach (GameObject player in GameObject.FindGameObjectsWithTag ("Player")) {
			for (int i = 1; i < 11; i++) {
				for (int j = 1; j < 11; j++) {
					GameObject foll = Instantiate (minion, player.transform.position + player.transform.right * 10f * i + player.transform.up * 10f * j, player.transform.rotation);
					//NetworkServer.SpawnWithClientAuthority (foll, gameObject);
					NetworkServer.Spawn(foll);
					foll.GetComponent<follower> ().setFlagship (player.transform, 10f * i, 10f * j);
					//flota.Add (foll);
					foll = Instantiate (minion, player.transform.position + player.transform.right * 10f * i + player.transform.up * 10f * -j, player.transform.rotation);
					//NetworkServer.SpawnWithClientAuthority (foll, gameObject);
					NetworkServer.Spawn(foll);
					foll.GetComponent<follower> ().setFlagship (player.transform, 10f * i, 10f * -j);
					//flota.Add (foll);
					foll = Instantiate (minion, player.transform.position + player.transform.right * 10f * -i + player.transform.up * 10f * j, player.transform.rotation);
					//NetworkServer.SpawnWithClientAuthority (foll, gameObject);
					NetworkServer.Spawn(foll);
					foll.GetComponent<follower> ().setFlagship (player.transform, 10f * -i, 10f * j);
					//flota.Add (foll);
					foll = Instantiate (minion, player.transform.position + player.transform.right * 10f * -i + player.transform.up * 10f * -j, player.transform.rotation);
					//NetworkServer.SpawnWithClientAuthority (foll, gameObject);
					NetworkServer.Spawn(foll);
					foll.GetComponent<follower> ().setFlagship (player.transform, 10f * -i, 10f * -j);
					//flota.Add (foll);
				}
			}
		}*/
	}
	// Update is called once per frame

}
