using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    public int best;
    public int score = 0;
    public int currentStage = 0;
    public float yPassed = 20f;

    public bool isAlive = true;
    [SerializeField] private GameObject DeathLevelCanvas;
    [SerializeField] private GameObject LevelPassedCanvas;

    [SerializeField] private Text curStageText;
    [SerializeField] private Text nextStageText;

    public GameObject scoreAddText;
    [SerializeField] private Transform addscoreSpawn;

    private void Awake()
    {
        //Application.targetFrameRate = 60;
        /*
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(LevelPassedCanvas);
        DontDestroyOnLoad(addscoreSpawn.GetComponentInParent<Canvas>().gameObject);
        */
        //FindObjectOfType<GoogleAdMobController>().LoadBanner();
        //admobBanner.loadbanner();

        
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);

        // Yuksek Skoru Yukler
        best = PlayerPrefs.GetInt("Highscore");
        currentStage = PlayerPrefs.GetInt("CurrentStage");
        score = PlayerPrefs.GetInt("TempScore");
        PlayerPrefs.SetInt("TempScore", 0);
        curStageText.text = (currentStage + 1).ToString();
        nextStageText.text = (currentStage + 2).ToString();
        
    }
    public void NextLevel()
    {
        yPassed = 3;
        curStageText.text = (currentStage + 1).ToString();
        nextStageText.text = (currentStage + 2).ToString();/*
        FindObjectOfType<PlayerMovement>().ResetBall();
        FindObjectOfType<CenterController>().LoadStage(currentStage);*/
        Invoke("LoadNextScene", .2f);
    }

    public void RestartLevel()
    {
        score = 0;
        yPassed = 3;/*
        FindObjectOfType<PlayerMovement>().ResetBall();
        FindObjectOfType<CenterController>().LoadStage(currentStage);*/
        DeathLevelCanvas.SetActive(false);
        if (Random.Range(1,6) <= 3)
        {

        }
        Invoke("ReloadScene", .2f);
    }
    public void ReloadScene()
    {
        DeathLevelCanvas.SetActive(false);
        LevelPassedCanvas.SetActive(false);
        Time.timeScale = 1.5f;
        SceneManager.LoadScene(0);   
    }
    public void LoadNextScene()
    {
        PlayerPrefs.SetInt("TempScore", score);
        DeathLevelCanvas.SetActive(false);
        LevelPassedCanvas.SetActive(false);
        Time.timeScale = 1.5f;
        SceneManager.LoadScene(0);
    }
    public void Resurraction()
    {
        //adRewarded.UserChoseToWatchAd();
        backToLife();

    }
    public void RewardedAdfin()
    {
        backToLife();
    }
    public void backToLife()
    {
        yPassed += 4;
        FindObjectOfType<PlayerMovement>().Resurrection();
        FindObjectOfType<CenterController>().Resurrection();
        DeathLevelCanvas.SetActive(false);
        Time.timeScale = 1.5f;
    }
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreAddText.GetComponent<Text>().text = "+" + scoreToAdd;
        Instantiate(scoreAddText, addscoreSpawn);
        if (score > best)
        {
            PlayerPrefs.SetInt("Highscore", score);
            best = score;
        }
    }
    public void DeadCanvas()
    {
        DeathLevelCanvas.SetActive(true);
    }
    public void GoalReached()
    {
        currentStage += 1;
        PlayerPrefs.SetInt("CurrentStage", currentStage);
        LevelPassedCanvas.SetActive(true);
        isAlive = false;
    }
    public void DestroyAllDontDestroyOnLoadObjects()
    {

        var go = new GameObject("Sacrificial Lamb");
        DontDestroyOnLoad(go);

        foreach (var root in go.scene.GetRootGameObjects())
            Destroy(root);

    }
}
