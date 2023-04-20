using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public LoadingScreen loadingScreen;
    public string transferMapName;
    public void OnClickSceneChange()
    {
        loadingScreen.LoadScene(transferMapName);
    }
    

}
