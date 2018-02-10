using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVLUpButton : MonoBehaviour {


	public void onClick(){
		gameObject.transform.parent.parent.GetComponent<TownBehaviour> ().LVLUpProduction (gameObject.name);
	}
}
