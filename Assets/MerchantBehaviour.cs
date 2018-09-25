using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantBehaviour : MonoBehaviour {

	public Ressources ressources;
	public TownBehaviour goal;
	public int maxStorageSpace = 10;
	public int currentStorageSpace = 10;
	public bool driving;
	public string currentTown = "One";
	public float speed = 0.01f;
	// Use this for initialization
	void Start () {
		ressources = new Ressources (gameObject.name + ".xml");
	}
	
	// Update is called once per frame
	void Update () {
		if (driving) {
			Vector3 direction = new Vector3 ((goal.transform.position.x - gameObject.transform.position.x)*speed, (goal.transform.position.y - gameObject.transform.position.y) * speed, (goal.transform.position.z - gameObject.transform.position.z) * speed);
			gameObject.transform.position += direction*speed;
			print ("moving " + direction);
		}
	}

	public int setGoal (TownBehaviour town, int amount, string ressourcename){
		goal = town;
		int amountTaken = amount;
		if (currentStorageSpace < amount) {
			ressources.addRessource (ressourcename, currentStorageSpace);
			amountTaken = currentStorageSpace;
			currentStorageSpace = 0;
			} else {
			ressources.addRessource (ressourcename, amount);
			currentStorageSpace -= amount;
		}
		if (currentStorageSpace == 0) {
			driving = true;
			currentTown = "";
		}
		return -amountTaken;
	}
}
