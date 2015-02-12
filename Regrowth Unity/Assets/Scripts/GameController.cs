using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	RaycastHit mouseClick;
	public GameObject tree;
	public int playerSeeds = 3;
	float gameTime = 0;
	int treeCount;
	bool water = false;
	public int levelTrees = 18;
	public GUIStyle myGUItext;

	public Texture treeTex;
	public Texture seedTex;
	public Texture eBucketTex;
	public Texture fBucketTex;
	public Texture clockTex;
	Texture currentBucket;

	void Start () {
		currentBucket = eBucketTex;

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

		//Space - Water
		if (Input.GetKeyDown ("space")) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast(ray, out mouseClick, 1000)){
				if (mouseClick.collider.tag == "Tree" && water){
					mouseClick.collider.BroadcastMessage("Extinguish");
				} else if (mouseClick.collider.tag == "Water"){
					water = true;
					currentBucket = fBucketTex;
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
		GUI.DrawTexture (new Rect (Screen.width * .05f, Screen.height * .3f, 64, 64), seedTex);
		GUI.DrawTexture (new Rect (Screen.width * .05f, Screen.height * .4f, 64, 64), treeTex);
		GUI.DrawTexture (new Rect (Screen.width * .05f, Screen.height * .5f, 64, 64), clockTex);
		GUI.DrawTexture (new Rect (Screen.width * .05f, Screen.height * .6f, 64, 64), currentBucket);

		GUI.Label (new Rect (Screen.width * .1f, Screen.height * .32f, Screen.width, 20),playerSeeds.ToString(), myGUItext);
		GUI.Label (new Rect (Screen.width * .1f, Screen.height * .42f, Screen.width, 20),treeCount + "/" + levelTrees, myGUItext);
		GUI.Label (new Rect (Screen.width * .1f, Screen.height * .52f, Screen.width, 20),gameTime.ToString("0"), myGUItext);
		if (water) {
			GUI.Label (new Rect (Screen.width * .1f, Screen.height * .62f, Screen.width, 20), "Bucket full!", myGUItext);
		} else {
			GUI.Label (new Rect (Screen.width * .1f, Screen.height * .62f, Screen.width, 20), "Bucket empty!", myGUItext);
		}
	}


	//Message receive funtions for adding resources
	void AddTree(){
		treeCount ++;
	}

	void RemoveTree(){
		treeCount --;
	}

	void UsedWater(){
		currentBucket = eBucketTex;
		water = false;
	}
}
