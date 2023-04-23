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
    public void StartGame()
    {
        isGameStarted = true;
        FindObjectOfType<Road>().StartCreating();
    }
    void Update()
    {
        TogglePanel();
    }

    public void PlayGame()
    {
        StartGame();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = "Score : " + score.ToString();
        

        if( score > GetHighScore())
        {
            PlayerPrefs.SetInt("Highscore", score);
            highScoreText.text = "Best: " + score.ToString();
        }
    }

    public int GetHighScore()
    {
        int i = PlayerPrefs.GetInt("Highscore");
        return i;
    }

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
