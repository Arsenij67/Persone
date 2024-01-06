using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class FileScript : MonoBehaviour
{
    public Text text;
    private Image image;
    private AvatarManager avatarManager;
    private Button filebut;

  

  

    public void OnClick()
    {
       
         
        avatarManager = FindObjectOfType<AvatarManager>();
        StartCoroutine(avatarManager.LoadAvatarTexture(text.text.ToString()));
        ConditionFileLocation conditionData = avatarManager._imageinput.GetComponent<ConditionFileLocation>();
        conditionData.CheckData(text.text.ToString());
 

    }


    private void Awake()
    {
      
        filebut = GetComponent<Button>();
        image = GetComponent<Image>();

    }
    

}
