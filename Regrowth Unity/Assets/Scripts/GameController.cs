using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	RaycastHit mouseClick;
	public GameObject tree;
	public int playerSeeds = 3;
	float gameTime = 0;
	int treeCount;
	bool water = false;
	public int levelTrees = 20;
	public GUIStyle myGUItext;
	public GUIStyle myGUItextLge;
	public Font BauHaus;

	public Texture instructionsTex;
	public Texture instructionsTex1;
	public Texture instructionsTex2;
	public Texture instructionsTex3;
	public Texture instructionsTex4;
	public Texture instructionsTex5;


	public Texture treeTex;
	public Texture seedTex;
	public Texture eBucketTex;
	public Texture fBucketTex;
	public Texture clockTex;
	Texture currentBucket;

	public Texture winTex;
	public float winDelay = 6;
	public bool win = false;

	public float SFXvol = 0.3f;
	public AudioClip extinguish;
	public AudioClip dig;
	public AudioClip plant;
	public AudioClip seedpu;
	public AudioClip waterscoop;
	public AudioClip Bop;
	public AudioClip WinDing;
	
	int instructions = 0;

	void Start () {
		currentBucket = eBucketTex;

		myGUItext = new GUIStyle ();
		myGUItext.fontSize = 30;
		myGUItext.font = BauHaus;

		myGUItextLge = new GUIStyle ();
		myGUItextLge.fontSize = 45;
		myGUItextLge.font = BauHaus;
	}

	void Update () {

		if (Input.GetKey(KeyCode.Escape)){
			Application.LoadLevel("Menu");
		}

		if (treeCount >= levelTrees && !win) {
			audio.PlayOneShot (WinDing, SFXvol);
			win = true;
		}

		if (win == true) {
			winDelay -= Time.deltaTime;
			if (winDelay <= 0){
				Application.LoadLevel("Menu");
			}
		} else if (instructions <=5) {
			if (Input.GetMouseButtonDown (0)) {
				instructions ++;
				audio.PlayOneShot (Bop, SFXvol);
			}

		} else if (instructions > 5) {

				gameTime += Time.deltaTime;

				//Left click - seed pick up / placement
				if (Input.GetMouseButtonDown (0)) {
					Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
					if (Physics.Raycast (ray, out mouseClick, 1000)) {
						if (mouseClick.collider.tag == "Ground" && playerSeeds > 0) {
							Instantiate (tree, mouseClick.point, Quaternion.identity);
							playerSeeds --;
							audio.PlayOneShot (plant, SFXvol);
						} else if (mouseClick.collider.tag == "Seed") {
							audio.PlayOneShot (seedpu, SFXvol);
							playerSeeds ++;
							Destroy (mouseClick.collider.gameObject);
						}
					}
				}

				//Space - Water
				if (Input.GetKeyDown ("space")) {
					Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
					if (Physics.Raycast (ray, out mouseClick, 1000)) {
						if (mouseClick.collider.tag == "Tree" && water) {
							mouseClick.collider.BroadcastMessage ("Extinguish");
						} else if (mouseClick.collider.tag == "Water") {
							if (!water) {
								water = true;
								currentBucket = fBucketTex;
								audio.PlayOneShot (waterscoop, SFXvol);
							}
						}
					}
				}

				//Rick click - remove weeds
				if (Input.GetMouseButtonDown (1)) {
					Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
					if (Physics.Raycast (ray, out mouseClick, 1000)) {
						if (mouseClick.collider.tag == "Vine") {
							mouseClick.collider.BroadcastMessage ("VineClick");
							audio.PlayOneShot (dig, SFXvol);
						}
					}
				}

		}

	}

	//Temp UI
	void OnGUI(){

		if (win) {
			GUI.DrawTexture (new Rect (Screen.width / 2 - 512, Screen.height / 2 - 256, 1024, 512), winTex);
		} else if (instructions <= 5) {

			if (instructions == 0){
				GUI.DrawTexture (new Rect (Screen.width / 2 - 512, Screen.height / 2 - 256, 1024, 512), instructionsTex);
			} else if (instructions == 1){
				GUI.DrawTexture (new Rect (Screen.width / 2 - 512, Screen.height / 2 - 256, 1024, 512), instructionsTex1);
			} else if (instructions == 2){
				GUI.DrawTexture (new Rect (Screen.width / 2 - 512, Screen.height / 2 - 256, 1024, 512), instructionsTex2);
			} else if (instructions == 3){
				GUI.DrawTexture (new Rect (Screen.width / 2 - 512, Screen.height / 2 - 256, 1024, 512), instructionsTex3);
			} else if (instructions == 4){
				GUI.DrawTexture (new Rect (Screen.width / 2 - 512, Screen.height / 2 - 256, 1024, 512), instructionsTex4);
			} else if (instructions == 5){
				GUI.DrawTexture (new Rect (Screen.width / 2 - 512, Screen.height / 2 - 256, 1024, 512), instructionsTex5);
			}

		} else {

			GUI.DrawTexture (new Rect (Screen.width * .05f, Screen.height * .2f - 64, 128, 128), treeTex);

			GUI.DrawTexture (new Rect (Screen.width * .05f, Screen.height * .5f - 24, 64, 64), seedTex);
			GUI.DrawTexture (new Rect (Screen.width * .05f, Screen.height * .6f - 24, 64, 64), clockTex);
			GUI.DrawTexture (new Rect (Screen.width * .05f, Screen.height * .7f - 24, 64, 64), currentBucket);

			GUI.Label (new Rect (Screen.width * .15f, Screen.height * .2f, Screen.width, 20), treeCount + "/" + levelTrees, myGUItextLge);

			GUI.Label (new Rect (Screen.width * .1f, Screen.height * .5f, Screen.width, 20), playerSeeds.ToString (), myGUItext);
			GUI.Label (new Rect (Screen.width * .1f, Screen.height * .6f, Screen.width, 20), gameTime.ToString ("0"), myGUItext);
			if (water) {
				GUI.Label (new Rect (Screen.width * .1f, Screen.height * .7f, Screen.width, 20), "Bucket full!", myGUItext);
			} else {
				GUI.Label (new Rect (Screen.width * .1f, Screen.height * .7f, Screen.width, 20), "Bucket empty!", myGUItext);
			}

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
		audio.PlayOneShot(extinguish,SFXvol);
	}
}
