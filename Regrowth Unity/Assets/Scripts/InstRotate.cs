using UnityEngine;
using System.Collections;

public class InstRotate : MonoBehaviour {

	public Transform centerobj;
	
	void Start () {
		transform.LookAt (centerobj);
	}

}
