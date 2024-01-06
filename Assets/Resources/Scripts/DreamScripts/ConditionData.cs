using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConditionData : MonoBehaviour
{
    [SerializeField] private Texture[] textures;
    protected TMP_InputField inputField;
    private RawImage imageCondition;
    private void Awake()
    {
        inputField = transform.parent.GetComponent<TMP_InputField>();
        imageCondition = GetComponent<RawImage>();
    }
    public virtual void CheckData(string path)
    {
      
        if (!string.IsNullOrEmpty(inputField.text))
        {
            SetTrue();
        }

        else
        {

            SetFalse();
        }
    }

    protected void SetTrue()
    {
       
        imageCondition.texture = textures[0];
        imageCondition.color = Color.green;
    }
    protected void SetFalse()
    {
        imageCondition.texture = textures[1];
        imageCondition.color = Color.red;
    }
}


