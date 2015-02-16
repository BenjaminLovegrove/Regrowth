using UnityEngine;
using System.Collections;

public class SCR_Menu : MonoBehaviour {

	public Texture menuHeader;
	public Texture BGMcredit;
	public Transform centerobj;

	void Update () {

		if (Input.GetKey(KeyCode.Escape)){
			Application.Quit();
		}

		transform.LookAt (centerobj);
		transform.Translate(Vector3.right * 1 * Time.deltaTime);
		transform.LookAt (centerobj);


	}

	void OnGUI(){

		GUI.DrawTexture (new Rect (Screen.width * 0.5f, 0, 512, 256), menuHeader);
		//GUI.DrawTexture (new Rect (Screen.width * 0.9f, Screen.height * 0.9f, 128, 64), BGMcredit);

		if (GUI.Button(new Rect(Screen.width * 0.7f, Screen.height * 0.45f, 200, 50), "Play")){
			Application.LoadLevel ("Homeworld");
		}
		if (GUI.Button(new Rect(Screen.width * 0.7f, Screen.height * 0.555f, 200, 50), "Quit")){
			Application.Quit();
		}

	}
}
