using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class follower : NetworkBehaviour {
	[SyncVar]
	public Transform flagship;
	[SyncVar]
	public Vector2 xy;
	[SyncVar]
	public bool alive = true;
	[SyncVar]
	public string namerino;
	[SyncVar]
	public int iderino;
	// Use this for initialization
	void Start () {

		GetComponent<MeshRenderer> ().material.color = Color.blue;
		//Debug.Log (x + ", " + y);
	}
	// Update is called once per frame
	void Update () {
		/*
		if (flagship == null ) {
			Debug.Log (isServer);
			flagship = GameObject.FindGameObjectsWithTag ("Player") [GameObject.FindGameObjectsWithTag ("Player").Length - 1].transform;
			x = flagship.position.x - transform.position.x;
			y = flagship.position.y - transform.position.y;
		}*/
		//if (flagship != null) {
			/*float distancia = Vector3.Distance ( flagship.position + flagship.up * y + flagship.right* x, transform.position);
			float speeeeeeed = Time.deltaTime * distancia;
			transform.position = Vector3.MoveTowards (transform.position,  flagship.position + flagship.up * y + flagship.right* x, speeeeeeed);*/
		//}
		//if (isLocalPlayer) {
		if (isServer && alive) {/*
			transform.position = flagship.position + flagship.up * xy.y + flagship.right * xy.x;
			*/
			float distancia = Vector3.Distance ( flagship.position + flagship.up * xy.y + flagship.right* xy.x, transform.position);
			float speeeeeeed = Time.deltaTime * distancia;
			transform.position = Vector3.MoveTowards (transform.position,  flagship.position + flagship.up * xy.y + flagship.right* xy.x, speeeeeeed);
			transform.rotation = flagship.GetChild(0).rotation;
		}
	}
	public void setFlagship(Transform flag, float der, float upo, string nme, int ide){
		flagship = flag;
		xy.x = der;
		xy.y = upo; 
		namerino = nme;
		iderino = ide;
	}
}
