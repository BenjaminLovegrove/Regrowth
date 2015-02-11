using UnityEngine;
using System.Collections;

public class Vine : MonoBehaviour {

	bool vactive = true;
	float vineTimer = 0f;
	Transform vineGraphics;
	Transform dugVine;
	Transform seedSpawn;
	int seedRNG;
	public GameObject seed;

	void Start () {
		vineGraphics = transform.Find ("Graphics");
		dugVine = transform.Find ("VineDug");
		seedSpawn = transform.Find ("SeedSpawn");
	}

	void VineClick () {
	
		if (vactive == true) {
			vineGraphics.renderer.enabled = false;
			dugVine.renderer.enabled = true;
			vactive = false;
			vineTimer = Random.Range(10,40);

			seedRNG = Random.Range (0,4);
			if (seedRNG <= 1){
				Instantiate (seed, seedSpawn.position, Quaternion.identity);
			}
		}

	}

	void Update () {
	
		vineTimer -= Time.deltaTime;

		if (vineTimer <= 0 && vactive == false) {
			dugVine.renderer.enabled = false;
			vineGraphics.renderer.enabled = true;
			vactive = true;
		}

	}
}
