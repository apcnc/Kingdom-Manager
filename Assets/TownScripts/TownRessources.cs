using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownRessources{
	private double food;
	private double wood;
	private double stone;
	private double metal;
	public Ressource[] resource;
	public double foodusage;
	public double woodusage;
	public double stoneusage;
	public double metalusage;
	public TownRessources(double food = 0, double wood = 0, double stone = 0, double metal = 0){
		RessourceContainer ressources = RessourceContainer.Load ();
		resource = ressources.ressources;

		this.food = food;
		this.wood = wood;
		this.stone = stone;
		this.metal = metal;
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
