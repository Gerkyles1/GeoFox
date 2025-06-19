using EnemyScripts;
using MeinMenuScripts;
using PlayerScripts;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UIScripts
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Text _score;
        [SerializeField] private GameObject _endLevelScreen;
        [SerializeField] private GameObject _learn;
        [SerializeField] private GameObject _winScreen;
        [SerializeField] private GameObject _loseScreen;
        [SerializeField] private GameObject _pauseMenu;
        private int _enemyCounter = 0;
        private bool showAd = true;

        public static event Action SceneReload;
        void Start()
        {
            EnemyController.OnEnemyDied += OneMoreEnemyDeated;
            PlayerController.OnPlayerDied += PlayerLose;
            WaveController.OnPlayerWin += PlayerWin;
            _score.text = "Score: " + _enemyCounter;
            _learn.SetActive(true);
        }

        public void Pause()
        {
            Time.timeScale = 0f;
            _pauseMenu.SetActive(true);
        }

        public void Continue()
        {

            Time.timeScale = 1f;
            _pauseMenu.SetActive(false);
        }

        private void PlayerLose()
        {
            if (showAd)
            {
                AdsManager.Instance.interstitialAds.ShowInterstitialAd();
                showAd = false;
            }
            _endLevelScreen.SetActive(true);
            _loseScreen.SetActive(true);

            _winScreen.SetActive(false);
        }

        private void PlayerWin()
        {
            if (showAd)
            {
                AdsManager.Instance.interstitialAds.ShowInterstitialAd();
                showAd = false;
            }

            SavesController.curentMaxLevel += (SavesController.curentMaxLevel == StartGame._choosedLevel ? 1 : 0);

            _endLevelScreen.SetActive(true);
            _winScreen.SetActive(true);

            _loseScreen.SetActive(false);
        }

        private void OneMoreEnemyDeated()
        {
            _enemyCounter++;
            _score.text = "Score: " + _enemyCounter;
        }
        public void Next()
        {
            StartGame.StartNextLevel();
        }

        public void Replay()
        {
            SceneReload?.Invoke();
            PlayerController._playerAlive = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public void Exit()
        {
            Application.Quit();
        }

        private void OnDestroy()
        {
            EnemyController.OnEnemyDied -= OneMoreEnemyDeated;
            PlayerController.OnPlayerDied -= PlayerLose;
            WaveController.OnPlayerWin -= PlayerWin;
        }

    }
}