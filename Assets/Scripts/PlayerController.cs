using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {
	public GameObject laserPrefab;
	public GameObject follower;
	public Transform camara;
	public List<GameObject> flota;
	// Use this for initialization
	void Start () {
		camara = GameObject.FindGameObjectWithTag("MainCameraPointer").transform;
		Cursor.lockState = CursorLockMode.Locked;
		if (!isLocalPlayer)
			return;
		CmdSpawn ();
	}
	public override void OnStartLocalPlayer ()
	{
		GetComponent<MeshRenderer> ().material.color = Color.red;
		//base.OnStartLocalPlayer ();
	}
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
			return;

		//camara.transform.parent = transform;
		camara.position = transform.position;
		if (Input.GetButton ("Fire2")) {

			Cursor.lockState = CursorLockMode.None;
		} else 
			Cursor.lockState = CursorLockMode.Locked;
		//camara.rotation = transform.rotation;
		var x = Input.GetAxis("Horizontal")*0.1f;
		var z = Input.GetAxis("Vertical")*0.1f;

		transform.Translate(x, 0, z);
		if (Input.GetKeyDown (KeyCode.Space)) {
			CmdFire ();
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
	void CmdSpawn() {



		for (int i= 1; i< 11;i++){
			for (int j= 1; j< 11;j++){
				GameObject foll = Instantiate (follower, transform.position + transform.right * 10f * i + transform.up * 10f * j, transform.rotation);
				NetworkServer.SpawnWithClientAuthority (foll, gameObject);
				//NetworkServer.Spawn(foll);
				foll.GetComponent<follower> ().setFlagship (transform,10f * i, 10f * j);
				flota.Add (foll);
				foll = Instantiate (follower, transform.position + transform.right * 10f * i + transform.up * 10f * -j, transform.rotation);
				NetworkServer.SpawnWithClientAuthority (foll, gameObject);
				//NetworkServer.Spawn(foll);
				foll.GetComponent<follower> ().setFlagship (transform,10f * i, 10f * -j);
				flota.Add (foll);
				foll = Instantiate (follower, transform.position + transform.right * 10f * -i + transform.up * 10f * j, transform.rotation);
				NetworkServer.SpawnWithClientAuthority (foll, gameObject);
				//NetworkServer.Spawn(foll);
				foll.GetComponent<follower> ().setFlagship (transform,10f * -i, 10f * j);
				flota.Add (foll);
				foll = Instantiate (follower, transform.position + transform.right * 10f * -i + transform.up * 10f * -j, transform.rotation);
				NetworkServer.SpawnWithClientAuthority (foll, gameObject);
				//NetworkServer.Spawn(foll);
				foll.GetComponent<follower> ().setFlagship (transform,10f * -i, 10f * -j);
				flota.Add (foll);
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
