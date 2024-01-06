using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class InteractGUI: MonoBehaviour 
{
    private GameObject _gui;
    public abstract Vector3 coordinforGui { get;}

    [SerializeField] private  GameObject GUIPrefab;

  
    public bool OpenFirstPeace { get; set; } = true;

    public void CreateGui(GameObject Parent)
    {

        if (OpenFirstPeace)
        {

            _gui = Instantiate(GUIPrefab, Vector3.zero, Quaternion.identity);
            OpenFirstPeace = !OpenFirstPeace;
            _gui.transform.parent = Parent.transform;
            _gui.transform.localPosition = coordinforGui;
            Button _button = _gui.GetComponentInChildren<Button>();
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(SetReactionOnClick);
           
        }



    }
    public abstract void SetReactionOnClick();

    public void DestroyGui()
    {
        print("Удалено");
        Destroy(_gui);

    }


}
