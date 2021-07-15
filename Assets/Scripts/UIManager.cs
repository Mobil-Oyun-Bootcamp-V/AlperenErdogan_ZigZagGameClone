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
        failedPanelScoreText.text = "SCORE\n" + GameManager.Instance.Score + "\nBEST SCORE\n" + "864";
        failedPanel.SetActive(true);
    }

    public void OnRetryButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
