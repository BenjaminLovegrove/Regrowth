using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	RaycastHit mouseClick;
	public GameObject tree;
	public int playerSeeds = 3;
	float gameTime = 0;
	int treeCount;
	public int levelTrees = 30;
	public GUIStyle myGUItext;

	void Start () {
		myGUItext = new GUIStyle ();
		myGUItext.fontSize = 20;
	}

	void Update () {

		gameTime += Time.deltaTime;

		//Left click - seed pick up / placement
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast(ray, out mouseClick, 1000)){
				if (mouseClick.collider.tag == "Ground" && playerSeeds > 0){
					Instantiate (tree, mouseClick.point, Quaternion.identity);
					playerSeeds --;
				} else if (mouseClick.collider.tag == "Seed"){
					playerSeeds ++;
					Destroy (mouseClick.collider.gameObject);
				}
			}
		}

		//Rick click - remove weeds
		if (Input.GetMouseButtonDown (1)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast(ray, out mouseClick, 1000)){
				if (mouseClick.collider.tag == "Vine"){
					mouseClick.collider.BroadcastMessage("VineClick");
				}
			}
		}

	}

	//Temp UI
	void OnGUI(){
		GUI.Label (new Rect (Screen.width * .05f, Screen.height * .45f, Screen.width, 20),"Seed Count: " + playerSeeds, myGUItext);
		GUI.Label (new Rect (Screen.width * .05f, Screen.height * .5f, Screen.width, 20), "Mature Trees: " + treeCount + "/" + levelTrees, myGUItext);
		GUI.Label (new Rect (Screen.width * .05f, Screen.height * .55f, Screen.width, 20), "Game Time: " + gameTime.ToString("0"), myGUItext);
	}


	//Message receive funtions for adding resources
	void AddTree(){
		treeCount ++;
	}

	void RemoveTree(){
		treeCount --;
	}
}
