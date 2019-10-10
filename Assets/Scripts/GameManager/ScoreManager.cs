using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    [SerializeField]
    private Text scoreText;
    private int score;

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

    /** 
        This function increments the score variable and updates the scoretext.
    */
    public void IncrementScore(){
        score++;
        scoreText.text = "" +score;
    }

    /** 
        Returns the local private score variable
    */
    public int GetScore(){
        return this.score;
    }
}
