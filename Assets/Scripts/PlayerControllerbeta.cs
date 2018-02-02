using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerControllerbeta : NetworkBehaviour {
	public GameObject laserPrefab;
	public GameObject follower;
	public GameObject bullet;
	public Transform camara;
	public List<GameObject> flota;
	[SyncVar]
	public Color colorino;
	private float rof;//rate of fire
	// Use this for initialization
	void Start () {
		camara = GameObject.FindGameObjectWithTag("MainCameraPointer").transform;
		Cursor.lockState = CursorLockMode.Locked;
		//if (isLocalPlayer)
		//	CmdSpawn();
		if (isLocalPlayer)
			CmdSpawn();
		rof = Time.time;
		GetComponent<MeshRenderer> ().material.color = colorino;
	}
	// Update is called once per frame
	/*
	public override void OnStartLocalPlayer(){

		CmdSpawnZeta();
	}
	*/

	void Update () {
		if (!isLocalPlayer)
			return;

		//camara.transform.parent = transform;
		camara.position = transform.position;
		if (Input.GetButton ("Fire2")) {

			Cursor.lockState = CursorLockMode.None;
			flota [0].GetComponent<follower> ().x *= -1;
		} else 
			Cursor.lockState = CursorLockMode.Locked;
		//camara.rotation = transform.rotation;
		var x = Input.GetAxis("Horizontal")*0.1f;
		var z = Input.GetAxis("Vertical")*0.1f;

		transform.Translate(x, 0, z);
		if (Input.GetKeyDown (KeyCode.Space)) {
			CmdFire ();
		}		
		if (Input.GetButton ("Fire1")) {
			CmdFireBullet ();
		}
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		transform.LookAt (ray.GetPoint(1000f));
		//Debug.Log (Camera.main.ScreenPointToRay (Input.mousePosition));
	}
	[Command]
	void CmdFire() {
		GameObject laser = Instantiate (laserPrefab, transform.position, transform.rotation);
		laser.transform.SetParent (transform);
		NetworkServer.Spawn (laser);
		Destroy (laser, 3f);
		foreach (GameObject nave in flota) {
			laser = Instantiate (laserPrefab, nave.transform.position, nave.transform.rotation);
			laser.transform.SetParent (nave.transform);
			NetworkServer.Spawn (laser);
			Destroy (laser, 3f);
		}
	}

	[Command]
	void CmdFireBullet() {
		if (rof > Time.time)
			return;

		rof = Time.time + 0.3f;
		GameObject bull = Instantiate (bullet, transform.position + transform.forward + 
												transform.up * Random.Range(-0.5f,0.5f) + 
												transform.right * Random.Range(-0.5f,0.5f),	
												transform.rotation);
		bull.GetComponent<Rigidbody> ().velocity = transform.forward * 100f;
		NetworkServer.Spawn (bull);
		Destroy (bull, 2.0f);
	}


	[Command]
	void CmdSpawnZeta() {
		GameObject foll = Instantiate (follower, transform.position + transform.right * 10f + transform.up * 10f, transform.rotation);
		//NetworkServer.SpawnWithClientAuthority (foll, gameObject);
		foll.GetComponent<follower> ().setFlagship (transform,10f, 10f);
		flota.Add (foll);
		//foll.name = "follower" + GameObject.FindGameObjectsWithTag ("Player").Length;
		//NetworkServer.SpawnWithClientAuthority (foll, connectionToClient);
		NetworkServer.Spawn(foll);
	}
	[Command]
	void CmdSpawn() {

		/*foreach (GameObject player in GameObject.FindGameObjectsWithTag ("Player")) {
			for (int i= 1; i< 6;i++){
				for (int j= 1; j< 6;j++){
					GameObject foll = Instantiate (follower, player.transform.position + player.transform.right * 10f * i + player.transform.up * 10f * j, player.transform.rotation);
					NetworkServer.SpawnWithClientAuthority (foll, player);
					//NetworkServer.Spawn(foll);
					foll.GetComponent<follower> ().setFlagship (player.transform,10f * i, 10f * j);
					player.GetComponent<PlayerController> ().flota.Add (foll);
					foll.name = "follower" + GameObject.FindGameObjectsWithTag ("Player").Length;
					foll = Instantiate (follower, player.transform.position + player.transform.right * 10f * i + player.transform.up * 10f * -j, player.transform.rotation);
					NetworkServer.SpawnWithClientAuthority (foll, player);
					//NetworkServer.Spawn(foll);
					foll.GetComponent<follower> ().setFlagship (player.transform,10f * i, 10f * -j);
					player.GetComponent<PlayerController> ().flota.Add (foll);
					foll.name = "follower" + GameObject.FindGameObjectsWithTag ("Player").Length;
					foll = Instantiate (follower, player.transform.position + player.transform.right * 10f * -i + player.transform.up * 10f * j, player.transform.rotation);
					NetworkServer.SpawnWithClientAuthority (foll, player);
					//NetworkServer.Spawn(foll);
					foll.GetComponent<follower> ().setFlagship (player.transform,10f * -i, 10f * j);
					player.GetComponent<PlayerController> ().flota.Add (foll);
					foll.name = "follower" + GameObject.FindGameObjectsWithTag ("Player").Length;
					foll = Instantiate (follower, player.transform.position + player.transform.right * 10f * -i + player.transform.up * 10f * -j, player.transform.rotation);
					NetworkServer.SpawnWithClientAuthority (foll, player);
					//NetworkServer.Spawn(foll);
					foll.GetComponent<follower> ().setFlagship (player.transform,10f * -i, 10f * -j);
					player.GetComponent<PlayerController> ().flota.Add (foll);
					foll.name = "follower" + GameObject.FindGameObjectsWithTag ("Player").Length;
				}
			}
		}*/
		for (int i= 1; i< 11;i++){
			for (int j= 1; j< 6;j++){
				GameObject foll = Instantiate (follower, transform.position + transform.right * 10f * i + transform.up * 10f * j, transform.rotation);
				foll.GetComponent<follower> ().setFlagship (transform,10f * i, 10f * j);
				flota.Add (foll);
				NetworkServer.Spawn(foll);
				foll = Instantiate (follower, transform.position + transform.right * 10f * i + transform.up * 10f * -j, transform.rotation);
				foll.GetComponent<follower> ().setFlagship (transform,10f * i, 10f * -j);
				flota.Add (foll);
				NetworkServer.Spawn(foll);
				foll = Instantiate (follower, transform.position + transform.right * 10f * -i + transform.up * 10f * j, transform.rotation);
				foll.GetComponent<follower> ().setFlagship (transform,10f * -i, 10f * j);
				flota.Add (foll);
				NetworkServer.Spawn(foll);
				foll = Instantiate (follower, transform.position + transform.right * 10f * -i + transform.up * 10f * -j, transform.rotation);
				foll.GetComponent<follower> ().setFlagship (transform,10f * -i, 10f * -j);
				flota.Add (foll);
				NetworkServer.Spawn(foll);
			}
		}

		/*		
		 * 
		 * GameObject foll = Instantiate (follower, transform.position + transform.right * 5f, transform.rotation);
		NetworkServer.SpawnWithClientAuthority (foll, gameObject);
		foll.GetComponent<follower> ().setFlagship (transform,5f,0f);
		*/
	}
}
