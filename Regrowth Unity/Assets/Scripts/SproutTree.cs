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

		if (growTimer <= 0 && grown == false) {
			Grow();
			grown = true;
		}

		//When tree is grown
		if (grown == true) {
			//Do tree stuff

			//Burst into flames
			fireRNG = Random.Range (0,10000);
			if (fireRNG < 500 && burning == false){
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
			}


			if (lifetimer <= 0){
				Destroy(this.gameObject);
				Camera.main.SendMessage("RemoveTree");
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
