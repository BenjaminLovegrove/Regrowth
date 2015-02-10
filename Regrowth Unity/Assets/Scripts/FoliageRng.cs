using UnityEngine;
using System.Collections;

public class FoliageRng : MonoBehaviour {

	public Material foliage;
	public Material foliage1;
	public Material foliage2;
	public Material foliage3;
	Material chosenMat;
	int rnd;
	Material[] mats;
	
	void Start () {

		rnd = Random.Range (0, 3);

		if (rnd == 0) {
			chosenMat = foliage;
		} else if (rnd == 1) {
			chosenMat = foliage1;
		} else if (rnd == 2) {
			chosenMat = foliage2;
		} else if (rnd == 3) {
			chosenMat = foliage3;
		}

		mats = renderer.materials;
		mats[0] = chosenMat;
		renderer.materials = mats;

	}

}
