using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {
	public ManagerStatus status {get; private set;}

	
	public int health {get; set;}
	public int maxHealth {get; private set;}

	



	public void UpdateData(int health, int maxHealth) {
		this.health = health;
		this.maxHealth = maxHealth;
	}

	public void ChangeHealth(int value) {
		health += value;
		if (health > maxHealth) {
			health = maxHealth;
		} else if (health < 0) {
			health = 0;
		}

		if (health == 0) {
			Messenger.Broadcast(GameEvent.LEVEL_FAILED);
		}
		Messenger.Broadcast(GameEvent.HEALTH_UPDATED);
	}

	public void Respawn() {
		UpdateData(50, 100);
	}
}
