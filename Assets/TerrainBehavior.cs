using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerrainBehavior : MonoBehaviour {
	private double[] totalRessources;
	void Start () {
		StartCoroutine (updateMainOverlay ());

	}
	void OnMouseDown(){
		print ("mouse hit terrain");
		Canvas[] villageCanvases;
		villageCanvases = gameObject.GetComponentsInChildren<Canvas> ();
		foreach (Canvas canvas in villageCanvases) {
			if (canvas.tag != "MainOverlay") {
				//canvas.gameObject.SetActive (false);
			}
		}
	}

	IEnumerator updateMainOverlay(){
		yield return new WaitForSeconds (0.2f);
		while (true) {
			
			int totalLCPop = 0;
			int totalMCPop = 0;
			int totalUCPop = 0;
			int totalPop = 0;
			TownBehaviour[] towns;
			towns = gameObject.GetComponentsInChildren<TownBehaviour> ();

			totalRessources = new double[towns[0].ressources.resource.Length];
			for(int ii = 0;ii<totalRessources.Length;ii++){
				totalRessources [ii] = 0;
			}

			foreach (TownBehaviour town in towns) {
				if (gameObject.transform.FindChild ("Canvas").transform.childCount < 2) {
					
					GameObject textbox = gameObject.transform.FindChild ("Canvas").transform.FindChild ("Total").gameObject;
					foreach (Ressource ressource in town.ressources.resource) {
						GameObject ressourcBox = Instantiate (textbox, new Vector3 (textbox.transform.position.x+80, textbox.transform.position.y, textbox.transform.position.z),textbox.transform.rotation,gameObject.transform.FindChild ("Canvas"));
					
						ressourcBox.name = ressource.name;
						textbox = ressourcBox;
					}
					textbox = Instantiate (textbox, new Vector3 (textbox.transform.position.x+80, textbox.transform.position.y, textbox.transform.position.z),textbox.transform.rotation,gameObject.transform.FindChild ("Canvas"));
					textbox.name = "Population";
				}
				int i = 0;
				foreach (Ressource ressource in town.ressources.resource) {
					totalRessources [i] += ressource.amount;
					i++;
				}

				totalLCPop += town.population.getLowerClass ();
				totalMCPop += town.population.getMiddleClass ();
				totalUCPop += town.population.getUpperClass ();
				totalPop += town.population.getTotalPopulation ();
			}
			int iii =0;
			foreach(Ressource ressource in towns[0].ressources.resource){
				totalRessources [iii] = roundToPointOne(totalRessources [iii]);

				gameObject.transform.FindChild ("Canvas").FindChild (ressource.name).GetComponent<Text> ().text = ressource.name + ": " + totalRessources [iii];
				iii++;
			}
			/*totalFood = roundToPointOne (totalFood);
			totalWood = roundToPointOne (totalWood);
			totalStone = roundToPointOne (totalStone);
			totalMetal = roundToPointOne (totalMetal);

			gameObject.transform.FindChild ("Canvas").FindChild ("TotalFood").GetComponent<Text> ().text = "Total Food: " + totalFood;
			gameObject.transform.FindChild ("Canvas").FindChild ("TotalWood").GetComponent<Text> ().text = "Total Wood: " + totalWood;
			gameObject.transform.FindChild ("Canvas").FindChild ("TotalStone").GetComponent<Text> ().text = "Total Stone: " + totalStone;
			gameObject.transform.FindChild ("Canvas").FindChild ("TotalMetal").GetComponent<Text> ().text = "Total Metal: " + totalMetal;*/
			gameObject.transform.FindChild ("Canvas").FindChild ("Population").GetComponent<Text> ().text = "Population: " + totalLCPop + "/" + totalMCPop + "/" + totalUCPop + "(" + totalPop + ")";
			yield return new WaitForSeconds (0.2f);
		}
	}

	private double roundToPointOne(double number){
		number *= 10;
		number = (int)number;
		number /= 10;
		return number;
	}
}
