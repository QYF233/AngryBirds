using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class LoadLevelAsync : MonoBehaviour
{
    private void Start()
    {
        //设置分辨率
        // Screen.SetResolution(800,500,false);
        Invoke("Load", 2);
    }
    void Load()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
