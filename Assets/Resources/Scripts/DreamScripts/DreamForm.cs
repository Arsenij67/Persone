using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DreamForm: MonoBehaviour
{

    public Transform _content;
    public void CloseForm()
    { 
    
    gameObject.SetActive(false);
    
    }

    public void OpenForm()
    {

        gameObject.SetActive(true);
    }
  public void ClearForm()
    {

       var dataUser =  _content.GetComponentsInChildren<TMP_InputField>();

        foreach (var inputField in dataUser)
        {
            inputField.text = "";
        }
       RawImage image  =  dataUser.Last().gameObject.GetComponentInChildren<RawImage>();
       image.texture = null;
         
    }

  

}
