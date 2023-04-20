using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Portal : MonoBehaviour
{
    public LoadingScreen loadingScreen;
    public string transferMapName;

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.Instance.enterCount += 1;
            if (Player.Instance.enterCount == 2) return;
            Player.Instance.currentMapName = transferMapName;
            loadingScreen.LoadScene(transferMapName);
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Player.Instance.enterCount = 0;
    }
}