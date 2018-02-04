using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerControllerbeta : NetworkBehaviour {
	public GameObject laserPrefab;
	public GameObject follower;
	public GameObject bullet;
	public Transform camara;
	public Transform show;
	public List<GameObject> flota;
	[SyncVar]
	public Color colorino;
	[SyncVar]
	public string nick;
	private float rof;//rate of fire
	// Use this for initialization
	void Start () {
		camara = GameObject.FindGameObjectWithTag("MainCameraPointer").transform;
		Cursor.lockState = CursorLockMode.Locked;
		//if (isLocalPlayer)
		//	CmdSpawn();
		if (isLocalPlayer) {
			//name = "player";
			//nick = "player";
			CmdSpawn ();
		}
		rof = Time.time;
		show = transform.GetChild (0);
		show.GetComponent<MeshRenderer> ().material.color = colorino;
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
		/*
		if (flota.Count < 1) {
			foreach (GameObject nave in GameObject.FindGameObjectsWithTag("follower")) {
				Debug.Log (nave.GetComponent<follower> ().namerino);
			}
		}*/
		//camara.transform.parent = transform;
		camara.position = transform.position;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Input.GetButton ("Fire2")) {

			Cursor.lockState = CursorLockMode.None;
			show.LookAt (ray.GetPoint(1000f));
		} else {
			Cursor.lockState = CursorLockMode.Locked;
			transform.LookAt (ray.GetPoint (1000f));
			show.LookAt (ray.GetPoint (1000f));
		}
		//camara.rotation = transform.rotation;
		var x = Input.GetAxis("Horizontal")*0.1f;
		var z = Input.GetAxis("Vertical")*0.1f;

		transform.Translate(x, 0, z);
		if (Input.GetKeyDown (KeyCode.Space)) {
			//CmdFire ();
			CmdDance ();
		}		
		if (Input.GetButton ("Fire1")) {
			CmdFireBullet ();

		}
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
	void CmdDance() {
		foreach (GameObject nave in flota) {
			nave.GetComponent<follower> ().xy.x = nave.GetComponent<follower> ().xy.x * -1;
		}
	}
	[Command]
	void CmdFireBullet() {
		if (rof > Time.time)
			return;

		rof = Time.time + 0.3f;
		GameObject bull = Instantiate (bullet, transform.position + show.forward * 10f + 
												transform.up * Random.Range(-0.5f,0.5f) + 
												transform.right * Random.Range(-0.5f,0.5f),	
												show.rotation);
		bull.GetComponent<Rigidbody> ().velocity = show.forward * 100f;
		NetworkServer.Spawn (bull);
		Destroy (bull, 2.0f);
		foreach (GameObject nave in flota) {
			bull = Instantiate (bullet, nave.transform.position + nave.transform.forward * 10f + 
				nave.transform.up * Random.Range(-0.5f,0.5f) + 
				nave.transform.right * Random.Range(-0.5f,0.5f),	
				nave.transform.rotation);
			bull.GetComponent<Rigidbody> ().velocity = nave.transform.forward * 100f;
			NetworkServer.Spawn (bull);
			Destroy (bull, 2.0f);
		}
	}


	[Command]
	void CmdSpawnZeta() {
		GameObject foll = Instantiate (follower, transform.position + transform.right * 10f + transform.up * 10f, transform.rotation);
		//NetworkServer.SpawnWithClientAuthority (foll, gameObject);
//		foll.GetComponent<follower> ().setFlagship (transform,10f, 10f);
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
		int iderino = 0;
		for (int i= 1; i< 7;i++){
			for (int j= 1; j< 3;j++){
				GameObject foll = Instantiate (follower, transform.position + transform.right * 10f * i + transform.up * 10f * j, transform.rotation);
				foll.GetComponent<follower> ().setFlagship (transform,10f * i, 10f * j, nick, iderino);
				flota.Add (foll);
				iderino++;
				NetworkServer.Spawn(foll);
				foll = Instantiate (follower, transform.position + transform.right * 10f * i + transform.up * 10f * -j, transform.rotation);
				foll.GetComponent<follower> ().setFlagship (transform,10f * i, 10f * -j, nick, iderino);
				flota.Add (foll);
				iderino++;
				NetworkServer.Spawn(foll);
				foll = Instantiate (follower, transform.position + transform.right * 10f * -i + transform.up * 10f * j, transform.rotation);
				foll.GetComponent<follower> ().setFlagship (transform,10f * -i, 10f * j, nick, iderino);
				flota.Add (foll);
				iderino++;
				NetworkServer.Spawn(foll);
				foll = Instantiate (follower, transform.position + transform.right * 10f * -i + transform.up * 10f * -j, transform.rotation);
				foll.GetComponent<follower> ().setFlagship (transform,10f * -i, 10f * -j, nick, iderino);
				flota.Add (foll);
				iderino++;
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
