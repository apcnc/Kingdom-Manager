using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownBehaviour : MonoBehaviour {
	public TownRessources ressources = new TownRessources (5,2,1,1);
	public TownPopulation population = new TownPopulation (500,100,2);
	public string townname = "One";

	// Use this for initialization
	void Start () {
		StartCoroutine (usingRessources ());

	}
	
	// Update is called once per frame
	void Update () {
		print(ressources.getFood() + " " + ressources.getWood() + " " +ressources.getStone()+ " " +ressources.getMetal());
		print(population.getLowerClass() + " " + population.getMiddleClass() + " " + population.getUpperClass() + " " +population.getTotalPopulation());
	}

	IEnumerator usingRessources(){
		while (true) {
			string reason = ressources.useRessources (population);
			if (reason.Equals ("")) {
			} else {
				population.populationDecline (reason);
			}
			updateGUI ();
			yield return new WaitForSeconds (5.0f);
		}
	}

	void OnMouseDown(){
		disableAllTownCanvases ();
		print ("mouse hit village");
		if (gameObject.transform.FindChild ("Canvas").gameObject.activeSelf) {
			gameObject.transform.FindChild ("Canvas").gameObject.SetActive (false);
		} else {gameObject.transform.FindChild ("Canvas").gameObject.SetActive (true);
		}
	}

	void updateGUI(){
		gameObject.transform.FindChild ("Canvas").FindChild("Townname").GetComponent<Text>().text = townname;
		gameObject.transform.FindChild ("Canvas").FindChild("Food").GetComponent<Text>().text = "Food: " + ressources.getFood();
		gameObject.transform.FindChild ("Canvas").FindChild("Wood").GetComponent<Text>().text = "Wood: " + ressources.getWood();
		gameObject.transform.FindChild ("Canvas").FindChild("Stone").GetComponent<Text>().text = "Stone: " + ressources.getStone();
		gameObject.transform.FindChild ("Canvas").FindChild("Metal").GetComponent<Text>().text = "Metal: " + ressources.getMetal();
		gameObject.transform.FindChild ("Canvas").FindChild("Population").GetComponent<Text>().text = "Population: " + population.getLowerClass() + "/" + population.getMiddleClass() + "/" + population.getUpperClass() + "(" + population.getTotalPopulation() + ")";

	}

	public void disableAllTownCanvases(){
		Canvas[] villageCanvases;
		villageCanvases = gameObject.transform.parent.GetComponentsInChildren<Canvas> ();
		foreach (Canvas canvas in villageCanvases) {
			if (canvas.tag != "MainOverlay") {
				canvas.gameObject.SetActive (false);
			}
		}
	}
}
