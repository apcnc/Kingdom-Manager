using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerrainBehavior : MonoBehaviour {
	
	void Start () {
		StartCoroutine (updateMainOverlay ());

	}
	void OnMouseDown(){
		print ("mouse hit terrain");
		Canvas[] villageCanvases;
		villageCanvases = gameObject.GetComponentsInChildren<Canvas> ();
		foreach (Canvas canvas in villageCanvases) {
			if (canvas.tag != "MainOverlay") {
				canvas.gameObject.SetActive (false);
			}
		}
	}

	IEnumerator updateMainOverlay(){
		while (true) {
			double totalFood = 0;
			double totalWood = 0;
			double totalStone = 0;
			double totalMetal = 0;
			int totalLCPop = 0;
			int totalMCPop = 0;
			int totalUCPop = 0;
			int totalPop = 0;
			TownBehaviour[] towns;
			towns = gameObject.GetComponentsInChildren<TownBehaviour> ();
			foreach (TownBehaviour town in towns) {
				totalFood += town.ressources.getFood ();
				totalWood += town.ressources.getWood ();
				totalStone += town.ressources.getStone ();
				totalMetal += town.ressources.getMetal ();
				totalLCPop += town.population.getLowerClass ();
				totalMCPop += town.population.getMiddleClass ();
				totalUCPop += town.population.getUpperClass ();
				totalPop += town.population.getTotalPopulation ();
			}

			totalFood = roundToPointOne (totalFood);
			totalWood = roundToPointOne (totalWood);
			totalStone = roundToPointOne (totalStone);
			totalMetal = roundToPointOne (totalMetal);

			gameObject.transform.FindChild ("Canvas").FindChild ("TotalFood").GetComponent<Text> ().text = "Total Food: " + totalFood;
			gameObject.transform.FindChild ("Canvas").FindChild ("TotalWood").GetComponent<Text> ().text = "Total Wood: " + totalWood;
			gameObject.transform.FindChild ("Canvas").FindChild ("TotalStone").GetComponent<Text> ().text = "Total Stone: " + totalStone;
			gameObject.transform.FindChild ("Canvas").FindChild ("TotalMetal").GetComponent<Text> ().text = "Total Metal: " + totalMetal;
			gameObject.transform.FindChild ("Canvas").FindChild ("TotalPopulation").GetComponent<Text> ().text = "Total Population: " + totalLCPop + "/" + totalMCPop + "/" + totalUCPop + "(" + totalPop + ")";
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
