using UnityEngine;
using System.Collections;

public class PlanetCollider : MonoBehaviour {

	bool touchdown = false;

	void Update(){
		if (!touchdown) {
			SendMessageUpwards("Bump", SendMessageOptions.DontRequireReceiver);
		}
	}

	void OnCollisionEnter (Collision col) {
		print ("fgt");
		if (col.gameObject.tag == "Ground") {

			touchdown = true;
		}

	}
}
