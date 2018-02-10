using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownBehaviour : MonoBehaviour {
	public TownRessources ressources = new TownRessources (5,2,1,1);
	public TownRessourceBuildings building = new TownRessourceBuildings (3,3,4);
	public TownPopulation population = new TownPopulation (500,100,2);
	public string townname = "One";
	// Use this for initialization
	void Start () {
		StartCoroutine (usingAndProducingRessources ());
		disableAllTownCanvases ();
	}
	
	// Update is called once per frame
	void Update () {
		print(ressources.getRessource("Food").amount + " " + ressources.getRessource("Wood").amount);
		print(population.getLowerClass() + " " + population.getMiddleClass() + " " + population.getUpperClass() + " " +population.getTotalPopulation());
	}

	IEnumerator usingAndProducingRessources(){
		while (true) {
			produceRessources ();
			useRessources ();
			updateGUI ();
			yield return new WaitForSeconds (5.0f);
		}
	}

	void OnMouseDown(){
		print ("mouse hit village");
		if (gameObject.transform.FindChild ("Canvas").gameObject.activeSelf) {
			gameObject.transform.FindChild ("Canvas").gameObject.SetActive (false);
		} else {
			disableAllTownCanvases ();
			gameObject.transform.FindChild ("Canvas").gameObject.SetActive (true);

		}
	}

	void updateGUI(){

		//creating textboxes
		if (gameObject.transform.FindChild ("Canvas").transform.childCount < 3) {
			GameObject textbox = gameObject.transform.FindChild ("Canvas").transform.FindChild ("Townname").gameObject;
			GameObject button = gameObject.transform.FindChild ("Canvas").transform.FindChild ("BasicButton").gameObject;
			foreach (Ressource ressource in ressources.resource) {
				//ressorcebox
				GameObject ressourcBox = Instantiate (textbox, new Vector3 (textbox.transform.position.x, textbox.transform.position.y - 20, textbox.transform.position.z),textbox.transform.rotation,gameObject.transform.FindChild ("Canvas"));

				ressourcBox.name = ressource.name;
				textbox = ressourcBox;

				//button
				button = Instantiate (button, new Vector3 (button.transform.position.x, button.transform.position.y - 25, button.transform.position.z),button.transform.rotation,gameObject.transform.FindChild ("Canvas"));
				button.name = "LVLUp" + ressource.name;
				button.GetComponentInChildren<Text> ().text = "LVLUp" + ressource.name;

			}
			gameObject.transform.FindChild ("Canvas").transform.FindChild ("BasicButton").gameObject.SetActive (false);
			textbox = Instantiate (textbox, new Vector3 (textbox.transform.position.x, textbox.transform.position.y - 20, textbox.transform.position.z),textbox.transform.rotation,gameObject.transform.FindChild ("Canvas"));
			textbox.name = "Population";
		}


		//updating ressources
		gameObject.transform.FindChild ("Canvas").FindChild("Townname").GetComponent<Text>().text = townname;
		foreach (Ressource ressource in ressources.resource) {
			gameObject.transform.FindChild ("Canvas").FindChild (ressource.name).GetComponent<Text> ().text = ressource.name + ": " + ressource.amount;
		}
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

	private void useRessources(){
		string reason = ressources.useRessources (population);
		if (reason.Equals ("")) {
		} else {
			population.populationDecline (reason);
		}
	}

	private void produceRessources(){
		ressources.addRessource ("Food",building.foodProduction());
		ressources.addRessource ("Wood",building.woodProduction());
	}



	public void LVLUpProduction(string name){
		print (name);
	}
}
