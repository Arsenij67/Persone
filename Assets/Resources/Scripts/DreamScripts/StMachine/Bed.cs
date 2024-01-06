using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bed : InteractGUI
{



    private Player _player;
    public override Vector3 coordinforGui { get { return new Vector3(0, 2, 0); } }

    public override void SetReactionOnClick()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _player._stMach.ChangeState(new SleepState(_player, this));


    }

   
   


}
