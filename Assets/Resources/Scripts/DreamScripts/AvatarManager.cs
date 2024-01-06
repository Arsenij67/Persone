using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using UnityEngine.Networking;


public class AvatarManager : MonoBehaviour
{
    public GameObject filesContent, filePrefab;
    public RawImage avatarImg;
    private static FileInfo[] files;
    public RawImage _imageinput;
 

    string[] massNames = new string[] { "*.png", "*.jpg", "*.bmp" };

    private Dictionary<string,FileInfo> filedict = new Dictionary<string, FileInfo>();

    private List<FileScript> filesscripts = new List<FileScript> ();
    
    public void LoadAvatarList()
    {
      
            DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
            transform.gameObject.SetActive(true);
            avatarImg.gameObject.SetActive(true);

            files = massNames.SelectMany((sel) => dir.GetFiles(sel, SearchOption.AllDirectories)).ToArray();


            foreach (var file in files)
            {

                FileScript fileload = Instantiate(filePrefab).GetComponent<FileScript>();

                fileload.gameObject.transform.SetParent(filesContent.transform);

                fileload.text.text = file.Name;

                filesscripts.Add(fileload);

           

            }

    }
    public void CloseAvatarList()
    {
        gameObject.SetActive(false);
        foreach (var modscripts in filesscripts)
        {
            Destroy(modscripts.gameObject);
         

        }
       filesscripts.Clear();

    }

    internal IEnumerator LoadAvatarTexture(string name)
    {
   
        filedict = files.ToDictionary<FileInfo, string>((key) => { return key.Name; });
         
        UnityWebRequest req = UnityWebRequestTexture.GetTexture("file://"+filedict[name].FullName);
    
        DataAboutDream._dataAboutDream.FullNameAvatar = filedict[name].FullName;

            yield return req.SendWebRequest();
       
         
            avatarImg.texture = (req.downloadHandler as DownloadHandlerTexture).texture;

        req.downloadHandler.Dispose();
        req.Dispose();

    }

  
 
    
    }


