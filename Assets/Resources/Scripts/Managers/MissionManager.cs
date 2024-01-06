using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public ManagerStatus status { get; private set; }
   public int curLevel { get; private set; }
    public int maxLevel { get; private set; }

   
    public void UpdateData(int curLevel, int maxLevel)
    {
        this.curLevel = curLevel;
        this.maxLevel = maxLevel;

    
    
    }

    public void RestartCurrent()
    {
        string name = "Level" + curLevel;
        Debug.Log("Loading "+ name);
        Application.LoadLevel(name);


    }
    public void ReachOjective()
    {

      

        Messenger.Broadcast(GameEvent.LEVEL_COMPLETE);
    
    }

    public void GoToNext()
    {

        if (curLevel < maxLevel)
        {

            curLevel++;
            string name = "Level" + curLevel;
            Debug.Log("Loading " + name);
            Application.LoadLevel(name);
        }
        else 
        {


            Debug.Log("Last level!!!");
            Messenger.Broadcast(GameEvent.GAME_COMPLETE);
        
        }
    
    }
}
