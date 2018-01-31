using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CamCon : NetworkBehaviour {
	public Transform focus;
	public float camSpeed;
	public GameObject camara;
	public GameObject camaraPitch;
	public GameObject camaraYaw;
	// Use this for initialization
	void Start () {
		camSpeed = 50f;
	}
	// Update is called once per frame
	void Update () {
		

		if (focus != null) {
			float y = camSpeed * Input.GetAxis ("Mouse X");
			float x = camSpeed * Input.GetAxis ("Mouse Y");
			camaraYaw.transform.Rotate(new Vector3 (0.0f, y,0.0f), Time.deltaTime * camSpeed,Space.Self);
			camaraPitch.transform.Rotate(new Vector3 (-x,0.0f, 0.0f), Time.deltaTime * camSpeed,Space.Self);
			if (Input.GetAxis ("Mouse ScrollWheel") != 0.0f) {
				float zoom = Input.GetAxis("Mouse ScrollWheel");
				camara.transform.Translate(new Vector3 (0.0f, 0.0f, zoom) * Time.deltaTime * 1000,Space.Self);
			}
		} else {
			if (GameObject.FindGameObjectWithTag ("Player") != null) {
				focus = GameObject.FindGameObjectWithTag ("Player").transform;
				transform.position = focus.position;
				//transform.SetParent (focus);



			}
		}

	}
}



