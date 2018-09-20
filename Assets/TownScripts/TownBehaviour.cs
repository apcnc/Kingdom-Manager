using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownBehaviour : MonoBehaviour {
	public TownRessources ressources;
	public TownRessourceBuildings building = new TownRessourceBuildings ();
	public TownPopulation population = new TownPopulation (5,1,0);
	public string townname = "One";
	public GameObject merchants;
	// Use this for initialization
	void Start () {
		ressources = new TownRessources (gameObject.name + ".xml");
		StartCoroutine (usingAndProducingRessources ());
		StartCoroutine (merchantChecks());
		disableAllTownCanvases ();
	}
	
	// Update is called once per frame
	void Update () {
		print(ressources.getRessource("Food").productionLVL + " " + ressources.getRessource("Wood").productionLVL);
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

	IEnumerator merchantChecks(){
		merchants = transform.parent.parent.FindChild ("Merchants").gameObject;
		print (merchants);
		while (true) {
			for(int i = 0;i<merchants.GetComponentsInChildren<Transform>().Length;i++){
				print (merchants.GetComponentsInChildren<Transform> ().Length);
				checkAndSendMerchant ();
			}
			yield return new WaitForSeconds (2.0f);
		}
	}

	//activating/deactivating the town-overlay
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
		foreach (Ressource ressource in ressources.resource) {
			ressources.addRessource (ressource.name, building.ressourceProduction (ressource));
		}
	}



	public void LVLUpProduction(string name){
		print (name);
		name = name.Replace ("LVLUp","");
		bool canLVL = true;
		string[] ressourceNames = null;
		string[] neededRessourceAmount = null;
		Ressource ressource = ressources.getRessource (name);
		switch (ressource.productionLVL+1){
		case 1: 
			ressourceNames = ressource.LVL1ressource.Split ('/');
			neededRessourceAmount = ressource.LVL1requierments.Split ('/');
			break;
		case 2: 
			ressourceNames = ressource.LVL2ressource.Split ('/');
			neededRessourceAmount = ressource.LVL2requierments.Split ('/');
			break;
		case 3: 
			ressourceNames = ressource.LVL3ressource.Split ('/');
			neededRessourceAmount = ressource.LVL3requierments.Split ('/');
			break;
		}

		for(int i = 0;i<ressourceNames.Length;i++) {
			if (System.Int32.Parse(neededRessourceAmount [i]) > ressources.getRessource (ressourceNames[i]).amount) {
				canLVL = false;
			}
		}
		if (canLVL) {
			for(int i = 0;i<ressourceNames.Length;i++) {
				ressources.addRessource (ressourceNames [i], -System.Int32.Parse (neededRessourceAmount [i]));
			
			}
			ressources.addProductionLVL (name);
			updateGUI ();
		}

	}

	private void checkAndSendMerchant (){
		TownBehaviour[] towns = gameObject.transform.parent.GetComponentsInChildren<TownBehaviour> ();
		;
		foreach (Ressource ressource in ressources.resource) {
			if (ressource.amount > population.getTotalPopulation ()) {
				foreach (TownBehaviour town in towns) {
					if (town.ressources.getRessource (ressource.name).amount < town.population.getTotalPopulation ()) {
						int maxAmount =  (int)(town.population.getTotalPopulation () - town.ressources.getRessource (ressource.name).amount);
						if (maxAmount > ressource.amount - population.getTotalPopulation ()) {
							maxAmount = (int)(ressource.amount - population.getTotalPopulation ());
						}
						sendMerchant (town, maxAmount);
					}
					print (town.gameObject.GetComponent<TownBehaviour> ().name);
				}
			}
		}
	}

	private void sendMerchant(TownBehaviour town, int maxAmount){
		
	}
}
