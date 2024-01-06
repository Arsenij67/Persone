using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Windows;

public class ModelScript :MonoBehaviour, ISubject<string>
{
    public Text text;
    private Image _fileBut;
    private static Action OnBeforeClick;
    private List<IObserver<string>> _observers = new List<IObserver<string>>();
    private ModelManager _manager;
    
  
    
    internal  string modelPath;
    void Awake()
    {
        _manager = FindObjectOfType<ModelManager>();
        AddObserver(DataAboutDream._dataAboutDream);
    }
    private void OnEnable()
    {
        OnBeforeClick += RemoveClickableBeforeClick;
        
    }
    private void OnDisable()
    {
        OnBeforeClick -= RemoveClickableBeforeClick;
    }
    public void OnClick()
    {
        
       
        _fileBut = GetComponent<Image>();
        _fileBut.color = Color.green;
        NotifyObservers(modelPath);
        ConditionFileLocation conditionfileModels = _manager._imageInput.GetComponent<ConditionFileLocation>();
        conditionfileModels.CheckData(DataAboutDream._dataAboutDream.FullNameModel);

        OnBeforeClick.Invoke();

    }

  

    internal void RemoveClickableBeforeClick()
    {
        Destroy(gameObject.GetComponent<EventTrigger>());
    
    }

    public void AddObserver(IObserver<string> o)
    {
        _observers.Add(o);  
        
    }

    public void RemoveObserver(IObserver<string> o)
    {
      _observers.Remove(o);
    }

    public void NotifyObservers( string fullName)
    {
       foreach( var o in _observers) o.UpdateInfo(fullName);
    }
}
