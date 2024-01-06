using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;




[RequireComponent(typeof(TMP_InputField))]
public class FieldForm : MonoBehaviour
{
    bool menuHasCreated = false;

    [SerializeField] private GameObject Prefab;
    private TMP_InputField  InputField;
    private void Awake()
    {
        InputField = GetComponent<TMP_InputField>();
    }
    public void CreateScrollView(string data)
    {

        menuHasCreated = true;
        Prefab.transform.position = transform.position -  new Vector3(0,45,0);
        Prefab.gameObject.SetActive(menuHasCreated);

    }

    public void WriteFromScrollView(string data)
    { 
        menuHasCreated = false;
        
        InputField.text = data;

        Prefab.gameObject.SetActive(menuHasCreated);

        DataAboutDream._dataAboutDream.TypeDream = data;

    }




      
}
