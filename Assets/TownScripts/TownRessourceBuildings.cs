using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownRessourceBuildings{
	public int farmLVL = 0;
	public int lumberjackLVL = 0;
	public int mineLVL = 0;

	public TownRessourceBuildings(int farmLVL = 1, int lumberjackLVL = 0, int mineLVL = 0){
		this.farmLVL = farmLVL;
		this.lumberjackLVL = lumberjackLVL;
		this.mineLVL = mineLVL;
	}
	public double foodProduction(){
		return farmLVL * farmLVL;
	}
	public double woodProduction(){
		return lumberjackLVL * lumberjackLVL;
	}
	public double stoneProduction(){
		return mineLVL * mineLVL;
	}
	public double metalProduction(){
		if (mineLVL > 2) {
			return (mineLVL - 2) * (mineLVL - 2);
		}
		return 0;
	}


}
