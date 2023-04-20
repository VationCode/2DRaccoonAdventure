using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class BirdCreate : MonoBehaviour
{
    public GameObject bird;
    public GameObject[] birdCreatePos;

    float t = 0;

    // Update is called once per frame
    void Update()
    {
        
        t += Time.deltaTime;

        int rand = UnityEngine.Random.Range(0,4);
        float randT = UnityEngine.Random.Range(3.0f, 7.0f);
        if (t > randT)
        {
            Instantiate(bird, birdCreatePos[rand].transform.position, birdCreatePos[rand].transform.rotation);
            t = 0;
        }
    }
}
