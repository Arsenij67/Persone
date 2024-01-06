using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Door))]
public class Door : InteractGUI
{


    public bool isOpen = false;

    public override Vector3 coordinforGui { get { return new Vector3(0.5f, 4f, -1f); } }


    private Player player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }


    public override void SetReactionOnClick() { print("Повзаимодействовали с дверью"); player._stMach.ChangeState(new OpenCloseDoorState(player)); }
}
  

