using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpScript : MonoBehaviour
{
    public static PlayerJumpScript instance;

    private Rigidbody2D rbody;
    private Animator anim;
    private float forceX, forceY;
    private float thresholdX = 7f;
    private float thresholdY = 14f;
    private bool setPower, didJump;

    private void Awake() {
        MakeInstance();
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

    public void SetPower(bool SetPower){
        setPower = SetPower;
        if(setPower){
            print("we are setting the power");
        }else{
            print("not setting power");
        }
    }
}
