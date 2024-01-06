
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Siccity.GLTFUtility;
using System;

public class ModelManager : MonoBehaviour
{
    public GameObject filesListPan, filesContent, filePrefab;
    private List<ModelScript> modelsScripts = new List<ModelScript> ();
    public Text text;
    private List<FileInfo> Allfiles = new List<FileInfo>();
    private string[] TypesModelUser = new string[] {  "*.gltf","*.glb"};
    private string[] TypesModelCustom = new string[] { "*.fbx" };
    public RawImage _imageInput;


    public void LoadModelList()
    {
        string pathmodelsUser = Application.persistentDataPath + "/UserModels";
        string pathmodelsCustom = "CustomModels";
        try
        {
 

            Debug.Log(pathmodelsCustom);
            DirectoryInfo [] targetdirs = { new DirectoryInfo(pathmodelsUser), new DirectoryInfo(pathmodelsCustom) };

            GameObject [] CustomObjects = Resources.LoadAll<GameObject>(pathmodelsCustom);
            if (!targetdirs[0].Exists )
            {
                targetdirs[0].Create();
               
            }

            filesListPan.SetActive(true);
            filesContent.SetActive(true);

            Allfiles.AddRange(TypesModelUser.SelectMany(res => targetdirs[0]?.GetFiles(res, SearchOption.AllDirectories)).ToList());
            
           
            foreach (var model in Allfiles)
            {
            var modelscript = Instantiate(filePrefab, filesContent.transform).GetComponent<ModelScript>();

            modelscript.text.text = model.Name;
            modelscript.modelPath = model.FullName;
            modelsScripts.Add(modelscript);




            }
            foreach (var cmodel in CustomObjects)
            {

                var modelscript = Instantiate(filePrefab, filesContent.transform).GetComponent<ModelScript>();
                modelscript.text.text = cmodel.name;
                modelscript.modelPath = "CustomModels/"+cmodel.name;
                modelsScripts.Add(modelscript);
            }

       

    }
        catch (Exception ex) { text.text += pathmodelsCustom; };





    }
 
    public void CloseModelList()
    { 
        gameObject.SetActive(false);
        filesContent.SetActive(false);
        foreach (var modscripts in modelsScripts)
        {
            Destroy(modscripts.gameObject);
            

        }
        modelsScripts.Clear();
        Allfiles.Clear();

    }

    //private void CopyFilesRecursively(string sourcePath, string targetPath)
    //{
    //    //Now Create all of the directories
         
    //    string[] newPathS = TypesModel.SelectMany(cel => Directory.GetFiles(sourcePath, cel, SearchOption.AllDirectories)).ToArray();

    //    foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
    //    {

    //        Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
    //    }
    //    //Copy all the files & Replaces any files with the same name
    //    foreach (string newPath in newPathS)
    //    {
    //       File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
    //    }
    //}
}
