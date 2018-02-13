using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownRessourceBuildings{
	
	public TownRessourceBuildings(){

	}


	public double ressourceProduction(Ressource ressource){
		return ressource.productionLVL * ressource.productionLVL * ressource.production;
	}


}
