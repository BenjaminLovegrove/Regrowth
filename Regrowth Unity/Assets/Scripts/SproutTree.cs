using UnityEngine;
using System.Collections;

public class SproutTree : MonoBehaviour {

	Transform treeGraph;
	Transform sproutGraph;
	public float growTimer = 999f;
	bool sprout = true;
	bool grown = false;

	void Start () {
		treeGraph = transform.Find ("Graphics");
		sproutGraph = transform.Find ("Sprout");
		growTimer = Random.Range (25, 50);
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
	}

	void Grow (){
		sproutGraph.renderer.enabled = false;
		treeGraph.renderer.enabled = true;
	}
}
