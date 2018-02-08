using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownPopulation{
	private int lowerClass;
	private int middleClass;
	private int upperClass;
	public TownPopulation(int lowerClass = 0, int middleClass = 0, int upperClass = 0){
		this.lowerClass = lowerClass;
		this.middleClass = middleClass;
		this.upperClass = upperClass;
	}

	//getter + setter
	public int getLowerClass(){
		return lowerClass;
	}

	public int getMiddleClass(){
		return middleClass;
	}

	public int getUpperClass(){
		return upperClass;
	}

	public int getTotalPopulation(){
		return lowerClass + middleClass + upperClass;
	}

	public void setLowerClass(int lowerClass){
		this.lowerClass = lowerClass;
	}

	public void setMiddleClass(int middleClass){
		this.middleClass = middleClass;
	}

	public void setUpperClass(int upperClass){
		this.upperClass = upperClass;
	}



	//Interactive Methods
	public void populationDecline(string reason){
		switch (reason) {
		case "food":
			lowerClass = (int)(lowerClass * 0.9);
			break;

		case "wood":
			if (middleClass > 0) {
				lowerClass += (int)(middleClass * 0.4)+1;
				middleClass -= (int)(middleClass * 0.4)+1;
			}
			break;
		default:
			break;
		}
	}
}
