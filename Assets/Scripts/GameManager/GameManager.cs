using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject platform;
    private float minX = -2.5f, maxX = 2.5f, minY = -4.7f, maxY = -3.7f;

    private void Awake() {
        MakeInstance();
        CreateInitialPlatforms();
    }

    /**
        This function creates a singleton that only exists within the current
        scene.
    */
    void MakeInstance(){
        if(instance == null){
            instance = this;
        }
    }

    /** 
        This fucntion will place the first two platforms in random positions and
        place the player slightly above the first platform.
    */
    void CreateInitialPlatforms(){
        Vector3 temp = new Vector3(
            Random.Range(minX, minX+1.2f), 
            Random.Range(minY, maxY)
        );

        Instantiate(platform, temp, Quaternion.identity);
        temp.y += 2f;
        Instantiate(player, temp, Quaternion.identity);

        temp = new Vector3(
            Random.Range(maxX, maxX-1.2f), 
            Random.Range(minY, maxY)
        );

        Instantiate(platform, temp, Quaternion.identity);
    }
}
