using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameOverManager instance;

    [SerializeField]
    private GameObject gameOverPanel;
    private Animator gameoverAnim;
    [SerializeField]
    private Button playAgainBtn, backBtn;
    [SerializeField]
    private Text finalScore;
    [SerializeField]
    private GameObject scoreText;

    private void Awake() {
        gameoverAnim = gameOverPanel.GetComponent<Animator>();
        playAgainBtn.onClick.AddListener(() => PlayAgain());
        backBtn.onClick.AddListener(() => BackToMenu());
        gameOverPanel.SetActive(false);
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

    public void PlayAgain(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    /** 
        This function will disable the active score text, and display the game
        over panel.
    */
    public void GameOverShowPanel(){
        scoreText.SetActive(false);
        gameOverPanel.SetActive(true);
        finalScore.text = "Score\n" + ScoreManager.instance.GetScore(); 
        gameoverAnim.Play("FadeIn");
    }
}
