using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	RaycastHit mouseClick;
	public GameObject tree;
	public int playerSeeds = 3;
	float gameTime = 0;


	void Start () {

	}

	void Update () {

		gameTime += Time.deltaTime;

		//Left click - seed pick up / placement
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast(ray, out mouseClick, 1000)){
				//if seeds > 0 instantiate seedling
				if (mouseClick.collider.tag == "Ground"){
					Instantiate (tree, mouseClick.point, Quaternion.identity);
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

	void OnGUI(){

	}
}
