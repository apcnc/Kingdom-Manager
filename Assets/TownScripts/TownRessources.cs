using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownRessources{
	private double wood;
	private double stone;
	private double metal;

	public TownRessources(){
	}

	//getter + setter
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
	public string useRessources(double wood = 0, double stone = 0, double metal= 0){
		this.wood -= wood;
		this.stone -= stone;
		this.metal -= metal;
		return "";
	}
}
