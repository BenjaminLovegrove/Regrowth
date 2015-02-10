using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	RaycastHit mouseClick;
	public GameObject tree;


	void Start () {
	
	}

	void Update () {

		//Left click - seed pick up / placement
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast(ray, out mouseClick, 1000)){
				//if seeds > 0 instantiate seedling
				if (mouseClick.collider.tag == "Ground"){
					Instantiate (tree, mouseClick.point, Quaternion.identity);
				}
			}
		}

	}
}
