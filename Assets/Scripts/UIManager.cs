using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]private Text scoreText;
    [SerializeField] private GameObject failedPanel;
    [SerializeField] private Text failedPanelScoreText;

    private void Start()
    {
        GameManager.Instance.ONScore += UpdateScore;
        GameManager.Instance.ONFailed += OnFailed;
    }
    private void UpdateScore()
    {
        scoreText.text = GameManager.Instance.Score.ToString();
    }
    private void OnFailed()
    {
        if (GameManager.Instance.Score>PlayerPrefs.GetInt("best"))
        {
            PlayerPrefs.SetInt("best",GameManager.Instance.Score);
        }
        
        failedPanelScoreText.text = "SCORE\n" + GameManager.Instance.Score 
                                              + "\nBEST SCORE\n" + PlayerPrefs.GetInt("best");
        failedPanel.SetActive(true);
    }
    public void OnRetryButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
