using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TownBehaviour : MonoBehaviour {
	TownRessources ressources = new TownRessources ();
	TownPopulation population = new TownPopulation (50,10,2);
	// Use this for initialization
	void Start () {
		StartCoroutine (usingRessources ());

	}
	
	// Update is called once per frame
	void Update () {
		print(ressources.getWood() + " " +ressources.getStone()+ " " +ressources.getMetal());
		print(population.getLowerClass() + " " + population.getMiddleClass() + " " + population.getUpperClass() + " " +population.getTotalPopulation());
	}

	IEnumerator usingRessources(){
		while (true) {
			string reason = ressources.useRessources (.1, .01, .01);
			if (reason.Equals ("")) {
			} else {
				population.populationDecline (reason);
			}
				
			yield return new WaitForSeconds (5.0f);
		}
	}
}
