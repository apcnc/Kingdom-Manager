using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownRessources{
	public Ressource[] resource;
	public double foodusage;
	public double woodusage;
	public double stoneusage;
	public double metalusage;
	public TownRessources(string filename){
		RessourceContainer ressources = RessourceContainer.Load (filename);
		resource = ressources.ressources;

	}

	//getter + setter
	public Ressource getRessource(string name){
		foreach (Ressource ressource in resource) {
			if (ressource.name.Equals (name)) {
				return ressource;
			}
		}
		return null;
	}

	public void addRessource(string name, double amount){
		foreach (Ressource ressource in resource) {
			if (ressource.name.Equals (name)) {
				ressource.amount += amount;
			}
		}
	}

	public void addProductionLVL(string name)  {
		foreach (Ressource ressource in resource) {
			if (ressource.name.Equals (name)) {
				ressource.productionLVL++;
			}
		}
	}




	//ressource Usage
	public string useRessources(TownPopulation population){
		string ressourceShortages = "";
		foreach (Ressource ressource in resource) {
			if (ressource.name.Equals ("Food")) {
				ressource.usage = population.getTotalPopulation () * 0.001;
			} else {
				ressource.usage = woodusage = population.getLowerClass () * 0.0001 + population.getMiddleClass () * 0.001 + population.getUpperClass () * 0.01;
			}
			if (ressource.usage < ressource.amount) {
				ressource.amount -= ressource.usage;
			} else {
				ressource.amount = 0;
				ressourceShortages += ressource.name;
			}
		}
		return ressourceShortages;
	}
}
