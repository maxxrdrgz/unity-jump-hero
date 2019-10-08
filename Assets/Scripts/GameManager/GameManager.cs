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
    private bool lerpCamera;
    private float lerpTime = 1.5f;
    private float lerpX;

    private void Awake() {
        MakeInstance();
        CreateInitialPlatforms();
    }
    
    private void Update() {
        if(lerpCamera){
            LerpTheCamera();
        }
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

    /** 
        This function will get a new x position based on the camera's x position
        and instantiate a new platform further right of the player.
    */
    void CreateNewPlatform(){
        float cameraX = Camera.main.transform.position.x;
        float newMaxX = (maxX * 2) + cameraX;
        Instantiate(
            platform, 
            new Vector3(Random.Range(newMaxX, newMaxX - 1.2f), 
            Random.Range(maxY, maxY-1.2f), 0), 
            Quaternion.identity
        );
    }

    /** 
        This function will set lerpCamera to true, calculate a new lerpX value
        and create a new platform.

        @param {float} starting position where the camera will lerp from. This
        will come from the x position of the platform the player just landed on.
    */
    public void CreateNewPlatformAndLerp(float lerpPosition){
        CreateNewPlatform();
        lerpX = lerpPosition + maxX;
        lerpCamera = true;
    }

    /** 
        This function will lerp the camera's x position. Once the camera's x
        position is close to lerpX, the lerpCamera bool will be reset back to
        false which will stop the camera from moving.
    */
    void LerpTheCamera(){
        float x = Camera.main.transform.position.x;
        x = Mathf.Lerp(
            x,
            lerpX,
            lerpTime * Time.deltaTime
        );
        Camera.main.transform.position = new Vector3(
            x, 
            Camera.main.transform.position.y, 
            Camera.main.transform.position.z
        );
        if(Camera.main.transform.position.x >= (lerpX - 0.07f)){
            lerpCamera = false;
        }
    }
}
