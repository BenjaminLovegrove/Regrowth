using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SproutTree : MonoBehaviour {

	Transform treeGraph;
	Transform sproutGraph;
	public float growTimer = 999f;
	bool grown = false;
	int danger = 0;
	float lifetimer = 4;

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
			if (danger < 0){
				danger = 0;
			}

			if (danger > 0){
				lifetimer -= Time.deltaTime;
			} else {
				lifetimer = 4;
			}


			if (lifetimer <= 0){
				Destroy(this.gameObject);
			}
		}
	}


	void Dying(){
		danger ++;
		if (danger > 0) {
			this.BroadcastMessage("DyingMat");
		}
	}

	void Living(){
		danger --;
		if (danger <= 0) {
			this.BroadcastMessage("LivingMat");
		}
	}

	void Grow (){
		sproutGraph.renderer.enabled = false;
		treeGraph.renderer.enabled = true;
		Camera.main.SendMessage ("AddTree");
	}
}
