using Siccity.GLTFUtility;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections;

public struct DataAboutDream :IObserver<string>,IObserver<Vector2>
{
 
    internal static DataAboutDream _dataAboutDream;
    internal Vector2 coord;
    internal string FullNameModel;
    internal string FullNameAvatar;
    internal string Name;
    internal string TypeDream;
    internal string MainObject;
    
    public void UpdateInfo(string info)
    {
        _dataAboutDream.FullNameModel = info;
    
    }

    public void UpdateInfo(Vector2 info)
    {
        _dataAboutDream.coord = info;
     
    }

}
public class MapLoader : MonoBehaviour
{
    [SerializeField] private Slider progressSlider;
    [SerializeField] private GameObject PanelLoading;
    private GameObject LoadedModel;
    private const int Koefscaler = 7;
 

    internal void OnWaitToLoadModels(float procentprogress)
    {
        PanelLoading.SetActive(true);
        progressSlider.value += procentprogress;
        Debug.Log(procentprogress);
        
    }
   
    internal void OnEndLoading(GameObject g, AnimationClip[] anim)
    {
        g.transform.position = Vector3.zero;
        g.transform.position = new Vector3(DataAboutDream._dataAboutDream.coord.x,0f,DataAboutDream._dataAboutDream.coord.y)* Koefscaler;
        Debug.Log(new Vector3(DataAboutDream._dataAboutDream.coord.x, 0f, DataAboutDream._dataAboutDream.coord.y));
        g.name = Path.GetFileName(DataAboutDream._dataAboutDream.FullNameModel);
        PanelLoading.SetActive(false);

    }
    internal void OnEndLoading(AsyncOperation asyncOperation)
    {

        PanelLoading.SetActive(false);
        LoadedModel.transform.position = new Vector3(DataAboutDream._dataAboutDream.coord.x, 0f, DataAboutDream._dataAboutDream.coord.y) * 9;
        int fullnameLength = DataAboutDream._dataAboutDream.FullNameModel.Length;
        LoadedModel.name = Path.GetFileName(DataAboutDream._dataAboutDream.FullNameModel).Remove(fullnameLength - ".fbx".Length, fullnameLength);

    }
    private void Awake()
    {
       StartCoroutine(LoadAvatarModel(DataAboutDream._dataAboutDream));

    }
    
    internal IEnumerator  LoadAvatarModel(DataAboutDream info)
    {
        
        FileInfo file = new FileInfo(info.FullNameModel);

        if (file.Extension.Equals(".gltf") && file.FullName.Contains("UserModels"))
            Importer.ImportGLTFAsync($"{info.FullNameModel}", new ImportSettings(), OnEndLoading, OnWaitToLoadModels);
        else if (file.Extension.Equals(".glb") && file.FullName.Contains("UserModels"))
            Importer.ImportGLBAsync($"{info.FullNameModel}", new ImportSettings(), OnEndLoading, OnWaitToLoadModels);

        else if (file.FullName.Contains("CustomModels"))
        {
          
            yield return null;
            ResourceRequest resourceRequest = Resources.LoadAsync<GameObject>(info.FullNameModel.Replace(".fbx",""));
             
            OnWaitToLoadModels(resourceRequest.progress);
               
            yield return resourceRequest;

            if (resourceRequest.asset == null)
            {
                Debug.LogError("Failed to load resource at path: ");
            }
            else
            {
                LoadedModel = Instantiate(resourceRequest.asset as GameObject);
                OnEndLoading(LoadedModel,null);
            }

     
        }

        



    }
}

 