using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pew : MonoBehaviour {
	private LineRenderer laser;
	public GameObject chispas;
	// Use this for initialization
	void Start () {
		laser = GetComponent<LineRenderer> ();
		//Vector3[] posIniciales = new Vector3[2]; 
		//posIniciales[0] = new Vector3 (0f, 0f, 0f);
		//posIniciales[1] = new Vector3(0f,0f,0f);
		//laser.SetPositions (posIniciales);
		GameObject lalilulelo = Instantiate (chispas, transform.position, transform.rotation);
		chispas = lalilulelo;
		Destroy (chispas, 3f);
		chispas.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		laser.SetPosition (0, transform.position);	
		laser.startWidth = Random.Range(0.5f,1.5f);
		laser.endWidth =  Random.Range(0.5f,1.5f);
		Ray ray = new Ray (transform.position, transform.forward*500f); //Camera.main.ScreenPointToRay (Input.mousePosition).direction);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, 5000f)) {
			if (hit.rigidbody) {	
				chispas.SetActive (true);
				chispas.transform.position = hit.point;

				chispas.transform.rotation = hit.transform.rotation;//Quaternion.Euler( hit.normal);
			} 
			laser.SetPosition (1, hit.point);	
			//Debug.Log (ray.GetPoint (4500f));
		} else {
			chispas.SetActive (false);
			laser.SetPosition (1, transform.forward*5000f);
		}


	}
}
