using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownRessources{
	private double food;
	private double wood;
	private double stone;
	private double metal;
	public double foodusage;
	public double woodusage;
	public double stoneusage;
	public double metalusage;
	public TownRessources(double food = 0, double wood = 0, double stone = 0, double metal = 0){
		this.food = food;
		this.wood = wood;
		this.stone = stone;
		this.metal = metal;
	}

	//getter + setter
	public double getFood(){
		return food;
	}
	public double getWood(){
		return wood;
	}
	public double getStone(){
		return stone;
	}
	public double getMetal(){
		return metal;
	}

	public void setWood(double wood){
		this.wood = wood;
	}
	public void setStone(double stone){
		this.stone = stone;
	}
	public void setMetal(double metal){
		this.metal = metal;
	}



	//ressource Usage
	public string useRessources(TownPopulation population){

		foodusage = population.getTotalPopulation() * 0.001;
		woodusage = population.getLowerClass() * 0.0001 + population.getMiddleClass() * 0.001 + population.getUpperClass() * 0.01;
		stoneusage = population.getLowerClass() * 0.0001 + population.getMiddleClass() * 0.001 + population.getUpperClass() * 0.01;
		metalusage = population.getLowerClass() * 0.0001 + population.getMiddleClass() * 0.001 + population.getUpperClass() * 0.01;
		this.food -= foodusage;
		if (this.food <= 0) {
			this.food = 0;
			return"food";
		}
		this.wood -= woodusage;
		if (this.wood <= 0) {
			this.wood = 0;
			return"wood";
		}
		this.stone -= stoneusage;
		if (this.stone <= 0) {
			this.stone = 0;
			return"stone";
		}
		this.metal -= metalusage;
		if (this.metal <= 0) {
			this.metal = 0;
			return"metal";
		}
		return "";
	}
}
