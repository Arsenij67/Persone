using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Calendar : MonoBehaviour
{
    [SerializeField] private Button _butAdd;
   
    [SerializeField] private GameObject ButOpenInfo;
    [SerializeField]private Transform _content;
 
  

     
 private TMP_InputField InputButton;
    public void Start()
    {
        InputButton = ButOpenInfo.transform.GetChild(0).GetComponent<TMP_InputField>();
        InputButton.onEndEdit.AddListener(KeepDate);
    }

    public void AddButtonInfo()
    { 
        GameObject butonInfo = Instantiate(ButOpenInfo,_content);

        InputButton = butonInfo.transform.GetChild(0).GetComponent<TMP_InputField>();

        InputButton.text = "";
        InputButton.onEndEdit.AddListener(KeepDate);

       

        _butAdd.gameObject.SetActive(false) ;

        _butAdd.transform.SetSiblingIndex(_content.childCount-1);

       

    
    }


    public void Close()=> transform.gameObject.SetActive(false);

    public void KeepDate(string data)
    {
        if (!string.IsNullOrEmpty(data))
        {
            InputButton.text = string.Format("{0:##-##-##}", Convert.ToInt64(data));

            _butAdd.gameObject.SetActive(true);
        }
     
    }



}
