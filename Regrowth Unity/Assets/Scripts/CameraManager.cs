using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	public Transform centerobj;
	public float camspeed = 75f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//Rotate Camera Around World
		transform.LookAt (centerobj);

		if (Input.GetKey (KeyCode.W) && transform.position.y < 12.5f) {
			transform.Translate(Vector3.up * camspeed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.A)) {
			transform.Translate(Vector3.left * camspeed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.S) && transform.position.y > -12.5f) {
			transform.Translate(Vector3.down * camspeed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.D)) {
			transform.Translate(Vector3.right * camspeed * Time.deltaTime);
		}

		transform.LookAt (centerobj);

	}


}
