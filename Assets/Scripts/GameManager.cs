using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] GameObject playPanel;

    public bool isGameStarted = false;

    int score = 0;

    private void Awake()
    {
        highScoreText.text = "Best: " + GetHighScore().ToString();
    }

    /// <summary>
    /// starts creating the procedural world
    /// </summary>
    public void StartGame()
    {
        isGameStarted = true;
        FindObjectOfType<Road>().StartCreating();
    }
    void Update()
    {
        //toggles the start and restart button panel
        TogglePanel();
    }

    /// <summary>
    /// starts the play game sequence
    /// </summary>
    public void PlayGame()
    {
        StartGame();
    }

    /// <summary>
    /// restarts the game scene
    /// </summary>
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// increases score when diamonds are picked up 
    /// </summary>
    public void IncreaseScore()
    {
        score++;
        scoreText.text = "Score : " + score.ToString();
        
        //updates highscore
        if( score > GetHighScore())
        {
            PlayerPrefs.SetInt("Highscore", score);
            highScoreText.text = "Best: " + score.ToString();
        }
    }

    /// <summary>
    /// getter method for highscore
    /// </summary>
    /// <returns></returns>
    public int GetHighScore()
    {
        int i = PlayerPrefs.GetInt("Highscore");
        return i;
    }

    /// <summary>
    /// toggles the game panel on and off
    /// </summary>
    void TogglePanel()
    {
        if(!isGameStarted)
        {
            playPanel.gameObject.SetActive(true);
        }
        else
        {
            playPanel.gameObject.SetActive(false);
        }
    }
    
}
