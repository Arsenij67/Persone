using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    [SerializeField] private int speed = 10;
   

    // Update is called once per frame
    void FixedUpdate()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * speed);
        
    }
}
