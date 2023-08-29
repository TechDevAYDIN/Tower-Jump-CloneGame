using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreText = null;
    [SerializeField] private Text bestscoreText = null;
    [SerializeField] private Text levelPassedText = null;

    void Update()
    {
        scoreText.text = GameManager.singleton.score.ToString();
        bestscoreText.text = "Best: " + GameManager.singleton.best;
        levelPassedText.text = "Level " + (GameManager.singleton.currentStage).ToString() + " Passed";
    }
}
