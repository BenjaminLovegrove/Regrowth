using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SproutTree : MonoBehaviour {

	Transform treeGraph;
	Transform sproutGraph;
	public float growTimer = 999f;
	bool grown = false;
	int danger = 0;
	float lifetimer = 8;
	bool burning = false;
	public GameObject[] trees;
	float wet;

	Transform fireGraph1;
	Transform fireGraph2;
	Transform fireGraph3;
	Transform fireGraph4;
	Transform fireGraph5;
	int fireRNG;

	void Start () {
		treeGraph = transform.Find ("Graphics");
		sproutGraph = transform.Find ("Sprout");
		fireGraph1 = transform.Find ("FlamingTree/Fire");
		fireGraph2 = transform.Find ("FlamingTree/Tree");
		fireGraph3 = transform.Find ("FlamingTree/Fire.001");
		fireGraph4 = transform.Find ("FlamingTree/Fire.002");
		fireGraph5 = transform.Find ("FlamingTree/Fire.003");


		growTimer = Random.Range (25, 50);
	}
	
	void Update () {
		//Make tree grow up
		growTimer -= Time.deltaTime;
		wet -= Time.deltaTime; // Wet = immune to fire

		if (growTimer <= 0 && grown == false) {
			Grow();
			grown = true;
		}

		//When tree is grown
		if (grown == true) {
			//Do tree stuff

			//Burst into flames
			fireRNG = Random.Range (0,10000);
			if (fireRNG < 500 && burning == false && wet <= 0){
				OnFire();
			}

			//If tree is dying from vines
			if (danger < 0){
				danger = 0;
			}
			// If vines nearby lower life time, else reset life time
			if (danger > 0){
				lifetimer -= Time.deltaTime;
			} else {
				lifetimer = 8;
				Living ();
			}


			if (lifetimer <= 0){
				Destroy(this.gameObject);
				Camera.main.SendMessage("RemoveTree");
			}

			if (burning){
				Burning();
			}
		}
	}

	void OnFire(){
		danger ++;
		burning = true;
		treeGraph.renderer.enabled = false;
		fireGraph1.renderer.enabled = true;
		fireGraph2.renderer.enabled = true;
		fireGraph3.renderer.enabled = true;
		fireGraph4.renderer.enabled = true;
		fireGraph5.renderer.enabled = true;
	}

	void Burning(){
		trees = GameObject.FindGameObjectsWithTag("Tree");
		foreach (GameObject tree in trees){
			if (tree.gameObject.GetInstanceID() != GetInstanceID()){
				if (Vector3.Distance(this.transform.position, tree.transform.position) < 2f){
					tree.SendMessage("FlameJump", SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}

	void FlameJump(){
		if (wet <= 0 && !burning) {
			OnFire ();
		}
	}

	void Extinguish(){
		if (burning) {
			Camera.main.SendMessage("UsedWater");
			wet = 10;
			danger --;
			burning = false;
			treeGraph.renderer.enabled = true;
			fireGraph1.renderer.enabled = false;
			fireGraph2.renderer.enabled = false;
			fireGraph3.renderer.enabled = false;
			fireGraph4.renderer.enabled = false;
			fireGraph5.renderer.enabled = false;
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
