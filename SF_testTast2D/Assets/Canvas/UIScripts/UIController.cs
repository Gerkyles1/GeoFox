using EnemyScripts;
using PlayerScripts;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UIScripts
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private Text textCounter;
        private int _enemyCounter = 0;
        [SerializeField] private GameObject replayMenu;

        public static event Action SceneReload;
        void Start()
        {
            EnemyController.OnEnemyDied += OneMoreEnemyDeated;
            PlayerController.OnPlayerDied += ShowReplayMenu;
            textCounter.text = "Score: " + _enemyCounter;
        }

        private void ShowReplayMenu()
        {
            replayMenu.SetActive(true);
        }

        private void OneMoreEnemyDeated()
        {
            _enemyCounter++;
            textCounter.text = "Score: " + _enemyCounter;
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
            PlayerController.OnPlayerDied -= ShowReplayMenu;

        }

    }
}