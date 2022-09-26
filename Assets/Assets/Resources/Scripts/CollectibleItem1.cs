using UnityEngine;
using System.Collections;

public class CollectibleItem1 : MonoBehaviour {
	[SerializeField] private string itemName;

         void OnTriggerEnter(Collider other) {
		Managers.Inventory.AddItem(itemName);
		Destroy(this.gameObject);
	     } 
    
  
}
