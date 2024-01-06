using UnityEngine;
using System.Collections;

public class PlayerCharacter : MonoBehaviour {
	public void Hurt(int damage) {
		Managers.Player.ChangeHealth(-damage);

	}
	private void Start()
	{
		Managers.Player.UpdateData(100, 100);
	}


}
