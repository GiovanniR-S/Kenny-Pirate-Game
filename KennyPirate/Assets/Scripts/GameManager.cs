using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("GameState")]
    [SerializeField] private string gameState = "Play";
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject gameOverMenuUI;
    [SerializeField] private GameObject congratulationsMenuUI;
    [SerializeField] private GameObject disableScore;
    [SerializeField] private GameObject disableTime;
    [SerializeField] private Text scoreText;
    public static bool gameIsPaused = false;
    public static bool gameOver = false;
    public static bool congratulations = false;

    [Header("MenuUI")]
    [SerializeField] private GameObject back;
    [SerializeField] private GameObject play;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject quit;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject titleEnemySpawn;
    [SerializeField] private GameObject titleSessionTime;
    [SerializeField] private GameObject time1, time2, time3;
    [SerializeField] private GameObject spwan1, spwan2, spwan3;
    public static bool menuOptions = false;
    

    [Header("Score")]
    [SerializeField] private int score;
    [Header("Numeros Inimigos")]
    [SerializeField] private int numbersEnemys;

    public static float maxTime = 1;
    public static int optionsTimeEnemy = 1;
    // Start is called before the first frame update
    void Start()
    {
        switch(gameState) {
            case "MenuMain":
                break;
            case "MenuControls":
                break;
            case "Play":
                gameOver = false;
                congratulations = false;
                Time.timeScale = 1f;
                score = 0;
                break;
            case "Pause":
                break;
            case "GameOver":
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Menu")) {
            if(menuOptions) {
                MenuOptions();
            } else {
                Menu();
            }
        }
        if(congratulations) { 
            Congratulations();
        }
        if(gameOver) {
            GameOver();
        }
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(!gameIsPaused && !gameOver) {
                PauseGame();
            } else {
                Resume();
            }
        }
        ChangeColorTimeSession();
        ChangeColorTimeEnemy();
    }
    public void MenuOptions () {
        menuOptions = true;
        back.SetActive(true);
        play.SetActive(false);
        title.SetActive(false);
        quit.SetActive(false);
        options.SetActive(false);
        titleEnemySpawn.SetActive(true);
        titleSessionTime.SetActive(true);
        time1.SetActive(true);
        time2.SetActive(true);
        time3.SetActive(true);
        spwan1.SetActive(true);
        spwan2.SetActive(true);
        spwan3.SetActive(true);
    }
    public void Menu () {
        menuOptions = false;
        back.SetActive(false);
        play.SetActive(true);
        title.SetActive(true);
        quit.SetActive(true);
        options.SetActive(true);
        titleEnemySpawn.SetActive(false);
        titleSessionTime.SetActive(false);
        time1.SetActive(false);
        time2.SetActive(false);
        time3.SetActive(false);
        spwan1.SetActive(false);
        spwan2.SetActive(false);
        spwan3.SetActive(false);
    }
    void ChangeColorTimeSession () {
        if(maxTime == 1) {
            time1.transform.GetChild(0).GetComponent<Text>().color = Color.gray;
        } else {
            time1.transform.GetChild(0).GetComponent<Text>().color = Color.white;
        }
        if(maxTime == 2) {
            time2.transform.GetChild(0).GetComponent<Text>().color = Color.gray;
        } else {
            time2.transform.GetChild(0).GetComponent<Text>().color = Color.white;
        }
        if(maxTime == 3) {
            time3.transform.GetChild(0).GetComponent<Text>().color = Color.gray;
        } else {
            time3.transform.GetChild(0).GetComponent<Text>().color = Color.white;
        }
    }
    void ChangeColorTimeEnemy () {
        if(optionsTimeEnemy == 1) {
            spwan1.transform.GetChild(0).GetComponent<Text>().color = Color.gray;
        } else {
            spwan1.transform.GetChild(0).GetComponent<Text>().color = Color.white;
        }
        if(optionsTimeEnemy == 2) {
            spwan2.transform.GetChild(0).GetComponent<Text>().color = Color.gray;
        } else {
            spwan2.transform.GetChild(0).GetComponent<Text>().color = Color.white;
        }
        if(optionsTimeEnemy == 3) {
            spwan3.transform.GetChild(0).GetComponent<Text>().color = Color.gray;
        } else {
            spwan3.transform.GetChild(0).GetComponent<Text>().color = Color.white;
        }
    }
    void PauseGame () {
        gameState = "Pause";
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
    void GameOver () {
        gameState = "GameOver";
        gameOverMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameOver = true;
    }
    void Congratulations () {
        Time.timeScale = 0f;
        gameState = "Congratulations";
        scoreText.text = "Score: " + score;
        congratulationsMenuUI.SetActive(true);
        disableScore.SetActive(false);
        disableTime.SetActive(false);
        gameIsPaused = true;
    }
    public void Resume () {
        gameState = "Play";
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    public void LoadMenu () {
        //Debug.Log("Loding Menu");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame () {
        //Debug.Log("Quit Game");
        Application.Quit();
    }
    public void PlayAgain () {
        gameState = "Play";
        Time.timeScale = 1f;
        SceneManager.LoadScene("PlayGame");
        gameOver = false;
    }
    public int GetScore () {
        var score = this.score;
        return score;
    }
    public void AddScore () {
        score += 1;
    }
    public int GetNumerosEnemys () {
        var numbersEnemys = this.numbersEnemys;
        return numbersEnemys;
    }
    public void AddNumerosEnemys () {
        numbersEnemys += 1;
    }
    public void RemoveNumerosEnemys () {
        numbersEnemys -= 1;
    }
    public void setMaxTime (float time) {
        maxTime = time;
    }
    public void setMaxTimeEnemy (int time) {
        optionsTimeEnemy = time;
    }
}
