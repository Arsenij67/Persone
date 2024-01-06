using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;


public class Book : InteractGUI
{
    [SerializeField] private TMP_InputField _dreamText;
    public override Vector3 coordinforGui { get { return new Vector3(0, 2f, 0); } }

    internal Player _player;


    public GameObject _calendar;
    public GameObject _map;
    string path = "";

    public override void SetReactionOnClick()
    {
        path = Application.persistentDataPath;
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _player._stMach.ChangeState(new TakeItemState(_player));

    }


    public void Close()
    {
        _calendar.SetActive(false);
    }
    public void Open()
    {
        _calendar.SetActive(true);
    }
    public void OpenMap()
    {
        _map.SetActive(true);
        FillCoordMap(12, 18+1);
    }

    public void CloseMap()
    {
        _map.SetActive(false);
    }
    public async void WriteDreamAsync()
    {

        path = Application.persistentDataPath + "/" + "Dreams.txt";

        using (StreamWriter Sw = new StreamWriter(path, true, encoding: Encoding.UTF8))
        {
            await Sw.WriteAsync("\n" + _dreamText.text);
        }
        Debug.Log("Запись прошла успешно" + " " + Application.persistentDataPath);

    }

    public async void ReadDreamAsync()
    {
        string result = "";
        using (StreamReader Sr = new StreamReader(path, Encoding.UTF8))
        {
            result = await Sr.ReadToEndAsync();
        }
        _dreamText.text = result;
        Debug.Log("Чтение прошло успешно!");
    }

    private void FillCoordMap(int row,int cols)
    {
        Cell[] cells = _map.transform.GetChild(0).GetComponentsInChildren<Cell>();
        int indexCel = 0;

        for (int i = 0; i <row; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                cells[indexCel].x = j;
                cells[indexCel].y = i;  
                indexCel++; 
            }
        
        }

    }
}

    
