using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Cell: MonoBehaviour, ISubject <Vector2>
{
    private List<IObserver<Vector2>> _observers = new List<IObserver<Vector2>>();
    private Button _button;
    private Color ChangedColor;
    public static Button ButtonClicked;
    public int x, y;   

   

    public void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OpenMenuObjects);
        AddObserver(DataAboutDream._dataAboutDream);
        AddObserver(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>());
    }
    private void  OpenMenuObjects()
    {
        ButtonClicked = this.GetComponent<Button>();
        var obj = GameObject.FindGameObjectWithTag("ScrollObject");
        var AllFigures =  obj.GetComponentsInChildren<Figures>();
        Figures ChoozedFig =  AllFigures.Where((obj) => obj.isPressed == true).FirstOrDefault();
        if (ChoozedFig == null) ChangedColor = Color.white;
        NotifyObservers(transform.position);
        obj.SetActive(true);
        obj.transform.position = this.transform.position;
    
    }

    public  void  AddObserver(IObserver<Vector2> o)
    {
    _observers.Add(o);
    }
   

    public   void RemoveObserver(IObserver<Vector2> o)
    {
    _observers.Remove(o);
    }
   

    public void NotifyObservers(Vector2 coord)
    {
    coord = new Vector2(x, y);  
    foreach( var ob in _observers)
    {
    ob.UpdateInfo(coord);
    }

    }

  
    


}
