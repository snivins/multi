using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class follower : NetworkBehaviour {
	public Transform flagship;
	private float x;
	private float y;
	// Use this for initialization
	void Start () {

		GetComponent<MeshRenderer> ().material.color = Color.blue;
	}
	
	// Update is called once per frame
	void Update () {

		//if (!Input.GetButton ("Fire2")) {
			transform.position = flagship.position + flagship.up * y + flagship.right* x;
			/* float distancia = Vector3.Distance ( flagship.position + flagship.up * y + flagship.right* x, transform.position);
			 * float speeeeeeed = Time.deltaTime * distancia;
			 * transform.position = Vector3.MoveTowards (transform.position,  flagship.position + flagship.up * y + flagship.right* x, speeeeeeed);
			*/

//}
		
		transform.rotation = flagship.rotation;
	}
	public void setFlagship(Transform flag, float der, float upo){
		flagship = flag;
		x = der;
		y = upo;

	}
}
