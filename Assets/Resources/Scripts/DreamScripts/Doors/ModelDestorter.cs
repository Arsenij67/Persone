using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using System.Linq;


public  class SpawnController : MonoBehaviour
{

    [Header("Количество дверей от 11 до бесконечности")]
    public int CountObjects = 100;
    [SerializeField] private Transform startPos;
    [SerializeField] private float distBetweenZ = 3;
    [SerializeField] private float distBetweenX = 2;
    private TMPro.TextMeshPro _text; 
    public GameObject[] models;
    [SerializeField] private TMP_Text info;


    private List<int> ExcludeValues;

    public void Awake()
    {
        
        ExcludeValues = GenerateExcludeNumbers( Random.Range(5, 8));
        info.text += "Пропущенные числа \n";
        ExcludeValues.ForEach((obj) => info.text += " "+obj);
        SpawnObjects(startPos.position,180,CountObjects-ExcludeValues.Count());
        HideExcludeNumbers();
        
   
    }

   
    private async void SpawnObjects(Vector3 startP, float rot,int CountObj)
    {
        int number = 0;
        float X = distBetweenX;
        bool chetn = false;
        int counthasSpawned = 0;
        float rotAngle = 180;

        

        while (CountObj > counthasSpawned)
        {
             

            startP = chetn ? startP + new Vector3(distBetweenX, 0, 0) : startP+new Vector3(-X, 0, distBetweenZ);

            rotAngle = chetn ? 0 : rot;

            chetn = !chetn;

           var obj =   Instantiate(GetRandomObject(), startP, Quaternion.Euler(-90, 90+rotAngle, 0));
           obj.isStatic = true;

            if(ExcludeValues.Contains (number)) number++;

        
            _text =   obj.transform.GetChild(0).GetComponentInChildren<TextMeshPro>();
            _text.text = number.ToString();

            counthasSpawned+=1;
            number += 1;

           await  Task.Yield();
        
        
        }
    
    
    }

    private List<int> GenerateExcludeNumbers( int kolNumbers)
    {
     
       
       int[] Numbers = new int[kolNumbers];
    
        for (int j = 0; j < Numbers.Length; j++)
        {
           Numbers[j] =  Random.Range(0, CountObjects);
           
        }
        Numbers = Numbers.Distinct().ToArray();

       

        return Numbers.ToList();  
    }

    private GameObject GetRandomObject()
    {
        int index = Random.Range(0, models.Length);
        return models[index];
    }

    public void ShowExcludeNumbers()=> info.gameObject.SetActive(true);


    public void HideExcludeNumbers() => info.gameObject.SetActive(false);
   
}
