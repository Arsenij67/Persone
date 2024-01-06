using UnityEditor;
using UnityEngine;
using UnityEditor.Callbacks;

public static class TestPostBuild
{

    [PostProcessBuild]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuildProject)
    {
        Debug.Log("build location: "+pathToBuildProject);
    
    }

 
}
