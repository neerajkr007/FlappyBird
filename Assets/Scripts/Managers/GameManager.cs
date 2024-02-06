using System;
using System.Collections;
using System.Collections.Generic;
using FlappyBird.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FlappyBird.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        private int points = 0;

        public Camera MainCamera;
        public bool CanAddInputFromPlayer => !UIManager.Instance.IfGameIsPaused;

        private void OnEnable()
        {
            // reset time scale after level retry
            Time.timeScale = 1;
        }

        public void AddPoint(int points = 1)
        {
            this.points += points;

            // update the points visual
            UIManager.Instance.UpdatePoints(this.points.ToString());
        }

        public void HandleGamePauseState(bool shouldPause)
        {
            Time.timeScale = shouldPause ? 0 : 1;

            UIManager.Instance.UpdateGamePauseState(shouldPause);
        }

        public void GameOver()
        {
            // pause the game
            Time.timeScale = 0;

            // update high score if needed
            if (points > PlayerPrefs.GetInt("highscore", 0))
                PlayerPrefs.SetInt("highscore", points);

            // show UI for game over
            UIManager.Instance.ShowGameOverUI(points, PlayerPrefs.GetInt("highscore", 0));
        }

        public void QuitGameToMainMenu()
        {
            SceneManager.LoadScene(ConstantsHelper.MAIN_MENU_SCENE_INDEX);
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(ConstantsHelper.GAME_SCENE_INDEX);
        }
    }
}