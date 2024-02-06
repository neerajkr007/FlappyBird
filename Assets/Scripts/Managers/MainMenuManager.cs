using System;
using FlappyBird.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FlappyBird.Managers
{
    public class MainMenuManager : MonoBehaviour
    {
        [Header("Text Fields")]
        [SerializeField] private TMP_Text highScoreText;

        [Header("Buttons")] 
        [SerializeField] private Button playButton;
        [SerializeField] private Button quitButton;

        private void OnEnable()
        {
            highScoreText.text = string.Format("High Score : {0}", PlayerPrefs.GetInt("highscore", 0));
            
            playButton.onClick.AddListener(PlayGame);
            quitButton.onClick.AddListener(QuitGame);
        }

        private void OnDisable()
        {
            playButton.onClick.RemoveAllListeners();
            quitButton.onClick.RemoveAllListeners();
        }

        private void PlayGame()
        {
            SceneManager.LoadSceneAsync(ConstantsHelper.GAME_SCENE_INDEX);
        }

        private void QuitGame()
        {
            Application.Quit();
        }
    }
}