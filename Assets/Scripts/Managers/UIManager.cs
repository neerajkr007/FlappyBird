using System;
using FlappyBird.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FlappyBird.Managers
{
    public class UIManager : MonoSingleton<UIManager>
    {
        [SerializeField] private TMP_Text pointsText;
        [SerializeField] private TMP_Text yourScoreText;
        [SerializeField] private TMP_Text highScoreText;

        [SerializeField] private Button pauseButton;
        [SerializeField] private Button continueButton;
        [SerializeField] private Button quitToMenuButton;
        [SerializeField] private Button retryButton;
        [SerializeField] private Button quitToMenuAfterGameOverButton;

        [SerializeField] private GameObject pausePanel;
        [SerializeField] private GameObject GameOverPanel;

        public bool IfGameIsPaused => pausePanel.activeInHierarchy;

        private void OnEnable()
        {
            pauseButton.onClick.AddListener(() => GameManager.Instance.HandleGamePauseState(true));
            continueButton.onClick.AddListener(() => GameManager.Instance.HandleGamePauseState(false));
            quitToMenuButton.onClick.AddListener(() => GameManager.Instance.QuitGameToMainMenu());
            retryButton.onClick.AddListener(() => GameManager.Instance.RestartLevel());
            quitToMenuAfterGameOverButton.onClick.AddListener(() => GameManager.Instance.QuitGameToMainMenu());
        }

        private void OnDisable()
        {
            pauseButton.onClick.RemoveAllListeners();
            continueButton.onClick.RemoveAllListeners();
            quitToMenuButton.onClick.RemoveAllListeners();
            retryButton.onClick.RemoveAllListeners();
            quitToMenuAfterGameOverButton.onClick.RemoveAllListeners();
        }

        public void UpdatePoints(string points)
        {
            pointsText.text = points;
        }

        public void ShowGameOverUI(int currentScore, int highScore)
        {
            pointsText.gameObject.SetActive(false);
            pauseButton.gameObject.SetActive(false);
            GameOverPanel.SetActive(true);
            yourScoreText.text = string.Format("Your Score : {0}", currentScore);
            highScoreText.text = string.Format("High Score : {0}", highScore);
        }

        public void UpdateGamePauseState(bool shouldPause)
        {
            pausePanel.SetActive(shouldPause);
            pauseButton.gameObject.SetActive(!shouldPause);
        }
    }
}