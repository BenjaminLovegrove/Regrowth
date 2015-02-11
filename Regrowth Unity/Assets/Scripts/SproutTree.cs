using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SproutTree : MonoBehaviour {

	Transform treeGraph;
	Transform sproutGraph;
	public float growTimer = 999f;
	bool grown = false;
	public GameObject[] vines;

	void Start () {
		treeGraph = transform.Find ("Graphics");
		sproutGraph = transform.Find ("Sprout");
		growTimer = Random.Range (25, 50);
		vines = GameObject.FindGameObjectsWithTag ("Vine");
	}
	
	void Update () {
		growTimer -= Time.deltaTime;

		if (growTimer <= 0 && grown == false) {
			Grow();
			grown = true;
		}

		if (grown == true) {
			//Do tree stuff
		}

		foreach (GameObject vine in vines) {
			if (Vector3.Distance(vine.transform.position, this.transform.position) < 5){

			}
		}
	}

	void Grow (){
		sproutGraph.renderer.enabled = false;
		treeGraph.renderer.enabled = true;
		Camera.main.SendMessage ("AddTree");
	}
}
