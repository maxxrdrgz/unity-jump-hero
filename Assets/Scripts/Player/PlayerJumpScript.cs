using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerJumpScript : MonoBehaviour
{
    public static PlayerJumpScript instance;

    private Rigidbody2D rbody;
    private Animator anim;
    [SerializeField]
    private float forceX, forceY;
    private float thresholdX = 7f;
    private float thresholdY = 14f;
    private bool setPower, didJump;
    private Slider powerBar;
    private float powerbarThreshold = 10f;
    private float powerBarValue =  0f;

    private void Awake() {
        MakeInstance();
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        powerBar = GameObject.Find("Power Bar").GetComponent<Slider>();
        powerBar.minValue = 0f;
        powerBar.maxValue = 10f;
        powerBar.value = powerBarValue;
    }

    private void Update() {
        SetPower();
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
        This function will increment the x and y force when setPower is true.
        The max x and y force is also kept here as well. This funciton will
        get called in update.
    */
    void SetPower(){
        if(setPower){
            forceX += thresholdX * Time.deltaTime;
            forceY += thresholdY * Time.deltaTime;

            if(forceX > 6.5f){
                forceX = 6.5f;
            }
            if(forceY > 13.5f){
                forceY = 13.5f;
            }
            powerBarValue += powerbarThreshold * Time.deltaTime;
            powerBar.value = powerBarValue;
        }
    }

    /** 
        This function will determine whether or not the power should continue
        to be set. Once setpower is set to false, the player will jump when
        didJump is also false to prevent double jumps.

        @param {bool} Boolean that decided whether or not force should be incremented
    */
    public void SetPower(bool SetPower){
        setPower = SetPower;
        if(!setPower && !didJump){
            Jump();
        }
    }

    /** 
        This function will set the gameobjects velocity using the forcex and y.
        Will also reset the forces and set didJump to true.
    */
    void Jump(){
        rbody.velocity = new Vector2(forceX, forceY);
        forceX = forceY = 0;
        didJump = true;
        anim.SetBool("Jump", didJump); 
        powerBarValue = 0f;
        powerBar.value = powerBarValue;
    }

    /** 
        This fucntion will detect collision when the player collides with a 
        platform only when didJump is true.

        @param {Collider2D} The other Collider2D involved in this collision.
    */
    private void OnTriggerEnter2D(Collider2D other) {
        if(didJump){
            didJump = false;
            anim.SetBool("Jump", didJump); 
            if(other.tag == "Platform"){
                if(GameManager.instance != null){
                    GameManager.instance.CreateNewPlatformAndLerp(other.transform.position.x);
                }
                if(ScoreManager.instance != null){
                    ScoreManager.instance.IncrementScore();
                }
            }
        }

        if(other.tag == "Dead"){
            if(GameOverManager.instance != null){
                GameOverManager.instance.GameOverShowPanel();
            }
            Destroy(gameObject);
        }
    }
}
