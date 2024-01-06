using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
[RequireComponent(typeof(DataMeneger))]
[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(InventoryManager))]
[RequireComponent(typeof(MissionManager))]

public class Managers : MonoBehaviour {
 
	public static PlayerManager Player {get; private set;}
	public static InventoryManager Inventory {get; private set;}
	public static MissionManager Mission {get; private set;}
	public static DataMeneger Data { get; private set; }

	
	void Awake() {
		DontDestroyOnLoad(gameObject);

		Data = GetComponent<DataMeneger>();

        Player = GetComponent<PlayerManager>();
		Inventory = GetComponent<InventoryManager>();
		Mission = GetComponent<MissionManager>();

	


		 
	}

	
   
}
